using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitcher : MonoBehaviour {
    public GameObject FrontLightsObj;
    public bool FrontLightsActiveVar = false;
    // Use this for initialization
    void Start () {
		
        if (FrontLightsObj)
        {
            FrontLightsActiveVar = true;
        }
        else
        {
            FrontLightsActiveVar = false;
        }


	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKey("l"))
        {

            FrontLightsObj.SetActive(true);
            FrontLightsActiveVar = true;
       }
        if (Input.GetKey("i"))
        {
            FrontLightsObj.SetActive(false);
            FrontLightsActiveVar = false;
        }


    }
}
