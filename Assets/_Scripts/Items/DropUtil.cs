using System;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Samuel Müller: sm184
/// Description: Helper class to load and instantiate collectable items when player or enemies shall drop them.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class DropUtil
{
    private static GameObject _collectableItem;
    private static GameObject _healItem;
    public static GameObject DropItem(EnemyStatHandler statHandler, float dropChance)
    {
        if (_collectableItem == null)
        {
            _collectableItem = Resources.Load<GameObject>("Prefabs/Items/Interactible");
        }

        if (_healItem == null)
        {
            _healItem = Resources.Load<GameObject>("Prefabs/Items/HealOrb");
        }

        float value = UnityEngine.Random.Range(0, 1f);
        Debug.Log("DropChance "+value);
        if (value > dropChance)
        {
            return null;
        }
        float healChance = 0.2f;
        float itemDecision = UnityEngine.Random.Range(0,1f);
        GameObject interactible;
        if (itemDecision > healChance)
        {
            interactible = GameObject.Instantiate(_collectableItem, statHandler.transform.position, statHandler.transform.rotation);
            interactible.GetComponent<CollectableItem>().Item = GetItemToDrop(statHandler);
        } else
        {
            interactible = GameObject.Instantiate(_healItem, statHandler.transform.position, statHandler.transform.rotation);
        }
        if (interactible)
        {
            GameObject.Destroy(interactible, Item.TIME_TO_LIVE); //deletes Item after 1 minute
        }
        return interactible;
    }

    public static Item GetItemToDrop(EnemyStatHandler statHandler)
    {
        int random = UnityEngine.Random.Range(1,11);
        int value = 10 - random;
        if (value > 8)
        {
            return new WildcardItem(1, AbilityUtil.GetRandomWildcard());
        }
        if (value > 6)
        {
            if (statHandler.sword != null)
            {
                return new WeaponItem(1, WeaponEnum.blade);
            }
            if (statHandler.shield != null)
            {
                return new WeaponItem(1, WeaponEnum.magiccore);
            }
            return new WeaponItem(1, WeaponEnum.barrel);
        }
        return new ElementItem(1, statHandler.Element);
    }

    public static void DropItem(Transform positionToDrop, Item itemToDrop)
    {
        if (_collectableItem == null)
        {
            _collectableItem = Resources.Load<GameObject>("Prefabs/Items/Interactible");
        }
        Transform playerPosition = positionToDrop.transform;
        GameObject interactible = GameObject.Instantiate(_collectableItem, playerPosition.position, positionToDrop.rotation);
        EventLog.Instance.ItemDropped(itemToDrop);
        interactible.GetComponent<CollectableItem>().Item = itemToDrop;
        GameObject.Destroy(interactible, Item.TIME_TO_LIVE);
        interactible.GetComponent<CollectableItem>().CollectCooldown();
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/Inventory/Drop");
    }
}
