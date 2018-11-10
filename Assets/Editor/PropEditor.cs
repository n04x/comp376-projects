using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Prop))]
public class PropEditor : Editor
{
    void OnSceneGUI() {
        Prop prop = target as Prop;

    }
}
