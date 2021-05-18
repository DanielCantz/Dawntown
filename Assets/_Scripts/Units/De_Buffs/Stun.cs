using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stun : Buff
{

    private StatHandler _statHandler;

    private static List<Type> _relatedTypes = new List<Type>() { typeof(Slow), typeof(Stun) };

    public override List<Type> RelatedTypes { get => _relatedTypes; }

    public Stun(StatHandler statHandler, IStat duration) : base(duration)
    {
        _statHandler = statHandler;
    }

    protected override void BuffEffect()
    {
        if (!_statHandler.IsStunned)
        {
            _statHandler.IsStunned = true;
        }
    }

    public override void OnBuffGain()
    {
        // Activate Stun Effect
        _statHandler.IsStunned = true;
        //TODO add enemy disable here
    }

    public override void OnBuffDecay()
    {
        // Deactivate Stun Effect
        _statHandler.IsStunned = false;
    }
}
