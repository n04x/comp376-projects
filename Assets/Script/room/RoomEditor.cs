using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    private int propCount = 0;

    public void Awake()
    {
        Room room = target as Room;
        room.Init();
    }

  public override void OnInspectorGUI()
  {
    Room room = target as Room;

    DrawSizeGUI(room);
    DrawTextureGui(room);
    DrawDoorsGUI(room);

    room.CreateWallTextures();
    room.UpdateWalls();
    room.UpdateWallTextures();
    room.UpdateDoors();
    room.UpdateFloor();
    room.UpdateWallTexturesForDoors();
  }

  void OnSceneGUI()
  {
    Room room = target as Room;
    if (propCount < room.transform.childCount)
    {
      propCount = room.transform.childCount;
      GameObject child;
      for (int i = 0; i < propCount; ++i)
      {
        child = room.transform.GetChild(i).gameObject;
        if (child.GetComponent<Prop>() == null)
        {
          Prop newProp = child.AddComponent<Prop>();
          newProp.targetRoom = room;
        }
      }
    }
  }

  private void DrawDoorsGUI(Room room) {
    GUILayout.Label("Doors: ");

    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();
    room.exits.North = GUILayout.Toggle(room.exits.North, "North");
    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    room.exits.West = GUILayout.Toggle(room.exits.West, "West");
    GUILayout.FlexibleSpace();
    room.exits.East = GUILayout.Toggle(room.exits.East, "East");
    GUILayout.EndHorizontal();

    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();
    room.exits.South = GUILayout.Toggle(room.exits.South, "South");
    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();

  }

  private void DrawTextureGui(Room room)
  {
    GUILayout.Label("\nTextures: ");
    room.FloorTexture = EditorGUILayout.ObjectField("Floor", room.FloorTexture, typeof(Sprite), false) as Sprite;
    room.WallTexture = EditorGUILayout.ObjectField("Wall", room.WallTexture, typeof(Sprite), false) as Sprite;
    room.CornerTexture = EditorGUILayout.ObjectField("Corner", room.CornerTexture, typeof(Sprite), false) as Sprite;
  }

  private void DrawSizeGUI(Room room) {
    GUILayout.Label("Sizes: ");
    room.dimensions.x = EditorGUILayout.FloatField("Height", room.dimensions.x);
    room.dimensions.y = EditorGUILayout.FloatField("Width", room.dimensions.y);
    room.Scale = EditorGUILayout.FloatField("Scale", room.Scale);
    if (room.Scale <= 0)
    {
      room.Scale = 1; // make sure scale is always greater than 0
    }
  }
}
