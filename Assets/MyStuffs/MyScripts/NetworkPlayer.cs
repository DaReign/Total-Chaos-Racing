using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6

public class NetworkPlayer : MonoBehaviour {
    public bool IsNetworkPlayer;
	// Use this for initialization
	void Start () {
		if(!IsNetworkPlayer)
        {
            // UNet components removed in Unity 6
            // GetComponent<NetworkIdentity>().enabled = false;
            // GetComponent<NetworkTransform>().enabled = false;
            print("Set active");
            this.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
