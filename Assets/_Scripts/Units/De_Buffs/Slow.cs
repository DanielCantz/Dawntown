using System;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Buff
{
    private StatHandler _statHandler;

    private ScalingStatModificator _slow;

    private static List<Type> _relatedTypes = new List<Type>() { typeof(Slow), typeof(Stun) };

    public override List<Type> RelatedTypes { get => _relatedTypes; }

    public Slow(StatHandler target, IStat duration, ScalingStatModificator slow) : base(duration)
    {
        _statHandler = target;
        _slow = slow;
    }

    public override void OnBuffDecay()
    {
        _statHandler.Speed.RemoveMod(_slow);
    }

    public override void OnBuffGain()
    {
        _statHandler.Speed.AddMod(_slow);
    }

    protected override void BuffEffect()
    {
        if (!_statHandler.Speed.HasMod(_slow))
        {
            _statHandler.Speed.AddMod(_slow);
        }
        //freezing sound here
    }
}
