using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;
using UnityEngine.UI;

public class CarStats : MonoBehaviour
{
    private GameObject Global;
    public GameObject GunHitPrefab; 
    public int racerId;
    // [SyncVar(hook = "OnChangeHull")] // UNet removed in Unity 6
    public int Hull = 100;
    public int Ammo =100;
    public int Mines;
    public int Missiles=10;
    public int HomingMissiles=10;
    public int Cash;
    public int StageCollectedCashVar=0;
    public int Nitro;
    public int HullMax;
    public int Tires;
    public int Engine;
    public int Armor;
    public int Gun;
    public int Points;
    public int Car;
    public String RacerName;
    //public String CarType;
    public String CarName;
    public string carColor = "yellow";
    public Transform target;
    private GameObject Fire;
    public Boolean wrecked = false;
    public Boolean removeFromStage = false;
    private GameObject RemoveAnimationObject;
    private Boolean removeAnimationExist = false;
    private Boolean removeCoroutinePlayed = false;
    private Boolean liftUp = false;

    private bool setWreckedView = false;

    public int currentLap = 0;
    public int waypointsToGo = 1000;
    public int waypointsCleared = 0;
    public int waypointsToFinish = 1000;
    //public int waypointsLasts = 0;
    public Boolean startWaypointReached = false;
    public Boolean raceFinished = false;
    private Boolean addedToFinishedList = false;
    private Boolean wreckedContainer = false;


    public bool readyForFinish = false;
    public bool finishLineCrossed = false;


    public bool NetworkPlayer;

    //debug variables
    public float speed;

    private GameObject MissileButton;
    private GameObject HomingMissileButton;
    private GameObject MineButton;

    public bool MissilesButtonInteractableVar;
    public bool HomingMissilesButtonInteractableVar;
    public bool MinesButtonInteractableVar;
    public bool NotMovieVar=true;


