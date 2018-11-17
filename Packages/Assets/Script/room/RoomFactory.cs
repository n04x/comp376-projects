using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH, SOUTH, EAST, WEST
}

public class RoomFactory
{
    public Sprite FloorSprite;
    public Sprite WallSprite;
    public Sprite CornerSprite;
    
    private float m_roomWidth;
    private float m_roomHeight;
    private int m_roomCtr;
    
    public RoomFactory(float roomWidth = 10, float roomHeight = 10)
    {
        m_roomCtr = 0;
        m_roomHeight = roomHeight;
        m_roomWidth = roomWidth;
    }

    public Room CreateRoom()
    {
        GameObject go = new GameObject("room_" + m_roomCtr);
        Room newRoom = go.AddComponent<Room>();
        newRoom.dimensions = new Vector2(m_roomWidth, m_roomHeight);
        newRoom.Scale = 1;
        newRoom.FloorTexture = FloorSprite;
        newRoom.WallTexture = WallSprite;
        newRoom.CornerTexture = CornerSprite;
        newRoom.exits = new Exits();
        ++m_roomCtr;

        // initialise the new room
        newRoom.Init();
        
        newRoom.CreateWallTextures();
        newRoom.UpdateWalls();
        newRoom.UpdateWallTextures();
        newRoom.UpdateFloor();

        return newRoom;
    }

    public Room CreateRoom(Room from, Direction direction)
    {
        Room newRoom = CreateRoom();
        float horizontalModifier = 0;
        float verticalModifier = 0;
        switch (direction)
        {
            case Direction.NORTH:
                verticalModifier = 1;
                from.exits.North = true;
                newRoom.exits.South = true;
            break;
            case Direction.SOUTH:
                verticalModifier = -1;
                from.exits.South = true;
                newRoom.exits.North = true;
            break;
            case Direction.EAST:
                horizontalModifier = 1;
                from.exits.East = true;
                newRoom.exits.West = true;
            break;
            case Direction.WEST:
                horizontalModifier = -1;
                from.exits.West = true;
                newRoom.exits.East = true;
            break;
        }
        newRoom.transform.position = new Vector3((horizontalModifier * (m_roomWidth + 2)) + from.transform.position.x, (verticalModifier * (m_roomHeight + 2)) + from.transform.position.y);
        
        return newRoom;
    }
}
