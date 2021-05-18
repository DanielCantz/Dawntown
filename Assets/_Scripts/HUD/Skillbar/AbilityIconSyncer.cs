using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
///
/// Author: Samuel Müller: sm184
/// Description: syncs Skillbar-HUD with the items inserted into the equipment slots of the inventory.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class AbilityIconSyncer : MonoBehaviour
{
    [SerializeField]
    private Image _weaponIcon;
    [SerializeField]
    private Image _elementIcon;
    [SerializeField]
    private Image _wildcardIcon;

    private void Start()
    {
        AbilityHUDElement info = AbilityUtil.GetNullHUDElement();
        SyncElement(info);
        SyncWeapon(info);
        SyncWildcard(info);
    }

    public void SyncElement(AbilityHUDElement info)
    {
        _elementIcon.sprite = info.Background;
    }

    public void SyncWeapon(AbilityHUDElement info)
    {
        _weaponIcon.sprite = info.Sprite;
        _weaponIcon.color = info.Color;
    }

    public void SyncWildcard(AbilityHUDElement info)
    {
        _wildcardIcon.sprite = info.Sprite;
        _wildcardIcon.color = info.Color;
    }
}
