using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatProxy : IStat
{
    private IStat _stat;

    private float _valueToReturn;

    public StatProxy(IStat stat, float valueToReturn)
    {
        _stat = stat;
        _valueToReturn = valueToReturn;
    }

    public void AddMod(IStatModificator mod)
    {
        _stat.AddMod(mod);
    }

    public float GetValue()
    {
        return _valueToReturn;
    }

    public bool HasMod(IStatModificator mod)
    {
        return _stat.HasMod(mod);
    }

    public void RemoveMod(IStatModificator mod)
    {
        _stat.RemoveMod(mod);
    }
}
