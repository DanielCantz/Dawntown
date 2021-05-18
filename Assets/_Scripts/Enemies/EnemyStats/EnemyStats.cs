using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Enemy Stats Variables
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "PluggableAI/Stat")]
public class EnemyStats : ScriptableObject
{
    public ElementEnum element;
    [Header("Default Settings")]
    public float StartingHealth;
    public float aggroRange;
    public float looseAggroRange;
    public float rotationSpeed;

    [Header("Idle Settings")]
    public bool idleWalkAround;
    [Range(0, 600)] public float idleChangePositionMin;
    [Range(0, 600)] public float idleChangePositionMax;
    public float IdleAreaSize;

    [Header("Weapon Settings")]
    public GameObject Weapon;
    public float attackRate;
    public float reloadTime;
    public float bulletAmount;
    public float attackRange;
    public float shootingInaccuracy;
    public float duration;
    public float slowAmount;
    public float damage;
}
