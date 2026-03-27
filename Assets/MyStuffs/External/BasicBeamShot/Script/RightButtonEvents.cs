using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Car;

public class RightButtonEvents : MonoBehaviour {

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
        GameObject.FindWithTag("Player").GetComponent<CarUserControl>().turnRightOn();
    }

    public void OnPointerExitDelegate(PointerEventData data)
    {
        GameObject.FindWithTag("Player").GetComponent<CarUserControl>().turnRightOff();
    }
}
