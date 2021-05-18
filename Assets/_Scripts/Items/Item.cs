using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Item Interface which provides base functionality
/// ==============================================
/// Changelog: 
/// ==============================================
///

[System.Serializable]
public abstract class Item
{
    public static int TIER_RANGE { get => 3; }
 
    public static int MERGE_COUNT { get => 3; }

    public static int TIME_TO_LIVE { get => 180; } //TTL in seconds

    private int _tier;

    public int Tier { get => _tier; }
    public abstract AbilityHUDElement HudInfo { get; }

    public Item(int tier)
    {
        _tier = tier;
    }

    public abstract bool Equals(Item item);

    public abstract Item CreateCopyWithTier(int tier);

    public abstract List<SlotType> GetValidSlotTypes();

    public static Item GetRandomItem(int range)
    {
        int random = Random.Range(0, range);
        switch (random)
        {
            case 0:
                return new WeaponItem(1, AbilityUtil.GetRandomWeapon());
            case 1:
                return new ElementItem(1, AbilityUtil.GetRandomElement());
            case 2:
                return new WildcardItem(1, AbilityUtil.GetRandomWildcard());
            default:
                return new NullItem();
        }
    }
}

public class ElementItem : Item
{
    private ElementEnum _element;
    private AbilityHUDElement _hudInfo;
    public override AbilityHUDElement HudInfo { get => _hudInfo; }

    public ElementEnum Element { get => _element; }

    public ElementItem(int tier, ElementEnum element) : base(tier)
    {
        _element = element;
        _hudInfo= AbilityUtil.GetAbilityHUDElement(element);
    }

    public override List<SlotType> GetValidSlotTypes()
    {
        return new List<SlotType>() { SlotType.element, SlotType.neutral };
    }

    public override Item CreateCopyWithTier(int tier)
    {
        return new ElementItem(tier, _element);
    }

    public override bool Equals(Item item)
    {
        if (!(item is ElementItem))
        {
            return false;
        }
        return ((ElementItem)item).Element == _element && item.Tier == Tier;
    }
}

public class WildcardItem : Item
{
    private WildcardEnum _wildcard;
    public WildcardEnum Wildcard { get => _wildcard; }
    private AbilityHUDElement _hudInfo;
    public override AbilityHUDElement HudInfo { get => _hudInfo; }

    public WildcardItem(int tier, WildcardEnum wildcard) : base(tier)
    {
        _wildcard = wildcard;
        _hudInfo = AbilityUtil.GetAbilityHUDElement(wildcard);
    }

    public override List<SlotType> GetValidSlotTypes()
    {
        return new List<SlotType>() { SlotType.wildcard, SlotType.neutral };
    }

    public override Item CreateCopyWithTier(int tier)
    {
        return new WildcardItem(tier, _wildcard);
    }
    public override bool Equals(Item item)
    {
        if (!(item is WildcardItem))
        {
            return false;
        }
        return ((WildcardItem)item).Wildcard == _wildcard && item.Tier == Tier;
    }
}

public class WeaponItem : Item
{
    private WeaponEnum _weapon;
    public WeaponEnum Weapon { get => _weapon; }
    private AbilityHUDElement _hudInfo;
    public override AbilityHUDElement HudInfo { get => _hudInfo; }

    public WeaponItem(int tier, WeaponEnum weapon) : base(tier)
    {
        _weapon = weapon;
        _hudInfo = AbilityUtil.GetAbilityHUDElement(weapon);
    }
    public override List<SlotType> GetValidSlotTypes()
    {
        return new List<SlotType>() { SlotType.weapon, SlotType.neutral };
    }

    public override Item CreateCopyWithTier(int tier)
    {
        return new WeaponItem(tier, _weapon);
    }
    public override bool Equals(Item item)
    {
        if (!(item is WeaponItem))
        {
            return false;
        }
        return ((WeaponItem)item).Weapon == _weapon && item.Tier == Tier;
    }
}

public class NullItem : Item
{
    private AbilityHUDElement _hudInfo;
    public override AbilityHUDElement HudInfo { get => _hudInfo; }
    public NullItem() : base(0)
    {
        _hudInfo = AbilityUtil.GetAbilityHUDElement(WeaponEnum.neutral);
    }

    public override List<SlotType> GetValidSlotTypes()
    {
        return new List<SlotType>() { SlotType.weapon, SlotType.element, SlotType.wildcard, SlotType.neutral };
    }

    public override Item CreateCopyWithTier(int tier)
    {
        return new NullItem();
    }
    public override bool Equals(Item item)
    {
        return false;
    }
}
