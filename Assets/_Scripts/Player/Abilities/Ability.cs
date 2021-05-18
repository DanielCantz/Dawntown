using System;
using System.Collections;
using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: just a placeholder class. Overwrite it as you need it. (existing members are needed though)
/// ==============================================
/// Changelog: 
/// ==============================================
///
[System.Serializable]
public class Ability
{
    [SerializeField]
    private AbstractSpell _spell;

    public AbstractSpell Spell
    {
        get
        {
            return _spell;
        }
        set
        {
            if (value != null)
            {
                _cooldown = value.aBaseCoolDown;
            }
            _spell = value;
        }
    }

    [SerializeField]
    private AbstractWildcard _wildcard;

    public AbstractWildcard Wildcard
    {
        get
        {
            return _wildcard;
        }
        set
        {
            _wildcard = value;
        }
    }

    [SerializeField]
    private Element _element;

    public Element Element
    {
        get
        {
            return _element;
        }

        set
        {
            _element = value;
        }
    }

    private float _cooldown;

    private bool _isOnCooldown = false;

    public bool IsOnCooldown { get => _isOnCooldown; set => _isOnCooldown = value; }

    public Ability(AbstractSpell spell, AbstractWildcard wildcard, Element element)
    {
        _spell = spell;
        _cooldown = spell.aBaseCoolDown;
        _wildcard = wildcard;
        _element = element;
    }
    public IEnumerator Cooldown()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(_cooldown);
        _isOnCooldown = false;
        //Debug.Log("Cooldown Over" + abilityAxisName);
    }
}
