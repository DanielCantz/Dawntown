using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatHandler))]
public class BuffHandler : MonoBehaviour
{
    [SerializeField]
    private List<Buff> _buffs = new List<Buff>();

    [SerializeField]
    private BuffBar _buffBar;
    public List<Buff> Buffs { get => _buffs; }

    private void Start()
    {
        if (CompareTag("Player"))
        {
            _buffBar = GameObject.FindGameObjectWithTag("Buffbar").GetComponent<BuffBar>();
        }
    }

    void LateUpdate()
    {
        _buffs.ForEach(buff => buff.TickBuff());
        List<Buff> decayedBuffs = GetDecayedBuffs();
        decayedBuffs.ForEach(buff => RemoveBuff(buff));
    }

    public void AddBuff(Buff buff)
    {
        Buff existingBuff = GetExistingRelative(buff);
        if (existingBuff != null)
        {
                RemoveBuff(existingBuff);
        }
        if (_buffBar!= null)
        {
            _buffBar.AddBuff(buff);
            _buffs.Add(buff);
        }
    }


    public bool ContainsBuffOfType(Type buffType)
    {
        foreach (Buff buff in _buffs)
        {
            if (buff.GetType() == buffType)
            {
                return true;
            }
        }
        return false;
    }

    private Buff GetExistingRelative(Buff buff)
    {
        foreach (Buff existingBuff in _buffs)
        {
            if (existingBuff.GetType() == buff.GetType())
            {
                return existingBuff;
            }
        }
        return null;
    }

    private List<Buff> GetDecayedBuffs()
    {
        List<Buff> decayedBuffs = new List<Buff>();
        foreach (Buff buff in _buffs)
        {
            if (buff.HasDecayed)
            {
                decayedBuffs.Add(buff);
            }
        }
        return decayedBuffs;
    }

    public void RemoveBuff(Buff buff)
    {
        Buff buffToRemove;
        if (_buffs.Contains(buff))
        {
            buffToRemove = buff;
        }
        else
        {
            buffToRemove = GetExistingRelative(buff);
        }

        if (buffToRemove != null)
        {
            if (_buffBar != null)
            {
                _buffBar.RemoveBuff(buff);
            }
            buffToRemove.OnBuffDecay();
            _buffs.Remove(buff);
        }
    }
}
