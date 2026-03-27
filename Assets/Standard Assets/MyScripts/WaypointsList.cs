using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class WaypointsList : MonoBehaviour
    {
        public List<GameObject> listOfWaypoints = new List<GameObject>();
        //public GameObject[] listOfWaypoints;

        public void reverseList() {
            listOfWaypoints.Reverse();        
        }


    // Use this for initialization
    void Start()
        {
        /*
        for (var i=0;i<listOfWaypoints.Count;i++)
        {
            listOfWaypoints[i].GetComponent<WayPointPlace>().enabled = false;
        }
        */


        }
    }
