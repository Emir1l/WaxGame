using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emir;

public class PlayerView : Singleton<PlayerView>
{
    #region Private Fields
    private Ray ray;
    private RaycastHit hit;
    private int armLayer;
    #endregion

    private void Update()
    {
        CheckInput();
    }

    /// <summary>
    /// This Function Helper For Check Inputs.
    /// </summary>
    private void CheckInput()
    {
        if (!TouchManager.Instance.IsTouching()) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        armLayer = LayerMask.GetMask(CommonTypes.ARM_LAYER);
        Vector3 TargetPos;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, armLayer))
        {
            TargetPos = hit.point + Vector3.up * 3.2F;
            LevelComponent.Instance.GetStick().position = TargetPos;
        }
    }
}
