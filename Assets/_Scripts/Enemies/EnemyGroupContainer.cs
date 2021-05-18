using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Enemy container for the spawner
/// ==============================================
/// Changelog: 
/// ==============================================
///

[CreateAssetMenu(menuName = "Enemy/Spawner/GroupContainer")]
public class EnemyGroupContainer : ScriptableObject
{
    public GameObject[] enemyTypes;

    public GameObject getRandomEnemy()
    {
        return enemyTypes[Random.Range(0, enemyTypes.Length)];
    }
}
