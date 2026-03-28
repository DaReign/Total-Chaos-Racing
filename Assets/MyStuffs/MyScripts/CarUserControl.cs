using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        [SerializeField] float CameraDistance = 10f;
        [SerializeField] float cameraHeight = 10f;
        Transform mainCamera;
        Vector3 cameraOffset;


        private CarController m_Car; // the car controller we want to use
        public float oldhMove;
        public bool NetworkPlayer = false;
        public bool KeyboardSteering = false;
        public bool ForcedControllVar = false;

        float hMove;
        float vMove;
        Boolean forwardMove = false;
        Boolean turnRight = false;
        Boolean turnLeft = false;
        Boolean brake = false;

        // Set by UI joystick (–1..1), merged with keyboard/gamepad
        [NonSerialized] public float touchSteerAxis;

        // Set by TestHUDController keyboard/gamepad polling
        [NonSerialized] public float kbSteerAxis;
        [NonSerialized] public bool kbGas;
        [NonSerialized] public bool kbBrake;


        public float speed;
        private GameObject SpeedText;
        public bool NotMovieVar = true;


        private void Start ()
        {
            SpeedText = GameObject.Find("SpeedText");


            if (GetComponent<IsNetworkPlayer>().Player == false)
            {
                this.enabled = false;
            }



           // print("set variable");
            NetworkPlayer = GetComponent<IsNetworkPlayer>().NetworkPlayer;
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

            if (NotMovieVar)
            {

                speed = GetComponent<CarController>().m_Rigidbody.linearVelocity.magnitude;

                if (SpeedText != null)
                {
                    SpeedText.GetComponent<UnityEngine.UI.Text>().text = "Speed" + speed;
                }
            }

            //Debug.Log("Car speed " + speed);

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
//#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            //h = hMove;
            //v = vMove;

            /*
            if (Input.GetKey("m"))
            {
                GetComponent<MissileFire>().missile(1);
                print("homing missile");
            }
            if (Input.GetKey("n"))
            {
                GetComponent<MissileFire>().missile(0);
                print("missile");
            }
            */



            // Merge touch/UI input on top of keyboard/gamepad
            // Keyboard/WASD/Gamepad (from TestHUDController)
            if (kbGas) v = 1.0f;
            else if (kbBrake) v = -1.0f;
            if (Mathf.Abs(kbSteerAxis) > 0.01f) h = kbSteerAxis;

            // Touch button overrides
            if (forwardMove) v = 1.0f;
            else if (brake) v = -1.0f;

            if (Mathf.Abs(touchSteerAxis) > 0.01f) h = touchSteerAxis;
            else if (turnRight) h = 1.0f;
            else if (turnLeft) h = -1.0f;


            if (NotMovieVar)
            {
                if (speed < 10)
                {
                    //  GetComponent<CarController>().m_SlipLimit = 5f;
                }
                else
                {
                    //GetComponent<CarController>().m_SlipLimit = 0.1f+(0.01f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car);
                }

                if (speed > 30.0f & v > 0.0f)
                {
                    GetComponent<CarController>().m_MaximumSteerAngle = 6.0f;

                    if (speed > 40.0f & v > 0.0f)
                    {
                        GetComponent<CarController>().m_MaximumSteerAngle = 5.0f;
                    }
                    if (speed > 50.0f & v > 0.0f)
                    {
                        GetComponent<CarController>().m_MaximumSteerAngle = 4.0f;
                    }
                }
                else
                {
                    GetComponent<CarController>().m_MaximumSteerAngle = 15.0f;
                }

                if (speed < 20.0f)
                {
                    GetComponent<CarController>().m_SlipLimit = 500-(50* GetComponent<CarStats>().Engine)+ (50 * GetComponent<CarStats>().Tires);
                    GetComponent<CarController>().m_MaximumSteerAngle = 45.0f;
                }
                else
                {
                    GetComponent<CarController>().m_SlipLimit = 101 - (10 * GetComponent<CarStats>().Engine) + (10 * GetComponent<CarStats>().Tires) + (30 * GetComponent<CarStats>().Car);
                }


                if (speed > (100.0f - (0.7f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car)) && (h > 0.9f || h < -0.9f))
                {
                    v = -0.003f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car;
                    print("Should brake");
                    handbrake = 1.0f;
                    if (speed > 100.0f - (0.6f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car))
                    {
                        v = -0.005f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car;
                    }

                    if (speed > 100.0f - (0.5f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car))
                    {
                        v = -0.007f * GetComponent<CarStats>().Tires * GetComponent<CarStats>().Car;
                    }


                }
                h = Mathf.Lerp(h, oldhMove, Time.deltaTime * 5f);
                oldhMove = h;
            }




            m_Car.Move(h, v, v, handbrake);
            //print(h);
            //print(v);
            //hMove = 0;
            //vMove = 0;
//#else
            //h=1.0f;
           // m_Car.Move(h, v, v, 0f);
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
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 25, 300, 50), "" + speed);
        }
        */

        public void turnLeftOn()
        {
            print("Move left clicked");
            turnLeft = true;
        }

        public void turnLeftOff()
        {
            turnLeft = false;
        }

        public void turnRightOn()
        {
            print("Move right clicked");
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
            print("Move brake clicked");
            brake = true;
        }

        public void BrakeOff()
        {
            brake = false;
        }

        /*Moje funkcje */




    }
}
