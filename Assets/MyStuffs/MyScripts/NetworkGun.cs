using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6

public class NetworkGun : MonoBehaviour {
    public bool NetworkPlayer;
    private GameObject LaserHitParticle;
    RaycastHit hit;
    AudioSource sound;
    public string GunName;
    public GameObject OtherGun;
    private Boolean IsShooting;
    private Boolean GunEnabled = false;
    private Boolean StartGunEnabled = false;
    private Boolean CoolDown = false;
    private GameObject Car;
    private float RayCastLenght;
    public GameObject NetworkGunObject; 
    // Use this for initialization
    void Start()
    {
        sound = GetComponent<AudioSource>();
        Car = transform.root.gameObject;
        //Car = transform.parent.parent.parent.gameObject;

        //print("Car");
        //print(Car);
        /*
        if (NetworkPlayer)
        {
            if (Car.GetComponent<NetworkIdentity>().isLocalPlayer == false)
            {
                Destroy(this);
                return;
            }
        }
        */


    }


    // Update is called once per frame
    void Update()
    {

        if (!StartGunEnabled)
        {
            StartCoroutine("enableGun");
        }


    }

    IEnumerator enableGun()
    {
        StartGunEnabled = true;
        yield return new WaitForSeconds(5.0f);
        GunEnabled = true;
        //print("GUNS ENABLED !!!!!!!!!!!!!!!!!!!!!!!!!");
        GameObject.Find("Global").GetComponent<Stage>().timedInfoEnable = true;
        GameObject.Find("Global").GetComponent<Stage>().timedInfoText = "GUNS ACTIVATED";
        GameObject.Find("Global").GetComponent<Stage>().timedInfoTime = 10.0f;
    }

    IEnumerator gunDelay()
    {
        CoolDown = true;
        yield return new WaitForSeconds(1.0f);
        CoolDown = false;
    }

    private void FixedUpdate()
    {


        if (!CoolDown)
        {
            RayCastLenght = 40.0f + (float)(Car.GetComponent<CarStats>().Gun * 4);
            drawRay(transform.position, 0, 0, 0, 0, 0, 1, RayCastLenght, hit, transform);
        }
        //else
        //{
        // }
    }


    private void drawRay(Vector3 startPos, float xOffset, float yOffset, float zOffset, float xPar, float yPar, float zPar, float rayDistance, RaycastHit hit, Transform transform)
    {
        Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.yellow);
        //print("draw ray");

        if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)), out hit, rayDistance))
        {
            if (hit.transform != this.transform)
            {

                if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                {
                    //&&(!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked)
                    if (Car.GetComponent<CarStats>().Ammo > 0 && (!hit.transform.root.gameObject.GetComponent<CarStats>().wrecked) && (!hit.transform.root.gameObject.GetComponent<CarStats>().raceFinished) && GunEnabled)
                    {

                        Car.GetComponent<CarStats>().Ammo--;
                        if (!sound.isPlaying)
                        {
                            sound.Play();
                        }
                        //Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.red);
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        if (NetworkPlayer)
                        {
                            print("network shot");
                            CmdFire(hit.point.x, hit.point.y, hit.point.z);
                        }
                        else
                        {
                            print("local shot");
                            LaserHitParticle = Instantiate(Resources.Load("LaserHitParticle", typeof(GameObject))) as GameObject;
                            LaserHitParticle.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                            LaserHitParticle.transform.parent = hit.transform;
                        }

                        IsShooting = true;
                        StartCoroutine("gunDelay");
                        //code to decrease enemy energy level
                        if (hit.transform.root.gameObject.GetComponent<CarStats>().Hull > 0)
                        {
                            //CmdHull();
                            hit.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(1);
                            // hit.transform.root.gameObject.GetComponent<CarStats>().Hull = hit.transform.root.gameObject.GetComponent<CarStats>().Hull - Car.GetComponent<CarStats>().Gun - Car.GetComponent<CarStats>().Car * 2;
                        }
                    }
                }
            }
        }
        else
        {
            IsShooting = false;
         /*   

            //print("nothing on target");
            //print("other gun");
            //print(OtherGun.GetComponent<Gun>().IsShooting);
            if (OtherGun.GetComponent<Gun>().IsShooting)
            {
                Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * 100.0f, Color.yellow);
                if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)), out hit, 100.0f))
                {
                    if (Car.GetComponent<CarStats>().Ammo > 0)
                    {
                        Car.GetComponent<CarStats>().Ammo--;
                        if (hit.transform)
                        {
                            if (!sound.isPlaying)
                            {
                                sound.Play();
                            }
                            //Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.red);
                            Debug.DrawLine(transform.position, hit.point, Color.red);
                            if (NetworkPlayer)
                            {
                                print("network shot");
                                CmdFire(hit.point.x, hit.point.y, hit.point.z);
                            }
                            else
                            {
                                print("local shot");
                                LaserHitParticle = Instantiate(Resources.Load("LaserHitParticle", typeof(GameObject))) as GameObject;
                                LaserHitParticle.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                                LaserHitParticle.transform.parent = hit.transform;
                            }
                            IsShooting = false;
                            StartCoroutine("gunDelay");

                            if (hit.transform.tag == "Enemy" || hit.transform.tag == "Player")
                            {
                                //code to decrease enemy energy level
                                if (hit.transform.root.gameObject.GetComponent<CarStats>().Hull > 0)
                                {
                                    hit.transform.root.gameObject.GetComponent<CarStats>().Hull = hit.transform.root.gameObject.GetComponent<CarStats>().Hull - Car.GetComponent<CarStats>().Gun - Car.GetComponent<CarStats>().Car * 2;
                                }
                            }

                        }
                    }

                    

                }
                else
                {
                    IsShooting = false;
                }

            }

            */

        }
    }

    // [Command] // UNet removed in Unity 6
    void CmdHull ()
    {
        hit.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(1);
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
