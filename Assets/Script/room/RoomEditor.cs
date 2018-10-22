using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Room))]
public class RoomEditor : Editor
{
    private int propCount = 0;

    public void Awake() {
        Room room = target as Room;
        room.CreateWalls();
    }

  public override void OnInspectorGUI()
  {
    Room room = target as Room;

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Width: ", GUILayout.Width(50));
    string width = GUILayout.TextField("" + room.dimensions.x, GUILayout.Width(50));
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.BeginHorizontal();
    GUILayout.Label("Height: ", GUILayout.Width(50));
    string heigth = GUILayout.TextField("" + room.dimensions.y, GUILayout.Width(50));
    EditorGUILayout.EndHorizontal();

    DrawDoorsGUI(room);


    float realWidth;
    float realHeight;
    if (float.TryParse(width, out realWidth) && float.TryParse(heigth, out realHeight))
    {
      room.dimensions = new Vector2(realWidth, realHeight);
    }
    room.UpdateWalls();
    room.UpdateDoors();
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

  private void DrawSizeGUI() {
    //   TODO
  }
}
