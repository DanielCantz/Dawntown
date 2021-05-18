using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for defensive attacks.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class WorldTransformTierStatHolder
{
    [SerializeField] private float _scale;
    [SerializeField] private float _lifetime;

    public float Scale { get => _scale; }
    public float Lifetime { get => _lifetime; }
}
