///
/// Author: Christopher Beck
/// Description: This class defines Array representing the Map and the functions to create the map
///              As there are a lot of functions, they are sorted alphabetically.
/// ==============================================
/// Changelog:
/// 07/12/2020 - Christopher Beck - Changed placeables: added placeableStruct class
/// 06/18/2020 - Christopher Beck - Minor changes to show art assets correctly
/// 06/07/2020 - Christopher Beck - Refactored into multiple functions to increase readability, added start to goal path verification
/// 05/25/2020 - Christopher Beck - Created class as part of changing the PCG code to new game design
/// ==============================================
///
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System;
using UnityEngine;

/// <summary>
/// Used in the editor accessible Array for placeables and their spawn chances
/// </summary>
[System.Serializable]
public class Placeable
{
    public string name;
    public int chance;
}

public class Map
{
    /// <summary>
    /// Amount of rooms in X direction
    /// </summary>
    public int xRooms;
    /// <summary>
    /// Amount of rooms in Y direction
    /// </summary>
    public int yRooms;
    /// <summary>
    /// Map of generated rooms, represented as a two dimensional xRooms * yRooms Array
    /// </summary>
    public Room[,] mapArray;
    /// <summary>
    /// List of Vector2's representing locations on the map which are walkable, so no houses or other only visual parts of the map
    /// </summary>
    public List<Vector2Int> walkableLocations;
    /// <summary>
    /// List of Vector2's representing locations on the map which are part of the river
    /// </summary>
    public List<Vector2Int> river;
    /// <summary>
    /// List of Vector2's representing locations on the map which are bridges
    /// </summary>
    public List<Vector2Int> bridges;
    /// <summary>
    /// Room the player starts in
    /// </summary>
    public Room startRoom;
    /// <summary>
    /// Boss room
    /// </summary>
    public Room goalRoom;

    /// <summary>
    /// Create necessary Arrays and Lists
    /// </summary>
    /// <param name="xRooms"></param>
    /// <param name="yRooms"></param>
    public Map(int xRooms, int yRooms)
    {
        this.xRooms = xRooms;
        this.yRooms = yRooms;
        this.mapArray = new Room[xRooms, yRooms];
        walkableLocations = new List<Vector2Int>();
        bridges = new List<Vector2Int>();
        river = new List<Vector2Int>();
    }

    /// <summary>
    /// Add borders around the map
    /// </summary>
    public void addBorders()
    {
        UnityEngine.Debug.Log("Adding map borders.");
        for (int i = 0; i < xRooms; i++)
        {
            mapArray[i, 0] = new Room(i, 0, RoomType.Placeholder);
            mapArray[i, yRooms - 1] = new Room(i, yRooms - 1, RoomType.Placeholder);
        }
        for (int i = 1; i < yRooms - 1; i++)
        {
            mapArray[0, i] = new Room(0, i, RoomType.Placeholder);
            mapArray[xRooms - 1, i] = new Room(xRooms - 1, i, RoomType.Placeholder);
        }
    }

