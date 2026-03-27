using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceChoose : MonoBehaviour {
    
    //lista dostepnych wyscigow
    public string[,] StagesData = new string[5, 8];
    public int[,] PlayerPower = new int[10, 2];
    //public string[,] RaceData = new string[10, 4];
    //public string[] Races = new string[10];


    public List<int> PlayerIdsList = new List<int>();


    //kto jest zapisany do konkretnego wyscigu
    public List<int>[] RaceData = new List<int>[6];
    //wylosowane wyscigi
    public List<int> Races = new List<int>();
    public List<int> RacesLvl = new List<int>();
    //public int[] RacesLvl;
    private int RacesNumber = 6;


    public List<int> tempArray = new List<int>();

    private int DrawRaceId;
    private int DrawPlayerId;
    private bool RaceIdExist;

    public float delay;
    private bool signupNext = false;
    private bool brakeAiSignUp = false;
    private int[] PlayerIdPlaceByPoints;

    private int drawLvl;
    private bool drawLvlAgain;

    private int drawChancesVarBonus=0;
    public int drawChancesVarBonusLevel6 = 0;
    public int drawChancesVarBonusLevel5 = 0;
    public int drawChancesVarBonusLevel4 = 0;
    public int drawChancesVarBonusLevel3 = 0;
    public int drawChancesVarBonusLevel2 = 0;
    public int drawChancesVarBonusLevel1 = 0;
    private int chancesToContinueSignUp;
    public bool playerSignedUp = false;
    public int playerSignedUpWaitForOthres = 0;

    private bool allSigned = false;
    private bool simulated = false;

    private int temp;
    private int tempId;
    private int tempBonus;


    private int PlayerRace;

    public GameObject RaceChoosing;
    public GameObject RacePlayerListText0;
    public GameObject RacePlayerListText1;
    public GameObject RacePlayerListText2;
    public GameObject RacePlayerListText3;
    public GameObject RacePlayerListText4;
    public GameObject RacePlayerListText5;

    public GameObject RaceSquare1;
    public GameObject RaceSquare2;
    public GameObject RaceSquare3;
    public GameObject RaceSquare4;
    public GameObject RaceSquare5;
    public GameObject RaceSquare6;

    public GameObject RaceNameText1;
    public GameObject RaceNameText2;
    public GameObject RaceNameText3;
    public GameObject RaceNameText4;
    public GameObject RaceNameText5;
    public GameObject RaceNameText6;

    public GameObject RacePriceText1;
    public GameObject RacePriceText2;
    public GameObject RacePriceText3;
    public GameObject RacePriceText4;
    public GameObject RacePriceText5;
    public GameObject RacePriceText6;

    public GameObject RaceLapsText1;
    public GameObject RaceLapsText2;
    public GameObject RaceLapsText3;
    public GameObject RaceLapsText4;
    public GameObject RaceLapsText5;
    public GameObject RaceLapsText6;

    public GameObject RaceConditionText1;
    public GameObject RaceConditionText2;
    public GameObject RaceConditionText3;
    public GameObject RaceConditionText4;
    public GameObject RaceConditionText5;
    public GameObject RaceConditionText6;

    public GameObject BeginRacesButton;

    private bool startAsyncLoading = false;

    public GameObject Loading;

    private int lapsMax=5;
    private int lapsMin = 3;
    private int drawLaps;


    void Start()
    {
      //  lapsMax = 5;

        drawChancesVarBonus = 0;
        BeginRacesButton.SetActive(false);
        //print("set up");
        //setUp();
        //print("Check if found !!!!!!!!!!!!!!!!!!");
        //print(RaceChoosing.transform.Find("RaceSquare1/RacePlayerListText"));
        RacePlayerListText0.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText1.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText2.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText3.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText4.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText5.GetComponent<UnityEngine.UI.Text>().text = "";


    }

    void Awake () {
       // setUp();
    }

    public void signUpToRace (string stringNumber) {
        int number = 0;
        //print("Clicked");
        //print(stringNumber);
        number = int.Parse(stringNumber);
        //print("Parsed");
        //print(number);

        if (RaceData[number].Count < 10)
        {

            GameObject.Find("RaceSquare1").GetComponent<Button>().interactable = false;
            GameObject.Find("RaceSquare2").GetComponent<Button>().interactable = false;
            GameObject.Find("RaceSquare3").GetComponent<Button>().interactable = false;
            GameObject.Find("RaceSquare4").GetComponent<Button>().interactable = false;
            GameObject.Find("RaceSquare5").GetComponent<Button>().interactable = false;
            GameObject.Find("RaceSquare6").GetComponent<Button>().interactable = false;


            RaceData[number].Add(0);
            PlayerRace = number;


            switch (number)
            {
                case 0: RacePlayerListText0.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                case 1: RacePlayerListText1.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                case 2: RacePlayerListText2.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                case 3: RacePlayerListText3.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                case 4: RacePlayerListText4.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                case 5: RacePlayerListText5.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP0") + "\n"; break;
                default: break;

            }


            PlayerIdsList.RemoveAt(0);
            brakeAiSignUp = true;

            playerSignedUp = true;
            //drawChancesVarBonus = 2;
            if (RacesLvl[number]==6){drawChancesVarBonusLevel6 = 10;}
            if (RacesLvl[number] == 5) { drawChancesVarBonusLevel5 = 1; }
            if (RacesLvl[number] == 4) { drawChancesVarBonusLevel4 = 1; }
            if (RacesLvl[number] == 3) { drawChancesVarBonusLevel3 = 1; }
            if (RacesLvl[number] == 2) { drawChancesVarBonusLevel2 = 1; }
            if (RacesLvl[number] == 1) { drawChancesVarBonusLevel1 = 1; }

            delay = 0.0f;



            PlayerPrefs.SetString("RaceBounty", "" + RacesLvl[number]);
            PlayerPrefs.SetString("RaceLvl", "" + RacesLvl[number]);
            PlayerPrefs.SetString("RaceWeather", ""+ StagesData[Races[number], 4]);
            PlayerPrefs.SetString("RaceDayState", "" + StagesData[Races[number], 3]);
            Debug.Log("Race laps is" +StagesData[Races[number], 2]);
            PlayerPrefs.SetString("RaceLaps", "" + StagesData[Races[number], 2]);
            PlayerPrefs.SetString("RaceVariant", "" + StagesData[Races[number], 7]);

            Debug.Log("Signedup to " + Races[number]);


            Debug.Log("Bounty " + RacesLvl[number]);
            Debug.Log("Pogoda " + StagesData[Races[number], 4]);
            Debug.Log("Dzien " + StagesData[Races[number], 3]);

        }

    }

    public string GetRaceImage(string RaceNameVar,string RaceVariantVar)
    {
        string ImageVar="";
        print(RaceNameVar);

        switch (RaceNameVar)
        {
            case "AgroMountain": ImageVar = "AgroMountainVariant" + RaceVariantVar; break;
            case "CaliforniaRoad": ImageVar = "CaliforniaRoadVariant" + RaceVariantVar; break;
            case "ForestStage": ImageVar = "ForestStageVariant" + RaceVariantVar; break;
            case "MountainRoad": ImageVar = "MountainRoadVariant" + RaceVariantVar; break;
            case "StageCountry1": ImageVar = "StageCountry1Variant" + RaceVariantVar; break;
            case "Stage1": ImageVar = "StageCountry1Variant" + RaceVariantVar; break;
            case "Stage2": ImageVar = "StageCountry1Variant" + RaceVariantVar; break;
            default: ImageVar = "StageCountry1Variant" + RaceVariantVar; break;
        }


        return ImageVar;
    }

    public string RaceLevelToStars (int RaceLevelVar)
    {
        string StarsVar = "";

        switch (RaceLevelVar)
        {
            case 1: StarsVar = "Stars1"; break;
            case 2: StarsVar = "Stars2"; break;
            case 3: StarsVar = "Stars3"; break;
            case 4: StarsVar = "Stars4"; break;
            case 5: StarsVar = "Stars5"; break;
            case 6: StarsVar = "Stars6"; break;
            default: StarsVar = "Stars1"; break;
        }

        return StarsVar;
    }


    public void setUp ()
    {
        
        BeginRacesButton.SetActive(false);
        //setUp();
        //print("Check if found !!!!!!!!!!!!!!!!!!");
        //print(RaceChoosing.transform.Find("RaceSquare1/RacePlayerListText"));
        RacePlayerListText0.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText1.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText2.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText3.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText4.GetComponent<UnityEngine.UI.Text>().text = "";
        RacePlayerListText5.GetComponent<UnityEngine.UI.Text>().text = "";
        RaceSquare1.GetComponent<Button>().interactable = true;
        RaceSquare2.GetComponent<Button>().interactable = true;
        RaceSquare3.GetComponent<Button>().interactable = true;
        RaceSquare4.GetComponent<Button>().interactable = true;
        RaceSquare5.GetComponent<Button>().interactable = true;
        RaceSquare6.GetComponent<Button>().interactable = true;





        for (var set=0;set<100;set++)
        {
            PlayerPrefs.SetString("GetPointsP" + set, "0");
            PlayerPrefs.SetString("GetMoneyP" + set, "0");
        }


        PlayerRace = 100;
        delay = 5.5f;

        PlayerIdPlaceByPoints = new int[100];

        PlayerIdPlaceByPoints = GetComponent<PlayerList>().PlayerIdPlaceByPoints;

        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //print("Player id by pointa");
        /*
        for (var points=0;points<100;points++)
        {
            Debug.Log("Player id"+ PlayerIdPlaceByPoints[points]);
        }
        */
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        //print("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");



        /*
        StagesData[0, 0] = "StageCountry1"; //unity name
        StagesData[0, 1] = "Country race"; //game name
        StagesData[0, 2] = ""+ Random.Range(1, lapsMax); //laps
        StagesData[0, 3] = ""+Random.Range(0, 2); //day state
        StagesData[0, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[0, 5] = "image"; // race image
        StagesData[0, 6] = "" + Random.Range(0, 6); // race level
        StagesData[0, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[1, 0] = "MountainRoad"; //unity name
        StagesData[1, 1] = "Mountain track"; //game name
        StagesData[1, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[1, 3] = "" + Random.Range(0, 2); //day state
        StagesData[1, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[1, 5] = "image"; // race image
        StagesData[1, 6] = "" + Random.Range(0, 6); // race level
        StagesData[1, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[2, 0] = "ForestStage"; //unity name
        StagesData[2, 1] = "Forest road"; //game name
        StagesData[2, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[2, 3] = "" + Random.Range(0, 2); //day state
        StagesData[2, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[2, 5] = "image"; // race image
        StagesData[2, 6] = "" + Random.Range(0, 6); // race level
        StagesData[2, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[3, 0] = "CaliforniaRoad"; //unity name
        StagesData[3, 1] = "Hills and Mountains"; //game name
        StagesData[3, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[3, 3] = "" + Random.Range(0, 2); //day state
        StagesData[3, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[3, 5] = "image"; // race image
        StagesData[3, 6] = "" + Random.Range(0, 6); // race level
        StagesData[3, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[4, 0] = "AgroMountain"; //unity name
        StagesData[4, 1] = "Country trip"; //game name
        StagesData[4, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[4, 3] = "" + Random.Range(0, 2); //day state
        StagesData[4, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[4, 5] = "image"; // race image
        StagesData[4, 6] = "" + Random.Range(0, 6); // race level
        StagesData[4, 7] = "" + Random.Range(0, 2); // race variant
*/

        StagesData[0, 0] = "Stage2"; //unity name
        StagesData[0, 1] = "Stage2"; //game name
        StagesData[0, 2] = "" + Random.Range(lapsMin, lapsMax); //laps
        StagesData[0, 3] = "" + Random.Range(0, 3); //day state
        StagesData[0, 4] = "" + Random.Range(0, 5); // wheather
        StagesData[0, 5] = "image"; // race image
        StagesData[0, 6] = "" + Random.Range(0, 6); // race level
        StagesData[0, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[1, 0] = "Stage2"; //unity name
        StagesData[1, 1] = "Stage2"; //game name
        StagesData[1, 2] = "" + Random.Range(lapsMin, lapsMax); //laps
        StagesData[1, 3] = "" + Random.Range(0, 3); //day state
        StagesData[1, 4] = "" + Random.Range(0, 5); // wheather
        StagesData[1, 5] = "image"; // race image
        StagesData[1, 6] = "" + Random.Range(0, 6); // race level
        StagesData[1, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[2, 0] = "Stage2"; //unity name
        StagesData[2, 1] = "Stage2"; //game name
        StagesData[2, 2] = "" + Random.Range(lapsMin, lapsMax); //laps
        StagesData[2, 3] = "" + Random.Range(0, 3); //day state
        StagesData[2, 4] = "" + Random.Range(0, 5); // wheather
        StagesData[2, 5] = "image"; // race image
        StagesData[2, 6] = "" + Random.Range(0, 6); // race level
        StagesData[2, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[3, 0] = "Stage1"; //unity name
        StagesData[3, 1] = "Stage1"; //game name
        StagesData[3, 2] = "" + Random.Range(lapsMin, lapsMax); //laps
        StagesData[3, 3] = "" + Random.Range(0, 3); //day state
        StagesData[3, 4] = "" + Random.Range(0, 5); // wheather
        StagesData[3, 5] = "image"; // race image
        StagesData[3, 6] = "" + Random.Range(0, 6); // race level
        StagesData[3, 7] = "" + Random.Range(0, 2); // race variant
    
        StagesData[4, 0] = "Stage1"; //unity name
        StagesData[4, 1] = "Stage1"; //game name
        StagesData[4, 2] = "" + Random.Range(lapsMin, lapsMax); //laps
        StagesData[4, 3] = "" + Random.Range(0, 3); //day state
        StagesData[4, 4] = "" + Random.Range(0, 5); // wheather
        StagesData[4, 5] = "image"; // race image
        StagesData[4, 6] = "" + Random.Range(0, 6); // race level
        StagesData[4, 7] = "" + Random.Range(0, 2); // race variant


        /*
        StagesData[5, 0] = "StageCountry1"; //unity name
        StagesData[5, 1] = "Test Scene 6"; //game name
        StagesData[5, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[5, 3] = "" + Random.Range(0, 2); //day state
        StagesData[5, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[5, 5] = "image"; // race image
        StagesData[5, 6] = "" + Random.Range(0, 6); // race level
        StagesData[5, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[6, 0] = "StageCountry1"; //unity name
        StagesData[6, 1] = "Test Scene 7"; //game name
        StagesData[6, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[6, 3] = "" + Random.Range(0, 2); //day state
        StagesData[6, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[6, 5] = "image"; // race image
        StagesData[6, 6] = "" + Random.Range(0, 6); // race level
        StagesData[6, 7] = "" + Random.Range(0, 2); // race variant

        StagesData[7, 0] = "StageCountry1"; //unity name
        StagesData[7, 1] = "Test Scene 8"; //game name
        StagesData[7, 2] = "" + Random.Range(1, lapsMax); //laps
        StagesData[7, 3] = "" + Random.Range(0, 2); //day state
        StagesData[7, 4] = "" + Random.Range(0, 6); // wheather
        StagesData[7, 5] = "image"; // race image
        StagesData[7, 6] = "" + Random.Range(0, 6); // race level
        StagesData[7, 7] = "" + Random.Range(0, 2); // race variant
        */

        /*
        for (var a=0;a<6;a++)
        {
            Debug.Log("stage "+a+" laps is"+ StagesData[a, 2]);
        }
        */

        for (var i = 0; i < RacesNumber; i++)
        {
            RaceData[i] = new List<int>();
        }


        //Reset ids list 
        /*
        for (var data = 0; data < PlayerIdsList.Count; data++)
        {
                PlayerIdsList.RemoveAt(data);
        }
        */
        PlayerIdsList.Clear();





        //create new ids list 

        for (var i = 0; i < 100; i++)
            {
                PlayerIdsList.Add(i);
            }

        //print("before races");
        while (Races.Count < RacesNumber)
        {
            //print("Races count");
            //print(Races.Count);

            RaceIdExist = true;
            while (RaceIdExist)
            {
                //print("first level");
                //print(StagesData.GetLength(0));
                //print("all");
                //print(StagesData.Length);

                DrawRaceId = Random.Range(0, StagesData.GetLength(0));

                RaceIdExist = false;
                for (var i = 0; i < Races.Count; i++)
                {
                    if (Races[i] == DrawRaceId)
                    {
                       // RaceIdExist = true;
                    }
                }

                if (!RaceIdExist)
                {
                    Races.Add(DrawRaceId);

                    drawLvlAgain = true;
                    while (drawLvlAgain)
                    {
                        drawLvl = Random.Range(1, 7);
                        drawLvlAgain = false;
                        for (var lvl = 0; lvl < RacesLvl.Count; lvl++)
                        {
                            if (RacesLvl[lvl] == drawLvl) 
                            {
                                drawLvlAgain = true;
                            }
                        }
                    }
                    RacesLvl.Add(drawLvl);
                }
            }
        }



        print( GetRaceImage(StagesData[Races[0], 0], StagesData[Races[0], 7]) );
        print(GetRaceImage(StagesData[Races[1], 0], StagesData[Races[1], 7]));
        print(GetRaceImage(StagesData[Races[2], 0], StagesData[Races[2], 7]));
        print(GetRaceImage(StagesData[Races[3], 0], StagesData[Races[3], 7]));
        print(GetRaceImage(StagesData[Races[4], 0], StagesData[Races[4], 7]));
        print(GetRaceImage(StagesData[Races[5], 0], StagesData[Races[5], 7]));

        RaceSquare1.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/"+ GetRaceImage(StagesData[Races[0], 0], StagesData[Races[0], 7]));
        RaceSquare2.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/" + GetRaceImage(StagesData[Races[1], 0], StagesData[Races[0], 7]));
        RaceSquare3.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/" + GetRaceImage(StagesData[Races[2], 0], StagesData[Races[0], 7]));
        RaceSquare4.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/" + GetRaceImage(StagesData[Races[3], 0], StagesData[Races[0], 7]));
        RaceSquare5.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/" + GetRaceImage(StagesData[Races[4], 0], StagesData[Races[0], 7]));
        RaceSquare6.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/" + GetRaceImage(StagesData[Races[5], 0], StagesData[Races[0], 7]));

        RaceSquare1.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/"+ RaceLevelToStars(RacesLvl[0]));
        RaceSquare2.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/" + RaceLevelToStars(RacesLvl[1]));
        RaceSquare3.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/" + RaceLevelToStars(RacesLvl[2]));
        RaceSquare4.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/" + RaceLevelToStars(RacesLvl[3]));
        RaceSquare5.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/" + RaceLevelToStars(RacesLvl[4]));
        RaceSquare6.transform.Find("Stars").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("StagesImages/Stars/" + RaceLevelToStars(RacesLvl[5]));


        RaceNameText1.GetComponent<UnityEngine.UI.Text>().text = ""+ StagesData[Races[0], 1];
        RaceNameText2.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[1], 1];
        RaceNameText3.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[2], 1];
        RaceNameText4.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[3], 1];
        RaceNameText5.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[4], 1];
        RaceNameText6.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[5], 1];

        RacePriceText1.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[0]));
        RacePriceText2.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[1]));
        RacePriceText3.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[2]));
        RacePriceText4.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[3]));
        RacePriceText5.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[4]));
        RacePriceText6.GetComponent<UnityEngine.UI.Text>().text = "Prize " + (GetComponent<RaceBonuses>().MaxPrize(RacesLvl[5]));

        RaceLapsText1.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[0], 2];
        RaceLapsText2.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[1], 2];
        RaceLapsText3.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[2], 2];
        RaceLapsText4.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[3], 2];
        RaceLapsText5.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[4], 2];
        RaceLapsText6.GetComponent<UnityEngine.UI.Text>().text = "Laps " + StagesData[Races[5], 2];

        RaceConditionText1.GetComponent<UnityEngine.UI.Text>().text = ""+StagesData[Races[0], 4]+ "\n" + StagesData[Races[0], 3];
        RaceConditionText2.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[1], 4] + "\n" + StagesData[Races[1], 3];
        RaceConditionText3.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[2], 4] + "\n" + StagesData[Races[2], 3];
        RaceConditionText4.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[3], 4] + "\n" + StagesData[Races[3], 3];
        RaceConditionText5.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[4], 4] + "\n" + StagesData[Races[4], 3];
        RaceConditionText6.GetComponent<UnityEngine.UI.Text>().text = "" + StagesData[Races[5], 4] + "\n" + StagesData[Races[5], 3];




        //Reset race data 
        for (var data = 0; data < RaceData.Length; data++)
        {
            for (var item = 0; item < RaceData[data].Count; item++)
            {
                RaceData[data].RemoveAt(item);
            }
        }

        signupNext = true;

    }


    // Update is called once per frame
    void Update () {

        

        if (signupNext)
        {
            if (PlayerIdsList.Count < 60)
                {
                    drawChancesVarBonus = 3;
                delay = 2.5f;
            }
            if (PlayerIdsList.Count < 55)
            {
                delay = 0.1f;
                drawChancesVarBonus = 6;
            }
            if (playerSignedUp|| PlayerIdsList.Count < 60) { playerSignedUpWaitForOthres++; }
            if (playerSignedUp) { delay = 0.001f;}
            if (playerSignedUpWaitForOthres > 50 && PlayerIdsList.Count < 60)
            {
                drawChancesVarBonus = 980;
            }
            if (PlayerIdsList.Count < 46)
            {
                drawChancesVarBonus = 3000;
            }
            if (playerSignedUpWaitForOthres>500)
            {
                drawChancesVarBonus = drawChancesVarBonus+playerSignedUpWaitForOthres;
            }

            StartCoroutine("SignUpRace");
        }
    }

    void OnGUI() {

        allSigned = true;
        for (var it = 0; it < RaceData.Length; it++)
        {
            if (RaceData[it].Count != 10)
            {
                allSigned = false;
            }
        }

        if (allSigned&&!simulated)
        {
            BeginRacesButton.SetActive(true);
            /*
            if (GUILayout.Button("Begin Races", GUILayout.Width(200), GUILayout.Height(50)))
            {
                print("begin races");
                simulateRaces();
            }
            */

        }
        if (simulated)
        {

            if (GUILayout.Button("Back to garage", GUILayout.Width(200), GUILayout.Height(50)))
            {
                GetComponent<MainStage>().visibleMenu = "MainStage";
                GetComponent<RaceChoose>().enabled = false;
                GetComponent<PlayerList>().sort();
                simulated = false;
            }

        }
       // GUILayout.EndHorizontal();


    }

    IEnumerator SignUpRace()
    {
    bool drawChancesVar;
    int drawChancesVarLimit;
    int drawChancesVarBase=0;
    int drawChanceTemp;
        drawChanceTemp = 0;

    signupNext = false;
        brakeAiSignUp = false;
        yield return new WaitForSeconds(delay);

        DrawPlayerId = Random.Range(0, PlayerIdsList.Count-1);
        //print("?????????????????????????????????????????????????????????????????????????????????????");
        //print("?????????????????????????????????????????????????????????????????????????????????????");
        //print("?????????????????????????????????????????????????????????????????????????????????????");
        //print("?????????????????????????????????????????????????????????????????????????????????????");
        if (PlayerIdsList.Count>100)
        {
            for (var lists=0;lists< PlayerIdsList.Count;lists++)
            {
               // Debug.Log("PlayerIdsList "+ lists+" = "+PlayerIdsList[lists]);
            }
        }

        for (DrawRaceId = 0; DrawRaceId < RaceData.Length; DrawRaceId++)
        {
           // Debug.Log("DrawRaceId" + DrawRaceId + " racers " + RaceData[DrawRaceId].Count);

            if (RaceData[DrawRaceId].Count < 10)
            {
               // print("assign racers");

                for (DrawPlayerId = 0; DrawPlayerId < PlayerIdsList.Count - 1; DrawPlayerId++)
                {

                    if (DrawPlayerId > 0)
                    {
                        // DrawRaceId = Random.Range(0, RaceData.Length);
                        chancesToContinueSignUp = 0;

                        if (PlayerIdPlaceByPoints[DrawPlayerId] < 10)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(4, 16)+ drawChancesVarBonus + drawChancesVarBonusLevel6; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(4, 16) + drawChancesVarBonus + drawChancesVarBonusLevel5; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(0, 6); }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = -3000; }
                            if (RacesLvl[DrawRaceId] < 3) { chancesToContinueSignUp = -3000; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 10 && PlayerIdPlaceByPoints[DrawPlayerId] < 20)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(2, 10) + drawChancesVarBonus + drawChancesVarBonusLevel6; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(2, 10) + drawChancesVarBonus + drawChancesVarBonusLevel5; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(3, 6); }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = -3000; }
                            if (RacesLvl[DrawRaceId] < 3) { chancesToContinueSignUp = -3000; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 20 && PlayerIdPlaceByPoints[DrawPlayerId] < 30)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(3, 10) + drawChancesVarBonus * 2; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(3, 10) + drawChancesVarBonus * 5; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(6, 10) + drawChancesVarBonus; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = 0; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = -2000; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 30 && PlayerIdPlaceByPoints[DrawPlayerId] < 40)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(1, 12); }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(2, 10); }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(3, 6) + drawChancesVarBonus * 5; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = 0 ; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = -1000; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 40 && PlayerIdPlaceByPoints[DrawPlayerId] < 50)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(1, 6); }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(1, 6); }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(3, 6); }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(3, 6) + drawChancesVarBonus * 10; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = 0; }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = 0; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 50 && PlayerIdPlaceByPoints[DrawPlayerId] < 60)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(0, 4); }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(0, 6); }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(3, 6) + drawChancesVarBonusLevel4; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(3, 6) + drawChancesVarBonus * 10; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = Random.Range(1, 6); }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = 0; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 60 && PlayerIdPlaceByPoints[DrawPlayerId] < 70)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = Random.Range(0, 4); }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = Random.Range(0, 4); }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(0, 6) + drawChancesVarBonusLevel4; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(4, 6) + drawChancesVarBonus * 10+20; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = Random.Range(2, 6); }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = Random.Range(2, 6); }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 70 && PlayerIdPlaceByPoints[DrawPlayerId] < 80)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = Random.Range(0, 4); }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(3, 12) + drawChancesVarBonusLevel3; }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = Random.Range(4, 12) + drawChancesVarBonus * 10; }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = Random.Range(6, 16) + drawChancesVarBonus * 6; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 80 && PlayerIdPlaceByPoints[DrawPlayerId] < 90)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(2, 6); }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = Random.Range(4, 16) + drawChancesVarBonusLevel2 ; }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = Random.Range(6, 16) + drawChancesVarBonus * 8; }
                        }
                        if (PlayerIdPlaceByPoints[DrawPlayerId] >= 90 && PlayerIdPlaceByPoints[DrawPlayerId] < 101)
                        {
                            if (RacesLvl[DrawRaceId] == 6) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 5) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 4) { chancesToContinueSignUp = -1000; }
                            if (RacesLvl[DrawRaceId] == 3) { chancesToContinueSignUp = Random.Range(0, 4); }
                            if (RacesLvl[DrawRaceId] == 2) { chancesToContinueSignUp = Random.Range(3, 12); }
                            if (RacesLvl[DrawRaceId] == 1) { chancesToContinueSignUp = Random.Range(6, 16) + drawChancesVarBonus * 10+ drawChancesVarBonusLevel1; }
                        }

                        chancesToContinueSignUp = chancesToContinueSignUp + drawChancesVarBonus - Random.Range(0, 10);
                       // Debug.Log("DrawPlayerId "+ DrawPlayerId+" DrawRaceId " + DrawRaceId + " chancesToContinueSignUp " + chancesToContinueSignUp);

                        if (chancesToContinueSignUp >= 5)
                        {


                            if (RaceData[DrawRaceId].Count < 10)
                            {
                                drawChancesVar = false;
                                // Debug.Log("DrawRaceId" + DrawRaceId);
                                // Debug.Log("RacesLvl[DrawRaceId] "+ RacesLvl[DrawRaceId]);
                                // Debug.Log("DrawPlayerId" + DrawPlayerId);
                                // Debug.Log("PlayerIdPlaceByPoints[DrawPlayerId] " + PlayerIdPlaceByPoints[DrawPlayerId]);


                                /*
                                drawChancesVarLimit = 100 - (10 * RacesLvl[DrawRaceId]) + (100 - PlayerIdPlaceByPoints[DrawPlayerId]) + Random.Range(0, 60);



                                if (RacesLvl[DrawRaceId] == 6)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 0; drawChancesVarLimit = Random.Range(0, 10); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 5; drawChancesVarLimit = Random.Range(0, 10); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 10; drawChancesVarLimit = Random.Range(0, 60); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 50; drawChancesVarLimit = Random.Range(35, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 90) { drawChancesVarBase = 60; drawChancesVarLimit = Random.Range(45, 80); }
                                }

                                if (RacesLvl[DrawRaceId] == 5)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 10; drawChancesVarLimit = Random.Range(0, 50); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 20; drawChancesVarLimit = Random.Range(0, 60); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 40; drawChancesVarLimit = Random.Range(0, 70); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 60; drawChancesVarLimit = Random.Range(0, 80); }
                                }

                                if (RacesLvl[DrawRaceId] == 4)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 20; drawChancesVarLimit = Random.Range(0, 50); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 40; drawChancesVarLimit = Random.Range(0, 70); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 50; drawChancesVarLimit = Random.Range(0, 50); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 40; drawChancesVarLimit = Random.Range(0, 70); }
                                }

                                if (RacesLvl[DrawRaceId] == 3)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 30; drawChancesVarLimit = Random.Range(0, 60); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 50; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 50; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 30; drawChancesVarLimit = Random.Range(0, 60); }
                                }

                                if (RacesLvl[DrawRaceId] == 2)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 50; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 40; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 20; drawChancesVarLimit = Random.Range(0, 10); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 10; drawChancesVarLimit = Random.Range(0, 10); }
                                }

                                if (RacesLvl[DrawRaceId] == 1)
                                {
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 0) { drawChancesVarBase = 55; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 40) { drawChancesVarBase = 40; drawChancesVarLimit = Random.Range(0, 80); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 60) { drawChancesVarBase = 10; drawChancesVarLimit = Random.Range(0, 10); }
                                    if (100 - PlayerIdPlaceByPoints[DrawPlayerId] > 80) { drawChancesVarBase = 0; drawChancesVarLimit = 0 + Random.Range(0, 10); }
                                }

                                //print("drawChancesVarLimit");
                                //print(drawChancesVarLimit);
                                drawChancesVarBonus = 0;
                                drawChancesVarBase = 0;

                                //drawChanceTemp = drawChancesVarBonus + drawChancesVarBase + Random.Range(0, drawChancesVarLimit);
                                //Debug.Log("Player place " + PlayerIdPlaceByPoints[DrawPlayerId]);

                                   drawChanceTemp = drawChancesVarLimit;
                                */

                                //print("drawChanceTemp");
                                //print(drawChanceTemp);
                                drawChanceTemp = chancesToContinueSignUp;


                               // Debug.Log("Race lvl " + RacesLvl[DrawRaceId] + "Player place " + PlayerIdPlaceByPoints[DrawPlayerId] + "and his chances are " + drawChanceTemp);

                                if (drawChanceTemp >= 5)
                                {
                                    drawChancesVar = true;
                                }
                                else
                                {
                                    drawChancesVar = false;
                                }



                                if (drawChancesVar)
                                {

                                    if (PlayerIdsList[DrawPlayerId] == 0)
                                    {
                                        print("Player id !!!!!!!!!!!!!!!!!!!!!");
                                    }

                                    // Debug.Log("DrawRaceId " + DrawRaceId);
                                    // Debug.Log("DrawPlayerId " + DrawPlayerId);
                                    // Debug.Log("PlayerIdsList.Count " + PlayerIdsList.Count);
                                    // Debug.Log("PlayerIdsList[DrawPlayerId] " + PlayerIdsList[DrawPlayerId]);
                                    //Debug.Log("DrawRaceId " + DrawRaceId+ "DrawPlayerId " + DrawPlayerId+ "PlayerIdsList.Count " + PlayerIdsList.Count+"PlayerIdsList[DrawPlayerId] " + PlayerIdsList[DrawPlayerId]);
                                    if (!brakeAiSignUp)
                                    {
                                        RaceData[DrawRaceId].Add(PlayerIdsList[DrawPlayerId]);

                                        //RacePlayerListText0.GetComponent<UnityEngine.UI.Text>().text = "";

                                        switch (DrawRaceId)
                                        {
                                            case 0: RacePlayerListText0.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            case 1: RacePlayerListText1.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            case 2: RacePlayerListText2.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            case 3: RacePlayerListText3.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            case 4: RacePlayerListText4.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            case 5: RacePlayerListText5.GetComponent<UnityEngine.UI.Text>().text += PlayerPrefs.GetString("NameP" + PlayerIdsList[DrawPlayerId]) + "\n"; break;
                                            default: break;

                                        }


                                        PlayerIdsList.RemoveAt(DrawPlayerId);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        brakeAiSignUp = false;
        signupNext = true;

        for (DrawRaceId = 0; DrawRaceId < RaceData.Length; DrawRaceId++)
        {
            //tu sie wywala po wybraniu wyscigu chyba nie znajduje przycisku
            if (RaceData[DrawRaceId].Count == 10)
            {
                switch (DrawRaceId)
                {
                    case 0: GameObject.Find("RaceSquare1").GetComponent<Button>().interactable = false; break;
                    case 1: GameObject.Find("RaceSquare2").GetComponent<Button>().interactable = false; break;
                    case 2: GameObject.Find("RaceSquare3").GetComponent<Button>().interactable = false; break;
                    case 3: GameObject.Find("RaceSquare4").GetComponent<Button>().interactable = false; break;
                    case 4: GameObject.Find("RaceSquare5").GetComponent<Button>().interactable = false; break;
                    case 5: GameObject.Find("RaceSquare6").GetComponent<Button>().interactable = false; break;
                    default: break;

                }
            }
        }

    }


    public void simulateRaces()
    {
        for (var i = 0; i < RaceData.Length; i++)
        {
            if (i != PlayerRace)
            {
                //Debug.Log("Race number " + i);
                for (var ii = 0; ii < RaceData[i].Count; ii++)
                {
                    //Debug.Log("Player id "+RaceData[i][ii]);
                    PlayerPower[ii, 0] = RaceData[i][ii];
                    PlayerPower[ii, 1] = int.Parse(PlayerPrefs.GetString("CarP" + ii)) * 12;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("EngineP" + ii)) * 8;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("TiresP" + ii)) * 6;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("ArmorP" + ii)) * 6;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("GunP" + ii)) * 6;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("NitroNextRaceP" + ii));
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("MinesNextRaceP" + ii))*2;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("MissilesNextRaceP" + ii))*3;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("HomingMissilesNextRaceP" + ii))*4;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("AirStrikeNextRaceP" + ii))*5;
                    PlayerPower[ii, 1] += int.Parse(PlayerPrefs.GetString("DefendStrikeNextRaceP" + ii))*4;
                    PlayerPower[ii, 1] += Random.Range(0, 30);

                    //Debug.Log("Player id " + PlayerPower[ii, 0] +"power " + PlayerPower[ii, 1]);

                }


                for (var write = 0; write < 10; write++)
                {
                    for (var sort = 0; sort < 9; sort++)
                    {
                        if (PlayerPower[sort, 1] < PlayerPower[sort + 1, 1])
                        {
                            temp = PlayerPower[sort + 1, 1];
                            tempId = PlayerPower[sort + 1, 0];
                            PlayerPower[sort + 1, 0] = PlayerPower[sort, 0];
                            PlayerPower[sort + 1, 1] = PlayerPower[sort, 1];
                            PlayerPower[sort, 0] = tempId;
                            PlayerPower[sort, 1] = temp;
                        }
                    }
                }

                print("Sorted !!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Debug.Log("Sorted !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                for (var it = 0; it < 10; it++)
                {
                    //Debug.Log("Player id " + PlayerPower[it, 0] + "power " + PlayerPower[it, 1]);
                    //RacesLvl[i]

                    GetComponent<RaceBonuses>().RaceBasicPrizes(RaceData[i][it], it, RacesLvl[i]);

                    /*
                    if (it == 0)
                    {
                        //Debug.Log("Player id " + RaceData[i][it] + "has money " + PlayerPrefs.GetString("MoneyP" + RaceData[i][it]));
                        tempBonus = RacesLvl[i] * 1000 + RacesLvl[i] * 500;
                        PlayerPrefs.SetString("GetMoneyP" + RaceData[i][it], "" + tempBonus);
                        tempBonus += int.Parse(PlayerPrefs.GetString("MoneyP" + RaceData[i][it]));
                        PlayerPrefs.SetString("MoneyP" + RaceData[i][it], "" + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "got money " + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "has points " + PlayerPrefs.GetString("PointsP" + RaceData[i][it]));
                        tempBonus = (20 + RacesLvl[i] * 2);
                        PlayerPrefs.SetString("GetPointsP" + RaceData[i][it], "" + tempBonus);
                        tempBonus += int.Parse(PlayerPrefs.GetString("PointsP" + RaceData[i][it]));
                        PlayerPrefs.SetString("PointsP" + RaceData[i][it], "" + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "have now points " + tempBonus);
                    }
                    else
                    {
                        //Debug.Log("Player id " + RaceData[i][it] + "has money " + PlayerPrefs.GetString("MoneyP" + RaceData[i][it]));
                        tempBonus = (RacesLvl[i] * 900 + RacesLvl[i] * 300) / it;
                        PlayerPrefs.SetString("GetMoneyP" + RaceData[i][it], "" + tempBonus);
                        tempBonus += int.Parse(PlayerPrefs.GetString("MoneyP" + RaceData[i][it]));
                        PlayerPrefs.SetString("MoneyP" + RaceData[i][it], "" + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "got money " + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "has points " + PlayerPrefs.GetString("PointsP" + RaceData[i][it]));
                        tempBonus = (16 + RacesLvl[i] * 2) - it;
                        PlayerPrefs.SetString("GetPointsP" + RaceData[i][it], "" + tempBonus);
                        tempBonus += int.Parse(PlayerPrefs.GetString("PointsP" + RaceData[i][it]));
                        PlayerPrefs.SetString("PointsP" + RaceData[i][it], "" + tempBonus);
                        //Debug.Log("Player id " + RaceData[i][it] + "have now points " + tempBonus);
                    }
                    */

                    //add points 
                    //add cash
                    //check if race for player

                }



            }

            /*
                    for (var i = 0; i < RaceData.Length; i++)
                    {
                        Debug.Log("Race number " + i);
                        for (var ii = 0; ii < RaceData[i].Count; ii++)
                        {
                            Debug.Log("Player id " + RaceData[i][ii]);
                            PlayerPower[ii, 0] = RaceData[i][ii];
                            PlayerPower[ii, 1] = int.Parse(PlayerPrefs.GetString("CarP" + ii)) * 12 + int.Parse(PlayerPrefs.GetString("EnigineP" + ii)) * 8 + int.Parse(PlayerPrefs.GetString("TireP" + ii)) * 6 + int.Parse(PlayerPrefs.GetString("ArmorP" + ii)) * 6 + int.Parse(PlayerPrefs.GetString("GunP" + ii)) * 6 + int.Parse(PlayerPrefs.GetString("NitroNextRaceP" + ii)) + int.Parse(PlayerPrefs.GetString("MinesNextRaceP" + ii)) + Random.Range(0, 50);

                        }
                    }
            */
        }


        if (PlayerRace < 100)
        {

            //SET UP DATA FOR PLAYER RACE
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("Player Race racers IDs");
            Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            for (var racer = 0; racer < RaceData[PlayerRace].Count; racer++)
            {
                PlayerPrefs.SetString("AI" + racer, "" + RaceData[PlayerRace][racer]);
            }

            for (var racer = 0; racer < RaceData[PlayerRace].Count; racer++)
            {
                Debug.Log("Racer " + racer + " id is " + PlayerPrefs.GetString("AI" + racer));
            }

            //Application.LoadLevel(StagesData[Races[PlayerRace], 0]);

            if (!startAsyncLoading)
            {
                //StartCoroutine(LevelCoroutine("garage"));
                startAsyncLoading = true;
                Loading.SetActive(true);
                print("bedzie wczytywał");
                print(StagesData[Races[PlayerRace], 0]);
                GetComponent<LoadScene>().LoadLevel(StagesData[Races[PlayerRace], 0]);
            }



        }


        //Clear ids list 
        for (var data = 0; data < PlayerIdsList.Count; data++)
        {
            PlayerIdsList.RemoveAt(data);
        }



        //GetComponent<MainStage>().visibleMenu = "RacesResults";
        GetComponent<RaceChoose>().enabled = false;
        GetComponent<PlayerList>().sort();
        simulated = false;
        print("Scores");
        GetComponent<MainStage>().ClearViews();
        if (PlayerRace == 100)
        {
            GetComponent<MainStage>().Scores();
        }
        /*
        if (!startAsyncLoading)
        {
            GetComponent<MainStage>().Scores();
        }
        else
        {
            GetComponent<MainStage>().MainMenu();
        }
        */
        //simulated = true;

    }



}
