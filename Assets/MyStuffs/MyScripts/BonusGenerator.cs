using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour {
//    private int bonusCount;
    public List<GameObject> bonusList = new List<GameObject>();
    public float delay;
    private Boolean canGenerate= true;
    private int waypointToPlace;
    public GameObject bonusObject;


    // Use this for initialization
    void Start () {
        delay = 7.0f;

        //print("list of waypoints");
        //print(GetComponent<WaypointsList>().listOfWaypoints.Count);
	}
	
	// Update is called once per frame
	void Update () {

        if (canGenerate)
        {
            StartCoroutine("generate");
        }

    }

    IEnumerator generate()
    {
        canGenerate = false;
        yield return new WaitForSeconds(delay);
        //Destroy(gameObject);
        waypointToPlace = UnityEngine.Random.Range(0, GetComponent<WaypointsList>().listOfWaypoints.Count-1);
        bonusObject = Instantiate(Resources.Load("Bonus", typeof(GameObject))) as GameObject;
        bonusObject.transform.position = new Vector3(GetComponent<WaypointsList>().listOfWaypoints[waypointToPlace].transform.position.x, GetComponent<WaypointsList>().listOfWaypoints[waypointToPlace].transform.position.y, GetComponent<WaypointsList>().listOfWaypoints[waypointToPlace].transform.position.z);
        bonusList.Add(bonusObject);
        //bonusObject.GetComponent<GetBonus>().type = UnityEngine.Random.Range(0, 5);
        //bonusObject.GetComponent<GetBonus>().switchSkin();
        canGenerate = true;
    }


}
