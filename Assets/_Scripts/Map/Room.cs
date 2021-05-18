///
/// Author: Christopher Beck
/// Description: This class defines variables and functions for PCG rooms ingame.
///              You can create a room, as well as access and change its type and rotation .
/// ==============================================
/// Changelog:
/// 06/18/2020 - Christopher Beck - Minor changes to show art assets correctly
/// 06/13/2020 - Christopher Beck - Cleaned up to better fit PCG rework.
/// 05/17/2020 - Christopher Beck - Created class as part of refactoring the PCG code.
/// ==============================================
///
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public enum RoomType
{
    Placeholder,
    River,
    Bridge,
    Large,
    Normal,
    Mossy,
    Start,
    Goal
}

/// <summary>
/// A room is a GameObject at a specific location.
/// See the RoomType enum for the different room types
/// You can instanciate the room in the game by calling the showRoom function.
/// </summary>
public class Room
{
    private int x;
    private int y;
    private Vector2Int vector2IntLocation;
    private Vector3 location;
    private int rotate;
    private GameObject roomObject;
    private RoomType roomType;

    public int getX() { return x; }
    public int getY() { return y; }
    public Vector2Int getVector2IntLocation() { return vector2IntLocation; }
    public Vector3 getLocation() { return location; }
    public int getRotate() { return rotate; }
    public GameObject getRoomObject() { return roomObject; }
    public RoomType getRoomType() { return roomType; }

    /// <summary>
    /// Constructor for creating a room, does not create a GameObject
    /// </summary>
    /// <param name="x">X Grid location</param>
    /// <param name="y">Y Grid location</param>
    /// <param name="rotate">Rotate room by this degree (on the y-Axis) (use between 0 and 360)</param>
    public Room(int x, int y, RoomType roomType, int rotate = 0)
    {
        this.x = x;
        this.y = y;
        this.vector2IntLocation = new Vector2Int(x, y);
        this.location = new Vector3(x * 10, 0, y * 10);
        this.rotate = rotate;
        this.roomType = roomType;
    }

    /// <summary>
    /// Change the room type of this room
    /// </summary>
    /// <param name="roomType">new room type</param>
    public void setRoomType(RoomType roomType)
    {
        this.roomType = roomType;
    }

    /// <summary>
    /// Change the y-Axis rotation of this room
    /// </summary>
    /// <param name="rotate">rotate on y-Axis by this degree (0 to 360)</param>
    public void setRotate(int rotate)
    {
        this.rotate = rotate;
    }

    /// <summary>
    /// Instantiates the room in the game.
    /// </summary>
    /// <param name="parent">GameObject to set as parent to keep structure clean</param>
    public void ShowRoom(GameObject parent)
    {
        string prefabname = "street";

        switch (roomType)
        {
            case RoomType.Bridge:
                prefabname = "bridge";
                break;
            case RoomType.Goal:
                prefabname = "goal";
                break;
            case RoomType.Start:
                prefabname = "start";
                break;
            case RoomType.Large:
                prefabname = "largestreet";
                break;
            case RoomType.Placeholder:
                if (Random.Range(0,2) == 0)
                {
                    prefabname = "Block_B";
                } else
                {
                    prefabname = "Block_A";
                }
                break;
            case RoomType.River:
                prefabname = "river";
                break;
            case RoomType.Mossy:
                prefabname = "street_mossy";
                break;
            default:
                prefabname = "street";
                break;
        }

        GameObject prefab = Resources.Load("Prefabs/Rooms/" + prefabname) as GameObject;

        // select prefab by name and instantiate the game object
        roomObject = GameObject.Instantiate(prefab, location, prefab.transform.rotation) as GameObject;

        // reparent the roomObject and rescale it
        if (roomType != RoomType.Placeholder && roomType != RoomType.River)
        {
            roomObject.transform.parent = parent.transform.Find("Walkable");
        }
        else
        {
            roomObject.transform.parent = parent.transform.Find("Placeholders");
        }

        if (rotate != 0)
        {
            roomObject.transform.Rotate(new Vector3(0, rotate, 0));
        }
    }
}
