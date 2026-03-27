using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
public class KeyboardControll : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setKeyboardControll()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CarUserControl>().KeyboardSteering = true;
    }

}
