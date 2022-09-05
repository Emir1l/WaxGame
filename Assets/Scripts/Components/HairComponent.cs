using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HairComponent : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation m_tweenAnimation;


    /// <summary>
    /// This Function Returns Related For Dotween Animation.
    /// </summary>
    /// <returns></returns>
    public DOTweenAnimation GetAnimation()
    {
        return m_tweenAnimation;
    }

}
