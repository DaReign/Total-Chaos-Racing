using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAddingsTest : MonoBehaviour {
    public int Car;
    public int Armor;
    public int Tires;
    public int Engine;
    public int Gun;
    private int wait=300;
    private bool setted = false;
    public int Mines;
    public int Nitro;
    public bool TestMode;
    // Use this for initialization
    void Start () {
		
	}

    public void setUpTestCarData (int i)
    {
        GetComponent<CarStats>().Car = Car;
        GetComponent<CarStats>().Armor = Armor;
        GetComponent<CarStats>().Tires = Tires;
        GetComponent<CarStats>().Engine = Engine;
        GetComponent<CarStats>().Gun = Gun;
        GetComponent<CarStats>().Mines = Mines;
        GetComponent<CarStats>().Nitro = Nitro;
        GetComponent<CarStats>().Hull = 10000;
        GetComponent<CarStats>().HullMax = 10000;
        GetComponent<CarStats>().Ammo = 100;

    }

    /*
// Update is called once per frame
void Update () {
    if (!setted)
    {
        if (wait > 0)
        {
            wait--;

        }
        else
        {
            setted = true;
            GetComponent<CarStats>().Car = Car;
            GetComponent<CarStats>().Armor = Armor;
            GetComponent<CarStats>().Tires = Tires;
            GetComponent<CarStats>().Engine = Engine;
            GetComponent<CarStats>().Gun = Gun;
            GetComponent<CarStats>().Mines = Mines;
            GetComponent<CarStats>().Nitro = Nitro;
        }
    }
}
*/
}
