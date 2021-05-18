using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller, sm184
/// Description: Holds all scaling values based on tier for Offensive attacks.
/// ==============================================
/// Changelog:
/// ==============================================
///
[System.Serializable]
public class ProjectileTierStatHolder : WorldTransformTierStatHolder
{
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _lifedrain;
    public float Damage { get => _damage; }
    public float Range { get => _range; }
    public float Lifedrain { get => _lifedrain; }
}