    public void loadRacerData ()
    {

        //Missiles = 10;
       // HomingMissiles = 10;

        print("load racer data");
        RacerName = PlayerPrefs.GetString("NameP"+racerId);
        CarName = PlayerPrefs.GetString("CarP" + racerId);
        Car = int.Parse(PlayerPrefs.GetString("CarP" + racerId));
        Hull = 8000+(int.Parse(PlayerPrefs.GetString("ArmorP" + racerId))*400)+(Car*800);
        HullMax = Hull;
        Armor = int.Parse(PlayerPrefs.GetString("ArmorP" + racerId));
        Gun = int.Parse(PlayerPrefs.GetString("GunP" + racerId));
        Ammo = (int.Parse(PlayerPrefs.GetString("GunP" + racerId))*30)*Car;
        Mines = 4+Car + int.Parse(PlayerPrefs.GetString("MinesNextRaceP" + racerId))*5;
        PlayerPrefs.GetString("MinesNextRaceP", "0");
        HomingMissiles = 4+Car + int.Parse(PlayerPrefs.GetString("HomingMissilesNextRaceP" + racerId))*5;
        PlayerPrefs.GetString("HomingMissilesNextRaceP", "0");
        Missiles = 4+Car + int.Parse(PlayerPrefs.GetString("MissilesNextRaceP" + racerId))*5;
        PlayerPrefs.GetString("MissilesNextRaceP", "0");
        Nitro = int.Parse(PlayerPrefs.GetString("NitroNextRaceP" + racerId));
        PlayerPrefs.GetString("NitroNextRaceP", "0");
       // print("Tires");
       // print("TiresP" + racerId);
       //  print(PlayerPrefs.GetString("TiresP" + racerId));
        Tires = int.Parse(PlayerPrefs.GetString("TiresP" + racerId));
        Engine = int.Parse(PlayerPrefs.GetString("EngineP" + racerId));
        Cash = int.Parse(PlayerPrefs.GetString("MoneyP" + racerId));
        Points = int.Parse(PlayerPrefs.GetString("PointsP" + racerId));
        //Debug.Log("RacerName "+RacerName); 

        /*
        if (racerId == 0)
        {
            HullMax = 10000;
            Hull = 10000;
            Tires = 10;
            Car = 10;
        }
        */

        GetComponent<CarController>().m_Topspeed = GetComponent<CarController>().m_Topspeed + (Engine * 10);


        // PlayerPrefs.SetString("MinesNextRaceP" + racerId, "10");
        // PlayerPrefs.SetString("HomingMissilesNextRaceP" + racerId, "10");
        // PlayerPrefs.SetString("MissilesNextRaceP" + racerId, "10");
        //HomingMissiles = 10;
        //Missiles = 10;
        //Mines = 10;
       // Hull = 10000;
       // HullMax = 10000;

        if (racerId == 0)
        {
             //Hull = 100000;
             //HullMax = 100000;
            //Car = 2;
            //  Engine = 10;
            GetComponent<CarController>().m_FullTorqueOverAllWheels = GetComponent<CarController>().m_FullTorqueOverAllWheels + (Engine * 20) + (Car * 50) + (Tires * 10);
            GetComponent<CarController>().m_SlipLimit = GetComponent<CarController>().m_SlipLimit + (0.5f * Tires * Car);
            GetComponent<CarController>().m_SteerHelper = GetComponent<CarController>().m_SteerHelper + (0.005f * Tires * Car)+0.1f;
            GetComponent<CarController>().m_ReverseTorque = 500f;

        }
        else
        {
            //PlayerPrefs.SetString("MinesNextRaceP" + racerId, "0");
            //PlayerPrefs.SetString("HomingMissilesNextRaceP" + racerId, "0");
            //PlayerPrefs.SetString("MissilesNextRaceP" + racerId, "0");

            //  HullMax = 10000;
            //  Hull = 10000;
            //  Car = 10;
            //  Engine = 10;
            GetComponent<CarAudio>().enabled = false;
            GetComponent<CarController>().m_SlipLimit = GetComponent<CarController>().m_SlipLimit + 150 + (10f * Tires * Car);
            GetComponent<CarController>().m_FullTorqueOverAllWheels = GetComponent<CarController>().m_FullTorqueOverAllWheels + (Engine * 50) + (Car * 200) + (Tires * 50);
        }

    }

    public void TakeDamage(int amount)
    {

        if (Car > 1)
        {
            amount = amount - (Car * 200);
            if (amount<0) { amount = 10; }
        }



        if (GetComponent<CarAIControl>())
        {
            GetComponent<CarAIControl>().cantReachWaypointCounter = 0;
        }


        // UNet isServer removed in Unity 6 - damage now applied locally
        // if (NetworkPlayer)
        // {
        //     if (!isServer)
        //         return;
        // }

        Hull -= amount;
        if (Hull <= 0)
        {
            Hull = 0;
            Debug.Log("Dead!");
        }
        if (racerId == 0) RaceEvents.FireHullChanged(Hull, HullMax);
    }

