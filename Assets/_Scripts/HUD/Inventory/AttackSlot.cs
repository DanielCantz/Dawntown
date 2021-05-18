using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: holds ability info
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class AttackSlot : MonoBehaviour
{
    [SerializeField]
    private Ability _ability;

    [SerializeField]
    private AbilityIconSyncer _iconSync;

    [SerializeField]
    private ItemSlot _weapon;
    public ItemSlot Weapon { get => _weapon; }
    [SerializeField]
    private ItemSlot _element;
    public ItemSlot Element { get => _element; }
    [SerializeField]
    private ItemSlot _wildcard;
    public ItemSlot Wildcard { get => _wildcard; }

    [SerializeField] private bool _isOffensiveSlot;

    public void SetUpSyncs(Ability ability)
    {
        Debug.Log(gameObject.name);
        _ability = ability;
        _weapon.onItemChange = SyncSpell;
        _element.onItemChange = SyncElement;
        _wildcard.onItemChange = SyncWildcard;
    }

    private void SyncSpell()
    {
        Item item = _weapon.GetItem();
        AbstractSpell spell;
        _iconSync.SyncWeapon(item.HudInfo);

        if (item is WeaponItem)
        {
            WeaponItem weaponItem = (WeaponItem)item;

            if (_isOffensiveSlot)
            {
                spell = AbilityUtil.GetSpell(weaponItem.Weapon);

            }
            else
            {
                spell = AbilityUtil.GetDefensiveSpell(weaponItem.Weapon);
            }
            
            spell.Tier = weaponItem.Tier;
        } else
        {
            spell = AbilityUtil.GetSpell(WeaponEnum.neutral);
        }
        Debug.Log(this.ToString()+": "+spell.Name);
        AbilityHandler.Instance.UpdateAbility(_ability, spell, AbilityHandler.Instance.WeaponHolder);
    }

    private void SyncWildcard()
    {
        Item item = _wildcard.GetItem();
        _iconSync.SyncWildcard(item.HudInfo);
        AbstractWildcard wildcard;
        if (item is WildcardItem)
        {
            WildcardItem wildcardItem = (WildcardItem) item;
            wildcard = AbilityUtil.GetWildcard(wildcardItem.Wildcard);
            wildcard.Tier = wildcardItem.Tier;
        } else
        {
            wildcard = AbilityUtil.GetWildcard(WildcardEnum.neutral);
        }

        AbilityHandler.Instance.UpdateAbility(_ability, wildcard);
    }

    private void SyncElement()
    {
        Debug.Log("<color=green>sync element - </color>"+"<color=red>"+_element.GetItem().ToString()+"</color>");
        Item item = _element.GetItem();
        _iconSync.SyncElement(item.HudInfo);

        Element element;
        if (item is ElementItem)
        {
            ElementItem elementItem = (ElementItem) item;
            element = AbilityUtil.GetElement(elementItem.Element);
            element.Tier = elementItem.Tier;
        } else
        {
            element = AbilityUtil.GetElement(ElementEnum.neutral);
        }
        Debug.Log("Element: "+element.ToString()+"; Enum: "+ element.ElementEnum.ToString());
        AbilityHandler.Instance.UpdateAbility(_ability, element);
    }
}
