using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;


public class MineButtonEvent : MonoBehaviour {
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
            GameObject.FindWithTag("Player").GetComponent<MinePlacer>().mine();
        }
        else
        {
            Player.GetComponent<MissileFire>().missile(1);
        }
        StartCoroutine("Delay");
    }

    IEnumerator Delay()
    {
        GameObject.FindWithTag("Player").GetComponent<CarStats>().MinesButtonInteractableVar = false;
        delay = 3.0f - (GameObject.FindWithTag("Player").GetComponent<CarStats>().Car * 0.2f) - (GameObject.FindWithTag("Player").GetComponent<CarStats>().Gun * 0.1f);
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<Button>().interactable = false;
        Debug.Log("delay " + delay);
        yield return new WaitForSeconds(delay);
        print("should show now");
        GetComponent<CanvasGroup>().alpha = 100;
        GetComponent<Button>().interactable = true;
        GameObject.FindWithTag("Player").GetComponent<CarStats>().MinesButtonInteractableVar = true;
    }

}
