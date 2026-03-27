using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class CalculatePlayerRaceResults : MonoBehaviour {
    public List<GameObject> listOfCars = new List<GameObject>();
    public List<GameObject> listOfWinners = new List<GameObject>();
    private GameObject PlayerCar;

    // Use this for initialization
    void Start () {
		
	}
	

    public void terminatedRaceCalculate ()
    {
        PlayerCar = GameObject.FindGameObjectWithTag("Player");
        PlayerCar.GetComponent<CarStats>().wrecked = true;
        GameObject.Find("Global").GetComponent<Stage>().listOfRaceEnd.Add(PlayerCar);
        GameObject.Find("Settings").SetActive(false);
        Time.timeScale = 1;
        calculate();
    }

	// Update is called once per frame
	public void calculate ()
    {
        listOfCars = GetComponent<Stage>().listOfCarsSorted;
        //listOfCars = GetComponent<Stage>().listOfCarsSorted;

        for (var i=0;i<listOfCars.Count;i++)
        {
            Debug.Log("racer id "+ listOfCars[i].GetComponent<CarStats>().racerId);
            if (!listOfCars[i].GetComponent<CarStats>().raceFinished&& !listOfCars[i].GetComponent<CarStats>().wrecked)
            {
                listOfCars[i].GetComponent<CarStats>().raceFinished = true;
                if (listOfCars[i].GetComponent<CarAIControl>().enabled==true)
                {
                    listOfCars[i].GetComponent<CarAIControl>().enabled = false;
                }
                /*
                if (listOfCars[i].GetComponent<CarUserControl>().enabled == true)
                {
                    listOfCars[i].GetComponent<CarUserControl>().enabled = false;
                }
                */


                GetComponent<Stage>().listOfWinners.Add(listOfCars[i]);
                GetComponent<Stage>().listOfRaceEnd.Add(listOfCars[i]);
            }
            /*
            if ()
            {
                GetComponent<Stage>().listOfRaceEnd.Add(listOfCars[i]);
            }
            */

        }


        //listOfCarsSorted[i].GetComponent<CarStats>().raceFinished == false
    }
}
