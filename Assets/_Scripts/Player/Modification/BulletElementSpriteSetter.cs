using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Yannick Pfeifer yp009
/// Description: original sprite setter for offense spells before getting reworked by Samuel Müller sm184. Defense
/// script got deleted somehow.
/// ==============================================
/// Changelog:
/// 
/// ==============================================
///
public class BulletElementSpriteSetter : MonoBehaviour
{

    [SerializeField]
    private Sprite fire;
    [SerializeField]
    private RuntimeAnimatorController fireAnimation;
    [SerializeField]
    private Sprite lightning;
    [SerializeField]
    private RuntimeAnimatorController lightningAnimation;
    [SerializeField]
    private Sprite ice;
    private void Awake()
    {
        Debug.Log(AbilityHandler.Instance.Attack.Element.ToString());
        Debug.Log("<color=green>" + AbilityHandler.Instance.Attack.Element.ElementEnum + "</color>");
        switch (AbilityHandler.Instance.Attack.Element.ElementEnum)
        {
            case ElementEnum.ice:
                GetComponent<SpriteRenderer>().sprite = ice;
                break;
            case ElementEnum.fire:
                GetComponent<SpriteRenderer>().sprite = fire;
                GetComponent<Animator>().runtimeAnimatorController = fireAnimation;
                break;
            case ElementEnum.lightning:
                GetComponent<SpriteRenderer>().sprite = lightning;
                GetComponent<Animator>().runtimeAnimatorController = lightningAnimation;
                break;
        }
    }
}
