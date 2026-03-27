using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTrigger : MonoBehaviour {

    public GameObject SplashAnimationObject;

    // Use this for initialization
    void Start () {
        

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with "+ other.gameObject);
        //Debug.Log("Collided tag " + other.gameObject.tag);

 

            if (other.gameObject.tag == "AnimationTrigger")
            {

            SplashAnimationObject.SetActive(true);

            print("Splash activated");
            /*
                if (other.transform.root.gameObject.GetComponent<CarStats>().Hull >= 30)
                {
                    //other.transform.root.gameObject.GetComponent<CarStats>().Hull -= 30;
                    //Debug.Log("take damage");
                    other.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(missileDamage);
                }
                else
                {
                    //other.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(30);
                    other.transform.root.gameObject.GetComponent<CarStats>().Hull = 0;
                }
                Debug.Log("Missile id " + missileId + " created by " + racerId + " car level " + Car + " and gun level " + Gun);
                Debug.Log("Collided with " + other.transform.root.gameObject.GetComponent<CarStats>().racerId + " and inflicted " + missileDamage + " damage ");
                other.transform.root.gameObject.GetComponent<Rigidbody>().AddExplosionForce(125000.0f, transform.position, 0.5f, 300.0F);
            */
            }
    }
}
