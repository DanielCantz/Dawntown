using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialWeaponSpawner : MonoBehaviour
{
    [SerializeField] private Transform swordTransform;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private Transform magiccoreTransform;


    // Start is called before the first frame update
    void Start()
    {
        GameObject _collectableItem = Resources.Load<GameObject>("Prefabs/Items/Interactible");
        GameObject sword = Instantiate(_collectableItem, swordTransform.position, swordTransform.rotation);
        GameObject barrel = Instantiate(_collectableItem, barrelTransform.position, barrelTransform.rotation);
        GameObject magiccore = Instantiate(_collectableItem, magiccoreTransform.position, magiccoreTransform.rotation);


        sword.GetComponent<CollectableItem>().Item = new WeaponItem(1, WeaponEnum.blade);
        barrel.GetComponent<CollectableItem>().Item = new WeaponItem(1, WeaponEnum.barrel);
        magiccore.GetComponent<CollectableItem>().Item = new WeaponItem(1, WeaponEnum.magiccore);
    }

}