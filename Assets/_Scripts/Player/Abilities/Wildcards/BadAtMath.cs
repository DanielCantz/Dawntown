using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Wildcard behaviour of BadAtMath
/// ==============================================
/// Changelog:
/// Samuel Müller: refactorings & Tier implememntation
/// ==============================================
///
[CreateAssetMenu(menuName = "Abilities/BadAtMath")]
public class BadAtMath : AbstractWildcard
{
    [SerializeField] private List<BadAtMathTierStatHolder> _tierStats = new List<BadAtMathTierStatHolder>(3);

    public override void TriggerWildcardAttack(WeaponEnum weaponMod)
    {
        //Debug.Log("Trigger LookClosely Attack");
        ProjectileSpell spell = AbilityHandler.Instance.Attack.Spell as ProjectileSpell;
        Debug.Log("<color=green>Trigger BadAtMath Attack</color>");
        Debug.Log("<color=green>Trigger BadAtMath old damage range: " + spell.DamageRange + "</color>");

        if (weaponMod != WeaponEnum.neutral)
        {
            spell.DamageRange = spell.BaseDamageRange + _tierStats[Tier - 1].DamageRangeFactor;
        }

        Debug.Log("<color=green>Trigger BadAtMath new damage range: " + spell.DamageRange + "</color>");
    }

    public override void TriggerWildcardDefense(WeaponEnum weaponMod)
    {
        WorldTransformSpell spell = AbilityHandler.Instance.Defense.Spell as WorldTransformSpell;
        Debug.Log("<color=green>Trigger BadAtMath Defense</color>");
        Debug.Log("<color=green>Trigger BadAtMath old lifetime: " + spell.BaseLifetime + "</color>");
        if (weaponMod != WeaponEnum.neutral)
        {
            float lifetimeRange = _tierStats[Tier-1].LifetimeRangeFactor;
            spell.IncreaseLifetime(spell.BaseLifetime * (Random.Range(-lifetimeRange, lifetimeRange)));
        }
        
        Debug.Log("<color=green>Trigger BadAtMath new lifetime: " + spell.Lifetime + "</color>");
    }
}
