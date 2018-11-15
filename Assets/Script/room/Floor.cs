using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

  public Sprite FloorSprite;
  public Sprite WallSprite;
  public Sprite CornerSprite;
  public List<GameObject> roomPrefabs;

  public float RoomWidth;
  public float RoomHeight;

  public int FloorWidth;
  public int FloorHeight;
  public int MaxRoomCount;
  public int MaxAttemptCount;

  private RoomMap m_map;
  private RoomFactory m_roomFactory;
  private GameObject player;

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
    FollowAlice camScript = GameObject.Find("Main Camera").GetComponent<FollowAlice>();
    player = GameObject.Find("Aris");

    camScript.target = player;
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

      bestStart = (bestStart == null || r.getExitCount() > bestStart.getExitCount()) ? r : bestStart;
    });

    // TODO? maybe create a start room prefab layout
    Destroy(bestStart.layout);
    bestStart.layout = null;
    player.transform.position = new Vector3(bestStart.transform.position.x + RoomWidth / 2, bestStart.transform.position.y - RoomHeight / 2, bestStart.transform.position.z);
  }
}
