using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for LookClosely.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class LookCloselyTierStatHolder
{
    [SerializeField] private float _barrelSpread;
    [SerializeField] private float _bladeSpread;
    [SerializeField] private float _aimDistance;
    [SerializeField] private float _placementOffset;

    public float BarrelSpread { get => _barrelSpread; }
    public float BladeSpread { get => _bladeSpread; }
    public float AimDistance { get => _aimDistance; }
    public float PlacementOffset { get => _placementOffset; }

}
