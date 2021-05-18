using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
///
/// Author: Samuel Müller, sm184
/// Audio-Part by Michael Dmoch, md118
/// Description: Essential component for visualization of items in ItemUI
/// ==============================================
/// Changelog: 
/// ==============================================

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Item _item;

    [SerializeField]
    private Image _itemIcon;

    [SerializeField]
    private Text _itemTier;

    [SerializeField]
    private SlotType _slotType;

    [SerializeField]
    private ItemTooltip _itemTooltip;

    [SerializeField]
    private bool _isDroppable = true;

    public bool IsItemDroppable { get => _isDroppable; }

    public delegate void OnItemChange();

    public OnItemChange onItemChange;

    public delegate void OnItemExit();

    public OnItemExit onItemExit;

    private void OnValidate()
    {
        if(_itemTooltip == null)
        {
            _itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    public bool SetItemAndReload(Item newItem, out Item fallbackItem)
    {
        if (newItem.GetValidSlotTypes().Contains(_slotType))
        {
            fallbackItem = _item;
            _item = newItem;
            Reload();
            return true;
        }
        fallbackItem = newItem;
        Reload();
        return false;
    }

    public void SetItemAndReload(Item item)
    {
        if (item.GetValidSlotTypes().Contains(_slotType))
        {
            _item = item;
        }
        Reload();
    }

    private bool ItemCanBeSlotted(Item item)
    {
        return item.GetValidSlotTypes().Contains(_slotType);
    }

    public Item GetItem()
    {
        return _item;
    }

    public void Reload()
    {
        if (!(_item is NullItem))
        {
            _itemIcon.enabled = true;
            _itemTier.enabled = true;
            _itemTier.text = _item.Tier.ToString();
            _itemIcon.sprite = _item.HudInfo.Sprite;
        }
        else
        {
            _itemTier.enabled = false;
            _itemIcon.enabled = false;
        }
        if (onItemChange != null)
        {
            onItemChange.Invoke();
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        ItemSlot previousOwner = ItemDragHandler.PreviousItemSlot;
        if (!_isDroppable || !(_item is NullItem) && !previousOwner.IsItemDroppable)
        {
            return;
        }
        
        if (this._slotType != SlotType.neutral)
        {
            if(this._item is NullItem)
            {
                //trigger drop sound for successful equip into empty slot   
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Place");
            }
            else
            {
                //trigger drop sound for successful equip into full slot (swap)
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Swap");
            }
        }
        else
        {
            //trigger sound for swapping items in backpack
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Swap");            
        }

        ItemSlot.SwapItems(previousOwner, this);
    }

    public static void SwapItems(ItemSlot origin, ItemSlot target)
    {
        //item can't be swapped, if one of the itemslots can't have the other item
        if (!origin.ItemCanBeSlotted(target.GetItem()) || !target.ItemCanBeSlotted(origin.GetItem()))
        {
            return;
        }

        if (target.SetItemAndReload(origin.GetItem(), out Item fallbackItem) && origin.onItemExit != null)
        {
            origin.onItemExit.Invoke();
            
        }
        origin.SetItemAndReload(fallbackItem);

    }

    public override string ToString()
    {
        return "[" + _item.ToString() + _item.Tier.ToString() + "]";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!(_item is NullItem))
        {
            _itemTooltip.ShowTooltip(_item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         _itemTooltip.HideTooltip();
    }
}

public enum SlotType
{
    weapon,
    element,
    wildcard,
    neutral
}



