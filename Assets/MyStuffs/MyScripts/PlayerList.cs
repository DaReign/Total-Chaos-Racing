using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour {
    public Vector2 scrollPosition = Vector2.zero;
    public int[] sortedByMoney;
    public int[] sortedByPoints;
    public int[] sortedBy;

    public int[] PlayerIdPlaceByPoints;

    //public string NamePString;

    //public List<string[]> data = new List<string[]>() { NamePString, list2, list3, list4 };

    //public List<GameObject> sortedByPoints = new List<GameObject>();
    private int temp;
    private int i;
    private int ii;
    // Use this for initialization
    public string[,] PlayersData = new string[100, 12];
    //string[,] Tablero = new string[3, 3];

    public List<GameObject> scoresList = new List<GameObject>();
    private GameObject ScoresObject;
    public GameObject Content;


    void Start () {
        for (var i = 0; i < 100; i++)
        {
            ScoresObject = Instantiate(Resources.Load("Line", typeof(GameObject))) as GameObject;
            ScoresObject.transform.parent = Content.transform;
            scoresList.Add(ScoresObject);
            scoresList[i].transform.Find("Position/Text").GetComponent<UnityEngine.UI.Text>().text = "" + (i + 1);
        }


        /*
        for (i = 0; i < 100; i++)
        {
            PlayerPrefs.SetString("PointsP" + i, "" + (100 - i));
        }
        */
        sort();
        updateScoresList();

    }


    public void sort()
    {
        //myArray[0, 1] = "test";

        //print("myArray[0, 1]");
        //print(myArray[0, 1]);
        



        sortedByMoney = new int[100];
        sortedByPoints = new int[100];
        sortedBy = new int[100];
        PlayerIdPlaceByPoints = new int[100];

        for (i = 0; i < 100; i++)
        {
            sortedByMoney[i] = i;
            sortedByPoints[i] = i;
            sortedBy[i] = i;

            for (ii = 0; ii < 12; ii++)
            {
                if (ii == 0)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("NameP" + i);
                }
                if (ii == 1)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("MoneyP" + i);
                }
                if (ii == 2)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("CarP" + i);
                }
                if (ii == 3)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("ColorP" + i);
                }
                if (ii == 4)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("EngineP" + i);
                }
                if (ii == 5)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("TireP" + i);
                }
                if (ii == 6)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("ArmorP" + i);
                }
                if (ii == 7)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("GunP" + i);
                }
                if (ii == 8)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("NitroNextRaceP" + i);
                }
                if (ii == 9)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("MinesNextRaceP" + i);
                }
                if (ii == 10)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("PointsP" + i);
                }
                if (ii == 11)
                {
                    PlayersData[i, ii] = PlayerPrefs.GetString("LeagueP" + i);
                }
            }

        }


        /*
        for (i = 0; i < 100; i++)
        {
            print("Player");
            print(i);
            for (ii = 0; ii < 12; ii++)
            {
                print(PlayersData[i, ii]);
            }
        }
        */


        for (var write = 0; write < sortedByMoney.Length; write++)
        {
            for (var sort = 0; sort < sortedByMoney.Length - 1; sort++)
            {
                if (int.Parse(PlayerPrefs.GetString("MoneyP" + sortedByMoney[sort])) < int.Parse(PlayerPrefs.GetString("MoneyP" + sortedByMoney[sort + 1])))
                {
                    temp = sortedByMoney[sort + 1];
                    sortedByMoney[sort + 1] = sortedByMoney[sort];
                    sortedByMoney[sort] = temp;
                }
            }
        }

        /*
        print("Sorted by money");
        for (i = 0; i < 100; i++)
        {
           Debug.Log("ID "+sortedByMoney[i]+" money "+ PlayerPrefs.GetString("MoneyP" + sortedByMoney[i]));
            
        }
        */

        for (var write = 0; write < sortedByPoints.Length; write++)
        {
            for (var sort = 0; sort < sortedByPoints.Length - 1; sort++)
            {
                if (int.Parse(PlayerPrefs.GetString("PointsP" + sortedByPoints[sort])) < int.Parse(PlayerPrefs.GetString("PointsP" + sortedByPoints[sort + 1])))
                {
                    temp = sortedByPoints[sort + 1];
                    sortedByPoints[sort + 1] = sortedByPoints[sort];
                    sortedByPoints[sort] = temp;
                }
            }
        }

        /*
        print("Sorted by points");
        for (i = 0; i < 100; i++)
        {
            Debug.Log("ID " + sortedByPoints[i] + " points " + PlayerPrefs.GetString("PointsP" + sortedByPoints[i]));

        }
        */

        //tablica w ktorej mam id od 0 do 100
        //każdemu przypisane jest ktore miejsce ma według punktow

        for (var i = 0; i < 100; i++)
        {
            for (var ii = 0; ii < 100; ii++)
            {
                if (sortedByPoints[ii] == i)
                {
                    PlayerIdPlaceByPoints[i] = ii;
                }

            }
        }

        /*
        print("test");   
        for (var i = 0; i < 100; i++)
        {
            print(PlayerIdPlaceByPoints[i]);
        }
        */

    }


    void updateScoresList()
    {
        for (var i = 0; i < 100; i++)
        {
            sortedBy[i] = sortedByPoints[i];
        }
        //PlayersData[sortedBy[i], 0]
        //0 name
        //10 points
        for (var i = 0; i < 100; i++)
        {
            scoresList[i].transform.Find("Position/Text").GetComponent<UnityEngine.UI.Text>().text = "" + (i + 1);
            scoresList[i].transform.Find("Name/Text").GetComponent<UnityEngine.UI.Text>().text = "" + PlayersData[sortedBy[i], 0];
            scoresList[i].transform.Find("Points/Text").GetComponent<UnityEngine.UI.Text>().text = "" + PlayersData[sortedBy[i], 10];
            scoresList[i].transform.Find("Gains").gameObject.SetActive(false);
            scoresList[i].GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1.0f);
            scoresList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(290.0f, 48.0f);
        }
    }


        // Update is called once per frame
    void Update () {		
	}

    void OnGUI()
    {
/*
        if (GUI.Button(new Rect(10, 50, 200, 50), "Sort Points"))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = sortedByPoints[i];
            }
        }

        if (GUI.Button(new Rect(10, 100, 200, 50), "Sort Money"))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = sortedByMoney[i];
            }
        }

        if (GUI.Button(new Rect(10, 150, 200, 50), "Sort Default"))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = i;
            }
        }
*/
/*

        if (GUI.Button(new Rect(10, 10, 100, 100), "Back"))
        {
            GetComponent<MainStage>().visibleMenu = "MainStage";
            GetComponent<PlayerList>().enabled = false;
        }

        GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.04f;
        GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.04f;

        scrollPosition = GUI.BeginScrollView(new Rect((Screen.width/2)-400, 10, 800, Screen.height-40), scrollPosition, new Rect(0, 0, 220,3600));
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Sort Points", GUILayout.Height(100)))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = sortedByPoints[i];
            }
        }

        if (GUILayout.Button("Sort Money", GUILayout.Height(100)))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = sortedByMoney[i];
            }
        }

        if (GUILayout.Button("Sort Default", GUILayout.Height(100)))
        {
            for (i = 0; i < 100; i++)
            {
                sortedBy[i] = i;
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginVertical("box");
        GUILayout.Box("LIST OF WINNERS");
        GUILayout.BeginHorizontal("box");
        GUILayout.Box("Name", GUILayout.Width(100));
        GUILayout.Box("MoneyP", GUILayout.Width(100));
        GUILayout.Box("CarP", GUILayout.Width(50));
        GUILayout.Box("ColorP", GUILayout.Width(50));
        GUILayout.Box("EngineP", GUILayout.Width(50));
        GUILayout.Box("TireP", GUILayout.Width(50));
        GUILayout.Box("ArmorP", GUILayout.Width(50));
        GUILayout.Box("GunP", GUILayout.Width(50));
        GUILayout.Box("NNRP", GUILayout.Width(50));
        GUILayout.Box("MNRP", GUILayout.Width(50));
        GUILayout.Box("PointsP", GUILayout.Width(50));
        GUILayout.Box("LeagueP", GUILayout.Width(50));
        GUILayout.EndHorizontal();

        for (var i = 0; i < 100; i++)
        {
            GUILayout.BeginHorizontal("box");
 //           PlayerPrefs.SetString("GetPointsP" + RaceData[i][it]
  //          GUILayout.Box("Player "+i+" got "+PlayerPrefs.GetString("GetPointsP" + i)+ " points and "+ PlayerPrefs.GetString("GetMoneyP" + i)+" money", GUILayout.Width(400));


            GUILayout.Box(PlayersData[sortedBy[i],0], GUILayout.Width(100));
            GUILayout.Box(PlayersData[sortedBy[i], 1], GUILayout.Width(100));
            GUILayout.Box(PlayersData[sortedBy[i], 2], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 3], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 4], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 5], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 6], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 7], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 8], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 9], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 10], GUILayout.Width(50));
            GUILayout.Box(PlayersData[sortedBy[i], 11], GUILayout.Width(50));
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        GUI.EndScrollView();

        */
    }

}
