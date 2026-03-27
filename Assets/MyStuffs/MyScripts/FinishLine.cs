using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerEnter(Collider other)
    {

        if (other.transform.root.gameObject.tag == "Player" || other.transform.root.gameObject.tag == "Enemy")
        {

            if (other.transform.root.gameObject.GetComponent<CarStats>().readyForFinish==true)
            {
                //print("Car ready for finish");

                other.transform.root.gameObject.GetComponent<CarStats>().finishLineCrossed = true; 
            }
            else
            {
               // Debug.Log("car not ready for finish " + other.transform.root.gameObject.GetComponent<CarStats>().readyForFinish);

            }


            //Debug.Log("finish colided with "+ other.transform.root.gameObject);
       }
    }



}