    /// <summary>
    /// Add bridges to the river. Also add the the 2 adjacent streets for each bridge
    /// </summary>
    /// <param name="minBridges">Minimum amount of bridges to spawn</param>
    /// <param name="maxBridges">MAximum amount of bridges to spawn</param>
    public void addBridges(int minBridges, int maxBridges)
    {
        UnityEngine.Debug.Log("Adding bridges.");
        List<Vector2Int> riverCopy = new List<Vector2Int>(river);
        riverCopy.RemoveAll(r => !((river.Contains(r + new Vector2Int(0, 1)) && river.Contains(r + new Vector2Int(0, -1))) || (river.Contains(r + new Vector2Int(1, 0)) && river.Contains(r + new Vector2Int(-1, 0)))) || r.x == 0 || r.x >= xRooms - 1);
        for (int i = 0; i < UnityEngine.Random.Range(minBridges, maxBridges + 1); i++)
        {
            Vector2Int bridgeVector = new Vector2Int(-1, -1); // Vector2Int is not nullable, -1,-1 does not exist on the map and is used instead of null here
            do
            {
                if (riverCopy.Count != 0)
                {
                    bridgeVector = riverCopy[UnityEngine.Random.Range(1, riverCopy.Count - 1)];
                    riverCopy.Remove(bridgeVector);
                }
                else
                {
                    bridgeVector = new Vector2Int(-1, -1);
                    break;
                }

            } while (!bridges.TrueForAll(b => Vector2.Distance(b, bridgeVector) > 2));

            if (bridgeVector != new Vector2Int(-1, -1))
            {
                bridges.Add(new Vector2Int(bridgeVector.x, bridgeVector.y));
                Room bridge = addWalkableRoom(bridgeVector.x, bridgeVector.y, RoomType.Bridge);

                if (river.Contains(bridgeVector + new Vector2Int(0, 1)) || river.Contains(bridgeVector + new Vector2Int(0, -1)))
                {
                    addWalkableRoom(bridgeVector.x + 1, bridgeVector.y, RoomType.Normal);
                    addWalkableRoom(bridgeVector.x - 1, bridgeVector.y, RoomType.Normal);
                    bridge.setRotate(90); //rotate the prefab 90 degrees 
                }
                else
                {
                    addWalkableRoom(bridgeVector.x, bridgeVector.y + 1, RoomType.Normal);
                    addWalkableRoom(bridgeVector.x, bridgeVector.y - 1, RoomType.Normal);
                }
            }
        }
    }

    /// <summary>
    /// Add 2*2 goal room to the map
    /// </summary>
    /// <param name="parent">Use this to reparent the portal spawned in the goal room to the "Placeables"</param>
    public void addGoal(GameObject parent)
    {
        UnityEngine.Debug.Log("Adding goal room.");
        int endY = UnityEngine.Random.Range(2, yRooms - 1);
        int endX = xRooms - 2;
        int i = 1;
        while (mapArray[endX, endY] != null || mapArray[endX - 1, endY] != null || mapArray[endX, endY - 1] != null || mapArray[endX - 1, endY - 1] != null)
        {
            endY = UnityEngine.Random.Range(2, yRooms - 1);
            i++;
            if (i % yRooms == 0)
            {
                endX--;
            }
        }
        UnityEngine.Debug.Log("Iterations to generate goal room: " + i);
        goalRoom = new Room(endX, endY, RoomType.Goal);
        mapArray[endX, endY] = goalRoom;
        mapArray[endX - 1, endY] = new Room(endX - 1, endY, RoomType.Goal);
        mapArray[endX, endY - 1] = new Room(endX, endY - 1, RoomType.Goal);
        mapArray[endX - 1, endY - 1] = new Room(endX - 1, endY - 1, RoomType.Goal);
        // spawn portal
        GameObject portal = Resources.Load("Prefabs/Placeable/Portal") as GameObject;
        var portalInstantiated = GameObject.Instantiate(portal, goalRoom.getLocation() + new Vector3(1, 0.5f, 1), portal.transform.rotation) as GameObject;
        portalInstantiated.transform.parent = parent.transform.Find("Placeables");
    }

