#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WayPointPlace))]
public class WayPointPlaceEditor : Editor
{
    GameObject WaypointsContainer;
    GameObject Global;

    public void Awake()
    {
        // get the car controller
        //Debug.Log("maybe awake");
        Global = GameObject.Find("Global");


        if (GameObject.Find("WaypointsContainer") != null)
        {
            //Debug.Log("container available");

            if (((WayPointPlace)target).transform.parent == null)
            {
                WaypointsContainer = GameObject.Find("WaypointsContainer");
                Global.GetComponent<WaypointsList>().listOfWaypoints.Add(((WayPointPlace)target).gameObject);
                ((WayPointPlace)target).transform.parent = WaypointsContainer.transform;
            }

        }
        else
        {
            //Debug.Log("container not available");
            WaypointsContainer = new GameObject();
            WaypointsContainer.name = "WaypointsContainer";
           // Debug.Log("container created");
        }

    }


    public void Start()
    {

    }


    public void NowDestroy()
    {
        Debug.Log("status destroyed");
        Debug.Log(Global.GetComponent<WayPointPlace>().status);
    }


    public void Update()
    {
        Debug.Log("status");
        Debug.Log(Global.GetComponent<WayPointPlace>().status);
    }


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