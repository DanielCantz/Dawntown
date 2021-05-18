using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
///
/// Author: Samuel Müller, sm184
/// Description: Instantiate inventory to store information about items the player is holding 
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<ItemSlot> _itemSlots = new List<ItemSlot>();

    public List<ItemSlot> ItemSlots { get => _itemSlots; }

    private void Awake()
    {
        SetUp();
    }

    public void SetUp()
    {
        _itemSlots = GetAllItemSlots(transform);
        InitializeUI();
    }

    private List<ItemSlot> GetAllItemSlots(Transform currentTransform)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        foreach (Transform child in currentTransform)
        {
            ItemSlot childItemSlot = child.gameObject.GetComponent<ItemSlot>();
            if (childItemSlot != null)
            {
                slots.Add(childItemSlot);
            }
            else
            {
                slots.AddRange(GetAllItemSlots(child));
            }
        }
        return slots;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Print"))
        {
            PrintInventory();
        }
    }


    public void Reset()
    {
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            itemSlot.SetItemAndReload(new NullItem());
        }
    }

    public void InitializeUI()
    {
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            if (itemSlot.GetItem() == null)
            {
                itemSlot.SetItemAndReload(new NullItem());
            }
        }
    }

    private void PrintInventory()
    {
        string outputString = "";
        foreach (ItemSlot itemSlot in _itemSlots)
        {
            outputString += System.Environment.NewLine + itemSlot.ToString();
        }
        Debug.Log(outputString);
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].GetItem() is NullItem)
            {
                _itemSlots[i].SetItemAndReload(item);

                return true;
            }
        }
        //Inventory Full sound here;

        return false;
    }

    public bool AddItem(Item item, out Item previousItem, int index)
    {
        if (index > _itemSlots.Count)
        {
            previousItem = new NullItem();
            return false;
        }
        previousItem = _itemSlots[index].GetItem();
        _itemSlots[index].SetItemAndReload(item);

        return true;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].GetItem() == item)
            {
                _itemSlots[i].SetItemAndReload(new NullItem());

                return true;
            }
        }
        return false;
    }

    public void MoveItemToOther(int index, Inventory other)
    {
        if (index <= _itemSlots.Count)
        {
            if (other.AddItem(_itemSlots[index].GetItem()))
            {
                _itemSlots[index].SetItemAndReload(new NullItem());

            }
        }
    }

    public bool IsFull()
    {
        {
            for (int i = 0; i < _itemSlots.Count; i++)
            {
                if (_itemSlots[i].GetItem() is NullItem)
                {
                    return false;
                }
            }
            return true;
        }
    }
}