using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLight : MonoBehaviour {
    private int DayState;
    public Material lightOn;
    public Material lightOff;
    public Renderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        PlayerPrefs.SetString("RaceDayState","1");

        DayState = int.Parse(PlayerPrefs.GetString("RaceDayState"));

        if (DayState == 0)
        {
            rend.sharedMaterial = lightOn;
        }

    }
}
