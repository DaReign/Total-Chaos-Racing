using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeReflectionProbe : MonoBehaviour {
    public GameObject MainCamera;
	// Use this for initialization
	void Start () {

        MainCamera = GameObject.Find("Main Camera");

    }
	
	// Update is called once per frame
	void Update () {
        print(transform.position);
        print(MainCamera.transform.position.x);
        transform.position = new Vector3(MainCamera.transform.position.x, -MainCamera.transform.position.x, MainCamera.transform.position.z);

	}
}
