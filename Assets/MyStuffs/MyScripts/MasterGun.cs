using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;


public class MasterGun : MonoBehaviour
{
    public bool NetworkPlayer;
    private GameObject LaserHitParticle;
    RaycastHit hit;
    AudioSource sound;
    public string GunName;
    private Boolean IsShooting;
    private Boolean GunEnabled = false;
    private Boolean StartGunEnabled = false;
    private Boolean CoolDown = false;
    private GameObject Car;
    private float RayCastLenght;
    // Use this for initialization

    public GameObject Gun1;
    public GameObject Gun2;

    private bool Gun1shooting;
    private bool Gun2shooting;
    public GameObject Gun;
    public bool AIPlayer;
    private bool AllowMissileShot=true;
    private bool AllowHomingMissileShot = true;
    private GameObject Global;

    private GameObject[] Enemies;
    public GameObject Target;
    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> enemiesSelectedList = new List<GameObject>();
    private float shortDistance = 10000f;
    private float distance;
    private float angle;
    private int randomHomingMissileDelay=100;
    private int homingMissileDelayCounter;
    private int randomMissileDelay = 100;
    private int MissileDelayCounter;

    void Start()
    {
        Global = GameObject.Find("Global");

        if (GetComponent<IsNetworkPlayer>().Player==true)
        {
            AIPlayer = false;
        }
        else
        {
            AIPlayer = true;
        }
        //Gun = GameObject.Find("GunSound");
        sound = Gun.GetComponent<AudioSource>();
        //Car = transform.root.gameObject;
        Car = this.gameObject;
    }



    void FindHomingMissileTargets()
    {
        Enemies = GameObject.FindGameObjectsWithTag("MissileTarget");
        foreach (GameObject Enemy in Enemies)
        {
            enemiesList.Add(Enemy);
            angle = Vector3.Angle(transform.forward, Enemy.transform.position - transform.position);
            if (angle < 90)
            {
                distance = Vector3.Distance(Enemy.transform.position, transform.position);
                if (distance < shortDistance)
                {
                    if (distance > 5.0f)
                    {
                        shortDistance = distance;
                        Target = Enemy;
                        enemiesSelectedList.Add(Target);
                    }
                }
            }
        }

        if (Target)
        {

            if (Vector3.Distance(Target.transform.position, transform.position) > 200f)
            {
                if (AIPlayer && AllowHomingMissileShot)
                {
                    StartCoroutine(homingMissileDelay());
                    if (GetComponent<CarStats>().HomingMissiles > 0)
                    {
                        GetComponent<MissileFire>().missile(1);
                    }
                }

            }
        }
        randomHomingMissileDelay = UnityEngine.Random.Range(100, 300);

    }


    void FindMissileTargets()
    {
        Enemies = GameObject.FindGameObjectsWithTag("MissileTarget");
        foreach (GameObject Enemy in Enemies)
        {
            enemiesList.Add(Enemy);
            angle = Vector3.Angle(transform.forward, Enemy.transform.position - transform.position);
            if (angle < 30)
            {
                distance = Vector3.Distance(Enemy.transform.position, transform.position);
                if (distance < shortDistance)
                {
                    if (distance > 5.0f)
                    {
                        shortDistance = distance;
                        Target = Enemy;
                        enemiesSelectedList.Add(Target);
                    }
                }
            }
        }

        if (Target)
        {

            if (Vector3.Distance(Target.transform.position, transform.position) < 200f)
            {
                if (AIPlayer && AllowMissileShot)
                {
                    StartCoroutine(missileDelay());
                    if (GetComponent<CarStats>().Missiles > 0)
                    {
                        GetComponent<MissileFire>().missile(0);
                    }
                }

            }
        }
        randomHomingMissileDelay = UnityEngine.Random.Range(100, 300);

    }



    // Update is called once per frame
    void Update()
    {

        if (!GunEnabled)
        {
            GunEnabled = Global.GetComponent<Stage>().WeaponsEnabled;
        }

        if (GetComponent<CarStats>().wrecked || GetComponent<CarStats>().raceFinished)
        {
            GunEnabled = false;
            this.enabled = false;
        }

        if (GunEnabled)
        {
            if (AIPlayer && AllowHomingMissileShot && GetComponent<CarStats>().Missiles > 0)
            {
                FindHomingMissileTargets();
            }
            if (AIPlayer && AllowMissileShot && GetComponent<CarStats>().HomingMissiles > 0)
            {
                FindMissileTargets();
            }
            if (!CoolDown&& GetComponent<CarStats>().Ammo>0)
            {
                RayCastLenght = 5000.0f + (float)(GetComponent<CarStats>().Gun * 100);
                drawRay(Gun1.transform.position, 0, 0, 0, 0, 0, 1, RayCastLenght, hit, Gun1.transform);
                drawRay(Gun2.transform.position, 0, 0, 0, 0, 0, 1, RayCastLenght, hit, Gun2.transform);
            }
        }

    }

