using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceBonuses : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public int MaxPrize (int RaceLevelVar)
    {
        int MaxPrizeVar;
        switch (RaceLevelVar)
        {
            case 1: MaxPrizeVar=1000; break;
            case 2: MaxPrizeVar = 2000; break;
            case 3: MaxPrizeVar = 4000; break;
            case 4: MaxPrizeVar = 6000; break;
            case 5: MaxPrizeVar = 9000; break;
            case 6: MaxPrizeVar = 12000; break;
            default: MaxPrizeVar = 1000; break;
        }
        return MaxPrizeVar;
    }

    public int MaxPoints(int RaceLevelVar)
    {
        int MaxPointsVar;
        switch (RaceLevelVar)
        {
            case 1: MaxPointsVar = 6; break;
            case 2: MaxPointsVar = 8; break;
            case 3: MaxPointsVar = 10; break;
            case 4: MaxPointsVar = 12; break;
            case 5: MaxPointsVar = 16; break;
            case 6: MaxPointsVar = 20; break;
            default: MaxPointsVar = 6; break;
        }
        return MaxPointsVar;
    }

    public void RaceBasicPrizes (int RacerIdVar,int RacePosition, int RaceLevelVar)
    {
        int RacerMoneyVar = int.Parse(PlayerPrefs.GetString("MoneyP" + RacerIdVar));
        int RacerPointsVar = int.Parse(PlayerPrefs.GetString("PointsP" + RacerIdVar));
        int TempMoneyVar=0;
        int TempPointsVar=0;
/*
        if (RacePosition==0)
        {
            RacerMoneyVar += MaxPrize(RaceLevelVar);
            TempMoneyVar = MaxPrize(RaceLevelVar);
            RacerPointsVar += MaxPoints(RaceLevelVar);
            TempPointsVar = MaxPoints(RaceLevelVar);
        }
        else
        {
            TempMoneyVar = MaxPrize(RaceLevelVar) - (MaxPrize(RaceLevelVar) / 5) - ( 100 * RacePosition*RaceLevelVar);
            if (TempMoneyVar < 100 * RaceLevelVar) { TempMoneyVar = 100* RaceLevelVar; }
            RacerMoneyVar += TempMoneyVar;

            TempPointsVar = MaxPoints(RaceLevelVar) - (MaxPoints(RaceLevelVar) / 10) - RacePosition;
            if (TempPointsVar < 0) { TempPointsVar = 0; }
            RacerPointsVar += TempPointsVar;
        }
*/
        TempPointsVar = CalculatePointsBonus(RacePosition, RaceLevelVar);
        RacerPointsVar += TempPointsVar;
        TempMoneyVar = CalculateMoneyBonus(RacePosition, RaceLevelVar);
        RacerMoneyVar += TempMoneyVar;
        PlayerPrefs.SetString("MoneyP" + RacerIdVar, "" + RacerMoneyVar);
        PlayerPrefs.SetString("PointsP" + RacerIdVar, "" + RacerPointsVar);

        //reset special race ammo
        PlayerPrefs.SetString("AirStrikeNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("DefendStrikeNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("NitroNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("MinesNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("MissilesNextRaceP" + RacerIdVar, "0");
        PlayerPrefs.SetString("HomingMissilesNextRaceP" + RacerIdVar, "0");

        Debug.Log("Racer " + RacerIdVar + " get " + TempPointsVar + " points and " + TempMoneyVar + " money in race level " + RaceLevelVar+" for position "+ (RacePosition+1));
    }

    public int CalculateMoneyBonus(int RacePosition, int RaceLevelVar)
    {
        int BonusVar = 0;
        if (RacePosition == 0)
        {
            BonusVar += MaxPrize(RaceLevelVar);
        }
        else
        {
            BonusVar = MaxPrize(RaceLevelVar) - (MaxPrize(RaceLevelVar) / 5) - (100 * RacePosition * RaceLevelVar);
            if (BonusVar < 0) { BonusVar = 100 * RaceLevelVar; }
        }
        return BonusVar;
    }

    public int CalculatePointsBonus (int RacePosition, int RaceLevelVar)
    {
        int BonusVar = 0;
        if (RacePosition == 0)
        {
            BonusVar += MaxPoints(RaceLevelVar);
        }
        else
        {
            BonusVar = MaxPoints(RaceLevelVar) - (MaxPoints(RaceLevelVar) / 10) - RacePosition;
            if (BonusVar < 0) { BonusVar = 0; }
        }
        return BonusVar;
    }

    public int GetPointsBonus (int RacePosition, int RaceLevelVar)
    {

        int BonusVar=0;
        return BonusVar;
    }

}
