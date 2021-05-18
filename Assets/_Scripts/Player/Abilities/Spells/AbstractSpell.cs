using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: defines base blueprint for all weapons/spells.
/// ==============================================
/// Changelog:
/// ==============================================
///
public abstract class AbstractSpell : AbstractAbilityComponent
{
    public string Name = "New Ability";
    public AudioClip aSound;

    public string fmodEventAttack, fmodEventDefense;

    public float aBaseCoolDown = .1f;
    [SerializeField] private WeaponEnum _weaponMod;
    public WeaponEnum WeaponMod { get => _weaponMod; }    

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerSpell();

}

