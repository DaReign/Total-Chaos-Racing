using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.ImageEffects;

public class CameraBlurEnabler : MonoBehaviour {
    public GameObject PlayerCar;
    public Boolean activated = false;
	// Use this for initialization
	void Start () {
        PlayerCar = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //print("Car speed");
        //print(PlayerCar.GetComponent<CarController>().CurrentSpeed);
        if (activated)
        {
            if (PlayerCar.GetComponent<CarController>() && PlayerCar.GetComponent<CarController>().CurrentSpeed > 400)
            {
                Camera.main.GetComponent<MotionBlur>().enabled = true;

                Camera.main.GetComponent<MotionBlur>().blurAmount = PlayerCar.GetComponent<CarController>().CurrentSpeed / 100 - 0.2f;
                if (Camera.main.GetComponent<MotionBlur>().blurAmount > 1.0f)
                {
                    Camera.main.GetComponent<MotionBlur>().blurAmount = 1.0f;
                }

                //print("blur");
                //print(Camera.main.GetComponent<MotionBlur>().blurAmount);
            }
            else
            {
                Camera.main.GetComponent<MotionBlur>().enabled = false;
            }

        }

    }
}