    /// <summary>
    /// Add 2*2 and 2*3 streets
    /// </summary>
    /// <param name="minLargeStreets">minumum amount of 2*2 streets</param>
    /// <param name="maxLargeStreets">maximum amount of 2*2 streets</param>
    /// <param name="minExtraLargeStreets">minumum amount of 2*3 streets</param>
    /// <param name="maxExtraLargeStreets">maximum amount of 2*3 streets</param>
    public void addLargeStreets(int minLargeStreets, int maxLargeStreets, int minExtraLargeStreets, int maxExtraLargeStreets)
    {
        UnityEngine.Debug.Log("Adding large streets.");
        // spawning 2*2 streets
        for (int i = 0; i < UnityEngine.Random.Range(minLargeStreets, maxLargeStreets + 1); i++)
        {
            int y;
            int x;
            int count = 0;
            do
            {
                y = UnityEngine.Random.Range(2, yRooms - 1);
                x = UnityEngine.Random.Range(2, xRooms - 1);
                count++;
            } while (count < 50 && (mapArray[x, y] != null || mapArray[x - 1, y] != null || mapArray[x, y - 1] != null || mapArray[x - 1, y - 1] != null));
            addWalkableRoom(x, y, RoomType.Large);
            addWalkableRoom(x - 1, y, RoomType.Large);
            addWalkableRoom(x, y - 1, RoomType.Large);
            addWalkableRoom(x - 1, y - 1, RoomType.Large);
        }
        // spawning 2*3 streets
        for (int i = 0; i < UnityEngine.Random.Range(minExtraLargeStreets, maxExtraLargeStreets + 1); i++)
        {
            int y;
            int x;
            int count = 0;
            do
            {
                y = UnityEngine.Random.Range(2, yRooms - 1);
                x = UnityEngine.Random.Range(3, xRooms - 1);
                count++;
            } while (count < 50 && (mapArray[x, y] != null || mapArray[x - 1, y] != null || mapArray[x - 2, y] != null || mapArray[x, y - 1] != null || mapArray[x - 1, y - 1] != null || mapArray[x - 2, y - 1] != null));
            addWalkableRoom(x, y, RoomType.Large);
            addWalkableRoom(x - 1, y, RoomType.Large);
            addWalkableRoom(x - 2, y, RoomType.Large);
            addWalkableRoom(x, y - 1, RoomType.Large);
            addWalkableRoom(x - 1, y - 1, RoomType.Large);
            addWalkableRoom(x - 2, y - 1, RoomType.Large);
        }
    }

    /// <summary>
    /// Add placeables to the streets
    /// </summary>
    /// <param name="parent">Use this to reparent the placeables to the "Placeables"</param>
    /// <param name="placeables">Array of Placeable, containg their names and spawn chance</param>
    public void addPlaceables(GameObject parent, Placeable[] placeables)
    {
        List<Vector2Int> streetsWithPlaceables = new List<Vector2Int>();

        foreach (Placeable p in placeables)
        {
            GameObject g = Resources.Load("Prefabs/Placeable/" + p.name) as GameObject;

            List<Vector2Int> spawnLocations = new List<Vector2Int>();
            foreach (Vector2Int location in walkableLocations.Except(bridges.Concat(new[] { startRoom.getVector2IntLocation() }).Concat(streetsWithPlaceables)))
            {
                if (spawnLocations.Count < (walkableLocations.Count * p.chance / 100) && UnityEngine.Random.Range(0, 100) < p.chance && (p.name == "tree" || spawnLocations.TrueForAll(l => Vector2Int.Distance(l, location) > 2)))
                {
                    GameObject s = GameObject.Instantiate(g, new Vector3(location.x * 10, 0, location.y * 10), g.transform.rotation) as GameObject;
                    s.transform.parent = parent.transform.Find("Placeables");
                    spawnLocations.Add(location);
                    streetsWithPlaceables.Add(location);
                }
            }
        }
    }

