using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missile : MonoBehaviour {
    private GameObject Explosion;
    public GameObject RocketObject;
    public bool HomingMissile;
    public float maxAngle;
    public int missileDamage=30;
    public int racerId;
    public int Car;
    public int Gun;
    public string missileId;
    private GameObject[] Enemies;
    public GameObject Target;
    private float shortDistance=10000f;
    private float distance;
    private float angle;
    // public GameObject Creator;
    public List<GameObject> enemiesList = new List<GameObject>();
    public List<GameObject> enemiesSelectedList = new List<GameObject>();

    private int regularMissileStart = 20;
    private int regularMissileTargetingPhase = 20;
    private int homingMissileStart = 100;
    private bool homingMissileStarting = true;

    private int Fuel = 1000;

    public bool missileCollided = false;

    // Use this for initialization
    void Start () {
        missileId = "" + UnityEngine.Random.Range(0, 100) + "" + UnityEngine.Random.Range(0, 100) + "" + UnityEngine.Random.Range(0, 100) + "";
        //Explosion.SetActive(false);
        Enemies = GameObject.FindGameObjectsWithTag("MissileTarget");
        //GetComponent<Rigidbody>().isKinematic = true;



        foreach (GameObject Enemy in Enemies)
        {

            enemiesList.Add(Enemy);
            // angle = Vector3.Angle(Enemy.transform.position, transform.forward);

            angle = Mathf.Atan2(Enemy.transform.position.z - transform.position.z, Enemy.transform.position.x - transform.position.x) * 180 / Mathf.PI;

            /*
            var heading = Enemy.transform.position - transform.position;
            float dot = Vector3.Dot(heading, transform.forward);
            print("dot");
            print(dot);
            */
            float angel = Vector3.Angle(transform.forward, Enemy.transform.position - transform.position);
            //print("angel");
            //print(angel);

            if (Mathf.Abs(angel) > 30)
            {

            }


            if (HomingMissile)
            {
                maxAngle = 60;
            }
            else
            {
                maxAngle = 30;
            }

            //   print(Enemy);
            //   print(angle);
            if (angel < maxAngle)
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


        for (int i=0;i< enemiesList.Count;i++)
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(enemiesList[i].transform.position.x, enemiesList[i].transform.position.y, enemiesList[i].transform.position.z), Color.red);
        }
        for (int i = 0; i < enemiesSelectedList.Count; i++)
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(enemiesSelectedList[i].transform.position.x, enemiesSelectedList[i].transform.position.y, enemiesSelectedList[i].transform.position.z), Color.black);
        }

        // Debug.Log("shortest distance "+ shortDistance);
        // Debug.Log("angle " + angle);

    }


    // Update is called once per frame
    void FixedUpdate () {

        if (Fuel > 0)
        {
            Fuel--;

            if (HomingMissile && Target != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;


                if (homingMissileStart > 0)
                {
                    homingMissileStart--;
                    GetComponent<BoxCollider>().enabled = false;

                    if (homingMissileStarting)
                    {
                        transform.Rotate(transform.rotation.x - 5.0f, transform.rotation.y, transform.rotation.z);
                        homingMissileStarting = false;
                    }
                    //GetComponent<Rigidbody>().isKinematic = false;
                    //GetComponent<Rigidbody>().AddForce(transform.forward * 1000);


                    //Vector3 relativePos = Target.transform.position - transform.position;
                    //Quaternion rotation = Quaternion.LookRotation(relativePos);
                    //transform.rotation = rotation;
                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, 2 * Time.deltaTime);
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x, Target.transform.position.y + 50, Target.transform.position.z), 10 * Time.deltaTime);
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().AddForce(transform.forward * 3000);

                    //transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 30 * Time.deltaTime);
                }
                else
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<BoxCollider>().enabled = true;
                    //print("now should moving to target");
                    //transform.LookAt(Target.transform);
                    //Poprawic ten obrót na wolniejszy jak sie da

                    Vector3 relativePos = Target.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    transform.rotation = rotation;



                    //transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, 100 * Time.deltaTime);


                    transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 120 * Time.deltaTime);
                }


                /*
                Vector3 relativePos = Target.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                transform.rotation = rotation;
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 30 * Time.deltaTime);
                */

            }
            else
            {
                if (regularMissileStart > 0)
                {
                    regularMissileStart--;
                    GetComponent<Rigidbody>().isKinematic = true;
                   // GetComponent<BoxCollider>().enabled = false;


                    if (Target != null)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 80 * Time.deltaTime);
                    }
                    else
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        GetComponent<Rigidbody>().AddForce(transform.forward * 8000);
                        GetComponent<BoxCollider>().enabled = false;
                    }

                    if (Target != null)
                    {
                        Vector3 relativePos = Target.transform.position - transform.position;

                        if (relativePos != Vector3.zero)
                        {
                            Quaternion rotation = Quaternion.LookRotation(relativePos);
                            transform.rotation = rotation;
                        }
                    }
                }
                else
                {
                    GetComponent<BoxCollider>().enabled = true;

                    if (regularMissileTargetingPhase > 0)
                    {
                        regularMissileTargetingPhase--;
                        GetComponent<Rigidbody>().isKinematic = true;
                        if (Target != null)
                        {
                            Vector3 relativePos = Target.transform.position - transform.position;
                            Quaternion rotation = Quaternion.LookRotation(relativePos);
                            transform.rotation = rotation;
                            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 500 * Time.deltaTime);
                        }
                        else
                        {
                            transform.position = Vector3.MoveTowards(transform.position, transform.position, 500 * Time.deltaTime);
                        }
                    }
                    else 
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                       // if (Vector3.Distance(Target.transform.position, transform.position) > 5.0f)
                       // {
                            GetComponent<Rigidbody>().AddForce(transform.forward * 30000);
                       // }
                       // else
                       // {
                            GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
                       // }
                    }
                    /*
                    Vector3 relativePos = Target.transform.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    transform.rotation = rotation;
                    GetComponent<Rigidbody>().AddForce(transform.forward * 10);
                    */
                }
            }
        }
        else
        {
            StartCoroutine("destroy");
        }


    }
    

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("Missile Collided with "+ other.transform.root.gameObject);
       // Debug.Log("Missile Collided tag " + other.transform.root.gameObject.tag);

        if (gameObject.tag == "Wall" || other.transform.root.gameObject.tag == "Road")
        {
            return;
        }

        if (!missileCollided)
        {
            missileCollided = true;
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("destroy");



            if (other.transform.root.gameObject.tag == "Player" || other.transform.root.gameObject.tag == "Enemy")
            {

                if (other.transform.root.gameObject.GetComponent<CarStats>().Hull >= 30)
                {
                    //other.transform.root.gameObject.GetComponent<CarStats>().Hull -= 30;
                    //Debug.Log("take damage");
                    other.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(missileDamage);
                }
                else
                {
                    //other.transform.root.gameObject.GetComponent<CarStats>().TakeDamage(30);
                    other.transform.root.gameObject.GetComponent<CarStats>().Hull = 0;
                }
                //Debug.Log("Missile id " + missileId + " created by " + racerId + " car level " + Car + " and gun level " + Gun);
                //Debug.Log("Collided with " + other.transform.root.gameObject.GetComponent<CarStats>().racerId + " and inflicted " + missileDamage + " damage ");
                other.transform.root.gameObject.GetComponent<Rigidbody>().AddExplosionForce(125000.0f, transform.position, 0.5f, 300.0F);
            }
        }
    }

    IEnumerator destroy()
    {
        if (gameObject != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            //print(Explosion);
            Explosion = Instantiate(Resources.Load("Explosion", typeof(GameObject))) as GameObject;
            Explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            GetComponent<BoxCollider>().enabled = false;
            RocketObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}
