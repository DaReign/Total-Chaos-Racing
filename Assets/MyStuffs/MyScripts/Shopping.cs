using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopping : MonoBehaviour {

    private int CarPrice1 = 1000;
    private int CarPrice2 = 10000;
    private int CarPrice3 = 50000;
    private int CarPrice4 = 120000;
    private int CarPrice5 = 250000;
    private int CarPrice6 = 500000;
    private int CarPrice7 = 1000000;
    private int CarPrice8 = 2000000;
    private int CarPrice9 = 5000000;
    private int CarPrice10 = 10000000;

/*
    private int CarPrice1 = 10000;
    private int CarPrice2 = 10000;
    private int CarPrice3 = 10000;
    private int CarPrice4 = 10000;
    private int CarPrice5 = 10000;
    private int CarPrice6 = 10000;
    private int CarPrice7 = 10000;
    private int CarPrice8 = 10000;
    private int CarPrice9 = 10000;
    private int CarPrice10 = 10000;
*/

    public int GetCarBasePrice (int level)
    {
        int basePrice = 0;
        switch (level)
        {
            case 1: basePrice = CarPrice1;break;
            case 2: basePrice = CarPrice2; break;
            case 3: basePrice = CarPrice3; break;
            case 4: basePrice = CarPrice4; break;
            case 5: basePrice = CarPrice5; break;
            case 6: basePrice = CarPrice6; break;
            case 7: basePrice = CarPrice7; break;
            case 8: basePrice = CarPrice8; break;
            case 9: basePrice = CarPrice9; break;
            case 10: basePrice = CarPrice10; break;
            default: basePrice = 0; break;
        }
        return basePrice;
    }


    public void BuyNewCarTransaction (int RacerIdVar, int CurrentCarLevelVar, int NextCarLevelVar)
    {
        int AvailableMoneyVar = int.Parse(PlayerPrefs.GetString("MoneyP" + RacerIdVar));
        AvailableMoneyVar -= NewCarPrice(RacerIdVar, CurrentCarLevelVar,NextCarLevelVar);
        PlayerPrefs.SetString("MoneyP" + RacerIdVar, "" + AvailableMoneyVar);
        PlayerPrefs.SetString("CarP" + RacerIdVar, "" + NextCarLevelVar);
        PlayerPrefs.SetString("EngineP" + RacerIdVar, "1");
        PlayerPrefs.SetString("TiresP" + RacerIdVar, "1");
        PlayerPrefs.SetString("ArmorP" + RacerIdVar, "1");
        PlayerPrefs.SetString("GunP" + RacerIdVar, "1");
        PlayerPrefs.SetString("NitroNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("MinesNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("MissilesNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("HomingMissilesNextRaceP" + RacerIdVar, "0");
        Debug.Log("Player " + RacerIdVar + "had car " + CurrentCarLevelVar + "and now have " + NextCarLevelVar);
    }

    public bool CanBuyCar (int RacerIdVar,int CurrentCarLevelVar,int NextCarLevelVar)
    {
        //Debug.Log("RacerId " + RacerIdVar + " CurrenCarLevel " + CurrentCarLevelVar + " NextCarLevel " + NextCarLevelVar);

        bool CanBuyCarVar = false;
        //int OldCarBasePriceVar = GetCarBasePrice(CurrentCarLevelVar);
        //int NewCarBasePriceVar = GetCarBasePrice(NextCarLevelVar);
        int AvailableMoneyVar = int.Parse(PlayerPrefs.GetString("MoneyP" + RacerIdVar));

        if (AvailableMoneyVar> NewCarPrice(RacerIdVar, CurrentCarLevelVar,NextCarLevelVar))
        {
            CanBuyCarVar = true;
        }
        return CanBuyCarVar;
    }

    public int NewCarPrice (int RacerIdVar, int CurrentCarLevelVar, int NextCarLevelVar)
    {
        int NewCarPriceVar = GetCarBasePrice(NextCarLevelVar)- MoneyForOldCar(RacerIdVar,CurrentCarLevelVar);
        if (NewCarPriceVar<0) { NewCarPriceVar = 0; }

        return NewCarPriceVar;
    }

    public int MoneyForOldCar (int RacerIdVar, int CurrentCarLevelVar)
    {
        int OldCarBasePriceVar = GetCarBasePrice(CurrentCarLevelVar);
        int OldEngineLevelVar = int.Parse(PlayerPrefs.GetString("EngineP" + RacerIdVar));
        int OldArmorLevelVar = int.Parse(PlayerPrefs.GetString("ArmorP" + RacerIdVar));
        int OldTiresLevelVar = int.Parse(PlayerPrefs.GetString("TiresP" + RacerIdVar));
        int OldGunLevelVar = int.Parse(PlayerPrefs.GetString("GunP" + RacerIdVar));
        int MoneyFromOldCarVar = 0;
        int ScrapMoneyVar = 0;
        for (var i = 1; i <= OldEngineLevelVar; i++)
        {
            ScrapMoneyVar += CalculateUpgradePrice(CurrentCarLevelVar, i,"Engine");
        }
        ScrapMoneyVar = ScrapMoneyVar / 3;
        MoneyFromOldCarVar += ScrapMoneyVar;
        ScrapMoneyVar = 0;
        for (var i = 1; i <= OldArmorLevelVar; i++)
        {
            ScrapMoneyVar += CalculateUpgradePrice(CurrentCarLevelVar, i,"Armor");
        }
        ScrapMoneyVar = ScrapMoneyVar / 3;
        MoneyFromOldCarVar += ScrapMoneyVar;
        ScrapMoneyVar = 0;
        for (var i = 1; i <= OldTiresLevelVar; i++)
        {
            ScrapMoneyVar += CalculateUpgradePrice(CurrentCarLevelVar, i,"Tires");
        }
        ScrapMoneyVar = ScrapMoneyVar / 3;
        MoneyFromOldCarVar += ScrapMoneyVar;
        ScrapMoneyVar = 0;
        for (var i = 1; i <= OldGunLevelVar; i++)
        {
            ScrapMoneyVar += CalculateUpgradePrice(CurrentCarLevelVar, i,"Gun");
        }
        ScrapMoneyVar = ScrapMoneyVar / 3;
        MoneyFromOldCarVar += ScrapMoneyVar;
        ScrapMoneyVar = OldCarBasePriceVar/3;
        MoneyFromOldCarVar += ScrapMoneyVar;

        //Debug.Log("Money form old car "+ MoneyFromOldCarVar);
        return MoneyFromOldCarVar;
    }


    public int CalculateUpgradePrice (int CarLevelVar, int UpgradeLevelVar,string WhatToUpgradeVar)
    {
        int CarBasePriceVar = GetCarBasePrice(CarLevelVar);
        int UpgradePrice = (CarBasePriceVar/10) * UpgradeLevelVar;
        return UpgradePrice;
    }

    public void UpgradeTransaction(int RacerIdVar,int CarLevelVar, int CurrentItemLevelVar, string WhatToUpgradeVar)
    {
        int NewLevelVar = CurrentItemLevelVar + 1;
        int AvailableMoneyVar = int.Parse(PlayerPrefs.GetString("MoneyP" + RacerIdVar));
        AvailableMoneyVar -= CalculateUpgradePrice(CarLevelVar, NewLevelVar, WhatToUpgradeVar);
        switch (WhatToUpgradeVar) {
            case "Engine": PlayerPrefs.SetString("EngineP" + RacerIdVar, ""+ NewLevelVar); break;
            case "Tires": PlayerPrefs.SetString("TiresP" + RacerIdVar, "" + NewLevelVar); break;
            case "Armor": PlayerPrefs.SetString("ArmorP" + RacerIdVar, "" + NewLevelVar); break;
            case "Gun": PlayerPrefs.SetString("GunP" + RacerIdVar, "" + NewLevelVar); break;
            case "NitroNextRace": PlayerPrefs.SetString("NitroNextRaceP" + RacerIdVar, "" + NewLevelVar); break;
            case "MinesNextRace": PlayerPrefs.SetString("MinesNextRaceP" + RacerIdVar, "" + NewLevelVar); break;
            case "MissilesNextRace": PlayerPrefs.SetString("MissilesNextRaceP" + RacerIdVar, "" + NewLevelVar); break;
            case "HomingMissilesNextRace": PlayerPrefs.SetString("HomingMissilesNextRaceP" + RacerIdVar, "" + NewLevelVar); break;
        }
        PlayerPrefs.SetString("MoneyP" + RacerIdVar, "" + AvailableMoneyVar);
    }




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
