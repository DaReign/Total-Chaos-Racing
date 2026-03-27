using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6

public class NetworkMinePlacer : MonoBehaviour {
    public GameObject MinePlacerObj;
    private GameObject Mine;
    private GameObject Car;
    private Boolean MinesEnabled = false;
    private Boolean MineReady = true;
    private Boolean StartEnableMinePlacing = false;
    RaycastHit hit;

    //private GameObject MinePlacer;

    // Use this for initialization
    void Start()
    {
        Car = transform.root.gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (!StartEnableMinePlacing)
        {
            StartCoroutine("enableMinePlacing");
        }


    }

    IEnumerator enableMinePlacing()
    {
        StartEnableMinePlacing = true;
        yield return new WaitForSeconds(1.0f);
        MinesEnabled = true;
        print("Mines ENABLED !!!!!!!!!!!!!!!!!!!!!!!!!");
        GameObject.Find("Global").GetComponent<Stage>().timedInfoEnable = true;
        GameObject.Find("Global").GetComponent<Stage>().timedInfoText = "MINES ACTIVATED";
        GameObject.Find("Global").GetComponent<Stage>().timedInfoTime = 10.0f;
    }


    private void FixedUpdate()
    {
        drawRay(MinePlacerObj.transform.position, 0, 0, 0, 0, 0, 1, 40.0f, hit, transform);

    }

    private void drawRay(Vector3 startPos, float xOffset, float yOffset, float zOffset, float xPar, float yPar, float zPar, float rayDistance, RaycastHit hit, Transform transform)
    {
        Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), MinePlacerObj.transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.black);
        //print("draw ray");

        if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), MinePlacerObj.transform.TransformDirection(new Vector3(xPar, yPar, zPar)), out hit, rayDistance))
        {
            //print("Hitted!!!!!!!!!!!!!");
            //print(hit.transform);


            if (hit.transform != this.transform)
            {

                if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                {

                    //print("Hited potential target !!!!!!!!!!!!");

                    //&&(!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked)
                    if (Car.GetComponent<CarStats>().Mines > 0 && (!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked) && (!hit.transform.root.gameObject.GetComponent<CarStats>().raceFinished) && MinesEnabled && MineReady)
                    {
                        Debug.DrawLine(MinePlacerObj.transform.position, hit.point, Color.red);
                        MineReady = false;
                        StartCoroutine("minePlacing");
                    }
                }
            }
        }
    }

    IEnumerator minePlacing()
    {
        Car.GetComponent<CarStats>().Mines--;
        CmdMinePlace();
        yield return new WaitForSeconds(1.0f);
        MineReady = true;
    }
    // [Command] // UNet removed in Unity 6
    void CmdMinePlace()
    {
        print("Server mine place");
        Mine = Instantiate(Resources.Load("at_mine_LOD0", typeof(GameObject))) as GameObject;
        Mine.transform.position = new Vector3(MinePlacerObj.transform.position.x, MinePlacerObj.transform.position.y + 0.07f, MinePlacerObj.transform.position.z);
        Mine.GetComponent<Mine>().CreatedBy = Car;
        // NetworkServer.Spawn(Mine); // UNet removed in Unity 6
    }

}