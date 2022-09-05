using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emir;

public class LevelComponent : Singleton<LevelComponent>
{
    [Header("Mesh")]
    [SerializeField] private MeshFilter[] meshFilters = default;
    [SerializeField] private MeshCollider[] colliders = default;
    [Header("Wax")]
    [SerializeField] private float impactDamage;
    [SerializeField] private float deformationRadius;
    [SerializeField] private float maxDeformation;
    [Header("Hair")]
    [SerializeField] private List<HairComponent> m_hairs = new List<HairComponent>();
    [Header("Stick")]
    [SerializeField] private Transform m_stickTransform;

    /// <summary>
    /// This Function Returns Related For Mesh Filters.
    /// </summary>
    /// <returns></returns>
    public MeshFilter[] GetMeshFilters()
    {
        return meshFilters;
    }

    /// <summary>
    /// This Function Returns Related For Colliders.
    /// </summary>
    /// <returns></returns>
    public MeshCollider[] GetColliders()
    {
        return colliders;
    }

    /// <summary>
    /// This Function Returns Related For Hairs.
    /// </summary>
    /// <returns></returns>
    public List<HairComponent> GetHairs()
    {
        return m_hairs;
    }

    /// <summary>
    /// This Function Returns Related For Stick.
    /// </summary>
    /// <returns></returns>
    public Transform GetStick()
    {
        return m_stickTransform;
    }

    /// <summary>
    /// This Function Returns Related For Impact Damage Value.
    /// </summary>
    /// <returns></returns>
    public float GetImpactDamage()
    {
        return impactDamage;
    }

    /// <summary>
    /// This Function Returns Related For Deformation Radius Value.
    /// </summary>
    /// <returns></returns>
    public float GetDeformationRadius()
    {
        return deformationRadius;
    }

    /// <summary>
    /// This Function Returns Related For Max Deformation Value.
    /// </summary>
    /// <returns></returns>
    public float GetMaxDeformation()
    {
        return maxDeformation;
    }

}
