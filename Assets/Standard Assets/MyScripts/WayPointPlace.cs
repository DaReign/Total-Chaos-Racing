using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using System;
[ExecuteInEditMode]
public class WayPointPlace : MonoBehaviour
{
    GameObject Global;
    public int status = 1;
    public Boolean running = false;

    // Use this for initialization
    void Start()
    {
        //running = true;
    }

    void Awake()
    {
        //running = true;
    }

    // Update is called once per frame
    void Update()
    {
        //running = true;
    }

    public void OnDestroy()
    {
 
            Global = GameObject.Find("Global");
        if (Global!=null&& this.gameObject!=null&& Global.GetComponent<WaypointsList>().listOfWaypoints!=null)
        {
            Global.GetComponent<WaypointsList>().listOfWaypoints.Remove(this.gameObject);
            status = 0;
        }
        /*
        if (!running)
        {

            if (EditorApplication.isPlaying) return;

            if (Application.isEditor)
            {
                //EditorApplication.isPlaying

                Global = GameObject.Find("Global");
                Global.GetComponent<WaypointsList>().listOfWaypoints.Remove(this.gameObject);
                status = 0;
            }
        }
        */
    }

}
#endif