    void OnChangeHull(int hull)
    {
        Hull = hull;
        //healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }


    // Use this for initialization
    void Start () {

        Global = GameObject.Find("Global");
        MissileButton = Global.GetComponent<Stage>().MissileButton;
        HomingMissileButton = Global.GetComponent<Stage>().HomingMissileButton;
        MineButton = Global.GetComponent<Stage>().MineButton;


        wrecked = false;
        raceFinished = false;

        NetworkPlayer = GetComponent<IsNetworkPlayer>().NetworkPlayer;

        //Hull = 100;
        //Ammo = 20;
        //Mines = 5;
        //HullMax = 1000;
        Fire = transform.Find("ETF_M_Fire Stream").gameObject;
        Fire.SetActive(false);
        CarName = gameObject.name;

        if (!NetworkPlayer)
        {
            GameObject.Find("Global").GetComponent<Stage>().listOfCars.Add(transform.gameObject);
        }
        //Przy network to musi ogarniać server czyli na serverze jest lista graczy
    }

    // Update is called once per frame

/*
    void addToRaceEnd()
    {
        GameObject.Find("Global").GetComponent<Stage>().listOfRaceEnd.Add(transform.gameObject);
    }
    */


    void Update () {
        speed = GetComponent<CarController>().m_Rigidbody.linearVelocity.magnitude;
        if (racerId == 0)
        {
            RaceEvents.FireSpeedChanged(GetComponent<CarController>().CurrentSpeed);
        }
        /*
        if (racerId == 0 && Global.GetComponent<Stage>().WeaponsEnabled)
        {
            MissileButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Missiles;
            HomingMissileButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + HomingMissiles;
            MineButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Mines;
        }
        */
        
        if (racerId == 0 && Global.GetComponent<Stage>().WeaponsEnabled)
        {


            if (Missiles <= 0)
            {
                MissileButton.SetActive(false);
                MissilesButtonInteractableVar=true;
            }
            else
            {
                MissileButton.SetActive(true);
                MissileButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Missiles;
                if (MissilesButtonInteractableVar)
                {
                    MissileButton.GetComponent<Button>().interactable = true;
                    MissileButton.GetComponent<CanvasGroup>().alpha = 100;

                }
            }
            if (HomingMissiles <= 0)
            {
                HomingMissileButton.SetActive(false);
                HomingMissilesButtonInteractableVar = true;
            }
            else
            {
                HomingMissileButton.SetActive(true);
                HomingMissileButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + HomingMissiles;
                if (HomingMissilesButtonInteractableVar)
                {
                    HomingMissileButton.GetComponent<Button>().interactable = true;
                    HomingMissileButton.GetComponent<CanvasGroup>().alpha = 100;

                }
            }
            if (Mines <= 0)
            {
                MineButton.SetActive(false);
                MinesButtonInteractableVar = true;
            }
            else
            {
                MineButton.SetActive(true);
                MineButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Mines;
                if (MinesButtonInteractableVar)
                {
                    MineButton.GetComponent<Button>().interactable = true;
                    MineButton.GetComponent<CanvasGroup>().alpha = 100;

                }
            }

            // UI Toolkit ammo events
            RaceEvents.FireMissileCountChanged(Missiles);
            RaceEvents.FireHomingMissileCountChanged(HomingMissiles);
            RaceEvents.FireMineCountChanged(Mines);
        }
        




        waypointsToFinish = waypointsToGo - waypointsCleared;

        if (readyForFinish==true&&finishLineCrossed==true)
        {
            raceFinished = true;
            if (!addedToFinishedList)
            {
                Global.GetComponent<Stage>().listOfWinners.Add(transform.gameObject);
                Global.GetComponent<Stage>().listOfRaceEnd.Add(transform.gameObject);
                addedToFinishedList = true;
            }

        }


        if (waypointsToFinish <= 0)
        {
            readyForFinish = true;
        }

        /*
        if (Hull<=0)
        {
            GameObject.Find("Global").GetComponent<Stage>().listOfRaceEnd.Add(transform.gameObject);
        }
        */

        if (Hull<=0&&!wreckedContainer)
        {
            Fire.SetActive(true); 
            Ammo = 0;
            Mines = 0;
            //GetComponent<CarAIControl>().enabled = false;

            if (!wrecked)
            {
                Global.GetComponent<Stage>().listOfRaceEnd.Add(transform.gameObject);
            }
            wrecked = true;


            if (!setWreckedView)
            {
                setWreckedView = true;
                transform.Find("MissileTarget").gameObject.SetActive(false);
                PlayerPrefs.SetString("MinesNextRaceP" + racerId,"0");
                PlayerPrefs.SetString("HomingMissilesNextRaceP" + racerId,"0");
                PlayerPrefs.SetString("MissilesNextRaceP" + racerId,"0");
                RemoveAnimationObject = Instantiate(Resources.Load("ETF_M_Fire_03", typeof(GameObject))) as GameObject;
                RemoveAnimationObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                RemoveAnimationObject.transform.parent = transform;
            }

            /*
            if (!removeCoroutinePlayed)
            {
                transform.Find("MissileTarget").gameObject.SetActive(false);
                StartCoroutine("remove");
            }
            */
            //removeFromStage = true;

            /*
            RemoveAnimationObject = Instantiate(Resources.Load("ETF_M_Fire_03", typeof(GameObject))) as GameObject;
            RemoveAnimationObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            RemoveAnimationObject.transform.parent = transform;
            */


        }

        if (removeFromStage)
        {

            if (!removeAnimationExist)
            {
                RemoveAnimationObject = Instantiate(Resources.Load("ETF_M_Cyclone", typeof(GameObject))) as GameObject;
                RemoveAnimationObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                RemoveAnimationObject.transform.parent = transform;
                //removeFromStage = false;
                removeAnimationExist = true;
            }
        
            if (liftUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            }

        }



    }

    /*
    void OnGUI()
    {
        
        if (NotMovieVar)
        {

            if (!GetComponent<CarStats>().wrecked || !GetComponent<CarStats>().raceFinished)
            {

                //GUI.backgroundColor = Color.yellow;
                Vector3 targetPos;
                //print(Camera.main);
                //targetPos = Camera.main.WorldToViewportPoint(transform.position);
                targetPos = Camera.main.WorldToScreenPoint(transform.position);
                //print(targetPos.x);
                //print(targetPos.y);
                //print(Screen.height / 2);


                Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;


                //        GUI.Box(new Rect(targetPos.x-50, targetPos.y-50, 60, 20), ""+Hull);

                if (onScreen && (Vector3.Distance(Camera.main.transform.position, transform.position) < 50.0f))
                {


                    if (transform.tag == "Player")
                    {
                        GUI.Box(new Rect(targetPos.x - 80, Screen.height - targetPos.y + 50, 160, 20), "Hull " + Hull);
                        GUI.Box(new Rect(targetPos.x - 80, Screen.height - targetPos.y + 70, 160, 20), "Gun ammo " + Ammo);
                        //GUI.Box(new Rect(targetPos.x - 30, Screen.height - targetPos.y + 90, 160, 20), "Mines " + Mines);
                        //GUI.Box(new Rect(targetPos.x - 100, Screen.height - targetPos.y + 110, 200, 20), "" + (waypointsToGo - waypointsCleared));
                    }
                    else
                    {
                        //print(Vector3.Distance(Camera.main.transform.position, transform.position));
                        //GUI.Box(new Rect(targetPos.x - 100, Screen.height - targetPos.y - 130, 200, 20), "" + (waypointsToGo - waypointsCleared));
                        GUI.Box(new Rect(targetPos.x - 100, Screen.height - targetPos.y - 110, 200, 20), "Name " + RacerName);
                        GUI.Box(new Rect(targetPos.x - 80, Screen.height - targetPos.y - 90, 160, 20), "Hull " + Hull);
                        //GUI.Box(new Rect(targetPos.x - 30, Screen.height - targetPos.y - 70, 160, 20), "Ammo " + Ammo);
                        //GUI.Box(new Rect(targetPos.x - 30, Screen.height - targetPos.y - 50, 160, 20), "Mines " + Mines);
                    }

                }

            }

        }
        
        
    }
    */

    IEnumerator remove()
    {
        removeCoroutinePlayed = true;
 //       yield return new WaitForSeconds(5.0f);
        GetComponent<Rigidbody>().isKinematic = true;
        liftUp = true;
        yield return new WaitForSeconds(5.0f);
        GameObject WreckedContainer;
        WreckedContainer = GameObject.Find("WreckedCarContainer");
        liftUp = false;
        transform.position = new Vector3(WreckedContainer.transform.position.x, WreckedContainer.transform.position.y, WreckedContainer.transform.position.z);
        transform.parent = WreckedContainer.transform;
        RemoveAnimationObject.SetActive(false);
        wreckedContainer = true;
        Fire.SetActive(false);
        GetComponent<CarController>().enabled = false;
        GetComponent<CarAIControl>().enabled = false;


    }


}
