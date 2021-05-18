using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Abstract Element Applier to apply element debuffs to buffhandlers.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public abstract class ElementApplier : MonoBehaviour
{
    private Element _currentElement;

    public Element CurrentElement { get => _currentElement; set => _currentElement = value; }

    protected virtual void Start()
    {
        Physics.IgnoreLayerCollision(gameObject.layer, 0); //Ignore PLayer
    }
    private void OnTriggerEnter(Collider collision)
    {
        ApplyDebuff(collision);
    }

    protected void ApplyDebuff(Collider collider)
    {
        if (GetApplyCondition(collider))
        {
            StatHandler enemyStatHandler = collider.gameObject.GetComponent<StatHandler>();
            BuffHandler enemyBuffHandler = collider.gameObject.GetComponent<BuffHandler>();
            Buff debuff = AbilityUtil.GetDebuff(_currentElement, enemyStatHandler);
            if (debuff != null)
            {
                enemyBuffHandler.AddBuff(debuff);
            }
            OnSuccess(collider);
        }
    }

    protected virtual bool GetApplyCondition(Collider collider)
    {
        return collider.gameObject.layer == 8;
    }

    protected abstract void OnSuccess(Collider collider);
}