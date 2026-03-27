using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;


public class HomingMissileButtonEvent : MonoBehaviour {
    public bool NetworkPlayer = false;
    public GameObject Player;
    private float delay;

    // Use this for initialization
    void Start () {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
        /*
        EventTrigger trigger2 = GetComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnPointerExitDelegate((PointerEventData)data); });
        trigger2.triggers.Add(entry2);
        */
    }

    public void OnPointerDownDelegate(PointerEventData data)
    {
        /*
        if (GetComponent<CanvasGroup>().alpha == 0)
        {
            return;
        }
        */

        if (!NetworkPlayer)
        {
            GameObject.FindWithTag("Player").GetComponent<MissileFire>().missile(1);
        }
        else
        {
            Player.GetComponent<MissileFire>().missile(1);
        }
        StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        GameObject.FindWithTag("Player").GetComponent<CarStats>().HomingMissilesButtonInteractableVar = false;
        delay = 3.0f - (GameObject.FindWithTag("Player").GetComponent<CarStats>().Car * 0.2f) - (GameObject.FindWithTag("Player").GetComponent<CarStats>().Gun * 0.1f);
        GetComponent<Button>().interactable = false;
        GetComponent<CanvasGroup>().alpha = 0;
        Debug.Log("delay " + delay);
        yield return new WaitForSeconds(delay);
        print("should show now");
        GetComponent<CanvasGroup>().alpha = 100;
        GetComponent<Button>().interactable = true;
        GameObject.FindWithTag("Player").GetComponent<CarStats>().HomingMissilesButtonInteractableVar = true;
    }
    /*
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
    */

}
