using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///
/// Author: Samuel Müller: sm184
/// Description: syncs the hud element of a buff with the data inside the buff (e.g. remaining lifetime)
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class HUDBuffSyncer : MonoBehaviour
{
    [SerializeField]
    private Buff _buff;

    [SerializeField]
    private Image _icon;

    [SerializeField]
    private Image _fill;

    private Slider _slider;
    public Buff Buff
    {
        get { return _buff; }
        set
        {
            if (_buff == null)
            {
                _buff = value;
                AbilityHUDElement hudElement = AbilityUtil.GetHudElement(_buff);
                _icon.sprite = hudElement.Sprite;
                _fill.sprite = hudElement.Background;
            }
        }
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void Update()
    {
        _slider.value = 1 - (_buff.Age / _buff.Lifetime);
    }
}
