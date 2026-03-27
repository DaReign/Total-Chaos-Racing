using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Stage : MonoBehaviour {
    public int laps;
    public int RaceLvl;
    public GameObject StartingWaypoint;
    public List<GameObject> listOfCars = new List<GameObject>();
    public List<GameObject> listOfCarsSorted = new List<GameObject>();
    public List<GameObject> listOfWinners = new List<GameObject>();
    public List<GameObject> listOfRaceEnd = new List<GameObject>();
    public List<GameObject> VariantObjects = new List<GameObject>();
    public GameObject Cars;
    private Boolean CarsListCopied = false;
    private GameObject temp;
    private int write = 0;
    private int sort = 0;
    public int variant = 0;


    public Boolean timedInfoEnable = false;
    public Boolean timedInfoShowing = false;
    public string timedInfoText = "";
    public float timedInfoTime = 0f;

    private string textColor = "yellow";


    private int racerId;


    public int MainPrize = 10000;

    private int currMoney;
    private int currPoints;
    private Boolean bonusesAdded = false;


    public Boolean raceEnded = false;

    public Boolean WeaponsEnabled = false;
    private bool StartWeaponEnabling = false;
    private bool WeaponsEnabledText = false;


    public GameObject MissileButton;
    public GameObject HomingMissileButton;
    public GameObject MineButton;

    public int playerGotPoints;
    public int playerGotMoney;
    public int playerPosition;
    public int playerDestroyedEnemies;
    public int plyerColectedMoney;


    public bool NotMovieVar = true;

    private GameObject Position1;
    private GameObject Position2;
    private GameObject Position3;
    private GameObject Position4;
    private GameObject Position5;
    private GameObject Position6;
    private GameObject Position7;
    private GameObject Position8;
    private GameObject Position9;
    private GameObject Position10;

    private GameObject FinishedPosition1;
    private GameObject FinishedPosition2;
    private GameObject FinishedPosition3;
    private GameObject FinishedPosition4;
    private GameObject FinishedPosition5;
    private GameObject FinishedPosition6;
    private GameObject FinishedPosition7;
    private GameObject FinishedPosition8;
    private GameObject FinishedPosition9;
    private GameObject FinishedPosition10;

    private GameObject FinishRacePanel;
    private GameObject FinishRaceText;
    private GameObject GoToGarage;

    // Use this for initialization

    IEnumerator timedMessage ()
    {
        timedInfoShowing = true;
        yield return new WaitForSeconds(timedInfoTime);
        timedInfoEnable = false;
        timedInfoShowing = false;

    }

    IEnumerator enableWeapons()
    {
        StartWeaponEnabling = true;
        yield return new WaitForSeconds(2500.0f);
        WeaponsEnabled = true;
        MissileButton.SetActive(true);
        HomingMissileButton.SetActive(true);
        MineButton.SetActive(true);
        timedInfoText = "Weapons enabled";
        timedInfoEnable = true;
        timedInfoTime = 10.0f;

    }


    void Start () {

        FinishRacePanel = GameObject.Find("FinishRacePanel");
        FinishRaceText = GameObject.Find("FinishRaceText");
        FinishRacePanel.SetActive(false);
        GoToGarage = GameObject.Find("GoToGarage");
        GoToGarage.SetActive(false);
        

        Position1 = GameObject.Find("Position1");
        Position2 = GameObject.Find("Position2");
        Position3 = GameObject.Find("Position3");
        Position4 = GameObject.Find("Position4");
        Position5 = GameObject.Find("Position5");
        Position6 = GameObject.Find("Position6");
        Position7 = GameObject.Find("Position7");
        Position8 = GameObject.Find("Position8");
        Position9 = GameObject.Find("Position9");
        Position10 = GameObject.Find("Position10");

        FinishedPosition1 = GameObject.Find("FinishedPosition1");
        FinishedPosition2 = GameObject.Find("FinishedPosition2");
        FinishedPosition3 = GameObject.Find("FinishedPosition3");
        FinishedPosition4 = GameObject.Find("FinishedPosition4");
        FinishedPosition5 = GameObject.Find("FinishedPosition5");
        FinishedPosition6 = GameObject.Find("FinishedPosition6");
        FinishedPosition7 = GameObject.Find("FinishedPosition7");
        FinishedPosition8 = GameObject.Find("FinishedPosition8");
        FinishedPosition9 = GameObject.Find("FinishedPosition9");
        FinishedPosition10 = GameObject.Find("FinishedPosition10");


        // laps = int.Parse(PlayerPrefs.GetString("RaceDayState"));

        MissileButton = GameObject.Find("MissileButton");
        HomingMissileButton = GameObject.Find("HomingMissileButton");
        MineButton = GameObject.Find("MineButton");
        MissileButton.SetActive(false);
        HomingMissileButton.SetActive(false);
        MineButton.SetActive(false);


        listOfCars.Clear();
        listOfCarsSorted.Clear();
        listOfWinners.Clear();
        listOfRaceEnd.Clear();

        variant = int.Parse(PlayerPrefs.GetString("RaceVariant"));

        //        variant=UnityEngine.Random.Range(0,2);

        //variant = 1;

        if (variant==0)
        {
            GameObject.Find("Variant1").SetActive(true);
            GameObject.Find("Variant2").SetActive(false);
        }
        else
        {
            GameObject.Find("Global").GetComponent<WaypointsList>().reverseList();
            GameObject.Find("Variant1").SetActive(false);
            GameObject.Find("Variant2").SetActive(true);
        }




        if (StartingWaypoint == null) {
            StartingWaypoint = GameObject.Find("Global").GetComponent<WaypointsList>().listOfWaypoints[0]; 
        }

        RaceLvl = int.Parse(PlayerPrefs.GetString("RaceLvl"));
        laps = int.Parse(PlayerPrefs.GetString("RaceLaps"));
        //laps = 1;

    }
	

    private void LateUpdate()
    {
        if (!StartWeaponEnabling)
        {
            StartCoroutine("enableWeapons");
        }
        //Position1.GetComponent<UnityEngine.UI.Text>().text = "Laps ";
        UpdateRaceListUI();
        if (listOfRaceEnd.Count >= 10)
        {
            FinishRacePanel.SetActive(true);
            GoToGarage.SetActive(true);
            if (GameObject.Find("UserControl"))
            {
                GameObject.Find("UserControl").SetActive(false);
            }
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity < 1.0f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity += 0.01f;
            }
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity > 1.0f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity = 1.0f;
            }

            if (!bonusesAdded)
            {
                print("This should call once");

                for (var a = 0; a < listOfWinners.Count; a++)
                {
                    racerId = listOfWinners[a].GetComponent<CarStats>().racerId;

                    if (racerId == 0)
                    {
                        playerGotPoints = GetComponent<RaceBonuses>().CalculatePointsBonus(a, RaceLvl);
                        playerGotMoney = GetComponent<RaceBonuses>().CalculateMoneyBonus(a, RaceLvl);
                    }
                    GetComponent<RaceBonuses>().RaceBasicPrizes(racerId, a, RaceLvl);
                }
                bonusesAdded = true;
                raceEnded = true;
            }
            FinishRaceText.GetComponent<UnityEngine.UI.Text>().text = "Race finished \n You got " + playerGotPoints + " points \n" + "You got " + playerGotMoney + " cash";
        }
    }

    private void UpdateFinishedPosition(int i, string RacerNameVar)
    {

        switch (i)
        {
            case 1: FinishedPosition1.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 2: FinishedPosition2.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 3: FinishedPosition3.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 4: FinishedPosition4.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 5: FinishedPosition5.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 6: FinishedPosition6.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 7: FinishedPosition7.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 8: FinishedPosition8.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 9: FinishedPosition9.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            case 10: FinishedPosition10.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar; break;
            default: break;
        }

    }

    private void UpdatePosition(int i,string RacerNameVar, int HullVar, int WaypointsToFinishVar,string StatusVar)
    {

        switch (i)
        {
            case 1: Position1.GetComponent<UnityEngine.UI.Text>().text = ""+ RacerNameVar+" "+ HullVar + " "+StatusVar;  break;
            case 2: Position2.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 3: Position3.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 4: Position4.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 5: Position5.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 6: Position6.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 7: Position7.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 8: Position8.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 9: Position9.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            case 10: Position10.GetComponent<UnityEngine.UI.Text>().text = "" + RacerNameVar + " " + HullVar + " " + StatusVar; break;
            default: break;
        }

    }

    void UpdateRaceListUI ()
    {
        SortCarsList();
        int placeI = 0;
        for (int i = 0; i < listOfCarsSorted.Count; i++)
        {
            if (!listOfCarsSorted[i].GetComponent<CarStats>().wrecked)
            {
                if (listOfCarsSorted[i].GetComponent<CarStats>().raceFinished == false)
                {
                    UpdatePosition(i+1, listOfCarsSorted[i].GetComponent<CarStats>().RacerName, listOfCarsSorted[i].GetComponent<CarStats>().Hull, listOfCarsSorted[i].GetComponent<CarStats>().waypointsToFinish, "");
                    placeI++;
                }
            }
            else
            {
                UpdatePosition(i+1, listOfCarsSorted[i].GetComponent<CarStats>().RacerName, listOfCarsSorted[i].GetComponent<CarStats>().Hull, listOfCarsSorted[i].GetComponent<CarStats>().waypointsToFinish, "wrecked");
                placeI++;
            }
        }
        if (listOfWinners.Count > 0) {
            for (int i = 0; i < listOfWinners.Count; i++)
            {
                UpdateFinishedPosition(i+1, listOfWinners[i].GetComponent<CarStats>().RacerName);
                //GUI.Box(new Rect(0, (1 + i) * 20, 200, 20), "" + listOfWinners[i].GetComponent<CarStats>().RacerName + " " + (listOfWinners[i].GetComponent<CarStats>().waypointsToFinish));
            }

            //FinishedListUI();
        }
    }

    void SortCarsList()
    {
        if (!CarsListCopied)
        {
            listOfCarsSorted = listOfCars;
            CarsListCopied = true;
        }

        for (write = 0; write < listOfCarsSorted.Count; write++)
        {
            for (sort = 0; sort < listOfCarsSorted.Count - 1; sort++)
            {
                if (listOfCarsSorted[sort].GetComponent<CarStats>().waypointsToFinish > listOfCarsSorted[sort + 1].GetComponent<CarStats>().waypointsToFinish)
                {
                    temp = listOfCarsSorted[sort + 1];
                    listOfCarsSorted[sort + 1] = listOfCarsSorted[sort];
                    listOfCarsSorted[sort] = temp;
                }
            }
        }
    }

    void OnGUI() 
    {

        if (timedInfoEnable)
        {
            //print("Timed info enabled !!!!!!!!!!!!");
            if (!timedInfoShowing)
            {
              //  print("Before coroutine !!!!!!!!!!!!");
                StartCoroutine("timedMessage");
            }
            else
            {
//                timedInfoShowing = true;
                GUI.Box(new Rect(Screen.width/2-150, Screen.height/2-25, 300, 50), "" + timedInfoText);

            }
        } 
        /*
        if (listOfRaceEnd.Count < 10)
        {

            if (NotMovieVar)
            {

                if (!CarsListCopied)
                {
                    listOfCarsSorted = listOfCars;
                    CarsListCopied = true;
                }

                for (write = 0; write < listOfCarsSorted.Count; write++)
                {
                    for (sort = 0; sort < listOfCarsSorted.Count - 1; sort++)
                    {
                        if (listOfCarsSorted[sort].GetComponent<CarStats>().waypointsToFinish > listOfCarsSorted[sort + 1].GetComponent<CarStats>().waypointsToFinish)
                        {
                            temp = listOfCarsSorted[sort + 1];
                            listOfCarsSorted[sort + 1] = listOfCarsSorted[sort];
                            listOfCarsSorted[sort] = temp;
                        }
                    }
                }

            }

            //GetComponent<CalculatePlayerRaceResults>().calculate();

            if (listOfWinners.Count>0)
                {
                    GUI.Box(new Rect(0, 0, 200, 20), "Winners list");
                    for (int i = 0; i < listOfWinners.Count; i++)
                        {
                            GUI.Box(new Rect(0, (1 + i) * 20, 200, 20), "" + listOfWinners[i].GetComponent<CarStats>().RacerName + " " + (listOfWinners[i].GetComponent<CarStats>().waypointsToFinish));
                        }
                }
        }
        else
        {

            //Camera.main.GetComponent<LoadScene>().fadeOut = true;
            //Camera.main.GetComponent<LoadScene>().fadeIn = false;

            if (GameObject.Find("UserControl"))
            {
                GameObject.Find("UserControl").SetActive(false);
            }
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity < 1.0f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity += 0.01f;
            }
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity>1.0f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity = 1.0f;
            }



            GUILayout.BeginArea(new Rect(Screen.width/2-150, 10, 300, Screen.height-20));
            GUILayout.BeginVertical("box",GUILayout.Width(300));
            GUILayout.Box("LIST OF WINNERS on finish");

            for (var a = 0; a < listOfWinners.Count; a++)
            {
                GUILayout.Box(listOfWinners[a].GetComponent<CarStats>().RacerName);
            }


            if (!bonusesAdded)
            {
                print("This should call once");

                for (var a = 0; a < listOfWinners.Count; a++)
                {                
                    racerId = listOfWinners[a].GetComponent<CarStats>().racerId;

                    if (racerId==0) {
                        playerGotPoints = GetComponent<RaceBonuses>().CalculatePointsBonus(a, RaceLvl);
                        playerGotMoney = GetComponent<RaceBonuses>().CalculateMoneyBonus(a, RaceLvl);
                    }
                    GetComponent<RaceBonuses>().RaceBasicPrizes(racerId, a, RaceLvl);
                }
                bonusesAdded = true;
                raceEnded = true;
                //Time.timeScale = 1;
                //print("Time scale 1");
            }
            GUILayout.Box("You got "+ playerGotPoints+" points");
            GUILayout.Box("You got " + playerGotMoney + " cash");

            if (GUILayout.Button("GO TO GARAGE", GUILayout.Height(200)))
            {
                Debug.Log("Clicked Button");
                //SceneManager.LoadScene("Points");
                Application.LoadLevel("garage");
                listOfCars.Clear();
                listOfCarsSorted.Clear();
                listOfWinners.Clear();
                listOfRaceEnd.Clear();

                //SceneManager.LoadScene("Points", LoadSceneMode.Single);
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
        */
    }
}
