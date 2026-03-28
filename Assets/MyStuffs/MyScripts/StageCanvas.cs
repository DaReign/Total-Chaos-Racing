using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void LoadLevel ()
    {
        Debug.Log("Clicked Button");
        RaceEvents.ClearAll();
        Application.LoadLevel("garage");
        GameObject.Find("Global").GetComponent<Stage>().listOfCars.Clear();
        GameObject.Find("Global").GetComponent<Stage>().listOfCarsSorted.Clear();
        GameObject.Find("Global").GetComponent<Stage>().listOfWinners.Clear();
        GameObject.Find("Global").GetComponent<Stage>().listOfRaceEnd.Clear();
    }
}
