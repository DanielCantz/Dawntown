using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller, sm194
/// Description: Wildcard behaviour of Mull (when no wildcard is equipped)
/// ==============================================
/// Changelog:
/// ==============================================
///
[CreateAssetMenu(menuName = "Abilities/Null/Wildcard")]
public class NullWildcard : AbstractWildcard
{
    public override void TriggerWildcardAttack(WeaponEnum weaponMod)
    {
        //do nothing
    }

    public override void TriggerWildcardDefense(WeaponEnum weaponMod)
    {
        //do nothing
    }
}
