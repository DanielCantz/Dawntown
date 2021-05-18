using System;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184, Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Applies element debuffs to colliding buffhandlers.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class ApplyElement : MonoBehaviour
{
    private Element _currentElement;

    public Element CurrentElement { get => _currentElement; set => _currentElement = value; }

    private Dictionary<Collider, Timer> _timersByColliders = new Dictionary<Collider, Timer>();

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.GetComponent<BuffHandler>() == null)
        {
            return;
        }
        ApplyEffect(collision);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<BuffHandler>() == null)
        {
            return;
        }
        ApplyEffect(collision);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<BuffHandler>() == null)
        {
            return;
        }
        ApplyEffect(collision);
    }

    private void ApplyEffect(Collider collider)
    {
        BuffHandler enemyBuffHandler = collider.gameObject.GetComponent<BuffHandler>();
        if (_currentElement.ElementEnum == ElementEnum.neutral || enemyBuffHandler.ContainsBuffOfType(GetDebuffType(_currentElement.ElementEnum)))
        {
            return;
        }

        switch (collider.gameObject.tag)
        {
            case "Enemy":
                StatHandler enemyStatHandler = collider.gameObject.GetComponent<StatHandler>();
                switch (_currentElement.ElementEnum)
                {
                    case ElementEnum.fire:
                        Burn burn = new Burn(enemyStatHandler, new Stat(_currentElement.Duration), _currentElement.Value);
                        enemyBuffHandler.AddBuff(burn);
                        break;
                    case ElementEnum.ice:
                        Slow slow = new Slow(enemyStatHandler, new Stat(_currentElement.Duration), new ScalingStatModificator(_currentElement.Value));
                        enemyBuffHandler.AddBuff(slow);
                        break;
                    case ElementEnum.lightning:
                        Stun stun = new Stun(enemyStatHandler, new Stat(_currentElement.Duration));
                        enemyBuffHandler.AddBuff(stun);
                        break;
                    case ElementEnum.neutral:
                        break;
                }
                Debug.Log("<color=orange>Applied Debuff:" + _currentElement + "</color>");
                break;
        }
    }

    private Type GetDebuffType(ElementEnum element)
    {
        switch (element)
        {
            case ElementEnum.fire:
                return typeof(Burn);
            case ElementEnum.ice:
                return typeof(Slow);
            case ElementEnum.lightning:
                return typeof(Stun);
        }
        return null;
    }
}