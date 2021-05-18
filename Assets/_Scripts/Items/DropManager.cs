using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///
/// Author: Samuel Müller: sm184
/// Description: Scriptable object holding the information for drop probabilities. Essential to make balancing easier.
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(menuName = "Managers/Dropchance")]
public class DropManager : ScriptableObject
{
    [Tooltip("Chance an item will be dropped at all")]
    [Range(0, 1)]
    [SerializeField] private float _overallDropChance = 0.5f;

    public float OverallDropChance { get => _overallDropChance; }

    [Tooltip("Priority of the item being dropped to be of type Weapon")]
    [Min(1)]
    [SerializeField] private int _weaponDropPriority;

    [Tooltip("Priority of the item being dropped to be of type Wildcard")]
    [Min(1)]
    [SerializeField] private int _wildcardDropPriority;

    [Tooltip("Priority of the item being dropped to be of type Element")]
    [Min(1)]
    [SerializeField] private int _elementDropPriority;

    [Tooltip("Priority of the item being dropped to be a healing orb")]
    [Min(1)]
    [SerializeField] private int _healOrbDropPriority;

    private static GameObject _collectableItem;
    private static GameObject _healItem;

    public GameObject DropItem(EnemyStatHandler statHandler)
    {
        if (_collectableItem == null)
        {
            _collectableItem = Resources.Load<GameObject>("Prefabs/Items/Interactible");
        }

        if (_healItem == null)
        {
            _healItem = Resources.Load<GameObject>("Prefabs/Items/HealOrb");
        }

        float value = Random.Range(0, 1f);
        if (value > _overallDropChance)
        {
            return null;
        }

        GameObject interactible;
        int totalDropPriority = _healOrbDropPriority + _weaponDropPriority + _elementDropPriority + _wildcardDropPriority;

        int random = Random.Range(1, totalDropPriority+1);

        if (random <= totalDropPriority - _healOrbDropPriority)
        {
            interactible = Instantiate(_collectableItem, statHandler.transform.position, statHandler.transform.rotation);
            interactible.GetComponent<CollectableItem>().Item = GetItemToDrop(statHandler, random);
        }
        else
        {
            interactible = Instantiate(_healItem, statHandler.transform.position, statHandler.transform.rotation);
        }
        if (interactible)
        {
            Destroy(interactible, Item.TIME_TO_LIVE); //deletes Item after 3 minutes
        }
        return interactible;
    }

    private Item GetItemToDrop(EnemyStatHandler statHandler, int random)
    {
        if (random <= _wildcardDropPriority)
        {
            return new WildcardItem(1, AbilityUtil.GetRandomWildcard());
        }
        if (random <= _wildcardDropPriority + _weaponDropPriority)
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
}
