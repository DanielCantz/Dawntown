using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///
/// Author: Fred Newton, Akdogan : fa019
/// Description: Spawns at a spawn rate and a maximum spawn count as long as opponents in a spawn area on the Navmesh opponent. 
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn")]
    [Range(0, 600)] public float SpawnRate;
    public float SpawnAreaSize;
    public int StopSpawning;
    public int WaveCount;
    public EnemyGroupContainer[] enemyGroupContainers;

    private EnemyGroupContainer currentGroup;
    private float spawnTimer;
    private int waveDone = 0;
    private int spawnedEnemeys;

    private void Start()
    {
        spawnTimer = SpawnRate;
        currentGroup = enemyGroupContainers[Random.Range(0, enemyGroupContainers.Length)];
    }
    
    void Update()
    {
        if (waveDone < WaveCount)
        {
            if (spawnedEnemeys < StopSpawning)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0)
                {
                    SpawnEnemy();
                    spawnedEnemeys++;
                    spawnTimer = SpawnRate;
                }
            } else if (gameObject.transform.childCount == 0)
            {
                spawnedEnemeys = 0;
                waveDone++;
            }
        }
    }

    /// <summary>
    /// Spawns an enemy below the spawner in the hierarchy so that the scene is more tidy and gives his Idle Area position the point. 
    /// </summary>
    private void SpawnEnemy()
    {
        GameObject enemy = currentGroup.getRandomEnemy();

        var t = Instantiate(enemy, findSpawnPoint(), enemy.transform.rotation) as GameObject;
        t.GetComponent<EnemyStatHandler>().IdleArea = this.gameObject.transform.position;
        t.name = enemy.name;
        t.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Searches a position on the Navmesh within the SpawnArea
    /// </summary>
    /// <returns>Spawn Position</returns>
    private Vector3 findSpawnPoint()
    {
        bool foundPoint = false;
        Vector3 res = new Vector3();

        while (!foundPoint)
        {
            res = gameObject.transform.position + Random.insideUnitSphere * SpawnAreaSize;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(res, out hit, 1.0f, NavMesh.AllAreas) && Vector3.Distance(res, gameObject.transform.position) <= SpawnAreaSize )
            {
                if (hit.position.y >= -0.5)
                {
                    foundPoint = true;
                    return hit.position;
                }   
            }
        }
        return res;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, SpawnAreaSize);
    }
}

