///
/// Author: Samuel Müller, sm184
/// Description: This class manages the inventory of the player
/// ==============================================
/// Changelog:
/// 07/09/2020 - Daniel Cantz - relocated DontDestroyOnLoad to portal.cs - inventory should only be preserved, if he passes the level
/// ==============================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private AttackSlot _attackSlot;
    [SerializeField]
    private AbilityIconSyncer _attackSync;
    public AttackSlot AttackSlot { get => _attackSlot; }
    public AbilityIconSyncer AttackSync { get => _attackSync; }

    [SerializeField]
    private AttackSlot _defenseSlot;
    [SerializeField]
    private AbilityIconSyncer _defenseSync;
    public AttackSlot DefenseSlot { get => _defenseSlot; }
    public AbilityIconSyncer DefenseSync { get => _defenseSync; }

    public Inventory Inventory { get => _inventory; }

    [SerializeField]
    private CanvasGroup _deathScreen;
    public CanvasGroup DeathScreen { get => _deathScreen; }

    [SerializeField]
    private CanvasGroup _endOfGameScreen;
    public CanvasGroup EndOfGameScreen { get => _endOfGameScreen; }

    private void Start()
    {
        // Make this not get destroyed when new map is loaded
        if (FindObjectsOfType<InventoryManager>().Length > 1)
        {
            Destroy(gameObject);
        } else
        {
            _inventory.SetUp();
            //_inventory.AddItem(new WeaponItem(1, WeaponEnum.magiccore));
            //_inventory.AddItem(new WeaponItem(1, WeaponEnum.blade));
            //_inventory.AddItem(new WeaponItem(1, WeaponEnum.barrel));
            //_inventory.AddItem(new WildcardItem(1, WildcardEnum.badAtMath));
            //_inventory.AddItem(new WildcardItem(1, WildcardEnum.lookClosely));
            //_inventory.AddItem(new WildcardItem(1, WildcardEnum.wasThatThere));
        }
    }
}
