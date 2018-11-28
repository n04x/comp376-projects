using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

  public Sprite FloorSprite;
  public Sprite WallSprite;
  public Sprite CornerSprite;
  public GameObject BossLayout;
  public List<GameObject> roomPrefabs;

  public float RoomWidth;
  public float RoomHeight;

  public int FloorWidth;
  public int FloorHeight;
  public int MaxRoomCount;
  public int MaxAttemptCount;

  private RoomMap m_map;
  private RoomFactory m_roomFactory;
  private GameObject m_player;
  private int m_currentRoomX;
  private int m_currentRoomY;
  private Room m_currentRoom;

  // Start is called before the first frame update
  void Start()
  {
    // Debug.Log("Start");
    m_roomFactory = new RoomFactory(RoomWidth, RoomHeight);
    m_map = new RoomMap(FloorWidth, FloorHeight);
    m_roomFactory.CornerSprite = CornerSprite;
    m_roomFactory.WallSprite = WallSprite;
    m_roomFactory.FloorSprite = FloorSprite;

    CreateFloor();
    // Debug.Log("Finish");
  }

  void Update()
  {
    float currentRoomRelativeX = m_player.transform.position.x - m_currentRoom.transform.position.x;
    float currentRoomRelativeY = m_currentRoom.transform.position.y - m_player.transform.position.y;
    if (currentRoomRelativeX > RoomWidth + 2)
    {
      m_currentRoom.Exit();
      m_currentRoom = m_map.GetRoom(++m_currentRoomX, m_currentRoomY);
      m_currentRoom.Enter();
    }
    if (currentRoomRelativeX < -2)
    {
      m_currentRoom.Exit();
      m_currentRoom = m_map.GetRoom(--m_currentRoomX, m_currentRoomY);
      m_currentRoom.Enter();
    }
    if (currentRoomRelativeY > RoomHeight + 2)
    {
      m_currentRoom.Exit();
      m_currentRoom = m_map.GetRoom(m_currentRoomX, --m_currentRoomY);
      m_currentRoom.Enter();
    }
    if (currentRoomRelativeY < -2)
    {
      m_currentRoom.Exit();
      m_currentRoom = m_map.GetRoom(m_currentRoomX, ++m_currentRoomY);
      m_currentRoom.Enter();
    }
    Debug.Log("Current X: " + currentRoomRelativeX + " CurrentY: " + currentRoomRelativeY);
  }

  void CreateFloor()
  {
    CreateLayout();
    ConfigureRooms();
    // UpdateDoors();
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

      switch (dir)
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
      else
      {
        switch (dir)
        {
          case Direction.NORTH:
            m_map.GetRoom(currentX, currentY).exits.South = true;
            m_map.GetRoom(lastX, lastY).exits.North = true;
            break;
          case Direction.SOUTH:
            m_map.GetRoom(currentX, currentY).exits.North = true;
            m_map.GetRoom(lastX, lastY).exits.South = true;
            break;
          case Direction.EAST:
            m_map.GetRoom(currentX, currentY).exits.West = true;
            m_map.GetRoom(lastX, lastY).exits.East = true;
            break;
          case Direction.WEST:
            m_map.GetRoom(currentX, currentY).exits.East = true;
            m_map.GetRoom(lastX, lastY).exits.West = true;
            break;
        }
      }


    }
  }

  void UpdateDoors()
  {
    m_map.ForEach((Room r, int x, int y) =>
    {
      // do door logic for each room
    });
  }

  void ConfigureRooms()
  {
    Room bestStart = null;
    Room bestBoss = null;
    int bestX = -1;
    int bestY = -1;
    ScreenEffects camScript = GameObject.Find("Main Camera").GetComponent<ScreenEffects>();
    m_player = GameObject.Find("Aris");

    camScript.target = m_player;
    m_map.ForEach((Room r, int x, int y) =>
    {
      int index = Random.Range(0, roomPrefabs.Count);
      r.UpdateWalls();
      r.UpdateDoors();
      r.UpdateWallTexturesForDoors();
      r.layout = Instantiate(roomPrefabs[index], r.transform.position, Quaternion.identity);
      
      foreach (EnemyRandomWalk enemy in r.layout.GetComponentsInChildren<EnemyRandomWalk>())
      {
        enemy.minCoordX = r.transform.position.x + 1;
        enemy.minCoordY = r.transform.position.y - 1;
        enemy.maxCoordX = r.transform.position.x + RoomWidth - 1;
        enemy.maxCoordY = r.transform.position.y - RoomHeight + 1;
        enemy.areaTarget = new GameObject("RandomTarget").transform;
        enemy.init();
      }
      if (bestStart == null || r.getExitCount() > bestStart.getExitCount())
      {
        bestX = x;
        bestY = y;
        bestStart = r;
      }
      if ((bestStart != null && bestBoss == null) || r.getExitCount() < bestBoss.getExitCount())
      {
        bestBoss = r;
      }
    });

    // TODO? maybe create a start room prefab layout
    Destroy(bestStart.layout);
    Destroy(bestBoss.layout);
    bestStart.layout = null;
    bestBoss.layout = Instantiate(BossLayout, bestBoss.transform.position, Quaternion.identity);
    m_player.transform.position = new Vector3(bestStart.transform.position.x + RoomWidth / 2, bestStart.transform.position.y - RoomHeight / 2, bestStart.transform.position.z);
    m_currentRoom = bestStart;
    m_currentRoomX = bestX;
    m_currentRoomY = bestY;
  }
}
