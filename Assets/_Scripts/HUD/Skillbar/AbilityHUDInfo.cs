using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: provides information about abilities for SkillbarHUD
/// ==============================================
/// Changelog: 
/// ==============================================
///
[System.Serializable]
public class AbilityHUDInfo
{
    [SerializeField]
    private AbilityHUDElement _element;
    public AbilityHUDElement Element { get => _element; }
    [SerializeField]
    private AbilityHUDElement _weapon;
    public AbilityHUDElement Weapon { get => _weapon; }
    [SerializeField]
    private AbilityHUDElement _wildcard;
    public AbilityHUDElement Wildcard { get => _wildcard; }


    public AbilityHUDInfo(AbilityHUDElement element, AbilityHUDElement weapon, AbilityHUDElement wildcard)
    {
        _element = element;
        _weapon = weapon;
        _wildcard = wildcard;
    }

    public void Update(ElementEnum element)
    {
        _element = AbilityUtil.GetAbilityHUDElement(element);
    }

    public void Update(WeaponEnum weapon)
    {
        _weapon = AbilityUtil.GetAbilityHUDElement(weapon);

    }
    public void Update(WildcardEnum wildcard)
    {
        _wildcard = AbilityUtil.GetAbilityHUDElement(wildcard);
    }

    public static AbilityHUDInfo Build(ElementEnum element, WeaponEnum weapon, WildcardEnum wildcard)
    {
        AbilityHUDElement hudElement = AbilityUtil.GetAbilityHUDElement(element);
        AbilityHUDElement hudWeapon = AbilityUtil.GetAbilityHUDElement(weapon);
        AbilityHUDElement hudWildcard = AbilityUtil.GetAbilityHUDElement(wildcard);
        return new AbilityHUDInfo(hudElement, hudWeapon, hudWildcard);
    }
}
