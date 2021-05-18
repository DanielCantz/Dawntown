using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///
/// Author: Samuel Müller: sm184
/// Description: displays the health data provided from a stathandler component to the Healthbar HUD.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    [SerializeField]
    private Color _color = Color.green;

    [SerializeField]
    private Image _fill;

    [SerializeField]
    protected Image _elementIcon;

    [SerializeField]
    protected StatHandler _statHandler;

    [SerializeField]
    private bool _playerHealthBar = false;

    virtual protected void Start()
    {
        if (_playerHealthBar)
        {
            _statHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>();
        }
        _slider = GetComponent<Slider>();
        _fill.color = _color;
        if (_elementIcon != null)
        {
            _elementIcon.sprite = AbilityUtil.GetAbilityHUDElement(_statHandler.Element).Sprite;
        }
    }


    void Update()
    {
        _slider.value = (float) _statHandler.CurrentHealth / (float) _statHandler.MaxHealth;             
    }

}
