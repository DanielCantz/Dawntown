using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMergingFunction : MonoBehaviour
{
    private Inventory inventory;
    private List<ItemSlot> itemSlots;
    private void Start()
    {
         inventory = GetComponent<Inventory>();
         itemSlots = inventory.ItemSlots;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Test"))
        {
            Item item = GetRandomItem();
            foreach (ItemSlot slot in itemSlots)
            {
                slot.SetItemAndReload(item);
            }
        }
    }

    private Item GetRandomItem()
    {
        Item item;
        switch (Random.Range(0, 3))
        {
            case 0:
                item = new ElementItem(Random.Range(1, Item.TIER_RANGE + 1), AbilityUtil.GetRandomElement());
                Debug.Log(Item.TIER_RANGE);
                break;
            case 1:
                item = new WildcardItem(Random.Range(1, Item.TIER_RANGE + 1), AbilityUtil.GetRandomWildcard());
                break;
            case 2:
                item = new WeaponItem(Random.Range(1, Item.TIER_RANGE + 1), AbilityUtil.GetRandomWeapon());
                break;
            default:
                item = new NullItem();
                Debug.Log("Default Case");
                break;
        }
        return item;
    }

}
