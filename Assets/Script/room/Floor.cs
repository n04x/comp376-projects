using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    
    public Sprite FloorSprite;
    public Sprite WallSprite;
    public Sprite CornerSprite;

    public float RoomWidth;
    public float RoomHeight;
    
    public int FloorWidth;
    public int FloorHeight;
    public int MaxRoomCount;
    public int MaxAttemptCount;
    
    private RoomMap m_map;
    private RoomFactory m_roomFactory;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        m_roomFactory = new RoomFactory(RoomWidth, RoomHeight);
        m_map = new RoomMap(FloorWidth, FloorHeight);
        m_roomFactory.CornerSprite = CornerSprite;
        m_roomFactory.WallSprite = WallSprite;
        m_roomFactory.FloorSprite = FloorSprite;

        CreateFloor();
        Debug.Log("Finish");
    }

    void CreateFloor()
    {
        CreateLayout();
        // UpdateDoors();
        // ConfigureRooms();        
    }

    void CreateLayout()
    {
        int roomCtr = 1;
        int attempCtr = 0;
        int currentX = FloorWidth / 2;
        int currentY = FloorHeight / 2;

        m_map.AddRoom(currentX, currentY, m_roomFactory.CreateRoom());
        
        while (roomCtr < MaxRoomCount && attempCtr++ < MaxAttemptCount)
        {
            int lastX = currentX;
            int lastY = currentY;
            Direction dir = (Direction)Mathf.Round(Random.value * 4);

            switch(dir)
            {
                case Direction.NORTH:
                    ++currentY;
                break;
                case Direction.SOUTH:
                    --currentY;
                break;
                case Direction.EAST:
                    ++currentX;
                break;
                case Direction.WEST:
                    --currentX;
                break;
            }
            if (currentX < 0 || currentY < 0 || currentY >= FloorWidth || currentX >= FloorHeight) 
            {
                currentX = lastX;
                currentY = lastY;
                continue;
            }

            if (m_map.GetRoom(currentX, currentY) == null)
            {
                m_map.AddRoom(currentX, currentY, m_roomFactory.CreateRoom(m_map.GetRoom(lastX, lastY), dir));
                ++roomCtr;
            }
            
        }
    }

    void UpdateDoors()
    {
        m_map.ForEach((Room r, int x, int y) => {
            // do door logic for each room
        });
    }

    void ConfigureRooms()
    {
        m_map.ForEach((Room r, int x, int y) => {
            // figure out rooms layout to use
        });
    }
}
