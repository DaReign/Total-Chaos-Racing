using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;
public class GarageCar : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Clean();
    }
    void Awake()
    {
        Clean();
    }


    void Clean()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CarController>().enabled = false;
        GetComponent<CarStats>().enabled = false;
        GetComponent<NitroAi>().enabled = false;
        GetComponent<Human>().enabled = false;
        GetComponent<CarAddingsTest>().enabled = false;
        GetComponent<CarUserControl>().enabled = false;
        GetComponent<SettingCar>().enabled = false;
        GetComponent<MissileFire>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        GetComponent<MinePlacer>().enabled = false;
        GetComponent<MasterGun>().enabled = false;
        GetComponent<MissileFire>().enabled = false;
        // GetComponent<NetworkTransform>().enabled = false; // UNet removed in Unity 6
        GetComponent<ChangePlayerTag>().enabled = false;
        GetComponent<IsNetworkPlayer>().enabled = false;

        transform.Find("RocketBlender").gameObject.SetActive(false);
        transform.Find("launcher").gameObject.SetActive(false);
        transform.Find("LauncherEnd").gameObject.SetActive(false);
        transform.Find("Particles").gameObject.SetActive(false);
        transform.Find("ETF_M_Fire Stream").gameObject.SetActive(false);
        transform.Find("Gun").gameObject.SetActive(false);
        transform.Find("MinePlacer").gameObject.SetActive(false);
        transform.Find("RayCastSource").gameObject.SetActive(false);
        transform.Find("Nitro").gameObject.SetActive(false);
        transform.Find("MissileTarget").gameObject.SetActive(false);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
