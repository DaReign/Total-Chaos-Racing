using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour {

    public GameObject Thunder1;
    public GameObject Thunder2;
    public GameObject Thunder3;
    public GameObject Thunder4;
    public GameObject Thunder5;
    public GameObject Thunder6;
    public GameObject Thunder7;
    private int number;
    private bool thunderStrike = true;
    private GameObject Sun;
    private int DayState;
    private float SunIntensity;


    // Use this for initialization
    void Start () {
        DayState = int.Parse(PlayerPrefs.GetString("RaceDayState"));
        Thunder1.SetActive(false);
        Thunder2.SetActive(false);
        Thunder3.SetActive(false);
        Thunder4.SetActive(false);
        Thunder5.SetActive(false);
        Thunder6.SetActive(false);
        Thunder7.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (thunderStrike)
        {
            StartCoroutine("thunder");
        }

    }

    IEnumerator thunder()
    {
        thunderStrike = false;
        Thunder1.SetActive(false);
        Thunder2.SetActive(false);
        Thunder3.SetActive(false);
        Thunder4.SetActive(false);
        Thunder5.SetActive(false);
        Thunder6.SetActive(false);
        Thunder7.SetActive(false);

        number = Random.Range(1,7);
        //number = 1;
        switch (number)
        {
            case 1: Thunder1.SetActive(true); break;
            case 2: Thunder2.SetActive(true); break;
            case 3: Thunder3.SetActive(true); break;
            case 4: Thunder4.SetActive(true); break;
            case 5: Thunder5.SetActive(true); break;
            case 6: Thunder6.SetActive(true); break;
            case 7: Thunder7.SetActive(true); break;
            default: break;
        }
        Sun=GameObject.FindGameObjectWithTag("Sun");
        SunIntensity = Sun.GetComponent<Light>().intensity;
        Sun.GetComponent<Light>().intensity = 50.0f;
        Sun.GetComponent<Light>().color = new Color(1f, 1f, 1f, 1);
        yield return new WaitForSeconds(0.1f);
        Sun.GetComponent<Light>().intensity = SunIntensity;
        /*
        if (DayState < 1)
        {
            Sun.GetComponent<Light>().intensity = 0.0f;
        }
        else
        {
            Sun.GetComponent<Light>().intensity = 1.0f;
        }
        */
        yield return new WaitForSeconds(20.0f);
        thunderStrike = true;
    }



}

