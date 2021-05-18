using System;
using System.Collections;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: Defines Collectable Items and overrides the interfece according to the needs
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class CollectableItem : Interactible
{
    private Item _item;

    private bool _ableToCollect;

    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            UpdateSprite(_item.HudInfo.Sprite);
        }
    }


    [SerializeField]
    private Inventory _inventory;

    override protected void Awake()
    {
        base.Awake();
        _inventory = InventoryManager.Instance.Inventory;
        if (_item == null)
        {
            _item = Item.GetRandomItem(3);
        }
        UpdateSprite(_item.HudInfo.Sprite);
        _ableToCollect = true;
    }

    public override void Interact()
    {
        if (_ableToCollect)
        {
            if (_inventory.AddItem(_item))
            {
                EventLog.Instance.ItemCollected(_item);
                Destroy(gameObject);
            }
        }
    }

    public void CollectCooldown()
    {
        _ableToCollect = false;
        Invoke("SetAbleToPickUp", 5); //Item not collectable for 5 seconds
    }

    private void SetAbleToPickUp()
    {
        _ableToCollect = true;
    }
}
