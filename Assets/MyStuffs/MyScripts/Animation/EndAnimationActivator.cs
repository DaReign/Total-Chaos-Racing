using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimationActivator : MonoBehaviour {
    public GameObject LavaObj;
    public GameObject Fire_MistObj;
    public GameObject Burning_GroundObj;
    public GameObject ThunderObj;
    public GameObject AObj;
    public GameObject NapisObj;
    public GameObject MoonObj;
    public GameObject Car;
    public bool StartedGeneratingVar = false;


    // Use this for initialization
    void Start () {
       // QualitySettings.SetQualityLevel(5, true);
       // Debug.Log("quality "+ QualitySettings.GetQualityLevel());

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with "+ other.gameObject);
        //Debug.Log("Collided tag " + other.gameObject.tag);

        //LavaObj.SetActive(true);
        //Fire_MistObj.SetActive(true);
        Burning_GroundObj.SetActive(true);

        if (!StartedGeneratingVar)
        {
            StartedGeneratingVar = true;
            StartCoroutine("generate");
        }

        //ThunderObj.SetActive(true);


        if (other.gameObject.tag == "AnimationTrigger")
        {

//            SplashAnimationObject.SetActive(true);

//            print("Splash activated");
        }



    }

    IEnumerator generate()
    {
        //Car.GetComponent<Rigidbody>().mass = 10000000.0f;
        yield return new WaitForSeconds(0.0f);
        AObj.SetActive(true);
        yield return new WaitForSeconds(25.0f);
        NapisObj.SetActive(true);
        MoonObj.SetActive(false);
        yield return new WaitForSeconds(0.0f);
        ThunderObj.SetActive(true);
    }


}
