using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffectsController : MonoBehaviour {
    private int DayState;

    // Use this for initialization
    void Start () {
		
	}


    void Awake()
    {
        /*
        DayState = int.Parse(PlayerPrefs.GetString("RaceDayState"));
        if (DayState == 0)
        {
            GetComponent<BloomOptimized>().enabled = true;
        }
        */
    }
}
