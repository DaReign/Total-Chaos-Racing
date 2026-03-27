using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceDrivers : MonoBehaviour {
    public int[] RacersIds;
    private int id;
    private Boolean drawAgain;
    // Use this for initialization
    void Start () {

        RacersIds = new int[10];
        /*
        RacersIds[0] = 0;
        RacersIds[1] = 0;
        RacersIds[2] = 0;
        RacersIds[3] = 0;
        RacersIds[4] = 0;
        RacersIds[5] = 0;
        RacersIds[6] = 0;
        RacersIds[7] = 0;
        RacersIds[8] = 0;
        RacersIds[9] = 0;
        */

        /*
        for (var i=0;i<10;i++)
        {
            drawAgain = true;
            while (drawAgain)
            {
                drawAgain = false;
                id = UnityEngine.Random.Range(1, 100);
                for (var a = 0; a < RacersIds.Length; a++)
                {                    
                    if (RacersIds[a] == id)
                    {
                        drawAgain = true;
                    }
                }
            }

            //print("i");
            //print(i);
            //print("id");
            //print(id);
            RacersIds[i] = id;

        }

        RacersIds[UnityEngine.Random.Range(1, 9)] = 0;
        */

        
        for (var racer = 0; racer < 10; racer++)
        {
        //    Debug.Log("Racer " + racer + " id is " + PlayerPrefs.GetString("AI" + racer));
            RacersIds[racer] = int.Parse(PlayerPrefs.GetString("AI" + racer));
        }
        

        GetComponent<SetUpRace>().enabled = true;

		
	}
}
