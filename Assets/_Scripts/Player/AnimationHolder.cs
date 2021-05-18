using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Holds all Animation controller for the player character and returns them in form of a matrix.
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName ="Managers/AnimationHolder")]
public class AnimationHolder : ScriptableObject
{
    [SerializeField] private AnimatorOverrideController _bladeBarrel;
    [SerializeField] private AnimatorOverrideController _bladeBlade;
    [SerializeField] private AnimatorOverrideController _bladeMagic;
    [SerializeField] private AnimatorOverrideController _magicBarrel;
    [SerializeField] private AnimatorOverrideController _magicBlade;
    [SerializeField] private AnimatorOverrideController _magicMagic;
    [SerializeField] private AnimatorOverrideController _barrelBarrel;
    [SerializeField] private AnimatorOverrideController _barrelBlade;
    [SerializeField] private AnimatorOverrideController _barrelMagic;

    public AnimatorOverrideController[,] GetOverrideMatrix()
    {
        AnimatorOverrideController[,] overrides = new AnimatorOverrideController[4,4];
        overrides[(int)WeaponEnum.blade, (int)WeaponEnum.blade] = _bladeBlade;
        overrides[(int)WeaponEnum.blade, (int)WeaponEnum.barrel] = _bladeBarrel;
        overrides[(int)WeaponEnum.blade, (int)WeaponEnum.magiccore] = _bladeMagic;
        overrides[(int)WeaponEnum.blade, (int)WeaponEnum.neutral] = _bladeBlade;
        overrides[(int)WeaponEnum.barrel, (int)WeaponEnum.blade] = _barrelBlade;
        overrides[(int)WeaponEnum.barrel, (int)WeaponEnum.barrel] = _barrelBarrel;
        overrides[(int)WeaponEnum.barrel, (int)WeaponEnum.magiccore] = _barrelMagic;
        overrides[(int)WeaponEnum.barrel, (int)WeaponEnum.neutral] = _barrelBarrel;
        overrides[(int)WeaponEnum.magiccore, (int)WeaponEnum.blade] = _magicBlade;
        overrides[(int)WeaponEnum.magiccore, (int)WeaponEnum.barrel] = _magicBarrel;
        overrides[(int)WeaponEnum.magiccore, (int)WeaponEnum.magiccore] = _magicMagic;
        overrides[(int)WeaponEnum.magiccore, (int)WeaponEnum.neutral] = _magicMagic;
        overrides[(int)WeaponEnum.neutral, (int)WeaponEnum.blade] = _magicBlade;
        overrides[(int)WeaponEnum.neutral, (int)WeaponEnum.barrel] = _magicBarrel;
        overrides[(int)WeaponEnum.neutral, (int)WeaponEnum.magiccore] = _magicMagic;
        overrides[(int)WeaponEnum.neutral, (int)WeaponEnum.neutral] = _magicMagic;
        return overrides;
    }
}
