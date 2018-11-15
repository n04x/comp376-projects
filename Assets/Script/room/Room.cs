using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Exits
{
  public bool North, East, South, West;
}

public class Room : MonoBehaviour
{
  public Vector2 dimensions;
  public Exits exits;
  public float Scale;

  public Sprite FloorTexture;
  public Sprite WallTexture;
  public Sprite CornerTexture;

  public GameObject layout;

  #region sprites
  public SpriteRenderer floorSprite;
  // room corners
  public SpriteRenderer topLeftCorner;
  public SpriteRenderer topRightCorner;
  public SpriteRenderer bottomLeftCorner;
  public SpriteRenderer bottomRightCorner;
  // walls
  public SpriteRenderer topLeftWall;
  public SpriteRenderer topRightWall;
  public SpriteRenderer bottomLeftWall;
  public SpriteRenderer bottomRightWall;
  public SpriteRenderer leftTopWall;
  public SpriteRenderer leftBottomWall;
  public SpriteRenderer rightTopWall;
  public SpriteRenderer rightBottomWall;
  // door corners
  // public SpriteRenderer topLeftDoorCorner;
  // public SpriteRenderer topRightDoorCorner;
  // public SpriteRenderer bottomLeftDoorCorner;
  // public SpriteRenderer bottomRightDoorCorner;
  // public SpriteRenderer leftTopDoorCorner;
  // public SpriteRenderer leftBottomDoorCorner;
  // public SpriteRenderer rigthTopDoorCorner;
  // public SpriteRenderer rightBottomDoorCorner;
  #endregion

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

  public int getExitCount()
  {
    int exitCount = (exits.North ? 1 : 0) + (exits.South ? 1 : 0) + (exits.East ? 1 : 0) + (exits.West ? 1 : 0);

    return exitCount;
  }

  public void Init()
  {
    if (!initialised)
    {
      CreateWallTextures();
      floorSprite = gameObject.transform.Find("floor").GetComponent<SpriteRenderer>();
      CreateWalls();
      UpdateFloor();
    }

    initialised = true;
  }

