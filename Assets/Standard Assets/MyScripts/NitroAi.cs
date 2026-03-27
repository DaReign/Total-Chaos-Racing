using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class NitroAi : MonoBehaviour {
    private Rigidbody rb;
    public Boolean nitro = false;
    public Boolean test = false;
    private GameObject NitroParticle;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        NitroParticle = transform.Find("Nitro").gameObject;
        NitroParticle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (!test)
        {
            if (GetComponent<CarStats>().wrecked || GetComponent<CarStats>().raceFinished )
            {
                nitro = false;
            }


            if (nitro)
            {
                if (GetComponent<CarStats>().Nitro > 0)
                {
                    rb.AddForce(transform.forward * 10000);
                    //Camera.main.GetComponent<MotionBlur>().enabled = true;
                    NitroParticle.SetActive(true);
                    GetComponent<CarStats>().Nitro--;
                }
            }
            else
            {
                // Camera.main.GetComponent<MotionBlur>().enabled = false;
                NitroParticle.SetActive(false);
            }

            //print (GetComponent<CarController>().CurrentSpeed) ;

        }

    }
}
