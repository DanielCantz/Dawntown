using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for Elements.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class ElementTierStatHolder
{
    [SerializeField] private float _value;
    [SerializeField] private float _chance;
    [SerializeField] private float _delay;
    [SerializeField] private float _strength;

    public float Value { get => _value; }
    public float Chance { get => _chance; }
    public float Delay { get => _delay; }
    public float Strength { get => _strength; }
}
