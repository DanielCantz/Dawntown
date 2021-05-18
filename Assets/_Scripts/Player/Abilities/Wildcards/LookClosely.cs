using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Wildcard behaviour of LookClosely
/// ==============================================
/// Changelog:
/// Samuel Müller: refactorings & Tier implememntation
/// ==============================================
///
[CreateAssetMenu(menuName = "Abilities/LookClosely")]
public class LookClosely : AbstractWildcard
{
    [Tooltip("Increases the tolerance around the cursor")][SerializeField] private float _aimBotTolerance = 3;
    [SerializeField] private List<LookCloselyTierStatHolder> _tierStats = new List<LookCloselyTierStatHolder>(3);

    public override void TriggerWildcardAttack(WeaponEnum weaponMod)
    {
        ProjectileSpell spell = AbilityHandler.Instance.Attack.Spell as ProjectileSpell;

        switch (weaponMod)
        {
            case WeaponEnum.barrel:
                spell.ProjectileSpread = spell.BaseProjectileSpread * _tierStats[Tier - 1].BarrelSpread;
                break;
            case WeaponEnum.blade:
                spell.ProjectileSpread = spell.BaseProjectileSpread + spell.BaseProjectileSpread * _tierStats[Tier - 1].BladeSpread;
                break;
            case WeaponEnum.magiccore:
                Debug.Log("<color=green>LookClosely Magiccore</color>");
                float aimDistance = _tierStats[Tier - 1].AimDistance;
                Debug.Log("Aim distance: "+aimDistance);
                RaycastHit[] hits = Physics.SphereCastAll(GameObject.FindWithTag("Player").transform.position, _aimBotTolerance, spell.DirectionVector, aimDistance, Physics.AllLayers);
                RaycastHit nearestEnemy = new RaycastHit();
                nearestEnemy.distance = aimDistance;
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject.layer == 8 && hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        if (nearestEnemy.distance >= hit.distance)
                        {
                            nearestEnemy = hit;
                        }
                    }
                }
                if (nearestEnemy.collider != null)
                {
                    Vector3 positionWithoutY = new Vector3(spell.BulletSpawn.position.x, 0, spell.BulletSpawn.position.z);
                    Vector3 bulletVector = nearestEnemy.transform.position - positionWithoutY;
                    spell.DirectionVector = bulletVector;
                }

                break;
            case WeaponEnum.neutral:
                break;
        }
    }

    public override void TriggerWildcardDefense(WeaponEnum weaponMod)
    {
        WorldTransformSpell spell = AbilityHandler.Instance.Defense.Spell as WorldTransformSpell;
        if (weaponMod != WeaponEnum.neutral)
        {
            spell.PlacementOffset = spell.BasePlacementOffset * _tierStats[Tier - 1].PlacementOffset;
        }
    }
}