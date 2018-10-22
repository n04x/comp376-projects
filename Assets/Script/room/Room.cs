using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Exits {
    public bool North, East, South, West;
}

public class Room : MonoBehaviour
{
  public Vector2 dimensions;
  public Exits exits;

  #region walls
  public BoxCollider2D topLeft;
  public BoxCollider2D topRight;
  public BoxCollider2D bottomLeft;
  public BoxCollider2D bottomRight;
  public BoxCollider2D leftLeft;
  public BoxCollider2D leftRight;
  public BoxCollider2D rightLeft;
  public BoxCollider2D rightRight;
  #endregion

  private bool initialised = false;

  public void CreateWalls()
  {
    if (!initialised && gameObject.GetComponent<BoxCollider2D>() != null)
    {
      foreach (BoxCollider2D collider in gameObject.GetComponents<BoxCollider2D>())
      {
        DestroyImmediate(collider);
      }


      topLeft = gameObject.AddComponent<BoxCollider2D>();
      topRight = gameObject.AddComponent<BoxCollider2D>();
      bottomLeft = gameObject.AddComponent<BoxCollider2D>();
      bottomRight = gameObject.AddComponent<BoxCollider2D>();
      leftLeft = gameObject.AddComponent<BoxCollider2D>();
      leftRight = gameObject.AddComponent<BoxCollider2D>();
      rightLeft = gameObject.AddComponent<BoxCollider2D>();
      rightRight = gameObject.AddComponent<BoxCollider2D>();

      topLeft.usedByComposite = true;
      topRight.usedByComposite = true;
      bottomLeft.usedByComposite = true;
      bottomRight.usedByComposite = true;
      leftLeft.usedByComposite = true;
      leftRight.usedByComposite = true;
      rightLeft.usedByComposite = true;
      rightRight.usedByComposite = true;

      UpdateWalls();
      initialised = true;
    }

  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdateWalls()
  {
    float halfWidth = dimensions.x / 2;
    float halfHeight = dimensions.y / 2;

    #region wall sizes
    topLeft.size = new Vector2(halfWidth, 1);
    topRight.size = new Vector2(halfWidth, 1);
    bottomLeft.size = new Vector2(halfWidth, 1);
    bottomRight.size = new Vector2(halfWidth, 1);

    leftLeft.size = new Vector2(1, halfHeight);
    leftRight.size = new Vector2(1, halfHeight);
    rightLeft.size = new Vector2(1, halfHeight);
    rightRight.size = new Vector2(1, halfHeight);
    #endregion

    #region wall positions
    topLeft.offset = new Vector2(halfWidth / 2, 0.5f);
    topRight.offset = new Vector2(halfWidth + halfWidth / 2, 0.5f);
    bottomLeft.offset = new Vector2(halfWidth / 2, -dimensions.y - 0.5f);
    bottomRight.offset = new Vector2(halfWidth + halfWidth / 2, -dimensions.y - 0.5f);

    leftLeft.offset = new Vector2(-0.5f, -(halfHeight / 2));
    leftRight.offset = new Vector2(-0.5f, -(halfHeight + halfHeight / 2));
    rightLeft.offset = new Vector2(dimensions.x + 0.5f, -(halfHeight / 2));
    rightRight.offset = new Vector2(dimensions.x + 0.5f, -(halfHeight + halfHeight / 2));
    #endregion


  }

  public void UpdateDoors()
  {
    if (exits.North)
    {
      topLeft.size = new Vector2(topLeft.size.x - 0.5f, topLeft.size.y);
      topLeft.offset = new Vector2(topLeft.offset.x - 0.25f, topLeft.offset.y);
      topRight.size = new Vector2(topRight.size.x - 0.5f, topRight.size.y);
      topRight.offset = new Vector2(topRight.offset.x + 0.25f, topRight.offset.y);
    }
    if (exits.South)
    {
      bottomLeft.size = new Vector2(bottomLeft.size.x - 0.5f, bottomLeft.size.y);
      bottomLeft.offset = new Vector2(bottomLeft.offset.x - 0.25f, bottomLeft.offset.y);
      bottomRight.size = new Vector2(bottomRight.size.x - 0.5f, bottomRight.size.y);
      bottomRight.offset = new Vector2(bottomRight.offset.x + 0.25f, bottomRight.offset.y);
    }

    if (exits.West)
    {
        leftLeft.size = new Vector2(leftLeft.size.x, leftLeft.size.y - 0.5f);
        leftLeft.offset = new Vector2(leftLeft.offset.x, leftLeft.offset.y + 0.25f);
        leftRight.size = new Vector2(leftRight.size.x, leftRight.size.y - 0.5f);
        leftRight.offset = new Vector2(leftRight.offset.x, leftRight.offset.y - 0.25f);
    }
    if (exits.East) 
    {
        rightLeft.size = new Vector2(rightLeft.size.x, rightLeft.size.y - 0.5f);
        rightLeft.offset = new Vector2(rightLeft.offset.x, rightLeft.offset.y + 0.25f);
        rightRight.size = new Vector2(rightRight.size.x, rightRight.size.y - 0.5f);
        rightRight.offset = new Vector2(rightRight.offset.x, rightRight.offset.y - 0.25f);
    }
  }
}
