using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
///
/// Author: Samuel Müller: sm184
/// Description: manages stats and health for units
/// ==============================================
/// Changelog: 
/// ==============================================
///
[RequireComponent(typeof(BuffHandler))]
public class StatHandler : UnitHasHealth
{
    [HideInInspector]
    public bool IsStunned = false;

    private IStat _armor = new Stat(0);

    private IStat _damage = new Stat(1);

    [SerializeField]
    private Stat _speed = new Stat(5);


    private Boolean isInvinsible = false;

    public bool IsInvinsible
    {
        get => isInvinsible;
        set => isInvinsible = value;
    }

    //public Stat Speed { get => _speed; set => _speed = value; }

    public IStat Speed
    {
        get
        {
            if (IsStunned)
            {
                return new StatProxy(_speed, 0);
            }
            return _speed;
        }
    }

    public IStat Damage { get => _damage; set => _damage = value; }

    #region Enemy
    [SerializeField]
    private ElementEnum _element = ElementEnum.neutral;

    private NavMeshAgent _agent;
    public ElementEnum Element { get => _element; set => _element = value; }

    private void Update()
    {
        if (_agent != null)
        {
            _agent.speed = Speed.GetValue();
            if (IsStunned)
            {
                _agent.velocity = Vector3.zero;
                _agent.angularSpeed = 0;
            }
        }

    }

    #endregion

    override protected void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();
    }

    public override void TakeDamage(float damage)
    {
        if (!isInvinsible)
        {
            float damageToTake = damage;
            damageToTake = Mathf.Clamp(damageToTake - _armor.GetValue(), 0, damage);
            base.TakeDamage(damageToTake);

            //if (damageToTake > 3)
            //{
            //    FMODUnity.RuntimeManager.PlayOneShot("event:/PC/Taking Damage");
            //}
            
        }
    }

    internal void TakeTrueDamage(float damage)
    {
        base.TakeDamage(Mathf.Clamp(damage, 0, MaxHealth));
    }
}
