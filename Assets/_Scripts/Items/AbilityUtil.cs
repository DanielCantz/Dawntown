using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Helper class. Mainly to load item-prefabs based on related enums
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class AbilityUtil
{
    private static readonly string HUDELEMENT_PATH_PREFIX = "Prefabs/HUD/SkillElements/";

    private static readonly string SCRIPTABLE_OBJECT_PATH_PREFIX = "Prefabs/ScriptableObjects/";

    public static string Path { get => HUDELEMENT_PATH_PREFIX; }

    public static AbilityHUDElement GetAbilityHUDElement(ElementEnum element)
    {
        return Resources.Load<AbilityHUDElement>(HUDELEMENT_PATH_PREFIX + "Elements/" + element.ToString());
    }

    public static AbilityHUDElement GetAbilityHUDElement(WeaponEnum weapon)
    {
        return Resources.Load<AbilityHUDElement>(HUDELEMENT_PATH_PREFIX + "Weapons/" + weapon.ToString());
    }
    public static AbilityHUDElement GetAbilityHUDElement(WildcardEnum wildcard)
    {
        return Resources.Load<AbilityHUDElement>(HUDELEMENT_PATH_PREFIX + "Wildcards/" + wildcard.ToString());
    }

    public static AbilityHUDElement GetNullHUDElement()
    {
        return Resources.Load<AbilityHUDElement>(HUDELEMENT_PATH_PREFIX + ElementEnum.neutral.ToString());
    }

    internal static AbilityHUDElement GetHudElement(Buff buff)
    {
        ElementEnum element = ElementEnum.neutral;
        if (buff is Burn)
        {
            Debug.Log("hot sprite");
            element = ElementEnum.fire;
        }
        if (buff is Stun)
        {
            Debug.Log("shocking sprite");
            element = ElementEnum.lightning;
        }
        if (buff is Slow)
        {
            Debug.Log("cool sprite");
            element = ElementEnum.ice;
        }
        if (buff is Regenerate)
        {
            Debug.Log("fresh sprite");
            element = ElementEnum.heal;
        }
        return GetAbilityHUDElement(element);
    }

    public static ElementEnum GetRandomElement()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                return ElementEnum.lightning;
            case 2:
                return ElementEnum.fire;
            case 3:
                return ElementEnum.ice;
            default:
                return ElementEnum.neutral;
        }
    }

    public static WeaponEnum GetRandomWeapon()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                return WeaponEnum.barrel;
            case 2:
                return WeaponEnum.blade;
            case 3:
                return WeaponEnum.magiccore;
            default:
                return WeaponEnum.neutral;
        }
    }

    public static WildcardEnum GetRandomWildcard()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                return WildcardEnum.lookClosely;
            case 2:
                return WildcardEnum.badAtMath;
            case 3:
                return WildcardEnum.wasThatThere;
            default:
                return WildcardEnum.neutral;
        }
    }

    public static float ApplyScaling(float baseValue, int tier, float[] scaling)
    {
        if (tier <= 0)
        {
            return baseValue;
        }
        float finalValue = baseValue;
        for (int i = 0; i < (tier - 1); i++)
        {
            finalValue *= scaling[i];
        }
        return finalValue;
    }

    public static float ApplyReduction(float baseValue, int tier, float[] scaling)
    {
        if (tier <= 0)
        {
            return baseValue;
        }
        float finalValue = baseValue;
        for (int i = 0; i < (tier - 1); i++)
        {
            finalValue *= (scaling[i] % 1);
        }
        return finalValue;
    }

    public static AbstractSpell GetSpell(WeaponEnum weapon)
    {
        switch (weapon)
        {
            case WeaponEnum.blade:
                return Resources.Load<ProjectileSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "SwordAttack");
            case WeaponEnum.magiccore:
                return Resources.Load<ProjectileSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "MagiccoreAttack");
            case WeaponEnum.barrel:
                return Resources.Load<ProjectileSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "BarrelAttack");
        }
        return Resources.Load<NullSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "NullSpell");
    }

    public static AbstractSpell GetDefensiveSpell(WeaponEnum weapon)
    {
        switch (weapon)
        {
            case WeaponEnum.blade:
                return Resources.Load<WorldTransformSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "SwordDefense");
            case WeaponEnum.magiccore:
                return Resources.Load<WorldTransformSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "MagicoreDefense");
            case WeaponEnum.barrel:
                return Resources.Load<WorldTransformSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "BarrelDefense");
        }
        return Resources.Load<NullSpell>(SCRIPTABLE_OBJECT_PATH_PREFIX + "NullSpell");
    }

    public static AbstractWildcard GetWildcard(WildcardEnum wildcard)
    {
        switch (wildcard)
        {
            case WildcardEnum.badAtMath:
                return Resources.Load<AbstractWildcard>(SCRIPTABLE_OBJECT_PATH_PREFIX + "BadAtMath");
            case WildcardEnum.lookClosely:
                return Resources.Load<AbstractWildcard>(SCRIPTABLE_OBJECT_PATH_PREFIX + "LookClosely");
            case WildcardEnum.wasThatThere:
                return Resources.Load<AbstractWildcard>(SCRIPTABLE_OBJECT_PATH_PREFIX + "WasThatThere");
        }
        return Resources.Load<AbstractWildcard>(SCRIPTABLE_OBJECT_PATH_PREFIX + "NullWildcard");
    }

    public static Element GetElement(ElementEnum element)
    {
        switch (element)
        {
            case ElementEnum.fire:
                return Resources.Load<Element>(SCRIPTABLE_OBJECT_PATH_PREFIX + "FireElement");
            case ElementEnum.ice:
                return Resources.Load<Element>(SCRIPTABLE_OBJECT_PATH_PREFIX + "IceElement");
            case ElementEnum.lightning:
                return Resources.Load<Element>(SCRIPTABLE_OBJECT_PATH_PREFIX + "LightningElement");
        }
        return Resources.Load<NullElement>(SCRIPTABLE_OBJECT_PATH_PREFIX + "NullElement");
    }

    public static Buff GetDebuff(Element element, StatHandler target)
    {
        Buff buff = null;
        switch (element.ElementEnum)
        {
            case ElementEnum.fire:
                buff = new Burn(target, new Stat(element.Duration), element.Value);
                break;
            case ElementEnum.ice:
                buff = new Slow(target, new Stat(element.Value), new ScalingStatModificator(element.Value));
                break;
            case ElementEnum.lightning:
                buff = new Stun(target, new Stat(element.Value));
                break;
            default:
                break;
        }
        return buff;
    }

}




public enum WeaponEnum
{
    blade,
    barrel,
    magiccore,
    neutral
}

public enum ElementEnum
{
    neutral,
    fire,
    lightning,
    ice,
    heal
}

public enum WildcardEnum
{
    lookClosely,
    wasThatThere,
    badAtMath,
    neutral
}