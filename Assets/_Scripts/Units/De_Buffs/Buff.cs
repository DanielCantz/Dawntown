using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: class mend to be overwritten. Defines behaviour of buffs and debuffs.
/// ==============================================
/// Changelog: 
/// ==============================================
///
[System.Serializable]
public abstract class Buff
{
    [SerializeField]
    private IStat _lifetime;

    [SerializeField]
    private float _tickTime = 0;

    [SerializeField]
    private float _lastTickTime = 0;

    [SerializeField]
    protected float _age = 0;
    [SerializeField]
    private bool _hasDecayed = false;

    public bool HasDecayed { get => _hasDecayed; }

    public float Age { get => _age; }

    public float Lifetime { get => _lifetime.GetValue(); }

    public abstract List<Type> RelatedTypes { get; }

    public Buff(IStat lifetime)
    {
        _lifetime = lifetime;
    }

    public Buff(IStat lifetime, float tickTime)
    {
        _lifetime = lifetime;
        _tickTime = tickTime;
    }

    public void ResetAge()
    {
        _age = 0.001f;
    }


    //Call this in MonoBehaviour Update method
    public virtual void TickBuff()
    {
        if (!_hasDecayed && _age <= _lifetime.GetValue())
        {
            _hasDecayed = false;
            if (_age == 0)
            {
                OnBuffGain();
            }

            _age += Time.deltaTime;
            if (_age > _lifetime.GetValue())
            {
                OnBuffDecay();
                _hasDecayed = true;
            }
            else
            {
                if ((_age - _lastTickTime) > _tickTime)
                {
                    BuffEffect();
                    _lastTickTime = _age;
                }
            }
        }
    }

    //overwrite this method to define effects, when unit gets this buff.
    public abstract void OnBuffGain();

    //overwrite this method to define the effect this buff has every tick.
    protected abstract void BuffEffect();

    //overwrite this method to define the effect this buff has when it decays.
    public abstract void OnBuffDecay();
}
