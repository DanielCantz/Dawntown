using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Shield Script that controlls the debuffs
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Shield : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public float slowAmount;
    [HideInInspector] public float duration;
    [HideInInspector] public ElementEnum element;

    
    private void OnTriggerEnter(Collider other)
    {
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
        } else if (other.CompareTag("Projectile"))
        {
            Debug.Log("SHIELD ENTER PROJECTILE: " + other.gameObject.transform);
            Destroy(other.gameObject);
        }
    }
    
}
