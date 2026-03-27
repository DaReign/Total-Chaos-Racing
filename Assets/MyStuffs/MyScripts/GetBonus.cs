using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonus : MonoBehaviour {
    public int type;
    private GameObject Car;
    public GameObject Global;
    public GameObject CashObj;
    public GameObject HullObj;
    public GameObject MissileObj;
    public GameObject HomingMissileObj;
    public GameObject MineObj;
    //1 ammo
    //2 hull
    //3 mines
    // Use this for initialization
    void Start () {
        Global = GameObject.Find("Global");
        CashObj = transform.Find("Cash").gameObject;
        CashObj.SetActive(false);
        HullObj = transform.Find("Hull").gameObject;
        HullObj.SetActive(false);
        MissileObj = transform.Find("Missile").gameObject;
        MissileObj.SetActive(false);
        HomingMissileObj = transform.Find("HomingMissile").gameObject;
        HomingMissileObj.SetActive(false);
        MineObj = transform.Find("Mine").gameObject;
        MineObj.SetActive(false);
        type = UnityEngine.Random.Range(0, 5);
        if (type==0) { type = 1; }
        // na razie kasy nie bedzie
        switchSkin();
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Collided with");
        //print(other);
        //print(other.tag);
        //print(other.transform.root.gameObject);
        // if (other.tag == "Enemy")
        // {
        //     print(other.transform.root.gameObject.GetComponent<CarStats>());
        // }

        if (other.transform.root.gameObject.tag == "Player" || other.transform.root.gameObject.tag == "Enemy")
        {
            int temp;
            switch (type)
            {
                case 0:
                    //               hit.transform.root.gameObject.GetComponent<CarStats>().Hull--;
                    //print("Cash");
                    other.transform.root.gameObject.GetComponent<CarStats>().StageCollectedCashVar += UnityEngine.Random.Range(1,20)*10;
                    Destroy(gameObject);
                    break;
                case 1:
                    //print("Hull");
                    //other.transform.root.gameObject.GetComponent<CarStats>().Hull+=50;
                    //temp = UnityEngine.Random.Range(1, 50) * 10;

                    temp = other.transform.root.gameObject.GetComponent<CarStats>().HullMax / (13-Global.GetComponent<Stage>().RaceLvl);
                    Debug.Log("Hull bonus "+temp);
                    if (other.transform.root.gameObject.GetComponent<CarStats>().Hull + temp <= other.transform.root.gameObject.GetComponent<CarStats>().HullMax)
                    {
                        other.transform.root.gameObject.GetComponent<CarStats>().Hull += temp;

                    }
                    else
                    {
                        other.transform.root.gameObject.GetComponent<CarStats>().Hull = other.transform.root.gameObject.GetComponent<CarStats>().HullMax;
                    }

                    Destroy(gameObject);
                    break;
                case 2:
                    //print("Mine");
                    temp = UnityEngine.Random.Range(1, 10);
                    other.transform.root.gameObject.GetComponent<CarStats>().Mines += temp;
                    Destroy(gameObject);
                    break;
                case 3:
                    temp = UnityEngine.Random.Range(1, 5);
                    //print("misiles");
                    other.transform.root.gameObject.GetComponent<CarStats>().Missiles += temp;
                    Destroy(gameObject);
                    break;
                case 4:
                    temp = UnityEngine.Random.Range(1, 5);
                    //print("homingmissiles");
                    other.transform.root.gameObject.GetComponent<CarStats>().HomingMissiles += temp;
                    Destroy(gameObject);
                    break;
                default:
                    //print("Ammo");
                    other.transform.root.gameObject.GetComponent<CarStats>().StageCollectedCashVar += UnityEngine.Random.Range(1, 20) * 10;
                    Destroy(gameObject);
                    break;
            }
        }
        
        // Destroy(other.gameObject);
    }

    public void OnDestroy()
    {
        if (this.gameObject)
        {
            Global.GetComponent<BonusGenerator>().bonusList.Remove(this.gameObject);
        }
    }

    public void switchSkin()
    {
        /*
        Ammo = transform.Find("PowerUpContainerFlame-1").gameObject;
        Ammo.SetActive(false);
        Hull = transform.Find("PowerUpContainerRed").gameObject;
        Hull.SetActive(false);
        Cash = transform.Find("PowerUpContainerYellowDollar").gameObject;
        Cash.SetActive(false);
        Mines = transform.Find("PowerUpContainerDoubleShine").gameObject;
        Mines.SetActive(false);
        Nitro = transform.Find("PowerUpContainerBlueFlame").gameObject;
        Nitro.SetActive(false);
        */

        switch (type)
        {
            case 0:
                CashObj.SetActive(true);
                print("set cash");
                break;
            case 1:
                HullObj.SetActive(true);
                print("set hull");
                break;
            case 2:
                MineObj.SetActive(true);
                print("set mines");
                break;
            case 3:
                MissileObj.SetActive(true);
                print("set missile");
                break;
            case 4:
                HomingMissileObj.SetActive(true);
                print("set homing missile");
                break;
            default:
                CashObj.SetActive(true);
               // print("set default ammo");
                break;
        }

    }


}
