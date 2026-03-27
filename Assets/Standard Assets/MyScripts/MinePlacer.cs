using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;


public class MinePlacer : MonoBehaviour {
    private GameObject Mine;
    private GameObject Car;
    private Boolean MinesEnabled = false;
    private Boolean MineReady = true;
    private Boolean StartEnableMinePlacing = false;
    RaycastHit hit;
    public GameObject MinePlacerObj;
    public bool NetworkPlayer;
    public bool Player;

    private GameObject Global;
    //private GameObject MinePlacer;
    private GameObject[] Mines;
    private GameObject MineObj;

    // Use this for initialization
    void Start () {
        NetworkPlayer = GetComponent<IsNetworkPlayer>().NetworkPlayer;
        Player = GetComponent<IsNetworkPlayer>().Player;
        Global = GameObject.Find("Global");
        Car = transform.root.gameObject;

//        MinePlacerObj.transform.position = new Vector3(MinePlacerObj.transform.position.y + 5.19f, MinePlacerObj.transform.position.y, MinePlacerObj.transform.position.z);
        MinePlacerObj.transform.eulerAngles = new Vector3(Car.transform.eulerAngles.x, Car.transform.eulerAngles.y, Car.transform.eulerAngles.z);

    }

    void RandomMinePlace()
    {
        if (Car.GetComponent<CarStats>().Mines > 0 && MinesEnabled& Car.GetComponent<CarStats>().speed>10)
        {
            //print("Random mine place");
            StartCoroutine("minePlacing");
        }
    }

    // Update is called once per frame
    void Update () {

        if (!Player)
        {
            if (UnityEngine.Random.Range(0, 3000) > 2990)
            {
                RandomMinePlace();
            }
        }

        /*
        if (!StartEnableMinePlacing)
        {
            StartCoroutine("enableMinePlacing");
        }
        */

        if (!MinesEnabled)
        {
            MinesEnabled = Global.GetComponent<Stage>().WeaponsEnabled;
        }
        if (MinesEnabled && GetComponent<CarStats>().Mines>0 )
        {
            if (!Player)
            {
                drawRay(MinePlacerObj.transform.position, 0, 0, 0, 0, 0, 1, 40.0f, hit, MinePlacerObj.transform);
            }

        }

    }

    IEnumerator enableMinePlacing()
    {
        StartEnableMinePlacing = true;
        yield return new WaitForSeconds(15.0f);
        MinesEnabled = true;
        /*
        print("Mines ENABLED !!!!!!!!!!!!!!!!!!!!!!!!!");
        
        GameObject.Find("Global").GetComponent<Stage>().timedInfoEnable = true;
        GameObject.Find("Global").GetComponent<Stage>().timedInfoText = "MINES ACTIVATED";
        GameObject.Find("Global").GetComponent<Stage>().timedInfoTime = 10.0f;
        */
    }


    private void drawRay(Vector3 startPos, float xOffset, float yOffset, float zOffset, float xPar, float yPar, float zPar, float rayDistance, RaycastHit hit, Transform transform)
    {
        Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.blue);
        //print("draw ray");

        if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)), out hit, rayDistance))
        {
            //print("Hitted!!!!!!!!!!!!!");
            //print(hit.transform);


            if (hit.transform != this.transform)
            {
              //  print("not self");

                if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                {

                //    print("enemy or player");

                    //print("Hited potential target !!!!!!!!!!!!");

                    //&&(!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked)
                    if (Car.GetComponent<CarStats>().Mines > 0 && (!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked) && (!hit.transform.root.gameObject.GetComponent<CarStats>().raceFinished) && MinesEnabled && MineReady)
                    {
                  //      print("after if");

                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        MineReady = false;
                        StartCoroutine("minePlacing");
                    }
                }
            }
            else
            {
            //    print("trafia w siebie");
            }
        }
    }


    public void mine()
    {

        if (Car.GetComponent<CarStats>().Mines > 0)
        {
            MineReady = false;
            StartCoroutine("minePlacing");
        }
    }

    IEnumerator minePlacing()
    {
        //print("start placing");
        Car.GetComponent<CarStats>().Mines--;
        if (NetworkPlayer)
        {
            CmdMinePlace();
        }
        else
        {

            //Mine = Instantiate("Mines", transform.position + (transform.forward * 2), transform.rotation);
            //MineObj = Instantiate(Resources.Load("Mine2", typeof(GameObject)), transform.position + (transform.forward * 4), transform.rotation) as GameObject;

            /*
            Mine = Instantiate(Resources.Load("at_mine_LOD0", typeof(GameObject))) as GameObject;
            Mine.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z);
            Mine.transform.eulerAngles = new Vector3(MinePlacerObj.transform.eulerAngles.x, MinePlacerObj.transform.eulerAngles.y, MinePlacerObj.transform.eulerAngles.z);
            Mine.GetComponent<Mine>().CreatedBy = Car;

            Mine = Instantiate(Resources.Load("at_mine_LOD0", typeof(GameObject))) as GameObject;
            Mine.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z)+Vector3.right;
            Mine.transform.eulerAngles = new Vector3(MinePlacerObj.transform.eulerAngles.x, MinePlacerObj.transform.eulerAngles.y, MinePlacerObj.transform.eulerAngles.z);
            Mine.GetComponent<Mine>().CreatedBy = Car;
            */

            MineObj = Instantiate(Resources.Load("Mines", typeof(GameObject)), MinePlacerObj.transform.position, MinePlacerObj.transform.rotation) as GameObject;
            for (int i = 0; i < MineObj.transform.childCount; i++)
            {
                Transform child = MineObj.transform.GetChild(i);
                if (child.tag == "Mine")
                {
                    child.gameObject.GetComponent<Mine>().CreatedBy = Car;
                }
            }

            /*
            Mine = Instantiate(Resources.Load("Mines", typeof(GameObject))) as GameObject;
            Mine.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z);
            Mine.transform.eulerAngles = new Vector3(MinePlacerObj.transform.eulerAngles.x, MinePlacerObj.transform.eulerAngles.y, MinePlacerObj.transform.eulerAngles.z);
        */
            //Mine.GetComponent<Mine>().CreatedBy = Car;


            /*
            MineObj = Instantiate(Resources.Load("Mines", typeof(GameObject))) as GameObject;
            MineObj.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z);
            MineObj.transform.eulerAngles = new Vector3(MinePlacerObj.transform.eulerAngles.x, MinePlacerObj.transform.eulerAngles.y, MinePlacerObj.transform.eulerAngles.z);

*/

        }
#if UNITY_EDITOR
        //      EditorApplication.isPaused = true;
#endif
        yield return new WaitForSeconds(1.0f);
        MineReady = true;
    }

    void CmdMinePlace()
    {
        print("Server mine place");
        Mine = Instantiate(Resources.Load("at_mine_LOD0", typeof(GameObject))) as GameObject;
        Mine.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z);
        Mine.GetComponent<Mine>().CreatedBy = Car;
        // NetworkServer.Spawn(Mine); // UNet removed in Unity 6
    }

}
