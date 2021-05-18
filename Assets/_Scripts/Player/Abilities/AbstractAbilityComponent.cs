using UnityEngine;
///
/// Author: Samuel Müller
/// Description: Parent of all AbilityComponents. Necessary for tierimplementation.
/// ==============================================
/// Changelog:
/// ==============================================
///
public abstract class AbstractAbilityComponent : ScriptableObject
{
    [SerializeField] private int _tier = 1;

    public int Tier { get => _tier; set => _tier = value; }
}
