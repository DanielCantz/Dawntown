using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller, sm184, Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: defines blueprint for all wildcards
/// ==============================================
/// Changelog:
/// ==============================================
///
public abstract class AbstractWildcard : AbstractAbilityComponent
{
    [SerializeField] private string Name = "New Wildcard";

    // Offensive Variables
    [SerializeField] private float _effectMultiplier = 0.2f;

    [SerializeField] private WildcardEnum _wildcardType;

    public abstract void TriggerWildcardAttack(WeaponEnum weaponMod);
    public abstract void TriggerWildcardDefense(WeaponEnum weaponMod);
}

