using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Cameras;
using MyNamespace;
// using UnityEngine.Networking; // UNet removed in Unity 6

public class SetUpRace : MonoBehaviour {
    public List<GameObject> listOfStartingPlaces = new List<GameObject>();
    public GameObject StartingPlacesContainer;
    public GameObject Car;
    public List<GameObject> CarsList = new List<GameObject>();
    private int startingPoint;
    private int countDownSec = 5;
    private bool ready = false;
    private bool nextSec = true;
    private bool isfired = false;
    private GameObject BackToTrackObj;

    // Use this for initialization
    void Start () {
        BackToTrackObj = GameObject.Find("BackToTrack");
        if (BackToTrackObj) { BackToTrackObj.SetActive(false); }

        StartingPlacesContainer = GameObject.Find("StartingPlaceContainer");
        Debug.Log("StartingPlacesContainer "+ StartingPlacesContainer);
        //Screen.SetResolution(1280, 720, true);
        listOfStartingPlaces.Clear();
        foreach (Transform child in StartingPlacesContainer.transform)
        {
            print(child.gameObject);
            listOfStartingPlaces.Add(child.gameObject);
            //child is your child transform
        }



    }


    void delayedStart()
    {







        for (var i = 0; i < 10; i++)
        {
            startingPoint = UnityEngine.Random.Range(0, listOfStartingPlaces.Count - 1);

            //CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CarStarter", typeof(GameObject))) as GameObject);

            //int.Parse(GetComponent<RaceDrivers>().RacersIds[i])
            switch (int.Parse(PlayerPrefs.GetString("CarP"+GetComponent<RaceDrivers>().RacersIds[i])))
            {
                case 1: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Car1", typeof(GameObject))) as GameObject); break;
                case 2: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Sports_07", typeof(GameObject))) as GameObject); break;
                case 3: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/racing_car_05", typeof(GameObject))) as GameObject); break;
                case 4: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Racing_Car_01", typeof(GameObject))) as GameObject); break;
                case 5: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Sports_05", typeof(GameObject))) as GameObject); break;
                case 6: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/racing_car_03", typeof(GameObject))) as GameObject); break;
                case 7: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Super_Sports_01", typeof(GameObject))) as GameObject); break;
                case 8: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/racing_car_04", typeof(GameObject))) as GameObject); break;
                case 9: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/SuperCar04", typeof(GameObject))) as GameObject); break;
                case 10: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/s_Sports_02", typeof(GameObject))) as GameObject); break;
                default: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Car1", typeof(GameObject))) as GameObject); break;
            }


            //           PlayerPrefs.GetString("CarP"+ GetComponent<RaceDrivers>().RacersIds[i]);
            /*
            switch (Random.Range(0, 10))
            {
                case 0: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/C1GT2", typeof(GameObject))) as GameObject); break;
                case 1: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CC12", typeof(GameObject))) as GameObject); break;
                case 2: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CarsonCarbonGT", typeof(GameObject))) as GameObject); break;
                case 3: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CC", typeof(GameObject))) as GameObject); break;
                case 4: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CC2", typeof(GameObject))) as GameObject); break;
                case 5: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/CC12", typeof(GameObject))) as GameObject); break;
                case 6: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/FFC", typeof(GameObject))) as GameObject); break;
                case 7: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Infernus", typeof(GameObject))) as GameObject); break;
                case 8: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/Iridium", typeof(GameObject))) as GameObject); break;
                case 9: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/SLMCC", typeof(GameObject))) as GameObject); break;
                default: CarsList.Add(Instantiate(Resources.Load("Cars/Ready/FFC", typeof(GameObject))) as GameObject); break;

            }
            */
            
            CarsList[i].transform.position = new Vector3(listOfStartingPlaces[startingPoint].transform.position.x, listOfStartingPlaces[startingPoint].transform.position.y, listOfStartingPlaces[startingPoint].transform.position.z);
            listOfStartingPlaces.RemoveAt(startingPoint);
            CarsList[i].GetComponent<CarStats>().racerId = GetComponent<RaceDrivers>().RacersIds[i];

            if (CarsList[i].GetComponent<CarAddingsTest>().TestMode)
            {
                CarsList[i].GetComponent<CarAddingsTest>().setUpTestCarData(i);
            }
            else
            {
                CarsList[i].GetComponent<CarStats>().loadRacerData();
            }

            if (CarsList[i].GetComponent<IsNetworkPlayer>().NetworkPlayer==false)
            {
                // NetworkTransform removed in Unity 6 (UNet)
                // if (CarsList[i].GetComponent<NetworkTransform>())
                // {
                //     Destroy(CarsList[i].GetComponent<NetworkTransform>());
                // }
                if (CarsList[i].GetComponent<CameraRotateAroundPoint>())
                {
                    Destroy(CarsList[i].GetComponent<CameraRotateAroundPoint>());
                }                
            }


            if (GetComponent<RaceDrivers>().RacersIds[i] == 0)
            {
                Car = CarsList[i];
                CarsList[i].tag = "Player";
                //Camera.main.GetComponent<CameraBlurEnabler>().PlayerCar = Car;
                CarsList[i].GetComponent<Human>().enabled = true;
                CarsList[i].GetComponent<CarUserControl>().enabled = false;
                CarsList[i].GetComponent<CarAIControl>().enabled = false;
                CarsList[i].GetComponent<IsNetworkPlayer>().Player = true;
            }
            else
            {
                CarsList[i].tag = "Enemy";
                CarsList[i].GetComponent<Human>().enabled = false;
                CarsList[i].GetComponent<CarUserControl>().enabled = false;
                CarsList[i].GetComponent<CarAIControl>().enabled = false;
                CarsList[i].GetComponent<IsNetworkPlayer>().Player = false;
            }

            //Car.transform.LookAt(GetComponent<WaypointsList>().listOfWaypoints[0].transform);
            CarsList[i].transform.LookAt(GameObject.FindWithTag("StartLine").transform);
            CarsList[i].GetComponent<Rigidbody>().isKinematic = true;
            ///GameObject.FindWithTag("Respawn");
            ///

        }


        ready = true;
        Time.timeScale = 1;
        Camera.main.gameObject.SetActive(true);
        if (GameObject.Find("MultipurposeCameraRig"))
        {
            var autoCam = GameObject.Find("MultipurposeCameraRig").GetComponent<AutoCam>();
            autoCam.enabled = false;
            autoCam.enabled = true;
            autoCam.SetTarget(Car.transform);
        }


    }


