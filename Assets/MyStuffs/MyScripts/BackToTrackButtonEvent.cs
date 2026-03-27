using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Car;

public class BackToTrackButtonEvent : MonoBehaviour {
    public bool NetworkPlayer = false;
    public GameObject Player;
    private Transform Target;
    private Transform LookTarget;
    private GameObject Global;

    // Use this for initialization
    void Start () {
        Global = GameObject.Find("Global");
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);

    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        //   print("Event Clicked");

        if (GameObject.FindWithTag("Player").GetComponent<Human>().currentWaypoint - 1 > 0)
        {
            Target = Global.GetComponent<WaypointsList>().listOfWaypoints[GameObject.FindWithTag("Player").GetComponent<Human>().currentWaypoint - 1].transform;
        }
        else
        {
            Target = Global.GetComponent<WaypointsList>().listOfWaypoints[Global.GetComponent<WaypointsList>().listOfWaypoints.Count-1].transform;
        }

        if (!NetworkPlayer)
        {
            //GameObject.FindWithTag("Player").GetComponent<Human>().Target;
            GameObject.FindWithTag("Player").transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y+50, Target.transform.position.z);
            GameObject.FindWithTag("Player").transform.localEulerAngles = new Vector3(0, 0, 0);
            GameObject.FindWithTag("Player").transform.LookAt(GameObject.FindWithTag("Player").GetComponent<Human>().Target);
        }
        else
        {
            //GameObject.FindWithTag("Player").GetComponent<Human>().Target;
        }
    }
}
