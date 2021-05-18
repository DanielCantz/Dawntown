using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: merges all items of the inventory component of the same game object into an upgraded version if possible.
/// ==============================================
/// Changelog: 
/// ==============================================
///
[RequireComponent(typeof(Inventory))]
public class ItemMerger : MonoBehaviour
{
    [SerializeField] private Inventory _fallbackInventory;

    [SerializeField] private ItemSlot _previewSlot;

    private Inventory _mergeInventory;

    private void Awake()
    {
        _mergeInventory = GetComponent<Inventory>();
        foreach (ItemSlot slot in _mergeInventory.ItemSlots)
        {
            slot.onItemChange += UpdatePreview;
        }
        _previewSlot.onItemExit += _mergeInventory.Reset;
    }

    private void UpdatePreview()
    {
        if (AreItemsMergable())
        {
            _previewSlot.SetItemAndReload(GenerateNewItem(_mergeInventory.ItemSlots[0].GetItem()));
        } else
        {
            _previewSlot.SetItemAndReload(new NullItem());
        }
    }

    public void MergeItems()
    {
        Debug.Log("Merging");
        if (AreItemsMergable())
        {
            if (_fallbackInventory.AddItem(GenerateNewItem(_mergeInventory.ItemSlots[0].GetItem())))
            {
                _mergeInventory.Reset();

                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Upgrade");
            }
        }
        else
        {
            Debug.Log("Merging failed");

            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/UpgradeFailed");

            PopItemsBackToInventory();
        }
    }

    public void PopItemsBackToInventory()
    {
        Debug.Log(_fallbackInventory.ItemSlots.Count);
        foreach (ItemSlot slot in _mergeInventory.ItemSlots)
        {
            Item slotItem = slot.GetItem();
            if (!(slotItem is NullItem) && !_fallbackInventory.AddItem(slotItem))
            {
                DropUtil.DropItem(GameObject.FindGameObjectWithTag("Player").transform.Find("BulletSpawn").transform, slot.GetItem());
            }
            slot.SetItemAndReload(new NullItem());
        }
    }

    private bool AreItemsMergable()
    {
        if (!_mergeInventory.IsFull())
        {
            return false;
        }
        return AreItemsMergable(_mergeInventory.ItemSlots);
    }

    public static bool AreItemsMergable(List<ItemSlot> slots)
    {
        Item firstItem = slots[0].GetItem();
        if (firstItem.Tier >= Item.TIER_RANGE)
        {
            return false;
        }
        foreach (ItemSlot itemSlot in slots)
        {
            if (!itemSlot.GetItem().Equals(firstItem))
            {
                return false;
            }
        }
        return true;
    }

    private Item GenerateNewItem(Item item)
    {
        Item generatedItem;
        switch (item)
        {
            case ElementItem type1:
                generatedItem = new ElementItem(Mathf.Clamp(item.Tier + 1, 0, Item.TIER_RANGE), ((ElementItem)item).Element);
                break;
            case WeaponItem type2:
                generatedItem = new WeaponItem(Mathf.Clamp(item.Tier + 1, 0, Item.TIER_RANGE), ((WeaponItem)item).Weapon);
                break;
            case WildcardItem type3:
                generatedItem = new WildcardItem(Mathf.Clamp(item.Tier + 1, 0, Item.TIER_RANGE), ((WildcardItem)item).Wildcard);
                break;
            default:
                generatedItem = new NullItem();
                break;
        }
        return generatedItem;
    }
}
