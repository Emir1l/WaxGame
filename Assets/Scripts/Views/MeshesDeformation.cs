using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Emir;

public class MeshesDeformation : MonoBehaviour
{
    #region Private Fields
    private LevelComponent level => LevelComponent.Instance;
    private float delayTimeDeform = 0.05f;
    private float nextTimeDeform = 0f;
    private float minVertsDistanceToRestore = 0.002f;
    private float vertsRestoreSpeed = 2f;
    private Vector3[][] originalVertices;
    private bool isRepairing = false;
    private bool isRepaired = false;
    #endregion

    private void Start()
    {

        originalVertices = new Vector3[level.GetMeshFilters().Length][];

        for (int i = 0; i < level.GetMeshFilters().Length; i++)
        {
            originalVertices[i] = level.GetMeshFilters()[i].mesh.vertices;
            level.GetMeshFilters()[i].mesh.MarkDynamic();
        }
    }
    private void Update()
    {
        RestoreMesh();
    }


    /// <summary>
    /// This Function Helper For Deformation Mesh.
    /// </summary>
    /// <param name="mesh"></param>
    /// <param name="localTransform"></param>
    /// <param name="contactPoint"></param>
    /// <param name="i"></param>
    private void DeformationMesh(Mesh mesh, Transform localTransform, Vector3 contactPoint, int i)
    {
        bool hasDeformated = false;

        Vector3 localContactPoint = localTransform.InverseTransformPoint(contactPoint);
        Vector3[] vertices = mesh.vertices;

        for (int j = 0; j < vertices.Length; j++)
        {
            float distance = (localContactPoint - vertices[j]).magnitude;

            if (distance <= LevelComponent.Instance.GetDeformationRadius())
            {
                vertices[j] += Vector3.back * (LevelComponent.Instance.GetDeformationRadius() - distance) * LevelComponent.Instance.GetImpactDamage();
                Vector3 deformation = vertices[j] - originalVertices[i][j];

                if (deformation.magnitude > LevelComponent.Instance.GetMaxDeformation())
                {
                    vertices[j] = originalVertices[i][j] + deformation.normalized * LevelComponent.Instance.GetMaxDeformation();
                }

                hasDeformated = true;
            }
        }

        if (hasDeformated)
        {
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            if (level.GetColliders().Length > 0)
            {
                if (level.GetColliders()[i] != null)
                {
                    level.GetColliders()[i].sharedMesh = mesh;
                }
            }
        }
    }

    /// <summary>
    /// This Function Helper For Restore Mesh
    /// </summary>
    private void RestoreMesh()
    {
        if (!isRepaired && isRepairing)
        {
            isRepaired = true;

            for (int i = 0; i < level.GetMeshFilters().Length; i++)
            {
                Mesh mesh = level.GetMeshFilters()[i].mesh;
                Vector3[] vertices = mesh.vertices;
                Vector3[] origVerts = originalVertices[i];

                for (int j = 0; j < vertices.Length; j++)
                {
                    vertices[j] += (origVerts[j] - vertices[j]) * Time.deltaTime * vertsRestoreSpeed;

                    if ((origVerts[j] - vertices[j]).magnitude > minVertsDistanceToRestore)
                    {
                        isRepaired = false;
                    }
                }

                mesh.vertices = vertices;
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();

                if (level.GetColliders()[i] != null)
                {
                    level.GetColliders()[i].sharedMesh = mesh;
                }
            }

            if (isRepaired)
            {
                isRepairing = false;

                for (int i = 0; i < level.GetMeshFilters().Length; i++)
                {
                    if (level.GetColliders()[i] != null)
                    {
                        level.GetColliders()[i].sharedMesh = level.GetMeshFilters()[i].mesh;
                    }
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals(CommonTypes.HAIR_TAG))
        {
            collision.gameObject.GetComponent<HairComponent>().GetAnimation().DOKill();
        }
        if (Time.time > nextTimeDeform)
        {
            isRepaired = false;

            Vector3 contactPoint = collision.contacts[0].point;

            for (int i = 0; i < level.GetMeshFilters().Length; i++)
            {
                if (level.GetMeshFilters()[i] != null)
                {
                    DeformationMesh(level.GetMeshFilters()[i].mesh, level.GetMeshFilters()[i].transform, contactPoint, i);
                }
            }

            nextTimeDeform = Time.time + delayTimeDeform;

        }
    }

}