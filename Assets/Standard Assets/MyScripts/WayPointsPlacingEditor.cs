#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WayPointsPlacing))]
public class WayPointsPlacingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //ObjectBuilderScript myScript = (ObjectBuilderScript)target;
        if (GUILayout.Button("Build Object"))
        {
          //  myScript.BuildObject();
        }
    }
}
#endif