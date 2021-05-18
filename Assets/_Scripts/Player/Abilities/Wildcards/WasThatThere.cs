using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Wildcard behaviour of WasThatThere
/// ==============================================
/// Changelog:
/// Samuel Müller: refactorings & Tier implememntation
/// ==============================================
///

[CreateAssetMenu(menuName = "Abilities/WasThatThere")]
public class WasThatThere : AbstractWildcard
{
    [SerializeField] private List<WasThatThereTierStatHolder> _tierStats = new List<WasThatThereTierStatHolder>(3);
    public override void TriggerWildcardAttack(WeaponEnum weaponMod)
    {
        ProjectileSpell spell = AbilityHandler.Instance.Attack.Spell as ProjectileSpell;
        float additionalCritChance = _tierStats[Tier - 1].CritChance;
        switch (weaponMod)
        {
            case WeaponEnum.barrel:
                spell.CritChance = spell.BaseCritChance + additionalCritChance;
                break;
            case WeaponEnum.blade:
                spell.CritChance = spell.BaseCritChance + additionalCritChance;
                break;
            case WeaponEnum.magiccore:
                spell.CritChance = spell.BaseCritChance + additionalCritChance;
                break;
            case WeaponEnum.neutral:
                break;
        }
    }

    public override void TriggerWildcardDefense(WeaponEnum weaponMod)
    {
        float invisChance = _tierStats[Tier - 1].InvisibilityChance; 
        if (Random.Range(0, 1) <= invisChance)
        {
            if (weaponMod == WeaponEnum.neutral)
            {
                return;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>().IsInvinsible = true;
        }
    }
}