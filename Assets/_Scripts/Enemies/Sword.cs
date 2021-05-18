using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Sword Script that controlls the debuffs
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Sword : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public float slowAmount;
    [HideInInspector] public float duration;
    [HideInInspector] public ElementEnum element;
    [HideInInspector] public Animator animator;
    private Collider _collider;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword Hit!\nDamage: " + damage + ". slowAmount: " + slowAmount + ". duration: " + duration + ". element: " + element + ".\nTag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            BuffHandler playerBuffHandler = other.GetComponent<BuffHandler>();
            StatHandler statHandler = other.GetComponent<StatHandler>();
            Buff debuff;

            switch (element)
            {
                case ElementEnum.fire:

                    debuff = new Burn(statHandler, new Stat(duration), damage);
                    playerBuffHandler.AddBuff(debuff);
                    break;
                case ElementEnum.ice:
                    debuff = new Slow(statHandler, new Stat(duration), new ScalingStatModificator(slowAmount));
                    statHandler.TakeDamage(damage);
                    playerBuffHandler.AddBuff(debuff);
                    break;
                case ElementEnum.lightning:
                    debuff = new Stun(statHandler, new Stat(duration));
                    statHandler.TakeDamage(damage);
                    playerBuffHandler.AddBuff(debuff);
                    break;
            }
        }
    }
}
