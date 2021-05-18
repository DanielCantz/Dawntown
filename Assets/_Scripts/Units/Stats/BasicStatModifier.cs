using System;
using UnityEngine;
///
/// Author: Samuel Müller
/// Description: Modifies Stats by adding values;
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class BasicStatModifier : IStatModificator
{
    [SerializeField]
    private float _value = 0;

    public BasicStatModifier(float value)
    {
        _value = value;
    }

    public float Modify(float value)
    {
        return value += _value;
    }

    public float Demodify(float value)
    {
        return value -= _value;
    }
}
