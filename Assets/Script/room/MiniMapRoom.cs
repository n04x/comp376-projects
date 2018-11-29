using UnityEngine;

public class MiniMapRoom
{
  public GameObject RoomGrapic;
  bool revealed;
  bool visited;
  
  public MiniMapRoom(GameObject roomGrapicPrefab, int x, int y, Vector3 mapPosition, GameObject parent)
  {
      this.RoomGrapic = Object.Instantiate(roomGrapicPrefab, new Vector3(mapPosition.x + x * 0.4f, mapPosition.y + y * 0.4f, mapPosition.z), Quaternion.identity);
      RoomGrapic.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
      RoomGrapic.transform.SetParent(parent.transform);
  }

  public void Reveal()
  {
    if (!visited)
    {
      revealed = true;
      RoomGrapic.GetComponentInChildren<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
    }
  } 

  public void PlayerEnter()
  {
    visited = true;
    RoomGrapic.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0.8f, 1, 1);
    //update room color

  }

  public void PlayerExit()
  {
    RoomGrapic.GetComponentInChildren<SpriteRenderer>().color = new Color(0.45f, 0.45f, 0.45f, 1);
  }
}