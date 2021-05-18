using System.Collections;
using UnityEngine;
using UnityEngine.UI;
///
/// Author: Samuel Müller: sm184
/// Description: Healthbar for enemy units.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class OverheadHealthBar : HealthBar
{
    override protected void Start()
    {
        base.Start();
        _elementIcon.sprite = AbilityUtil.GetAbilityHUDElement(_statHandler.Element).Sprite;
    }
}
