using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;
// using UnityEngine.Networking; // UNet removed in Unity 6

    public class CarUserControlNetwork : MonoBehaviour
    {
        [SerializeField]
        float CameraDistance = 16f;
        [SerializeField]
        float cameraHeight = 16f;
        Transform mainCamera;
        Vector3 cameraOffset;


        private CarController m_Car; // the car controller we want to use
        public float oldhMove;
        public bool NetworkPlayer = false;
        public bool KeyboardSteering = false;

        float hMove;
        float vMove;
        Boolean forwardMove = false;
        Boolean turnRight = false;
        Boolean turnLeft = false;
        Boolean brake = false;


        public float speed;


        private void Start()
        {
            if (NetworkPlayer)
            {
                // UNet isLocalPlayer removed in Unity 6
                // if (!isLocalPlayer)
                // {
                //     Destroy(this);
                //     return;
                // }


                cameraOffset = new Vector3(0f, cameraHeight, -CameraDistance);
                mainCamera = Camera.main.transform;
                MoveCamera();
            }
        }

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

        }


        private void FixedUpdate()
        {
            speed = GetComponent<CarController>().m_Rigidbody.linearVelocity.magnitude;



            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            //#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            //h = hMove;
            //v = vMove;

            if (!KeyboardSteering)
            {
                if (forwardMove)
                {
                    v = 1.0f;
                }
                else
                {
                    if (brake) { v = -1.0f; }
                    else
                    {
                        v = 0.0f;
                    }
                }

                if (turnRight)
                {
                    h = 1.0f;

                    if (forwardMove)
                    {
                        v = 1.0f;
                    }

                }
                else
                {
                    if (turnLeft)
                    {
                        h = -1.0f;
                        if (forwardMove)
                        {
                            v = 1.0f;
                        }

                    }
                    else { h = 0.0f; }
                }

            }



            //            Debug.Log("h " + h);
            //            Debug.Log("left " + turnLeft + " right " + turnRight);
            //            print("v");
            //            print(v);
            if (v > 0.0f)
            {
                GetComponent<CarController>().m_SteerHelper = 0.2f;
            }
            else
            {
                GetComponent<CarController>().m_SteerHelper = 0.4f;
            }

            if (Input.GetKey("space"))
            {
                GetComponent<CarController>().m_SteerHelper = 0.7f;
                GetComponent<CarController>().m_TractionControl = 0.2f;

                // GetComponent<CarController>().m_SlipLimit = 50.0f;
                v = -1.0f;
            }
            else
            {
                GetComponent<CarController>().m_SlipLimit = 0.9f;
            }




            if (speed > 30.0f & v > 0.0f)
            {
                GetComponent<CarController>().m_MaximumSteerAngle = 8.0f;

                if (speed > 40.0f & v > 0.0f)
                {
                    GetComponent<CarController>().m_MaximumSteerAngle = 5.0f;
                }
            }
            else
            {
                GetComponent<CarController>().m_MaximumSteerAngle = 25.0f;
            }


            if (speed > 30.0f && (h > 0.9f || h < -0.9f))
            {
                v = -0.03f;


                if (speed > 40.0f)
                {
                    v = -0.05f;
                }

                if (speed > 50.0f)
                {
                    v = -0.07f;
                }


            }

            h = Mathf.Lerp(h, oldhMove, Time.deltaTime * 5f);


            oldhMove = h;

            m_Car.Move(h, v, v, handbrake);
            //print(h);
            //print(v);
            //hMove = 0;
            //vMove = 0;
            //#else
            //h=1.0f;
            m_Car.Move(h, v, v, 0f);
            //print(h);
            //#endif

            if (NetworkPlayer)
            {
                MoveCamera();
            }
        }

        void MoveCamera()
        {
            mainCamera.position = transform.position;
            mainCamera.rotation = transform.rotation;
            mainCamera.Translate(cameraOffset);
            mainCamera.LookAt(transform);
        }

        /*
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), ""+speed);
        }
        */

        public void turnLeftOn()
        {
            turnLeft = true;
        }

        public void turnLeftOff()
        {
            turnLeft = false;
        }

        public void turnRightOn()
        {
            turnRight = true;
        }

        public void turnRightOff()
        {
            turnRight = false;
        }

        public void accOn()
        {
            print("Move forward clicked");

            forwardMove = true;
        }
        public void accOff()
        {
            forwardMove = false;
        }

        public void BrakeOn()
        {
            brake = true;
        }

        public void BrakeOff()
        {
            brake = false;
        }

        /*Moje funkcje */




    }
