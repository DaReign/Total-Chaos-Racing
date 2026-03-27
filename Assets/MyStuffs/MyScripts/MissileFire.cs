using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MissileFire : MonoBehaviour {
    public GameObject LauncherEnd;
    private GameObject Missile;
    public bool NetworkPlayer = false;
    public GameObject Global;
    //private GameObject Creator;
	// Use this for initialization
	void Start () {
        NetworkPlayer = GetComponent<IsNetworkPlayer>().NetworkPlayer;
        Global=GameObject.Find("Global");
    }

    public int CalculateDamage ()
    {
        int DamageVar = 0;

        if (Global.GetComponent<Stage>().RaceLvl==1)
        {
            DamageVar = 200 + (30 * GetComponent<CarStats>().Gun);
        }
        else
        {
            if (Global.GetComponent<Stage>().RaceLvl == 2)
            {
                DamageVar = (50 * GetComponent<CarStats>().Car) + (30 * GetComponent<CarStats>().Gun) + 100;
            }
            else  
            {
                DamageVar = (400 * GetComponent<CarStats>().Car) + (100 * GetComponent<CarStats>().Gun)+150 * Global.GetComponent<Stage>().RaceLvl;
            }
        }
        return DamageVar;
    }


    public void missile (int type)
    {
        if (!NetworkPlayer)
        {
           // print("Function fired");

            Missile = Instantiate(Resources.Load("Missile", typeof(GameObject))) as GameObject;
            Missile.transform.position = new Vector3(LauncherEnd.transform.position.x, LauncherEnd.transform.position.y + 0.65f, LauncherEnd.transform.position.z);
            Missile.transform.eulerAngles = new Vector3(LauncherEnd.transform.eulerAngles.x, LauncherEnd.transform.eulerAngles.y, LauncherEnd.transform.eulerAngles.z);
            Missile.GetComponent<Rigidbody>().linearVelocity = this.gameObject.transform.GetComponent<Rigidbody>().linearVelocity;


            Missile.GetComponent<Rigidbody>().AddForce(transform.forward * (2500 + (Random.Range(0, 300))));
            //Missile.GetComponent<Missile>().Creator = this.gameObject;
            Missile.GetComponent<Missile>().missileDamage = CalculateDamage();
            Missile.GetComponent<Missile>().racerId = GetComponent<CarStats>().racerId;
            Missile.GetComponent<Missile>().Car = GetComponent<CarStats>().Car;
            Missile.GetComponent<Missile>().Gun = GetComponent<CarStats>().Gun;
            // NetworkTransform removed in Unity 6 (UNet)
            // if (Missile.GetComponent<NetworkTransform>())
            // {
            //     Destroy(Missile.GetComponent<NetworkTransform>());
            // }

            if (type == 1)
            {
                Missile.GetComponent<Missile>().HomingMissile = true;
                GetComponent<CarStats>().HomingMissiles--;
                Missile.transform.position = new Vector3(LauncherEnd.transform.position.x, LauncherEnd.transform.position.y+0.65f, LauncherEnd.transform.position.z);
               // Missile.transform.eulerAngles = new Vector3(LauncherEnd.transform.eulerAngles.x-45f, LauncherEnd.transform.eulerAngles.y, LauncherEnd.transform.eulerAngles.z);
                // Missile.GetComponent<Rigidbody>().AddForce(transform.forward * (4000 + (Random.Range(0, 300))));
            }
            else
            {
                GetComponent<CarStats>().Missiles--;
            }
            //EditorApplication.isPaused = true;

        }
        else
        {
            CmdMissile(type);
        }
    }

    // [Command] // UNet removed in Unity 6
    void CmdMissile(int type)
    {
        print("Function fired");

        Missile = Instantiate(Resources.Load("Missile", typeof(GameObject))) as GameObject;
        Missile.transform.position = new Vector3(LauncherEnd.transform.position.x, LauncherEnd.transform.position.y + 0.65f, LauncherEnd.transform.position.z + 0.75f);
        Missile.transform.eulerAngles = new Vector3(LauncherEnd.transform.eulerAngles.x, LauncherEnd.transform.eulerAngles.y, LauncherEnd.transform.eulerAngles.z);
        Missile.GetComponent<Rigidbody>().AddForce(transform.forward * (2000 + (Random.Range(0, 300))));
        //Missile.GetComponent<Missile>().Creator = this.gameObject;

        if (type == 1)
        {
            Missile.GetComponent<Missile>().HomingMissile = true;
            GetComponent<CarStats>().HomingMissiles--;
        }
        else
        {
            GetComponent<CarStats>().Missiles--;
        }
        // NetworkServer.Spawn(Missile); // UNet removed in Unity 6
    }
}