    /// <summary>
    /// Fill all empty parts of the map with placeholders
    /// </summary>
    public void addPlaceholders()
    {
        UnityEngine.Debug.Log("Adding placeholders.");
        for (int x = 1; x < mapArray.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < mapArray.GetLength(1) - 1; y++)
            {
                if (mapArray[x, y] == null)
                {
                    mapArray[x, y] = new Room(x, y, RoomType.Placeholder);
                }
            }
        }
    }

    /// <summary>
    /// Add snake like river to the map
    /// </summary>
    public void addRiver()
    {
        UnityEngine.Debug.Log("Adding river.");
        int riverX = 0;
        int riverY = 0;
        do
        {
            riverY = UnityEngine.Random.Range(2, yRooms - 2);
        } while (startRoom.getY() == riverY);

        mapArray[riverX, riverY] = new Room(riverX, riverY, RoomType.River);
        river.Add(new Vector2Int(riverX, riverY));
        Vector2Int direction = Vector2Int.right;
        int roomsInDirection = 0;
        int a = 0;
        while (riverX < xRooms - 1)
        {
            if (roomsInDirection < UnityEngine.Random.Range(3, 11) && (river[river.Count - 1] + direction).y < yRooms - 1 && (river[river.Count - 1] + direction).y > 1)
            {
                riverX += direction.x;
                riverY += direction.y;

                mapArray[riverX, riverY] = new Room(riverX, riverY, RoomType.River);
                river.Add(new Vector2Int(riverX, riverY));
                roomsInDirection++;
            }
            else if (direction == Vector2Int.right && (river[river.Count - 1] + direction).x < xRooms - 2)
            {
                if (riverY < yRooms / 2)
                {
                    direction = Vector2Int.up;
                    roomsInDirection = 0;
                }
                else
                {
                    direction = Vector2Int.down;
                    roomsInDirection = 0;
                }
            }
            else
            {
                direction = Vector2Int.right;
                roomsInDirection = 0;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent">Use this to reparent the spawners to the "Spawner"</param>
    /// <param name="percentage"></param>
    public void addSpawner(GameObject parent, int percentage)
    {
        GameObject spawner = Resources.Load("Prefabs/Enemies/Spawner") as GameObject;

        List<GameObject> spawnLocations = new List<GameObject>();
        foreach (Vector2Int location in walkableLocations)
        {
            if (spawnLocations.Count < (walkableLocations.Count * percentage / 100) && UnityEngine.Random.Range(0, 100) < percentage && location != startRoom.getVector2IntLocation())
            {
                GameObject s = GameObject.Instantiate(spawner, new Vector3(location.x * 10, 0, location.y * 10), spawner.transform.rotation) as GameObject;
                s.transform.parent = parent.transform.Find("Spawner");
                spawnLocations.Add(s);
            }
        }
        // add Spawner to goal room
        GameObject goalSpawner = GameObject.Instantiate(spawner, goalRoom.getLocation(), spawner.transform.rotation) as GameObject;
        goalSpawner.transform.parent = parent.transform.Find("Spawner");
    }

    /// <summary>
    /// Add start room to the map
    /// </summary>
    public void addStart()
    {
        UnityEngine.Debug.Log("Adding start room.");
        startRoom = addWalkableRoom(1, UnityEngine.Random.Range(1, yRooms - 1), RoomType.Start);
    }

    /// <summary>
    /// Add streets from start room to goal room to the map.
    /// If more than 10000 iterations are needed, there is a problem and no path from start to goal could be found
    /// </summary>
    public void addStreets()
    {
        UnityEngine.Debug.Log("Adding streets.");
        int iterations = 1;
        while (!hasPathFromStartToFinish() && iterations < 5000)
        {
            iterations++;
            if (iterations % 1000 == 0)
            {
                UnityEngine.Debug.Log("Current iteration: " + iterations);
            }
            // get list of rooms with less than 4 neighbors
            List<Vector2Int> extendableLocations = walkableLocations.Where(room =>
            {
                bool noRightNeighbor = mapArray[room.x + 1, room.y] == null;
                bool noLeftNeighbor = mapArray[room.x - 1, room.y] == null;
                bool noTopNeighbor = mapArray[room.x, room.y + 1] == null;
                bool noBottomNeighbor = mapArray[room.x, room.y - 1] == null;
                return noRightNeighbor || noLeftNeighbor || noTopNeighbor || noBottomNeighbor;
            }).ToList();
            // choose one location from the extandables
            Vector2Int selectedLocation = extendableLocations[UnityEngine.Random.Range(0, extendableLocations.Count)];


            List<Vector2Int> options = new List<Vector2Int>();
            List<Vector2Int> directions = new List<Vector2Int>() { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
            // add all neighbors that don't break constraints to the options list
            foreach (Vector2Int direction in directions)
            {
                if (!isBreakingConstraints(selectedLocation + direction))
                {
                    options.Add(selectedLocation + direction);
                }
            }

            if (options.Count != 0)
            {
                Vector2Int selection = options[UnityEngine.Random.Range(0, options.Count)];
                addWalkableRoom(selection.x, selection.y, RoomType.Normal);
            }
        }
        UnityEngine.Debug.Log("Iterations to generate path from start to finish: " + iterations);
    }

    /// <summary>
    /// Add a new room at the location, also add it to the map array and list of walkable rooms
    /// </summary>
    /// <param name="x">x location</param>
    /// <param name="y">y location</param>
    /// <param name="roomType">type of room to add</param>
    /// <returns></returns>
    public Room addWalkableRoom(int x, int y, RoomType roomType)
    {
        Room room = new Room(x, y, roomType);
        mapArray[x, y] = room;
        walkableLocations.Add(new Vector2Int(x, y));
        return room;
    }

    /// <summary>
    /// Instantiate the maps rooms in the game world
    /// </summary>
    /// <param name="parent">Parent game object</param>
    public void show(GameObject parent)
    {
        foreach (Room room in mapArray)
        {
            if (room != null)
            {
                room.ShowRoom(parent);
            }

        }
    }

    /// <summary>
    /// Check if the given location is part of the goal room
    /// </summary>
    /// <param name="location">location to check</param>
    /// <returns>true if location is part of the goal room, otherwise false</returns>
    public bool isGoalRoom(Vector2Int location)
    {
        int endY = goalRoom.getY();
        int endX = goalRoom.getX();
        bool isGoalTopRight = location == new Vector2Int(endX, endY);
        bool isGoalBottomRight = location == new Vector2Int(endX, endY - 1);
        bool isGoalTopLeft = location == new Vector2Int(endX - 1, endY);
        bool isGoalBottomLeft = location == new Vector2Int(endX - 1, endY - 1);


        return isGoalTopRight || isGoalBottomRight || isGoalTopLeft || isGoalBottomLeft;
    }

    /// <summary>
    /// Do a full search of all created rooms to see if there is a path from start to goal
    /// </summary>
    /// <returns>true if there is a path from start to finish, otherwise false</returns>
    public bool hasPathFromStartToFinish()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        // list to be filled with locations to test
        List<Vector2Int> openSet = new List<Vector2Int>();
        // hashset to be filled with tested locations that were not the goal
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        openSet.Add(new Vector2Int(startRoom.getX(), startRoom.getY()));

        while (openSet.Count > 0)
        {
            Vector2Int currentLocation = openSet.OrderBy(location => Vector2Int.Distance(location, goalRoom.getVector2IntLocation())).First();

            if (isGoalRoom(currentLocation))
            {
                stopWatch.Stop();
                UnityEngine.Debug.Log("Found path to goal in " + stopWatch.ElapsedMilliseconds + " ms!");
                UnityEngine.Debug.Log("Found path to goal in " + stopWatch.ElapsedTicks + " Ticks!");
                return true;
            }
            else
            {
                // add all not-tested neighbors to the openset
                openSet.AddRange(getWalkableNeighbors(currentLocation).Where(l => !closedSet.Contains(l)));
            }

            openSet.Remove(currentLocation);
            closedSet.Add(currentLocation);
        }
        return false;
    }

    /// <summary>
    /// Return the list of walkable neighbors for the given location
    /// </summary>
    /// <param name="location"></param>
    /// <returns>list of locations next to the input location where the RoomType is Normal, Bridge or Goal</returns>
    private List<Vector2Int> getWalkableNeighbors(Vector2Int location)
    {
        List<Vector2Int> neighborLocations = new List<Vector2Int>() { location + Vector2Int.up, location + Vector2Int.down, location + Vector2Int.left, location + Vector2Int.right };
        return neighborLocations.Where(l =>
        {
            bool isXInBounds = l.x < xRooms;
            bool isYInBounds = l.y < yRooms;
            bool isNotNull = mapArray[l.x, l.y] != null;
            bool isNormal = isNotNull && mapArray[l.x, l.y].getRoomType() == RoomType.Normal;
            bool isBridge = isNotNull && mapArray[l.x, l.y].getRoomType() == RoomType.Bridge;
            bool isGoal = isNotNull && mapArray[l.x, l.y].getRoomType() == RoomType.Goal;
            return isXInBounds && isYInBounds && isNotNull && (isNormal || isBridge || isGoal);
        }).ToList();
    }

    /// <summary>
    /// Check if a new room at the given location would break the constraint of not having neighbors other than top, right, bottom or left
    /// </summary>
    /// <param name="location">location to check</param>
    /// <returns>true if the location would break constraints</returns>
    private bool isBreakingConstraints(Vector2Int location)
    {
        if (mapArray[location.x, location.y] != null)
        {
            return true;
        }

        Room top = mapArray[location.x, location.y + 1];
        Room topRight = mapArray[location.x + 1, location.y + 1];
        Room topLeft = mapArray[location.x - 1, location.y + 1];
        Room right = mapArray[location.x + 1, location.y];
        Room left = mapArray[location.x - 1, location.y];
        Room bottom = mapArray[location.x, location.y - 1];
        Room bottomRight = mapArray[location.x + 1, location.y - 1];
        Room bottomLeft = mapArray[location.x - 1, location.y - 1];

        bool hasTopNeighbor = top != null && top.getRoomType() != RoomType.River && top.getRoomType() != RoomType.Placeholder;
        bool hasTopRightNeighbor = topRight != null && topRight.getRoomType() != RoomType.River && topRight.getRoomType() != RoomType.Placeholder;
        bool hasTopLeftNeighbor = topLeft != null && topLeft.getRoomType() != RoomType.River && topLeft.getRoomType() != RoomType.Placeholder;
        bool hasRightNeighbor = right != null && right.getRoomType() != RoomType.River && right.getRoomType() != RoomType.Placeholder;
        bool hasLeftNeighbor = left != null && left.getRoomType() != RoomType.River && left.getRoomType() != RoomType.Placeholder;
        bool hasBottomNeighbor = bottom != null && bottom.getRoomType() != RoomType.River && bottom.getRoomType() != RoomType.Placeholder;
        bool hasBottomRightNeighbor = bottomRight != null && bottomRight.getRoomType() != RoomType.River && bottomRight.getRoomType() != RoomType.Placeholder;
        bool hasBottomLeftNeighbor = bottomLeft != null && bottomLeft.getRoomType() != RoomType.River && bottomLeft.getRoomType() != RoomType.Placeholder;

        return (hasLeftNeighbor && hasTopLeftNeighbor && hasTopNeighbor) || (hasRightNeighbor && hasTopRightNeighbor && hasTopNeighbor) || (hasLeftNeighbor && hasBottomLeftNeighbor && hasBottomNeighbor) || (hasRightNeighbor && hasBottomRightNeighbor && hasBottomNeighbor);
    }

    /// <summary>
    /// Remove lights that wont be visible to the player to improve game performance
    /// </summary>
    public void improveLights()
    {
        int count = 0;
        List<Room> placeholders = mapArray.Cast<Room>().Where(room => room.getRoomType() == RoomType.Placeholder).ToList();

        // destroy all lights for rooms with 4 neighbor placeholders
        foreach (Room room in placeholders)
        {
            Vector2Int location = room.getVector2IntLocation();
            int neighborPlaceholders = 0;

            // get number of neighbor placeholders
            if (location.y + 1 == yRooms || mapArray[location.x, location.y + 1].getRoomType() == RoomType.Placeholder)
                neighborPlaceholders++;
            if (location.y - 1 == -1 || mapArray[location.x, location.y - 1].getRoomType() == RoomType.Placeholder)
                neighborPlaceholders++;
            if (location.x + 1 == xRooms || mapArray[location.x + 1, location.y].getRoomType() == RoomType.Placeholder)
                neighborPlaceholders++;
            if (location.x - 1 == -1 || mapArray[location.x - 1, location.y].getRoomType() == RoomType.Placeholder)
                neighborPlaceholders++;

            if (neighborPlaceholders == 4)
            {
                foreach (Light light in room.getRoomObject().GetComponentsInChildren<Light>())
                {
                    GameObject.Destroy(light);
                    count++;
                }
            }
        }

        Component[] lightComponents = GameObject.FindObjectsOfType<Light>();

        foreach (Light light in lightComponents)
        {
            // destroy all lights outside map borders
            if (light.transform.position.x < 0.0f || light.transform.position.z < 0.0f || light.transform.position.x > xRooms * 10 || light.transform.position.z > yRooms * 10)
            {
                GameObject.Destroy(light);
                count++;
            }
            // destroy all lights overlapping other houses
            if (placeholders.Where(roomObject => roomObject.getRoomObject().transform != light.transform.parent).Where(roomObject => roomObject.getRoomObject().GetComponent<BoxCollider>().bounds.Contains(light.transform.position)).Count() > 0)
            {
                GameObject.Destroy(light);
                count++;
            }
        }

        UnityEngine.Debug.Log("Destroyed " + count + " lights.");
    }

    /// <summary>
    /// This replaces all streets on the shortest path from start to goal with mossy streets to help players find the right way.
    /// </summary>
    public void markPathFromStartToFinish()
    {
        // list to be filled with locations to test and the previous room (Tuple Item1: Location to test, Item2: previous room (needed to backtrace the path from found goal to start again))
        List<Tuple<Vector2Int, Vector2Int>> openSet = new List<Tuple<Vector2Int, Vector2Int>>();
        List<Tuple<Vector2Int, Vector2Int>> closedSet = new List<Tuple<Vector2Int, Vector2Int>>();

        openSet.Add(Tuple.Create(new Vector2Int(startRoom.getX(), startRoom.getY()), new Vector2Int(-1, -1)));
        // Vector2Int(-1,-1) used instead of null since Vector2Int not nullable
        Vector2Int markLocation = new Vector2Int(-1, -1);

        // Find path from start to goal
        while (openSet.Count > 0)
        {
            Tuple<Vector2Int, Vector2Int> currentLocation = openSet.OrderBy(location => Vector2Int.Distance(location.Item1, goalRoom.getVector2IntLocation())).First();

            if (isGoalRoom(currentLocation.Item1))
            {
                markLocation = currentLocation.Item2;
                break;
            }
            else
            {
                // add all not-tested neighbors to the openset
                openSet.AddRange(getWalkableNeighbors(currentLocation.Item1).Where(l => !closedSet.Select(x => x.Item1).Contains(l)).Select(x => Tuple.Create(x, currentLocation.Item1)));
            }

            openSet.Remove(currentLocation);
            closedSet.Add(currentLocation);
        }

        // Mark path from goal to start
        while (markLocation != new Vector2Int(-1, -1))
        {
            if (mapArray[markLocation.x, markLocation.y].getRoomType() == RoomType.Normal)
            {
                mapArray[markLocation.x, markLocation.y].setRoomType(RoomType.Mossy);
            }
            // set this locations previous location as next location to be marked
            markLocation = closedSet.Find(x => x.Item1 == markLocation).Item2;
        }

    }
}