    IEnumerator countDown()
    {
        nextSec = false;
        if (countDownSec == -1)
        {
            GameObject.Find("CounDownText").GetComponent<UnityEngine.UI.Text>().text = "GO";
        }
        else
        {
            GameObject.Find("CounDownText").GetComponent<UnityEngine.UI.Text>().text = "" + countDownSec;
        }

        if (countDownSec == -2)
        {
            GameObject.Find("CounDownText").GetComponent<UnityEngine.UI.Text>().text = "";
            if (BackToTrackObj) { BackToTrackObj.SetActive(true); }

            for (var i = 0; i < 10; i++)
            {

                if (CarsList[i].tag == "Enemy")
                {
                    CarsList[i].GetComponent<CarAIControl>().enabled = true;
                }
                else
                {
                    CarsList[i].GetComponent<CarUserControl>().enabled = true;
                  //  CarsList[i].GetComponent<CarAIControl>().enabled = true;
                    CarsList[i].GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
                }
                CarsList[i].GetComponent<Rigidbody>().isKinematic = false;

            }


        }


        yield return new WaitForSeconds(1);
        if (countDownSec > -2)
        {
            countDownSec--;
            nextSec = true;
        }

    }

    // Update is called once per frame
    void Update () {

        if (!isfired)
        {
            isfired = true;
            delayedStart();
        }


        if (ready&nextSec)
        {
            StartCoroutine("countDown");
        }
    }
}