  public void CreateWalls()
  {
    foreach (BoxCollider2D collider in gameObject.GetComponents<BoxCollider2D>())
    {
      DestroyImmediate(collider);
    }

    #region rigid bodies
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
    #endregion

    UpdateWalls();


  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdateFloor()
  {
    floorSprite.sprite = FloorTexture;
    floorSprite.drawMode = SpriteDrawMode.Tiled;
    floorSprite.transform.localScale = new Vector3(Scale, Scale, 1);
    floorSprite.size = dimensions / Scale;
    floorSprite.transform.position = new Vector3(dimensions.x / 2, -dimensions.y / 2) + floorSprite.transform.parent.position;
  }

  public void UpdateWalls()
  {
    float halfWidth = dimensions.x / 2;
    float halfHeight = dimensions.y / 2;

    #region wall sizes
    topLeft.size = new Vector2(halfWidth, Scale);
    topRight.size = new Vector2(halfWidth, Scale);
    bottomLeft.size = new Vector2(halfWidth, Scale);
    bottomRight.size = new Vector2(halfWidth, Scale);

    leftLeft.size = new Vector2(Scale, halfHeight);
    leftRight.size = new Vector2(Scale, halfHeight);
    rightLeft.size = new Vector2(Scale, halfHeight);
    rightRight.size = new Vector2(Scale, halfHeight);
    #endregion

    #region wall positions
    topLeft.offset = new Vector2(halfWidth / 2, Scale / 2);
    topRight.offset = new Vector2(halfWidth + halfWidth / 2, Scale / 2);
    bottomLeft.offset = new Vector2(halfWidth / 2, -dimensions.y - Scale / 2);
    bottomRight.offset = new Vector2(halfWidth + halfWidth / 2, -dimensions.y - Scale / 2);

    leftLeft.offset = new Vector2(-Scale / 2, -(halfHeight / 2));
    leftRight.offset = new Vector2(-Scale / 2, -(halfHeight + halfHeight / 2));
    rightLeft.offset = new Vector2(dimensions.x + Scale / 2, -(halfHeight / 2));
    rightRight.offset = new Vector2(dimensions.x + Scale / 2, -(halfHeight + halfHeight / 2));
    #endregion


  }

  public void UpdateWallTextures()
  {
    // room corners
    topRightCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    topLeftCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    bottomLeftCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    bottomRightCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // walls
    topLeftWall.transform.localScale = new Vector3(Scale, Scale, 1);
    topRightWall.transform.localScale = new Vector3(Scale, Scale, 1);
    bottomLeftWall.transform.localScale = new Vector3(Scale, Scale, 1);
    bottomRightWall.transform.localScale = new Vector3(Scale, Scale, 1);
    leftTopWall.transform.localScale = new Vector3(Scale, Scale, 1);
    leftBottomWall.transform.localScale = new Vector3(Scale, Scale, 1);
    rightTopWall.transform.localScale = new Vector3(Scale, Scale, 1);
    rightBottomWall.transform.localScale = new Vector3(Scale, Scale, 1);
    // door corners
    // topLeftDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // topRightDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // bottomLeftDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // bottomRightDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // leftTopDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // leftBottomDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // rigthTopDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);
    // rightBottomDoorCorner.transform.localScale = new Vector3(Scale, Scale, 1);


    float halfWidth = dimensions.x / 2;
    float halfHeight = dimensions.y / 2;

    float halfWidthScaled = dimensions.x / 2 / Scale;
    float halfHeightScaled = dimensions.y / 2 / Scale;

    // room corners
    topLeftCorner.transform.position = new Vector3(-Scale / 2, Scale / 2, 0) + transform.position;
    topRightCorner.transform.position = new Vector3(dimensions.x + Scale / 2, Scale / 2, 0) + transform.position;
    bottomLeftCorner.transform.position = new Vector3(-Scale / 2, -(dimensions.y + Scale / 2), 0) + transform.position;
    bottomRightCorner.transform.position = new Vector3(dimensions.x + Scale / 2, -(dimensions.y + Scale / 2), 0) + transform.position;
    // walls
    topLeftWall.transform.position = new Vector3(halfWidth / 2, Scale / 2, 0) + transform.position;
    topRightWall.transform.position = new Vector3(halfWidth / 2 + halfWidth, Scale / 2, 0) + transform.position;
    bottomLeftWall.transform.position = new Vector3(halfWidth / 2, -(dimensions.y + Scale / 2), 0) + transform.position;
    bottomRightWall.transform.position = new Vector3(halfWidth / 2 + halfWidth, -(dimensions.y + Scale / 2), 0) + transform.position;
    leftTopWall.transform.position = new Vector3(-Scale / 2, -(halfHeight / 2), 0) + transform.position;
    leftBottomWall.transform.position = new Vector3(-Scale / 2, -(halfHeight / 2 + halfHeight), 0) + transform.position;
    rightTopWall.transform.position = new Vector3(dimensions.x + Scale / 2, -(halfHeight / 2), 0) + transform.position;
    rightBottomWall.transform.position = new Vector3(dimensions.x + Scale / 2, -(halfHeight / 2 + halfHeight), 0) + transform.position;
    // door corners
    // topLeftDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // topRightDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // bottomLeftDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // bottomRightDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // leftTopDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // leftBottomDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // rigthTopDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;
    // rightBottomDoorCorner.transform.position = new Vector3(-100, -100, -100) + transform.position;

    // room corners
    topLeftCorner.size = new Vector2(1, 1);
    topRightCorner.size = new Vector2(1, 1);
    bottomLeftCorner.size = new Vector2(1, 1);
    bottomRightCorner.size = new Vector2(1, 1);
    // walls
    topLeftWall.size = new Vector2(halfWidthScaled, 1);
    topRightWall.size = new Vector2(halfWidthScaled, 1);
    bottomLeftWall.size = new Vector2(halfWidthScaled, 1);
    bottomRightWall.size = new Vector2(halfWidthScaled, 1);
    leftTopWall.size = new Vector2(halfHeightScaled, 1);
    leftBottomWall.size = new Vector2(halfHeightScaled, 1);
    rightTopWall.size = new Vector2(halfHeightScaled, 1);
    rightBottomWall.size = new Vector2(halfHeightScaled, 1);
    // door corners
    // topLeftDoorCorner.size = new Vector2(1, 1);
    // topRightDoorCorner.size = new Vector2(1, 1);
    // bottomLeftDoorCorner.size = new Vector2(1, 1);
    // bottomRightDoorCorner.size = new Vector2(1, 1);
    // leftTopDoorCorner.size = new Vector2(1, 1);
    // leftBottomDoorCorner.size = new Vector2(1, 1);
    // rigthTopDoorCorner.size = new Vector2(1, 1);
    // rightBottomDoorCorner.size = new Vector2(1, 1);

    // ROTATIONS
    Vector3 Z = new Vector3(0, 0, 1);
    // room corners
    topLeftCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    topRightCorner.transform.rotation = Quaternion.AngleAxis(90, Z);
    bottomLeftCorner.transform.rotation = Quaternion.AngleAxis(-90, Z);
    bottomRightCorner.transform.rotation = Quaternion.AngleAxis(180, Z);
    // walls
    topLeftWall.transform.rotation = Quaternion.AngleAxis(0, Z);
    topRightWall.transform.rotation = Quaternion.AngleAxis(0, Z);
    bottomLeftWall.transform.rotation = Quaternion.AngleAxis(180, Z);
    bottomRightWall.transform.rotation = Quaternion.AngleAxis(180, Z);
    leftTopWall.transform.rotation = Quaternion.AngleAxis(-90, Z);
    leftBottomWall.transform.rotation = Quaternion.AngleAxis(-90, Z);
    rightTopWall.transform.rotation = Quaternion.AngleAxis(90, Z);
    rightBottomWall.transform.rotation = Quaternion.AngleAxis(90, Z);
    // door corners
    // topLeftDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // topRightDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // bottomLeftDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // bottomRightDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // leftTopDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // leftBottomDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // rigthTopDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);
    // rightBottomDoorCorner.transform.rotation = Quaternion.AngleAxis(0, Z);

    // room corners
    topLeftCorner.sprite = CornerTexture;
    topRightCorner.sprite = CornerTexture;
    bottomLeftCorner.sprite = CornerTexture;
    bottomRightCorner.sprite = CornerTexture;
    // walls
    topLeftWall.sprite = WallTexture;
    topRightWall.sprite = WallTexture;
    bottomLeftWall.sprite = WallTexture;
    bottomRightWall.sprite = WallTexture;
    leftTopWall.sprite = WallTexture;
    leftBottomWall.sprite = WallTexture;
    rightTopWall.sprite = WallTexture;
    rightBottomWall.sprite = WallTexture;
    // door corners
    // topLeftDoorCorner.sprite = CornerTexture;
    // topRightDoorCorner.sprite = CornerTexture;
    // bottomLeftDoorCorner.sprite = CornerTexture;
    // bottomRightDoorCorner.sprite = CornerTexture;
    // leftTopDoorCorner.sprite = CornerTexture;
    // leftBottomDoorCorner.sprite = CornerTexture;
    // rigthTopDoorCorner.sprite = CornerTexture;
    // rightBottomDoorCorner.sprite = CornerTexture;
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

  public void UpdateWallTexturesForDoors()
  {
    float halfWidth = dimensions.x / 2;
    float halfHeight = dimensions.y / 2;

    float halfWidthScaled = dimensions.x / 2 / Scale;
    float halfHeightScaled = dimensions.y / 2 / Scale;
    
    if (exits.North)
    {
      topLeftWall.transform.position = new Vector3(halfWidth / 2, Scale / 2, 0) + transform.position - new Vector3(0.25f, 0, 0);
      topRightWall.transform.position = new Vector3(halfWidth / 2 + halfWidth, Scale / 2, 0) + transform.position + new Vector3(0.25f, 0, 0);
      topLeftWall.size = new Vector2(halfWidthScaled, 1) - new Vector2(0.5f, 0);
      topRightWall.size = new Vector2(halfWidthScaled, 1) - new Vector2(0.5f, 0);
    }
    if (exits.South)
    {
      bottomLeftWall.transform.position = new Vector3(halfWidth / 2, -(dimensions.y + Scale / 2), 0) + transform.position - new Vector3(0.25f, 0, 0);
      bottomRightWall.transform.position = new Vector3(halfWidth / 2 + halfWidth, -(dimensions.y + Scale / 2), 0) + transform.position + new Vector3(0.25f, 0, 0);
      bottomLeftWall.size = new Vector2(halfWidthScaled, 1) - new Vector2(0.5f, 0);
      bottomRightWall.size = new Vector2(halfWidthScaled, 1) - new Vector2(0.5f, 0);
    }

    if (exits.West)
    {
      leftBottomWall.transform.position = new Vector3(-Scale / 2, -(halfHeight / 2 + halfHeight), 0) + transform.position - new Vector3(0, 0.25f, 0);
      leftTopWall.transform.position = new Vector3(-Scale / 2, -(halfHeight / 2), 0) + transform.position + new Vector3(0, 0.25f, 0);
      leftTopWall.size = new Vector2(halfHeightScaled, 1) - new Vector2(0.5f, 0);
      leftBottomWall.size = new Vector2(halfHeightScaled, 1) - new Vector2(0.5f, 0);
    }
    if (exits.East)
    {
      rightBottomWall.transform.position = new Vector3(dimensions.x + Scale / 2, -(halfHeight / 2), 0) + transform.position + new Vector3(0, 0.25f, 0);
      rightTopWall.transform.position = new Vector3(dimensions.x + Scale / 2, -(halfHeight / 2 + halfHeight), 0) + transform.position - new Vector3(0, 0.25f, 0);
      rightTopWall.size = new Vector2(halfHeightScaled, 1) - new Vector2(0.5f, 0);
      rightBottomWall.size = new Vector2(halfHeightScaled, 1) - new Vector2(0.5f, 0);
    }
  }

  public void CreateWallTextures()
  {
    // Welcom to cancer town population infiniti
    floorSprite = (gameObject.transform.Find("floor") == null) ?  new GameObject("floor").AddComponent<SpriteRenderer>() : gameObject.transform.Find("floor").GetComponent<SpriteRenderer>();

    // room corners
    topLeftCorner = (gameObject.transform.Find("topLeftCorner") == null) ? new GameObject("topLeftCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topLeftCorner").GetComponent<SpriteRenderer>();
    topRightCorner = (gameObject.transform.Find("topRightCorner") == null) ? new GameObject("topRightCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topRightCorner").GetComponent<SpriteRenderer>();
    bottomLeftCorner = (gameObject.transform.Find("bottomLeftCorner") == null) ? new GameObject("bottomLeftCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomLeftCorner").GetComponent<SpriteRenderer>();
    bottomRightCorner = (gameObject.transform.Find("bottomRightCorner") == null) ? new GameObject("bottomRightCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomRightCorner").GetComponent<SpriteRenderer>();
    // walls
    topLeftWall = (gameObject.transform.Find("topLeftWall") == null) ? new GameObject("topLeftWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topLeftWall").GetComponent<SpriteRenderer>();
    topRightWall = (gameObject.transform.Find("topRightWall") == null) ? new GameObject("topRightWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topRightWall").GetComponent<SpriteRenderer>();
    bottomLeftWall = (gameObject.transform.Find("bottomLeftWall") == null) ? new GameObject("bottomLeftWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomLeftWall").GetComponent<SpriteRenderer>();
    bottomRightWall = (gameObject.transform.Find("bottomRightWall") == null) ? new GameObject("bottomRightWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomRightWall").GetComponent<SpriteRenderer>();
    leftTopWall = (gameObject.transform.Find("leftTopWall") == null) ? new GameObject("leftTopWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("leftTopWall").GetComponent<SpriteRenderer>();
    leftBottomWall = (gameObject.transform.Find("leftBottomWall") == null) ? new GameObject("leftBottomWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("leftBottomWall").GetComponent<SpriteRenderer>();
    rightTopWall = (gameObject.transform.Find("rigthTopWall") == null) ? new GameObject("rigthTopWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("rigthTopWall").GetComponent<SpriteRenderer>();
    rightBottomWall = (gameObject.transform.Find("rightBottomWall") == null) ? new GameObject("rightBottomWall").AddComponent<SpriteRenderer>() : gameObject.transform.Find("rightBottomWall").GetComponent<SpriteRenderer>();
    // door corners
    // topLeftDoorCorner = (gameObject.transform.Find("topLeftDoorCorner") == null) ? new GameObject("topLeftDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topLeftDoorCorner").GetComponent<SpriteRenderer>();
    // topRightDoorCorner = (gameObject.transform.Find("topRightDoorCorner") == null) ? new GameObject("topRightDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("topRightDoorCorner").GetComponent<SpriteRenderer>();
    // bottomLeftDoorCorner = (gameObject.transform.Find("bottomLeftDoorCorner") == null) ? new GameObject("bottomLeftDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomLeftDoorCorner").GetComponent<SpriteRenderer>();
    // bottomRightDoorCorner = (gameObject.transform.Find("bottomRightDoorCorner") == null) ? new GameObject("bottomRightDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("bottomRightDoorCorner").GetComponent<SpriteRenderer>();
    // leftTopDoorCorner = (gameObject.transform.Find("leftTopDoorCorner") == null) ? new GameObject("leftTopDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("leftTopDoorCorner").GetComponent<SpriteRenderer>();
    // leftBottomDoorCorner = (gameObject.transform.Find("leftBottomDoorCorner") == null) ? new GameObject("leftBottomDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("leftBottomDoorCorner").GetComponent<SpriteRenderer>();
    // rigthTopDoorCorner = (gameObject.transform.Find("rigthTopDoorCorner") == null) ? new GameObject("rigthTopDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("rigthTopDoorCorner").GetComponent<SpriteRenderer>();
    // rightBottomDoorCorner = (gameObject.transform.Find("rightBottomDoorCorner") == null) ? new GameObject("rightBottomDoorCorner").AddComponent<SpriteRenderer>() : gameObject.transform.Find("rightBottomDoorCorner").GetComponent<SpriteRenderer>();

    floorSprite.transform.parent = transform;
    // room corners
    topLeftCorner.transform.parent = transform;
    topRightCorner.transform.parent = transform;
    bottomLeftCorner.transform.parent = transform;
    bottomRightCorner.transform.parent = transform;
    // walls
    topLeftWall.transform.parent = transform;
    topRightWall.transform.parent = transform;
    bottomLeftWall.transform.parent = transform;
    bottomRightWall.transform.parent = transform;
    leftTopWall.transform.parent = transform;
    leftBottomWall.transform.parent = transform;
    rightTopWall.transform.parent = transform;
    rightBottomWall.transform.parent = transform;
    // door corners
    // topLeftDoorCorner.transform.parent = transform;
    // topRightDoorCorner.transform.parent = transform;
    // bottomLeftDoorCorner.transform.parent = transform;
    // bottomRightDoorCorner.transform.parent = transform;
    // leftTopDoorCorner.transform.parent = transform;
    // leftBottomDoorCorner.transform.parent = transform;
    // rigthTopDoorCorner.transform.parent = transform;
    // rightBottomDoorCorner.transform.parent = transform;

    // room corners
    topLeftCorner.drawMode = SpriteDrawMode.Sliced;
    topRightCorner.drawMode = SpriteDrawMode.Sliced;
    bottomLeftCorner.drawMode = SpriteDrawMode.Sliced;
    bottomRightCorner.drawMode = SpriteDrawMode.Sliced;
    // walls
    topLeftWall.drawMode = SpriteDrawMode.Tiled;
    topRightWall.drawMode = SpriteDrawMode.Tiled;
    bottomLeftWall.drawMode = SpriteDrawMode.Tiled;
    bottomRightWall.drawMode = SpriteDrawMode.Tiled;
    leftTopWall.drawMode = SpriteDrawMode.Tiled;
    leftBottomWall.drawMode = SpriteDrawMode.Tiled;
    rightTopWall.drawMode = SpriteDrawMode.Tiled;
    rightBottomWall.drawMode = SpriteDrawMode.Tiled;
    // door corners
    // topLeftDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // topRightDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // bottomLeftDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // bottomRightDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // leftTopDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // leftBottomDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // rigthTopDoorCorner.drawMode = SpriteDrawMode.Sliced;
    // rightBottomDoorCorner.drawMode = SpriteDrawMode.Sliced;
    
    floorSprite.sortingLayerName = "Floor";
    
    // room corners
    topLeftCorner.sortingLayerName = "Wall";
    topRightCorner.sortingLayerName = "Wall";
    bottomLeftCorner.sortingLayerName = "Wall";
    bottomRightCorner.sortingLayerName = "Wall";
    // walls
    topLeftWall.sortingLayerName = "Wall";
    topRightWall.sortingLayerName = "Wall";
    bottomLeftWall.sortingLayerName = "Wall";
    bottomRightWall.sortingLayerName = "Wall";
    leftTopWall.sortingLayerName = "Wall";
    leftBottomWall.sortingLayerName = "Wall";
    rightTopWall.sortingLayerName = "Wall";
    rightBottomWall.sortingLayerName = "Wall";
    // door corners
    // topLeftDoorCorner.sortingLayerName = "Wall";
    // topRightDoorCorner.sortingLayerName = "Wall";
    // bottomLeftDoorCorner.sortingLayerName = "Wall";
    // bottomRightDoorCorner.sortingLayerName = "Wall";
    // leftTopDoorCorner.sortingLayerName = "Wall";
    // leftBottomDoorCorner.sortingLayerName = "Wall";
    // rigthTopDoorCorner.sortingLayerName = "Wall";
    // rightBottomDoorCorner.sortingLayerName = "Wall";

    UpdateWallTextures();
  }
}
