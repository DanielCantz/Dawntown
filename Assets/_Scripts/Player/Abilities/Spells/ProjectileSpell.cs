using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: base behaviour and values of all offensive attacks.
/// ==============================================
/// Changelog:
/// Samuel Müller - refactorings & tier implementation
/// ==============================================
///
[CreateAssetMenu(menuName = "Abilities/ProjectileSpell")]
public class ProjectileSpell : AbstractSpell
{
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private float _projectileForce = 500f;
    [Tooltip("Only for sword: changes the size of the sword swing around the player (360 swings one time around)")]
    [SerializeField] private float _baseProjectileSpread;
    [SerializeField] private float _baseDamageRange;
    [SerializeField] private float _baseCritChance;
    [SerializeField] private int _numberOfProjectiles;
    [SerializeField] private List<ProjectileTierStatHolder> _tierStats;

    //wild card mods
    private float _projectileSpread;
    private float _damageRange;
    private float _critChance;


    private Vector3 _directionVector;

    private Transform _bulletSpawn;
    private ProjectileShootTriggerable _launcher;

    public Rigidbody Projectile { get => _projectile; }
    public float ProjectileForce { get => _projectileForce; }

    public float BaseLifetime
    {
        get => _tierStats[0].Lifetime;
    }

    public float Lifetime { get => _tierStats[Tier - 1].Lifetime; }
    public float Range { get => _tierStats[Tier - 1].Range; }

    public int NumberOfProjectiles { get => _numberOfProjectiles; }
    public Transform BulletSpawn { get => _bulletSpawn; }
    public float BaseDamageRange { get => _baseDamageRange; }
    public float Damage { get
        {
            return _tierStats[Tier - 1].Damage;
        }
    }
    public float Lifedrain { get
        {
            return _tierStats[Tier - 1].Lifedrain;
        }
    }

    public float BaseScale { get => _tierStats[0].Scale; }

    public float Scale
    {
        get
        {
            Debug.Log(Tier+" => "+(Tier-1));
            return _tierStats[Tier - 1].Scale;
        }
    }
    public float BaseProjectileSpread { get => _baseProjectileSpread; }
    public float ProjectileSpread { get => _projectileSpread; set => _projectileSpread = value; }
    public float DamageRange { get => _damageRange; set => _damageRange = value; }

    public Vector3 DirectionVector
    {
        get => _directionVector;
        set => _directionVector = value;
    }


    public float BaseCritChance => _baseCritChance;

    public float CritChance
    {
        get => _critChance;
        set => _critChance = value;
    }

    public override void Initialize(GameObject obj)
    {
        _launcher = new ProjectileShootTriggerable(this);
        _projectileSpread = _baseProjectileSpread;
        _damageRange = _baseDamageRange;
        _critChance = _baseCritChance;
        _bulletSpawn = GameObject.FindGameObjectWithTag("PlayerBulletSpawn").transform;
    }

    public override void TriggerSpell()
    {
        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            _launcher.Launch();
        }
    }

    public void ResetSpell()
    {
        _projectileSpread = _baseProjectileSpread;
        _damageRange = _baseDamageRange;
        _critChance = _baseCritChance;
    }
}
