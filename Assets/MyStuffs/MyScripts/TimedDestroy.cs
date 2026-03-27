using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {
    public float delay = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("FastFade");
    }

    IEnumerator FastFade()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
