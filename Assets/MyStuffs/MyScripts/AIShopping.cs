using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShopping : MonoBehaviour {
    private int money;
    private int lvl;
    private int nextLvl;
    private int dice;
    private int nextRace;
    //private Component Shopping;
	// Use this for initialization
	void Start () {

        //Shopping = GetComponent<Shopping>();
        for (var i = 1; i < 100; i++)
        {
            lvl = int.Parse(PlayerPrefs.GetString("CarP" + i));
            //buy new car
            if (lvl < 10)
            {
                if (GetComponent<Shopping>().CanBuyCar(i, lvl, lvl + 1))
                {
                    dice = Random.Range(0, 3);

                    if (dice == 1)
                    {
                        GetComponent<Shopping>().BuyNewCarTransaction(i, lvl, lvl + 1);
                        /*
                        nextLvl = lvl + 1;
                        PlayerPrefs.SetString("CarP" + i, "" + nextLvl);
                        money = money - (lvl * 10000 + lvl * 1125);
                        PlayerPrefs.SetString("MoneyP" + i, "" + money);
                        */
                        Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought car lvl " + PlayerPrefs.GetString("CarP" + i));
                    }
                }
            }

            carUpgrade(i, "HomingMissilesNextRace", 3);
            carUpgrade(i, "MissilesNextRace", 3);
            carUpgrade(i, "MinesNextRace", 3);
            carUpgrade(i, "Engine", 5);
            carUpgrade(i, "Armor", 8);
            carUpgrade(i, "Tires", 8);
            carUpgrade(i, "Gun", 8);


            /*
            //buy nitro
            nextRace = int.Parse(PlayerPrefs.GetString("NitroNextRaceP" + i));
            lvl = int.Parse(PlayerPrefs.GetString("CarP" + i));
            if (money > lvl * 300 && nextRace == 0)
            {
                dice = Random.Range(0, 3);

                if (dice == 1)
                {
                    nextLvl = lvl*20;
                    PlayerPrefs.SetString("NitroNextRaceP" + i, "" + nextLvl);
                    money = money - (lvl * 300);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                   // Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought nitro");
                }
            }


            money = int.Parse(PlayerPrefs.GetString("MoneyP" + i));
            nextRace = int.Parse(PlayerPrefs.GetString("HomingMissilesNextRaceP" + i));
            lvl = int.Parse(PlayerPrefs.GetString("CarP" + i));
            //buy mines
            if (money > GetComponent<Shopping>().CalculateUpgradePrice(lvl, nextRace+1,"HomingMissilesNextRace"))
            {
                dice = Random.Range(0, 3);

                if (dice == 1)
                {
                    nextRace++;
                    PlayerPrefs.SetString("HomingMissilesNextRaceP" + i, "" + nextRace);
                    money = money - (lvl * 1000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                   Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought hm");
                }
            }

            nextRace = int.Parse(PlayerPrefs.GetString("MissilesNextRaceP" + i));
            lvl = int.Parse(PlayerPrefs.GetString("CarP" + i));
            //buy mines
            if (money > lvl * 1000 && nextRace < 10)
            {
                dice = Random.Range(0, 3);

                if (dice == 1)
                {
                    nextRace++;
                    PlayerPrefs.SetString("MissilesNextRaceP" + i, "" + nextRace);
                    money = money - (lvl * 1000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                     Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought missile");
                }
            }


            nextRace = int.Parse(PlayerPrefs.GetString("MinesNextRaceP" + i));
            lvl = int.Parse(PlayerPrefs.GetString("CarP" + i));
            //buy mines
            if (money > lvl * 700 && nextRace < 10)
            {
                dice = Random.Range(0, 3);

                if (dice == 1)
                {
                    nextRace++;
                    PlayerPrefs.SetString("MinesNextRaceP" + i, "" + nextRace);
                    money = money - (lvl * 700);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                    Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought mines");
                }
            }
            


            lvl = int.Parse(PlayerPrefs.GetString("EngineP" + i));
            //buy engine
            if (money > lvl * 5000&&lvl<5)
            {
                dice = Random.Range(0, 7);

                if (dice == 1)
                {
                    nextLvl = lvl + 1;
                    PlayerPrefs.SetString("EngineP" + i, "" + nextLvl);
                    money = money - (lvl * 5000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                   // Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought engine lvl " + nextLvl);
                }
            }

            lvl = int.Parse(PlayerPrefs.GetString("ArmorP" + i));
            //buy armor
            if (money > lvl * 4000 && lvl < 5)
            {
                dice = Random.Range(0, 8);

                if (dice == 1)
                {
                    nextLvl = lvl + 1;
                    PlayerPrefs.SetString("ArmorP" + i, "" + nextLvl);
                    money = money - (lvl * 4000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                   // Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought armor lvl " + nextLvl);
                }
            }

            lvl = int.Parse(PlayerPrefs.GetString("TireP" + i));
            //buy tire
            if (money > lvl * 3000 && lvl < 5)
            {
                dice = Random.Range(0, 8);

                if (dice == 1)
                {
                    nextLvl = lvl + 1;
                    PlayerPrefs.SetString("TireP" + i, "" + nextLvl);
                    money = money - (lvl * 3000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                    //Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought tire lvl " + nextLvl);
                }
            }

            lvl = int.Parse(PlayerPrefs.GetString("GunP" + i));
            //buy gun
            if (money > lvl * 5000 && lvl < 5)
            {
                dice = Random.Range(0, 5);

                if (dice == 1)
                {
                    nextLvl = lvl + 1;
                    PlayerPrefs.SetString("GunP" + i, "" + nextLvl);
                    money = money - (lvl * 5000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                   // Debug.Log("Player " + PlayerPrefs.GetString("NameP" + i) + "bought gun lvl " + nextLvl);
                }
            }
            */

        }


    }
	
    void carUpgrade(int RacerIdVar, string WhatToUpgradeVar,int maxDiceVar)
    {
        int CarLevelVar = int.Parse(PlayerPrefs.GetString("CarP" + RacerIdVar));
        int AvailableMoneyVar = int.Parse(PlayerPrefs.GetString("MoneyP" + RacerIdVar));
        int CurrentItemLevelVar = int.Parse(PlayerPrefs.GetString(WhatToUpgradeVar+"P"+ RacerIdVar));

        if (CurrentItemLevelVar < 10)
        {
            if (AvailableMoneyVar > GetComponent<Shopping>().CalculateUpgradePrice(CarLevelVar, CurrentItemLevelVar + 1, WhatToUpgradeVar))
            {
                dice = Random.Range(0, maxDiceVar);

                if (dice == 1)
                {
                    GetComponent<Shopping>().UpgradeTransaction(RacerIdVar, CarLevelVar, CurrentItemLevelVar, WhatToUpgradeVar);
                    /*
                    nextRace++;
                    PlayerPrefs.SetString("HomingMissilesNextRaceP" + i, "" + nextRace);
                    money = money - (lvl * 1000);
                    PlayerPrefs.SetString("MoneyP" + i, "" + money);
                    */
                    Debug.Log("Player " + PlayerPrefs.GetString("NameP" + RacerIdVar) + " bought "+WhatToUpgradeVar+ " and now have " + PlayerPrefs.GetString(WhatToUpgradeVar+"P" + RacerIdVar));
                }
            }
        }
    }
}
