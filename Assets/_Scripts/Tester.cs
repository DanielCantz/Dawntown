using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: test component that can be added to GameObjects to test other components. Feel free to add testing methods.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class Tester : MonoBehaviour
{
    //inventory
    public bool TestItemCreation = true;
    public bool TestTierUpdate = false;

    void Update()
    {
        if (Input.GetButtonDown("Test"))
        {
            Test();
        }

        Inventory inventory = GetComponent<Inventory>();
        if (inventory && TestTierUpdate)
        {
            for (int i = 1; i <= Item.TIER_RANGE; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    foreach (ItemSlot slot in inventory.ItemSlots)
                    {
                        Item item = slot.GetItem();
                        if (!(item is NullItem))
                        {
                            int tier = Mathf.Clamp(i, 1, Item.TIER_RANGE);
                            slot.SetItemAndReload(item.CreateCopyWithTier(tier));
                        }
                    }
                }
            }
        }
    }

    private void Test()
    {
        TestInventory();
        TestStageIndicator();
        TestUnitHasHealth();
        TestBuffHandler();
    }
    private void TestInventory()
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            return;
        }
        if (TestItemCreation)
        {
            Item item = Item.GetRandomItem(3);
            inventory.Reset();
            while (!inventory.IsFull())
            {
                inventory.AddItem(item);
            }
        }
    }

    private void TestUnitHasHealth()
    {
        StatHandler health = GetComponent<StatHandler>();
        if (health == null)
        {
            return;
        }
        health.TakeDamage(10);
    }

    private void TestStageIndicator()
    {
        StageIndicator indicator = GetComponent<StageIndicator>();
        if (indicator == null)
        {
            return;
        }
        indicator.IncreaseStage();
    }

    private void TestBuffHandler()
    {
        StatHandler statHandler = GetComponent<StatHandler>();
        BuffHandler buffHandler = GetComponent<BuffHandler>();
        if (buffHandler == null)
        {
            return;
        }
        buffHandler.AddBuff(new Burn(statHandler, new Stat(5), 10));
        Debug.Log("Burned!");
        buffHandler.AddBuff(new Slow(statHandler, new Stat(5), new ScalingStatModificator(0.5f)));
        Debug.Log("Slowed!");
    }
}
