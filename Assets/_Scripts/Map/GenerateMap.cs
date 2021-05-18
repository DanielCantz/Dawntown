///
/// Author: Christopher Beck
/// Description: Attach this script to an object in the game world to procedurally generate the world.
///              This is the main controller of how the world is generated, so change the function calls in the Awake() function to your liking.
///              Change the SerializableFields to create a different sized map or more/less of some feature.
/// ==============================================
/// Changelog:
/// 07/12/2020 - Christopher Beck - Changed placeables
/// 06/18/2020 - Christopher Beck - Minor changes to show art assets correctly
/// 06/07/2020 - Christopher Beck - Refactored calling of map generation
/// 05/17/2020 - Christopher Beck - Refactored public variables to [SerializedField] and moved Room class to separate file.
/// 07/09/2020 - Daniel Cantz - relocated DontDestroyOnLoad to portal.cs - the player should only be preserved, when he passes the level
/// ==============================================
///
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GenerateMap : MonoBehaviour
{
    /// <summary>
    /// Amount of rooms on the X axis
    /// </summary>
    [SerializeField]
    private int initialXRooms;
    /// <summary>
    /// Amount of rooms on the Y axis
    /// </summary>
    [SerializeField]
    private int initialYRooms;
    /// <summary>
    /// Minimum amount of 2*2 streets
    /// </summary>
    [SerializeField]
    private int minLargeStreets;
    /// <summary>
    /// Maximum amount of 2*2 streets
    /// </summary>
    [SerializeField]
    private int maxLargeStreets;
    /// <summary>
    /// Minimum amount of 2*3 streets
    /// </summary>
    [SerializeField]
    private int minExtraLargeStreets;
    /// <summary>
    /// Maximum amount of 2*3 streets
    /// </summary>
    [SerializeField]
    private int maxExtraLargeStreets;
    /// <summary>
    /// Minimum amount bridges
    /// </summary>
    [SerializeField]
    private int minBridges;
    /// <summary>
    /// Maximum amount bridges
    /// </summary>
    [SerializeField]
    private int maxBridges;
    /// <summary>
    /// Chance that a street contains an enemy spawner (in percent e.g. 30 = 30%)
    /// </summary>
    [SerializeField]
    private int enemySpawnerChance;
    /// <summary>
    /// Array of placeable names and their spawn chance
    /// </summary>
    [SerializeField]
    private Placeable[] placeables;

    // Map of generated rooms
    private Map map;

    /// <summary>
    /// Player prefab object to spawn after world generation
    /// </summary>
    [SerializeField]
    private GameObject playerPrefab;

    /// <summary>
    /// Set to true after the map has been generated
    /// </summary>
    private bool hasMapFinishedGenerating = false;

    void Awake()
    {
        if (!hasMapFinishedGenerating)
        {
            // Create new Map object and add map features
            map = new Map(initialXRooms, initialYRooms);
            map.addBorders();
            map.addStart();
            map.addRiver();
            map.addBridges(minBridges, maxBridges);
            map.addGoal(gameObject);
            map.addLargeStreets(minLargeStreets, maxLargeStreets, minExtraLargeStreets, maxExtraLargeStreets);
            map.addStreets();
            map.addPlaceholders();
            map.addPlaceables(gameObject, placeables);
            map.addSpawner(gameObject, enemySpawnerChance);
            map.markPathFromStartToFinish();
            map.show(gameObject);
            map.improveLights();
            GenerateNavMesh();
            putPlayerInStartRoom();

            hasMapFinishedGenerating = true;
        }
    }

    /// <summary>
    /// Generate the nav mesh
    /// </summary>
    void GenerateNavMesh()
    {
        GameObject walkable = gameObject.transform.Find("Walkable").gameObject;
        walkable.AddComponent<NavMeshSurface>();
        walkable.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;
        walkable.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    /// <summary>
    /// Create a new player and place it in the start room
    /// If a player already exist, move that player to the start room
    /// </summary>
    void putPlayerInStartRoom()
    {
        GameObject oldPlayer = GameObject.FindWithTag("Player");
        if (oldPlayer == null)
        {
            // spawn player
            GameObject player = GameObject.Instantiate(playerPrefab, map.startRoom.getLocation() + new Vector3(0, 1, 0), playerPrefab.transform.rotation);
        }
        else
        {
            oldPlayer.transform.position = map.startRoom.getLocation() + new Vector3(0, 1, 0);
        }
    }
}
