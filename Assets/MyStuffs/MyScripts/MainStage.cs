using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class MainStage : MonoBehaviour {
    public Vector2 scrollPosition = Vector2.zero;
    public string visibleMenu = "MainStage";
    public GameObject Loading;
    private bool loading;
    private bool start;
    public GameObject Camera;
    public GameObject PanelScores;
    public GameObject RaceChoose;
    public GameObject Upgrade;

    public GameObject MainMenuButtons;
    public GameObject BackButton;
    public GameObject CameraEffects;

    public GameObject Item;
    public GameObject ItemList;
    public GameObject InfoText;
    public GameObject TextInfoObject;
    public string currentUpgradeItem;
    public GameObject AmmoItems;
    public GameObject NewCar;
    public GameObject Car1;
    public GameObject Car2;
    public GameObject Car3;
    public GameObject Car4;
    public GameObject Car5;
    public GameObject Car6;
    public GameObject Car7;
    public GameObject Car8;
    public GameObject Car9;
    public GameObject Car10;
    //private GameObject ActiveCar;
    public GameObject CarPrice;
    public GameObject CarBuyButton;
    public GameObject CarName;
    //public GameObject BackToUpgradeListButton;

    public int visibleCarLevel;
    // Use this for initialization

    private string fieldName;
    private string fieldValue;
    private int number;

    private int money;
    private int currentCar;
    private int currentItemLevel;
    private int upgradePrice;
    private string itemToUpgrade;
    private string tempString;
    public List<GameObject> Stars = new List<GameObject>();
    private bool startAsyncLoading = false;
    public GameObject AudioSourceObj;

    void Start () {

        if (PlayerPrefs.GetString("Music") == "Off")
        {
            AudioSourceObj.SetActive(false);
        }
        else
        {
            AudioSourceObj.SetActive(true);
        }


        /*TEST SETUP*/
        /*
        PlayerPrefs.SetString("MoneyP0", "10000");
        PlayerPrefs.SetString("CarP0", "1");
        PlayerPrefs.SetString("GunP0", "1");
        PlayerPrefs.SetString("ArmorP0", "1");
        PlayerPrefs.SetString("EngineP0", "1");
        PlayerPrefs.SetString("TiresP0", "1");
        PlayerPrefs.SetString("MinesNextRaceP0", "1");
        PlayerPrefs.SetString("NitroNextRaceP0", "1"); 
        */
        /*TEST SETUP*/

        //print("car level");
        //print(PlayerPrefs.GetString("CarP0"));

        visibleCarLevel = int.Parse(PlayerPrefs.GetString("CarP0"));
        showCar(visibleCarLevel);
        //PlayerPrefs.SetString("CarP0", "3");

        start = true;
        Camera.GetComponent<VignetteAndChromaticAberration>().intensity = 1.0f;
        PanelScores.SetActive(false);
        RaceChoose.SetActive(false);
                ClearViews();

    }

    // Update is called once per frame
    void Update () {

        /*
        if (start)
        {
            if (Camera.GetComponent<VignetteAndChromaticAberration>().intensity > 0.368f)
            {
                Camera.GetComponent<VignetteAndChromaticAberration>().intensity -= 0.01f;
            }
            else
            {
                start = false;
            }
        }
        */



        if (loading)
        {
            start = false;
            //Camera.GetComponent<ScreenSpaceAmbientOcclusion>().enabled = true;
            /*
            if (Camera.GetComponent<VignetteAndChromaticAberration>().intensity < 1.0f)
            {
                Camera.GetComponent<VignetteAndChromaticAberration>().intensity += 0.01f;
            }
            else
            {
                Application.LoadLevel("MainMenu");
            }
            */
            if (!startAsyncLoading)
            {
                //StartCoroutine(LevelCoroutine("garage"));
                print("wczytuje main menu");
                startAsyncLoading = true;
                GetComponent<LoadScene>().LoadLevel("MainMenu");
            }



        }

    }

    public void upgradeListMenu ()
    {
        ClearViews();
        Upgrade.SetActive(true);
        ItemList.SetActive(true);
    }


    public void newCar(string what)
    {
        ItemList.SetActive(false);
        NewCar.SetActive(true);

        visibleCarLevel = int.Parse(PlayerPrefs.GetString("CarP0"));

        showCar(visibleCarLevel);
        canBuyNewCar();

    }

    public void buyNewCarTransaction()
    {
        currentCar = int.Parse(PlayerPrefs.GetString("CarP0"));
        //visibleCarLevel = int.Parse(PlayerPrefs.GetString("CarP0"));
        GetComponent<Shopping>().BuyNewCarTransaction(0, currentCar, visibleCarLevel);
        visibleCarLevel = int.Parse(PlayerPrefs.GetString("CarP0"));
        canBuyNewCar();
    }


    public void canBuyNewCar ()
    {
        currentCar = int.Parse(PlayerPrefs.GetString("CarP0"));
        NewCar.transform.Find("Cash/Text").GetComponent<UnityEngine.UI.Text>().text = "Cash " + PlayerPrefs.GetString("MoneyP0");
        CarPrice.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + GetComponent<Shopping>().NewCarPrice(0, currentCar, visibleCarLevel);
        CarName.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + visibleCarLevel;
        //int.Parse(PlayerPrefs.GetString("MoneyP0")) > visibleCarLevel * 10000
        if ( GetComponent<Shopping>().CanBuyCar(0, currentCar, visibleCarLevel) )
        {
            CarBuyButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Buy";
            CarBuyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            CarBuyButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Not enough money";
            CarBuyButton.GetComponent<Button>().interactable = false;
        }

        if (visibleCarLevel<=int.Parse(PlayerPrefs.GetString("CarP0")))
        {
            CarBuyButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "You have better car";
            CarBuyButton.GetComponent<Button>().interactable = false;
        }

    }

    public void nextCar ()
    {

        if (visibleCarLevel<10)
        {
            visibleCarLevel++;
            showCar(visibleCarLevel);
        }
        else
        {
            visibleCarLevel=1;
            showCar(visibleCarLevel);
        }
        canBuyNewCar();
    }

    public void previousCar()
    {
        if (visibleCarLevel > 1)
        {
            visibleCarLevel--;
            showCar(visibleCarLevel);
        }
        else
        {
            visibleCarLevel = 10;
            showCar(visibleCarLevel);
        }
        canBuyNewCar();
    }


    private void showCar (int level)
    {
        Car1.SetActive(false);
        Car2.SetActive(false);
        Car3.SetActive(false);
        Car4.SetActive(false);
        Car5.SetActive(false);
        Car6.SetActive(false);
        Car7.SetActive(false);
        Car8.SetActive(false);
        Car9.SetActive(false);
        Car10.SetActive(false);

        switch (level)
        {
            case 1: Car1.SetActive(true); break;
            case 2: Car2.SetActive(true); break;
            case 3: Car3.SetActive(true); break;
            case 4: Car4.SetActive(true); break;
            case 5: Car5.SetActive(true); break;
            case 6: Car6.SetActive(true); break;
            case 7: Car7.SetActive(true); break;
            case 8: Car8.SetActive(true); break;
            case 9: Car9.SetActive(true); break;
            case 10: Car10.SetActive(true); break;
            default: break;
        }



    }

    public void Ammo()
    {
        ClearViews();
        ItemList.SetActive(false);
        AmmoItems.SetActive(true);
    }

    public void infoTextShow()
    {
        string infoTextHelp;
        ClearViews();
        InfoText.SetActive(true);


        switch (currentUpgradeItem)
        {
            case "Engine": infoTextHelp = "Better engine increase max speed of your car"; break;
            case "Armor": infoTextHelp = "Armor increae max health of your car. With high level armor your car can take more damage"; break;
            case "Gun": infoTextHelp = "Every car has two guns. Both of them works automatically. Better level of weapons system means more ammo to your gun, more damage and longer distance"; break;
            case "NitroNextRace": infoTextHelp = "Nitro greatly increase speed of your car"; break;
            case "Tires": infoTextHelp = "Good tires are more important than good engine. If you have better tires then steering of your car would be much easier"; break;
            case "MinesNextRace": infoTextHelp = "If your enemy is very close to you then mine is best defend system. Just place mine and watch your enemy drive on it"; break;
            case "Ammo": infoTextHelp = "There are to type of special ammor Missiles and Homing Missiles"; break;
            case "MissilesNextRace": infoTextHelp = "Regular missiles are best for short to medium range distance. They are faster than homing missile but less accurate"; break;
            case "HomingMissilesNextRace": infoTextHelp = "Homing missiles are best for long range distance. Even if you don't see target just direct your car to direction where your enemy shold be and fire homing missile"; break;
            default: infoTextHelp = "defaualt working"; break;

        }


        TextInfoObject.GetComponent<UnityEngine.UI.Text>().text = "" + infoTextHelp;
    }

    public void infoTextBack()
    {
        ClearViews();
        upgradeCar(currentUpgradeItem);

    }


    public void upgradeCar (string what)
    {
        currentUpgradeItem = what;
        itemToUpgrade = what;
        string textToshow;
        textToshow = what;
        if (what == "HomingMissilesNextRace") { textToshow = "Homing Missiles"; }
        if (what == "MissilesNextRace") { textToshow = "Missiles"; }
        if (what == "MinesNextRace") { textToshow = "Mines"; }


        ClearViews();
        ItemList.SetActive(false);
        Item.SetActive(true);
        //Item.transform.Find("What");
       // print(Item.transform.Find("What/WhatText"));
        Item.transform.Find("What/WhatText").GetComponent<UnityEngine.UI.Text>().text = textToshow;

        money = int.Parse(PlayerPrefs.GetString("MoneyP0"));        
        currentItemLevel = int.Parse(PlayerPrefs.GetString(what+"P0"));
        currentCar = int.Parse(PlayerPrefs.GetString("CarP0"));
        upgradePrice = GetComponent<Shopping>().CalculateUpgradePrice(currentCar, currentItemLevel+1,what);
        Item.transform.Find("Price/Text").GetComponent<UnityEngine.UI.Text>().text = "Price "+ upgradePrice;
        Item.transform.Find("Cash/Text").GetComponent<UnityEngine.UI.Text>().text = "Cash " + money;
        //print(currentItemLevel);
        showStars(currentItemLevel);

        if (currentItemLevel < 10) {
            Item.transform.Find("UpgradeButton").GetComponent<Button>().interactable = true;
        }
        else
        {
            Item.transform.Find("UpgradeButton").GetComponent<Button>().interactable = false;
        }
        if (money< upgradePrice) {
            Item.transform.Find("UpgradeButton").GetComponent<Button>().interactable = false;
        }
    }

    public void upgradeTransaction()
    {
        //money -= upgradePrice;
        //currentItemLevel++;

        //money = int.Parse(PlayerPrefs.GetString("MoneyP0"));
        int CurrentItemLevelVar = int.Parse(PlayerPrefs.GetString(itemToUpgrade + "P0"));
        int CarLevelVar = int.Parse(PlayerPrefs.GetString("CarP0"));

        GetComponent<Shopping>().UpgradeTransaction(0, CarLevelVar, CurrentItemLevelVar, itemToUpgrade);
        //PlayerPrefs.SetString("MoneyP0", "" + money);
        //PlayerPrefs.SetString(itemToUpgrade+"P0", "" + currentItemLevel);
        upgradeCar(itemToUpgrade);
    }

    private void showStars (int count)
    {
        for (var i = 0; i < Stars.Count; i++)
        {
        //    tempString = "Star" + i;
            Stars[i].SetActive(false);
        }

        for (var i=0;i< count; i++)
        {
       //     tempString = "Star" + i;
            Stars[i].SetActive(true);
        }

    }


    public void backToUpgradeList ()
    {
        Item.SetActive(false);
        ItemList.SetActive(true);
    }




    public void ClearViews()
    {
        NewCar.SetActive(false);
        PanelScores.SetActive(false);
        RaceChoose.SetActive(false);
        Item.SetActive(false);
        ItemList.SetActive(false);
        InfoText.SetActive(false);
        AmmoItems.SetActive(false);
    }

    public void Scores ()
    {
        ClearViews();
        //   visibleMenu = "Player list";
        //   GetComponent<PlayerList>().enabled = true;
        visibleMenu = "None";
        PanelScores.SetActive(true);
        MainMenuButtons.SetActive(false);
        BackButton.SetActive(true);
        CameraEffects.SetActive(false);
    }

    public void Race ()
    {
        ClearViews();
        visibleMenu = "RaceChoose";
        GetComponent<RaceChoose>().enabled = true;
        GetComponent<RaceChoose>().setUp();
        MainMenuButtons.SetActive(false);
        //BackButton.SetActive(true);
        CameraEffects.SetActive(false);
        RaceChoose.SetActive(true);
    }

    public void MainMenu()
    {
        visibleMenu = "None";
        Loading.SetActive(true);
        loading = true;
        MainMenuButtons.SetActive(false);
        BackButton.SetActive(false);
//        CameraEffects.SetActive(false);
    }

    public void Back()
    {
        ClearViews();
        BackButton.SetActive(false);
        MainMenuButtons.SetActive(true);
        CameraEffects.SetActive(true);
    }

    /*
    void OnGUI() {

        if (visibleMenu == "MainStage")
        {
            GetComponent<PlayerList>().enabled = false;

            GUILayout.BeginVertical("box");
            GUILayout.Box("Menu");

            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Race", GUILayout.Width(200),GUILayout.Height(100)))
            {
                //Application.LoadLevel("testScene");
                visibleMenu = "RaceChoose";
                GetComponent<RaceChoose>().enabled = true;
                GetComponent<RaceChoose>().setUp();

            }

            if (GUILayout.Button("Player list", GUILayout.Width(200), GUILayout.Height(100)))
            {
                visibleMenu = "Player list";
                GetComponent<PlayerList>().enabled = true;
            }

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal("box");

            if (GUILayout.Button("Race", GUILayout.Width(200), GUILayout.Height(100)))
            {
                Application.LoadLevel("testScene");
            }

            if (GUILayout.Button("Some menu", GUILayout.Width(200), GUILayout.Height(100)))
            {
                Application.LoadLevel("testScene");
            }

            GUILayout.EndHorizontal();


            GUILayout.EndVertical();


        }


        if (visibleMenu == "RacesResults")
        {


            GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.04f;
            GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.04f;

            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 800, 10, 600, Screen.height - 40), scrollPosition, new Rect(0, 0, 220, 3600));

            GUILayout.BeginVertical("box");
            GUILayout.Box("LIST OF WINNERS");

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("MainMenu", GUILayout.Width(200), GUILayout.Height(100)))
            {
                visibleMenu = "MainStage";
            }
            GUILayout.EndHorizontal();


            for (var i = 0; i < 100; i++)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Box("Player id " + i + "got " + PlayerPrefs.GetString("GetPointsP" + i) + " points and " + PlayerPrefs.GetString("GetMoneyP" + i) + " money", GUILayout.Width(400));
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUI.EndScrollView();





        }



    }
    */
}

