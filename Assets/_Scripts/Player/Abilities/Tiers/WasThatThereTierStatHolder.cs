using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for WasThatThere.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class WasThatThereTierStatHolder
{
    [SerializeField] private float _critChance;
    [SerializeField] private float _invisibilityChance;

    public float CritChance { get => _critChance; }
    public float InvisibilityChance { get => _invisibilityChance; }
}
