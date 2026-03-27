using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersData : MonoBehaviour {
    private string fieldName;
    private string fieldValue;
    private int number;

    // Use this for initialization
    void Start () {



        if (PlayerPrefs.GetString("GameData")=="Yes")
        {
           // print("Data existed before");
        }
        else
        {
            print("Data existed creating data");
            for (var i = 0; i < 100; i++)
            {

                fieldName = "NameP" + i;
                fieldValue = "Player "+i;
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "MoneyP" + i;
                fieldValue = "1000";
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "CarP" + i;
                fieldValue = "1";
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "CarLevelP" + i;
                fieldValue = "1";
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "ColorP" + i;
                fieldValue = "Blue";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "EngineP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "TireP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "ArmorP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "GunP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "NitroNextRaceP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "MinesNextRaceP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "PointsP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

                fieldName = "LeagueP" + i;
                fieldValue = "1";
                //tu bedzie funkcja losujaca kolory
                PlayerPrefs.SetString(fieldName, fieldValue);

            }

        }
        PlayerPrefs.SetString("GameData", "Yes");


    }
}
