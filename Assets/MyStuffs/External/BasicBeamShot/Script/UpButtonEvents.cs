using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Car;

public class UpButtonEvents : MonoBehaviour {
    public bool NetworkPlayer = false;
    public GameObject Player;

    // Use this for initialization
    void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
        EventTrigger trigger2 = GetComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
        trigger2.triggers.Add(entry2);
    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        if (!NetworkPlayer)
        {
            GameObject.FindWithTag("Player").GetComponent<CarUserControl>().accOn();
        }
        else
        {
            Player.GetComponent<CarUserControl>().accOn();
        } 
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        if (!NetworkPlayer)
        {
            GameObject.FindWithTag("Player").GetComponent<CarUserControl>().accOff();
        }
        else
        {
            Player.GetComponent<CarUserControl>().accOn();
        }
    }
}
