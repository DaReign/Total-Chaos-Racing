using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class Human : MonoBehaviour {
    // private GameObject WreckedCamera;
    public GameObject Global;
    private bool firstPass = true;
    public Transform Target;
    public Transform PreviousTarget;
    public int currentWaypoint;
    public int allWaypointsToGo;
    private int AllLapsVar;
    private GameObject LapsText;
    //private GameObject Reflection;

    // Use this for initialization
    void Start () {
        //print("human car enabled");
        // WreckedCamera = GameObject.Find("CameraWrecked");
        // WreckedCamera.SetActive(false);
        Global = GameObject.Find("Global");
        LapsText = GameObject.Find("LapsText");
        AllLapsVar = Global.GetComponent<Stage>().laps;
        LapsText.GetComponent<UnityEngine.UI.Text>().text = "Laps 0/"+ AllLapsVar;
        currentWaypoint = 0;
        Target = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform;
        PreviousTarget = Target;
        allWaypointsToGo = Global.GetComponent<WaypointsList>().listOfWaypoints.Count * Global.GetComponent<Stage>().laps;
        GetComponent<CarStats>().waypointsToGo = allWaypointsToGo;

        /*
        Reflection = GameObject.Find("ReflectionProbeDay");
        if (Reflection)
        {
            Reflection.transform.parent = transform;
            Reflection.transform.position = new Vector3(transform.position.x, transform.position.y+0.5f,transform.position.z);
            //player.transform.parent = newParent.transform;
        }
        */
    }

    private void LateUpdate()
    {
        LapsText.GetComponent<UnityEngine.UI.Text>().text = "Laps "+GetComponent<CarStats>().currentLap+"/"+AllLapsVar;
    }


    // Update is called once per frame
    void Update () {


        //Debug.Log("waypoint distance " +Vector3.Distance(Target.position, transform.position));

        //GetComponent<CheckIfOnTrack>().currentTargetWaypoint = Target;

        if (Vector3.Distance(Target.position, transform.position) < 60.0f)
        {

            if (currentWaypoint == 0)
            {
                GetComponent<CarStats>().currentLap++;
                GetComponent<CarStats>().startWaypointReached = true;
            }

            if (GetComponent<CarStats>().startWaypointReached)
            {

                if (currentWaypoint < Global.GetComponent<WaypointsList>().listOfWaypoints.Count - 1)
                {
                    PreviousTarget = Target;
                    currentWaypoint++;
                    Target = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform;
                }
                else
                {
                    PreviousTarget = Target;
                    currentWaypoint = 0;
                    Target = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform;
                }

                GetComponent<CarStats>().waypointsCleared++;

            }

        }

        


            /*
            if (firstPass)
            {
                firstPass = false;
                Camera.main.GetComponent<CameraBlurEnabler>().activated = true;
            }
            */

            if ( GetComponent<CarStats>().wrecked || GetComponent<CarStats>().raceFinished )
            {
            //print("time scale 20");

            //Time.timeScale = 20;
            //WreckedCamera.SetActive(true);
            //Camera.main.gameObject.SetActive(false);
                GetComponent<Human>().enabled = false;
                GetComponent<CarUserControl>().enabled = false;
                GameObject.Find("Global").GetComponent<CalculatePlayerRaceResults>().calculate();
            }


    }
}
