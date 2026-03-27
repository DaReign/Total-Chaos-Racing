using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateAroundPoint : MonoBehaviour {
    public GameObject Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        if (Target)
        {
            transform.RotateAround(new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z), Vector3.up, 20 * Time.deltaTime);
        }
    }
}
