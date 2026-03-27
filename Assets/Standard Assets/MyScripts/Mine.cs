using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;

public class Mine : MonoBehaviour {

    private Boolean activated = false;
    private Boolean destroyStarted = false;
    private Boolean allowBlowCreator = false;
    private Boolean allowBlowCreatorActivated = false;
    public GameObject Explosion;
    public string State = "";
    public string State2 = "";
    public GameObject CreatedBy;
    public GameObject TriggeredBy;
    public List<GameObject> TriggeredByList = new List<GameObject>();


    // Use this for initialization
    void Start () {
        //Explosion = transform.Find("ETF_M_Explosion").gameObject;
        Explosion.SetActive(false);
        transform.parent = null;
        if (CreatedBy.GetComponent<IsNetworkPlayer>().NetworkPlayer==false)
        {
            // NetworkTransform removed in Unity 6 (UNet)
            // if (GetComponent<NetworkTransform>())
            //     {
            //         Destroy(GetComponent<NetworkTransform>());
            //     }
        }

        //GetComponent<BoxCollider>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (!allowBlowCreatorActivated)
        {
            StartCoroutine("allowBlowCreatorActivatedDelay");
        }
		
	}
    /*
    IEnumerator activate()
    {
        
        yield return new WaitForSeconds(5.0f);
        GetComponent<BoxCollider>().enabled = true;
    }
    */

    IEnumerator destroy()
    {
        if (gameObject!=null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //print(Explosion);
            Explosion.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            State = "waiting for destroy";
            yield return new WaitForSeconds(5.0f);
            Destroy(gameObject);
        }
    }

    IEnumerator allowBlowCreatorActivatedDelay()
    {
        allowBlowCreatorActivated = true;
        yield return new WaitForSeconds(5.0f);
        allowBlowCreator = true;
    }




    void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        //print(other.transform.root.gameObject);
        // if (other.tag == "Enemy")
        // {
        //     print(other.transform.root.gameObject.GetComponent<CarStats>());
        // }
        if (other.transform.root.gameObject == CreatedBy)
        {
            //print("self destroying");
            //print(other.transform.root.gameObject);
            //print(CreatedBy);
            State2 = "self destroyed";

        }


            if (!destroyStarted)
            {
                if (other.transform.root.gameObject.tag == "Player" || other.transform.root.gameObject.tag == "Enemy")
                {

                    if (other.transform.root.gameObject == CreatedBy)
                    {
                        if (allowBlowCreator)
                        {

                            TriggeredBy = other.transform.root.gameObject;
                            TriggeredByList.Add(other.transform.root.gameObject);
                            //print("MINE triggered with !!!!!!!!!!!!");
                            //print(other);
                            destroyStarted = true;
                            State = "collider false";
                            StartCoroutine("destroy");
                            if (other.transform.root.gameObject.GetComponent<CarStats>().Hull >= 30)
                            {
                                other.transform.root.gameObject.GetComponent<CarStats>().Hull -= 30;
                            }
                            else
                            {
                                other.transform.root.gameObject.GetComponent<CarStats>().Hull = 0;
                            }
                            other.transform.root.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100000.0f, transform.position, 0.5f, 300.0F);
                        }
                    }
                    else
                    {
                        TriggeredBy = other.transform.root.gameObject;
                        TriggeredByList.Add(other.transform.root.gameObject);
                        //print("MINE triggered with !!!!!!!!!!!!");
                        //print(other);
                        destroyStarted = true;
                        State = "collider false";
                        StartCoroutine("destroy");
                        if (other.transform.root.gameObject.GetComponent<CarStats>().Hull >= 30)
                        {
//                            other.transform.root.gameObject.GetComponent<CarStats>().Hull -= 30;
                            other.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(30);
                        }
                        else
                        {
                            other.transform.root.gameObject.GetComponent<CarStats>().Hull = 0;
                        }
                        other.transform.root.gameObject.GetComponent<Rigidbody>().AddExplosionForce(300000.0f, transform.position, 0.5f, 1000.0F);

                }


            }
            }

    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Enemy")
        {
            print("MINE Collided with !!!!!!!!!!!!");
            print(collision.collider);
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("destroy");
        }
    }
    */

}
