using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Buff
{
    private StatHandler _statHandler;

    private float _lastSecondTick = 0;

    private float _damagePerSecond;

    private static List<Type> _relatedTypes = new List<Type>() { typeof(Burn) };

    public override List<Type> RelatedTypes { get => _relatedTypes; }

    public Burn(StatHandler statHandler, IStat lifetime, float damagePerSecond) : base(lifetime)
    {
        _statHandler = statHandler;
        _damagePerSecond = damagePerSecond;
    }

    public override void OnBuffDecay()
    {
        //endBurnSound
    }

    public override void OnBuffGain()
    {
        //_statHandler.Damage = _damagePerSecond;
    }

    protected override void BuffEffect()
    {
        _statHandler.TakeDamage(_damagePerSecond * Time.deltaTime);
        _lastSecondTick++;
    }
}
