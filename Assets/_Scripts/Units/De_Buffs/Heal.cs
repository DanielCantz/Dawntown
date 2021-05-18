using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Buff
{
    private StatHandler _statHandler;

    private float _healthPerSecond;

    public Regenerate(StatHandler statHandler, IStat lifetime, float healthPerSecond) : base(lifetime)
    {
        _statHandler = statHandler;
        _healthPerSecond = healthPerSecond;
    }

    private static List<Type> _relatedTypes = new List<Type>() { typeof(Regenerate) };
    public override List<Type> RelatedTypes { get => _relatedTypes; }
    public override void OnBuffDecay()
    {
        //do nothing
    }

    public override void OnBuffGain()
    {
        BuffEffect();
    }

    protected override void BuffEffect()
    {
        _statHandler.Heal(_healthPerSecond);
    }
}