    IEnumerator enableGun()
    {
        StartGunEnabled = true;
        yield return new WaitForSeconds(15.0f);
        GunEnabled = true;
        //print("GUNS ENABLED !!!!!!!!!!!!!!!!!!!!!!!!!");
        /*
        GameObject.Find("Global").GetComponent<Stage>().timedInfoEnable = true;
        GameObject.Find("Global").GetComponent<Stage>().timedInfoText = "GUNS ACTIVATED";
        GameObject.Find("Global").GetComponent<Stage>().timedInfoTime = 10.0f;
        */
    }

    IEnumerator missileDelay()
    {
        AllowMissileShot = false;
        yield return new WaitForSeconds(4.0f-(GetComponent<CarStats>().Car*0.2f)- (GetComponent<CarStats>().Gun * 0.1f));
        AllowMissileShot = true;
    }

    IEnumerator homingMissileDelay()
    {
        AllowHomingMissileShot = false;
        yield return new WaitForSeconds(4.0f - (GetComponent<CarStats>().Car * 0.2f) - (GetComponent<CarStats>().Gun * 0.1f));
        AllowHomingMissileShot = true;
    }

    IEnumerator gunDelay()
    {
        CoolDown = true;
        yield return new WaitForSeconds(0.3f);
        CoolDown = false;
    }

    private void drawRay(Vector3 startPos, float xOffset, float yOffset, float zOffset, float xPar, float yPar, float zPar, float rayDistance, RaycastHit hit, Transform transform)
    {
        Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.yellow);

        if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)), out hit, rayDistance))
        {
            if (hit.transform != this.transform)
            {
     //           print(hit.transform.tag);

                if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                {
     //               Debug.Log("Ammo "+ GetComponent<CarStats>().Ammo);
                    //&&(!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked)
                    if (GetComponent<CarStats>().Ammo > 0 && (!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked) && (!hit.transform.root.gameObject.GetComponent<CarStats>().raceFinished) && GunEnabled)
                    {

                        GetComponent<CarStats>().Ammo--;
                        if (!sound.isPlaying)
                        {
                            sound.Play();
                        }
                        //Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.red);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        if (NetworkPlayer)
                        {
                            //print("network shot");
                            CmdFire(hit.point.x, hit.point.y, hit.point.z);

                            

                        }
                        else
                        {
                            //print("local shot");
                            LaserHitParticle = Instantiate(Resources.Load("LaserHitParticle", typeof(GameObject))) as GameObject;
                            LaserHitParticle.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                            LaserHitParticle.transform.parent = hit.transform;

                            /*
                            if (AIPlayer&&AllowMissileShot)
                            {
                                StartCoroutine(missileDelay());
                                if (GetComponent<CarStats>().Missiles > 0)
                                {
                                    GetComponent<MissileFire>().missile(0);
                                }

                                
                                if (GetComponent<CarStats>().HomingMissiles > 0)
                                {
                                    //GetComponent<CarStats>().HomingMissiles--
                                    GetComponent<MissileFire>().missile(1);
                                }
                                else
                                {
                                    if (GetComponent<CarStats>().Missiles > 0)
                                    {
                                        GetComponent<MissileFire>().missile(0);
                                    }
                                }
                                
                            }
                            */
                        }

                        IsShooting = true;
                        StartCoroutine("gunDelay");
                        //code to decrease enemy energy level
                        if (hit.transform.root.gameObject.GetComponent<CarStats>().Hull > 0)
                        {
                            //hit.transform.root.gameObject.GetComponent<CarStats>().Hull = hit.transform.root.gameObject.GetComponent<CarStats>().Hull - Car.GetComponent<CarStats>().Gun - Car.GetComponent<CarStats>().Car * 2;
                            hit.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(GetComponent<CarStats>().Gun+GetComponent<CarStats>().Car);

                        }
                    }
                }
            }
        }
    }

    // [Command] // UNet removed in Unity 6
    void CmdFire(float x, float y, float z)
    {
        print("Server shot");
        //Car.GetComponent<CarStats>().
        //LaserHitParticle =  Instantiate(Car.GetComponent<CarStats>().GunHitPrefab, transform.position, Quaternion.identity) as GameObject;
        LaserHitParticle = Instantiate(Resources.Load("LaserHitParticle", typeof(GameObject))) as GameObject;
        LaserHitParticle.transform.position = new Vector3(x, y, z);
        LaserHitParticle.transform.parent = hit.transform;
        // NetworkServer.Spawn(LaserHitParticle); // UNet removed in Unity 6
    }

}
