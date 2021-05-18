using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: containter for buffs and debuffs. Also syncs informations to the hud
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class BuffBar : MonoBehaviour
{
    private Dictionary<Buff, GameObject> _buffs = new Dictionary<Buff, GameObject>();

    [SerializeField]
    private GameObject HUDBuffPrefab;

    public void AddBuff(Buff buff)
    {
        GameObject hudBuff = Instantiate(HUDBuffPrefab, transform);
        hudBuff.GetComponent<HUDBuffSyncer>().Buff = buff;
        _buffs.Add(buff, hudBuff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (_buffs.ContainsKey(buff))
        {
            GameObject hudBuff;
            _buffs.TryGetValue(buff, out hudBuff);
            if (hudBuff != null)
            {
                Destroy(hudBuff);
            }
        }
    }
}
