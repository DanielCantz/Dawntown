using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: Stats for player and enemies
/// ==============================================
/// Changelog: 
/// ==============================================
///
[System.Serializable]
public class Stat : IStat
{
    [SerializeField]
    private float _baseValue;
    [SerializeField]
    private float _modifiedValue;

    private List<IStatModificator> _mods = new List<IStatModificator>();

    public Stat(float value)
    {
        _baseValue = value;
        _modifiedValue = _baseValue;
    }

    public float GetValue()
    {
        return _modifiedValue;
    }

    public virtual void AddMod(IStatModificator mod)
    {
        if (!_mods.Contains(mod))
        {
            _modifiedValue = mod.Modify(_modifiedValue);
            _mods.Add(mod);
        }
    }

    public virtual bool HasMod(IStatModificator mod)
    {
        return _mods.Contains(mod);
    }

    public virtual void RemoveMod(IStatModificator mod)
    {
        if (_mods.Contains(mod))
        {
            _modifiedValue = mod.Demodify(_modifiedValue);
            _mods.Remove(mod);
        }
    }
}