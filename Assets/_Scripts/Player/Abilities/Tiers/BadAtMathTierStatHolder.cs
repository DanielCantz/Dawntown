using UnityEngine;
///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for BadAtMath.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class BadAtMathTierStatHolder
{
    [SerializeField] private float _damageRangeFactor;
    [SerializeField] private float _lifetimeRangeFactor;

    public float DamageRangeFactor { get => _damageRangeFactor; }
    public float LifetimeRangeFactor { get => _lifetimeRangeFactor; }
}
