using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, 1 * Time.deltaTime, 0, Space.World);
        //transform.Rotate(1 * Time.deltaTime, 0, 0, Space.World);
        //transform.Rotate(0, 0, 1 * Time.deltaTime, Space.World);
        //transform.Rotate(0, 1 * Time.deltaTime, 0);
        //transform.Rotate(1 * Time.deltaTime, 0, 0);
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
}
