using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class MainMenu : MonoBehaviour
{
    public GameObject ContinueButton;
    public GameObject NewGameButton;
    public GameObject ExitButton;
    public GameObject Loading;
    public GameObject AreYouSure;
    public GameObject Permisssion;
    public GameObject PermisssionDenied;
    public GameObject Version;

    private bool start;
    private string fieldName;
    private string fieldValue;
    private bool loading;
    public GameObject Camera;
    private bool startAsyncLoading = false;
    private string GamePlayerName="";

    public GameObject AudioSourceObj;
    public GameObject MusicButton;


    public void GrantStoragePermission ()
    {
        /*
        string GameDataVar = PlayerPrefs.GetString("GameData");
        if (GameDataVar == "Yes")
        {
            AreYouSureFnc();
        }
        else
        {
            NewGame();
        }
        */
        Permisssion.SetActive(false);

    }

    public void DenyStoragePermission ()
    {
        Permisssion.SetActive(false);
        ExitGame();
    }


    // Use this for initialization
    void Start()
    {
       // Debug.Log("Audio source "+ AudioSourceObj);


        if (PlayerPrefs.GetString("Music")=="Off")
        {
            AudioSourceObj.SetActive(false);
            MusicButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music Off";
        }
        else
        {
            AudioSourceObj.SetActive(true);
            MusicButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music On";
        }



        //print(Application.version);
        Version.GetComponent<UnityEngine.UI.Text>().text = ""+Application.version;


        CheckPermission();


        start = true;
        Camera.GetComponent<VignetteAndChromaticAberration>().intensity = 1.0f;

        Loading.SetActive(false);

        if (PlayerPrefs.GetString("GameData") != "Yes")
        {
            ContinueButton.SetActive(false);
        }

    }

    public void ContinueGame()
    {
        for (var i = 0; i < 100; i++)
        {

            fieldName = "NameP" + i;
            //fieldValue = "Player " + i;

            fieldValue = playerName(i);
            PlayerPrefs.SetString(fieldName, fieldValue);
        }


            loading = true;
        ContinueButton.SetActive(false);
        NewGameButton.SetActive(false);
        ExitButton.SetActive(false);
        Loading.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void CheckPermission()
    {
        Debug.Log("check permisssion " + GetComponent<MyPermissionCheck>().CheckPermissions());
        if (!GetComponent<MyPermissionCheck>().CheckPermissions())
        {
            Debug.LogWarning("Missing permission to browse device gallery, please grant the permission first");
            Permisssion.SetActive(true);
            print("permission denied");
            // Your code to show in-game pop-up with the explanation why you need this permission (required for Google Featuring program)
            // This pop-up should include a button "Grant Access" linked to the function "OnGrantButtonPress" below
            //return;
        }
        else
        {
            print("permission granted");
        }

    }

    public void CheckIfDataExist()
    {
        string GameDataVar = PlayerPrefs.GetString("GameData");

        if (GameDataVar == "Yes")
        {
            AreYouSureFnc();
        }
        else
        {
            NewGame();
        }
    }


    public void AreYouSureFnc()
    {
        ContinueButton.SetActive(false);
        NewGameButton.SetActive(false);
        ExitButton.SetActive(false);
        AreYouSure.SetActive(true);
    }

    public void AreYouSureFncYes()
    {
        AreYouSure.SetActive(false);
        NewGame();
    }

    public void AreYouSureFncNo()
    {
        ContinueButton.SetActive(true);
        NewGameButton.SetActive(true);
        ExitButton.SetActive(true);
        AreYouSure.SetActive(false);
    }

    public void NewGame()
    {
        for (var i = 0; i < 100; i++)
        {

            fieldName = "NameP" + i;
            //fieldValue = "Player " + i;

            fieldValue = playerName(i);
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "MoneyP" + i;
            fieldValue = ""+((10*i)+800);

            
            if (i==0)
            {
                fieldValue = "800"; 
            }
            
            

            PlayerPrefs.SetString(fieldName, fieldValue);


            
            if (i<100){fieldValue = "1";}
            if (i < 90) { fieldValue = "2"; }
            if (i < 80) { fieldValue = "3"; }
            if (i < 70) { fieldValue = "" + Random.Range(3, 4); }
            if (i < 60) { fieldValue = "" + Random.Range(3, 5); }
            if (i < 50) { fieldValue = "" + Random.Range(4, 6); }
            if (i < 40) { fieldValue = "" + Random.Range(4, 6); }
            if (i < 30) { fieldValue = "" + Random.Range(5, 7); }
            if (i < 20) { fieldValue = "" + Random.Range(5, 8); }
            if (i<11){fieldValue = ""+Random.Range(5, 9); }
            if (i ==0) { fieldValue = "1"; }
            
            //fieldValue = "1";

            fieldName = "CarP" + i;
            //fieldValue = "1";
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "ColorP" + i;
            fieldValue = "Blue";
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "EngineP" + i;
            fieldValue = "1";
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "TiresP" + i;
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

            fieldName = "AirStrikeNextRaceP" + i;
            fieldValue = "1";
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "DefendStrikeNextRaceP" + i;
            fieldValue = "1";
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "NitroNextRaceP" + i;
            fieldValue = ""+Random.Range(0,10);
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "MinesNextRaceP" + i;
            fieldValue = "0"; 
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "MissilesNextRaceP" + i;
            fieldValue = "0";
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "HomingMissilesNextRaceP" + i;
            fieldValue = "0";
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "PointsP" + i;
            fieldValue = ""+ playerPoints(i);
            PlayerPrefs.SetString(fieldName, fieldValue);

            fieldName = "LeagueP" + i;
            fieldValue = "1";
            //tu bedzie funkcja losujaca kolory
            PlayerPrefs.SetString(fieldName, fieldValue);
        }
        PlayerPrefs.SetString("GameData", "Yes");
        loading = true;
        ContinueButton.SetActive(false);
        NewGameButton.SetActive(false);
        ExitButton.SetActive(false);
        Loading.SetActive(true);
    }


    public int playerPoints (int i)
    {
        int PointsVar=0;


            if (i<50)
            {
                PointsVar = 501 - i;
            }
            else
            {
                PointsVar = 0;
            }

            if (i==0)
            {
                PointsVar = 0;
            }
        /*
        if (i < 11)
        {
            PointsVar = 100 - i;
        }
        */
        return PointsVar;
    }


    static string playerName(int i) { 
        string variable;
            
        switch (i)
        {
            case 0: variable = "YOU"; break;
            case 1: variable = "Dark Bob"; break;
            case 2: variable = "Hell Arnold"; break;
            case 3: variable = "Furious Jack"; break;
            case 4: variable = "Shady Veronica"; break;
            case 5: variable = "Thunder Phill"; break;
            case 6: variable = "Firestorm Mark"; break;
            case 7: variable = "Blitz Jim"; break;
            case 8: variable = "Mad Maddox"; break;
            case 9: variable = "Hellrazor Bob"; break;
            case 10: variable = "Apache Nick"; break;
            case 11: variable = "Fury Mike"; break;
            case 12: variable = "Angry Steven"; break;
            case 13: variable = "Evil Mat"; break;
            case 14: variable = "Bazooka Albert"; break;
            case 15: variable = "Random Tom"; break;
            case 16: variable = "Devil Desmond"; break;
            case 17: variable = "Black Alfred"; break;
            case 18: variable = "Cop Axel"; break;
            case 19: variable = "Famous Barry"; break;
            case 20: variable = "Heavy Benjamin"; break;
            case 21: variable = "Black Jack"; break;
            case 22: variable = "Handy Blake"; break;
            case 23: variable = "Cold Boris"; break;
            case 24: variable = "Hot Carmen"; break;
            case 25: variable = "Simply Brian"; break;
            case 26: variable = "Octopus Marlon"; break;
            case 27: variable = "Vanilla Brooke"; break;
            case 28: variable = "Silent Carl"; break;
            case 29: variable = "Red Casey"; break;
            case 30: variable = "Wooden Cecil"; break;
            case 31: variable = "Alabana Claude"; break;
            case 32: variable = "Border Colin"; break;
            case 33: variable = "Chip Dale"; break;
            case 34: variable = "Smoke Cyril"; break;
            case 35: variable = "Racer Claude"; break;
            case 36: variable = "Stupid Dalton"; break;
            case 37: variable = "Parnsley Elvis"; break;
            case 38: variable = "Mountain Chris"; break;
            case 39: variable = "Flaffy Dean"; break;
            case 40: variable = "Fountain Dennis"; break;
            case 41: variable = "Hill Derek"; break;
            case 42: variable = "Angel Dexter"; break;
            case 43: variable = "Sunny Marry"; break;
            case 44: variable = "Wolf Drake"; break;
            case 45: variable = "Eagle Duncan"; break;
            case 46: variable = "Green Elias"; break;
            case 47: variable = "Brown Leo"; break;
            case 48: variable = "Round Elmer"; break;
            case 49: variable = "Fifty Cecil"; break;
            case 50: variable = "Forest Erwin"; break;
            case 51: variable = "Wasabi Sam"; break;
            case 52: variable = "Pissing Fabian"; break;
            case 53: variable = "Music Frederick"; break;
            case 54: variable = "Freeman Frank"; break;
            case 55: variable = "Demon Gale"; break;
            case 56: variable = "Monkey George"; break;
            case 57: variable = "Freaky Will"; break;
            case 58: variable = "Cucumber Glenn"; break;
            case 59: variable = "Tomato Tom"; break;
            case 60: variable = "South Henry"; break;
            case 61: variable = "Plain Homer"; break;
            case 62: variable = "Beauty Igor"; break;
            case 63: variable = "French Ivet"; break;
            case 64: variable = "Cacao Jacob"; break;
            case 65: variable = "Bimber Justin"; break;
            case 66: variable = "Electro Jarvis"; break;
            case 67: variable = "Mini Kane"; break;
            case 68: variable = "Ball Kendrick"; break;
            case 69: variable = "Nerdy Kerry"; break;
            case 70: variable = "Brute Lee"; break;
            case 71: variable = "Yellow Leslie"; break;
            case 72: variable = "Love Monica"; break;
            case 73: variable = "Queen Lyndon"; break;
            case 74: variable = "Aqua Tom"; break;
            case 75: variable = "Water Marcus"; break;
            case 76: variable = "Banana Moris"; break;
            case 77: variable = "Steel Neal"; break;
            case 78: variable = "Hamster Norris"; break;
            case 79: variable = "Bread Owen"; break;
            case 80: variable = "Wild Perry"; break;
            case 81: variable = "Infamous Pierce"; break;
            case 82: variable = "Armor Rex"; break;
            case 83: variable = "Blank Roland"; break;
            case 84: variable = "Tree Roy"; break;
            case 85: variable = "Bombastic Ross"; break;
            case 86: variable = "Wonder Rufus"; break;
            case 87: variable = "Violet Alice"; break;
            case 88: variable = "Funny Alvin"; break;
            case 89: variable = "Onion Andrew"; break;
            case 90: variable = "Small John"; break;
            case 91: variable = "Big Ben"; break;
            case 92: variable = "Speedy Jimmy"; break;
            case 93: variable = "SLow Jane"; break;
            case 94: variable = "Balloon Mary"; break;
            case 95: variable = "Engine Stan"; break;
            case 96: variable = "Tiger Bob"; break;
            case 97: variable = "Doggy Gregory"; break;
            case 98: variable = "Lazy Lisa"; break;
            case 99: variable = "Pancake Tom"; break;
            default: variable= "Player" + i;break;
        }
        return variable;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (start)
        {
            if (Camera.GetComponent<VignetteAndChromaticAberration>().intensity > 0.1f)
            {
        //        Camera.GetComponent<VignetteAndChromaticAberration>().intensity -= 0.01f;
            }
            else
            {
                start = false;
            }
        }
        

        if (loading)
        {
            if (!startAsyncLoading)
            {
                //StartCoroutine(LevelCoroutine("garage"));
                startAsyncLoading = true;
                GetComponent<LoadScene>().LoadLevel("garage");
            }

            start = false;
            //Camera.GetComponent<ScreenSpaceAmbientOcclusion>().enabled = true;
            /*
            if (Camera.GetComponent<VignetteAndChromaticAberration>().intensity < 1.0f)
            {
          //      Camera.GetComponent<VignetteAndChromaticAberration>().intensity += 0.01f;
            }
            else
            {
                //Application.LoadLevel("garage");

//                GetComponent<LoadScene>().fadeIn = true;
                //StartCoroutine(LoadLevel("garage"));
                if (!startAsyncLoading)
                {
                    //StartCoroutine(LevelCoroutine("garage"));
                    startAsyncLoading = true;
                    GetComponent<LoadScene>().LoadLevel("garage");
                }
            }
            */

        }



    }
    /*
    private IEnumerator LoadLevel(string levelName)
    {
        async = Application.LoadLevelAsync(levelName);
        Debug.Log(async.progress);
        yield return async;
    }
    */
    /*
    IEnumerator LoadLevel()
    {
        AsyncOperation async = Application.LoadLevelAsync("garage");

        while (!async.isDone)
        {
            //levelProgressBar.sliderValue = async.progress;
            Debug.Log(async.progress);
            yield return null;
        }
    }
    */
}


