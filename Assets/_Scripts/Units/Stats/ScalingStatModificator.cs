using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller
/// Description: Modifies Stats by multiplying values;
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class ScalingStatModificator : IStatModificator
{
    [SerializeField]
    private float _value;
    [SerializeField]
    private float _changeValue;

    public ScalingStatModificator(float value)
    {
        _value = value;
    }

    public float Modify(float value)
    {
        float change = value * _value;
        _changeValue = change - value;
        return change;
    }

    public float Demodify(float value)
    {
        return value - _changeValue;
    }
}
