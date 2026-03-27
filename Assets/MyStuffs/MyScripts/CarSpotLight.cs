using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpotLight : MonoBehaviour {
    private int DayState;

    // Use this for initialization
    void Awake () {
        DayState = int.Parse(PlayerPrefs.GetString("RaceDayState"));

        if (DayState == 1)
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
