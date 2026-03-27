using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarAIControl : MonoBehaviour
    {
        public enum BrakeCondition
        {
            NeverBrake,                 // the car simply accelerates at full throttle all the time.
            TargetDirectionDifference,  // the car will brake according to the upcoming change in direction of the target. Useful for route-based AI, slowing for corners.
            TargetDistance,             // the car will brake as it approaches its target, regardless of the target's direction. Useful if you want the car to
                                        // head for a stationary target and come to rest when it arrives there.
        }

        // This script provides input to the car controller in the same way that the user control script does.
        // As such, it is really 'driving' the car, with no special physics or animation tricks to make the car behave properly.

        // "wandering" is used to give the cars a more human, less robotic feel. They can waver slightly
        // in speed and direction while driving towards their target.

        [SerializeField] [Range(0, 1)] private float m_CautiousSpeedFactor = 0.05f;               // percentage of max speed to use when being maximally cautious
        [SerializeField] [Range(0, 180)] private float m_CautiousMaxAngle = 50f;                  // angle of approaching corner to treat as warranting maximum caution
        [SerializeField] private float m_CautiousMaxDistance = 100f;                              // distance at which distance-based cautiousness begins
        [SerializeField] private float m_CautiousAngularVelocityFactor = 30f;                     // how cautious the AI should be when considering its own current angular velocity (i.e. easing off acceleration if spinning!)
        [SerializeField] private float m_SteerSensitivity = 0.05f;                                // how sensitively the AI uses steering input to turn to the desired direction
        [SerializeField] private float m_AccelSensitivity = 0.04f;                                // How sensitively the AI uses the accelerator to reach the current desired speed
        [SerializeField] private float m_BrakeSensitivity = 1f;                                   // How sensitively the AI uses the brake to reach the current desired speed
        [SerializeField] private float m_LateralWanderDistance = 3f;                              // how far the car will wander laterally towards its target
        [SerializeField] private float m_LateralWanderSpeed = 0.1f;                               // how fast the lateral wandering will fluctuate
        [SerializeField] [Range(0, 1)] private float m_AccelWanderAmount = 0.1f;                  // how much the cars acceleration will wander
        [SerializeField] private float m_AccelWanderSpeed = 0.1f;                                 // how fast the cars acceleration wandering will fluctuate
        [SerializeField] private BrakeCondition m_BrakeCondition = BrakeCondition.TargetDistance; // what should the AI consider when accelerating/braking?
        [SerializeField] private bool m_Driving;                                                  // whether the AI is currently actively driving or stopped.
        [SerializeField] private Transform m_Target;                                              // 'target' the target object to aim for.
        [SerializeField] private bool m_StopWhenTargetReached;                                    // should we stop driving when we reach the target?
        [SerializeField] private float m_ReachTargetThreshold = 40;                                // proximity to target to consider we 'reached' it, and stop driving.

        private float m_RandomPerlin;             // A random value for the car to base its wander on (so that AI cars don't all wander in the same pattern)
        private CarController m_CarController;    // Reference to actual car controller we are controlling
        private float m_AvoidOtherCarTime;        // time until which to avoid the car we recently collided with
        private float m_AvoidOtherCarSlowdown;    // how much to slow down due to colliding with another car, whilst avoiding
        private float m_AvoidPathOffset;          // direction (-1 or 1) in which to offset path to avoid other car, whilst avoiding
        private Rigidbody m_Rigidbody;
        public float steer;
        /*Obstacle Avoidance*/
        public float speed = 10.0f;
        //public Vector3 target = new Vector3(0, 0, 0);
        private Vector3 dir = new Vector3(0, 0, 0);
        private Vector3 dirFull = new Vector3(0, 0, 0);
        private Boolean obstacleDetected = false;
        private Vector3 transformOldPosition;
        private int stuck = 0;
        private int realStuck = 0;
        private GameObject PreviousWayPoint;
        private GameObject ForcedMoveAnimation;
        private int removeForcedMoveAnimationIterator = 0;
        private Boolean removeForcedMoveAnimation = false;
        private int roadClear = 0;
        private int reverseRideCount = 0;
        private Boolean reverseRide = false;
        private int wayPointNumberToFinish = 0;

        private GameObject Global;
        private int currentWaypoint = 0;
        private Boolean LeftHit = false;
        private Boolean LeftHit2 = false;
        private Boolean RightHit = false;
        private Boolean RightHit2 = false;
        private Boolean FrontHit = false;
        private Boolean FrontLeftHit = false;
        private Boolean FrontRightHit = false;
        private int RaysHit = 0;
        private int slowDown;
        private Boolean nitro;
        private Boolean startPhase = true;
        private int startPhaseCounter = 0;
        float rayDistance = 5.0f;
        RaycastHit hit;
        public GameObject RayCastSource;


        private int allWaypointsToGo;


        public float startTorque;
        public int torqueBonusCount;



        public float CarStat;
        public float EngineStat;
        public float TiresStat;
        private CarStats m_CarStats;    // Reference to actual car controller we are controlling


        public int cantReachWaypointCounter = 0;
        //private int startPhase;

        public float startSlipLimit;
        private bool SetThisOnceVar = false;

        //debug variables
        public float debugAccel;
        public float carSpeed;
        public bool NotMovieVar = true;


        private void Start()
        {
            startSlipLimit = m_CarController.m_SlipLimit;

            CarStat = (float)(GetComponent<CarStats>().Car);
            EngineStat = (float)(GetComponent<CarStats>().Engine);
            TiresStat = (float)(GetComponent<CarStats>().Tires);

            Global = GameObject.Find("Global");
            RayCastSource = transform.Find("RayCastSource").gameObject;

            //Debug.Log(Global.GetComponent<WaypointsList>().listOfWaypoints);
            //m_Target = Global.GetComponent<WaypointsList>().listOfWaypoints[0].transform;
            SetTarget(Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform);

            allWaypointsToGo = Global.GetComponent<WaypointsList>().listOfWaypoints.Count * Global.GetComponent<Stage>().laps;

            GetComponent<CarStats>().waypointsToGo = allWaypointsToGo;
            startTorque = GetComponent<CarController>().m_FullTorqueOverAllWheels;

        }



        private void Awake()
        {
            m_CarStats = GetComponent<CarStats>();

            // get the car controller reference
            m_CarController = GetComponent<CarController>();

            // give the random perlin a random value
            m_RandomPerlin = Random.value*100;

            m_Rigidbody = GetComponent<Rigidbody>();
        }


        private void drawRay(Vector3 startPos, float xOffset, float yOffset, float zOffset, float xPar, float yPar, float zPar, float rayDistance, RaycastHit hit, Transform transform, String rayName)
        {
            Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.green);

            if (Physics.Raycast(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)),out hit, rayDistance))
            {
                if (hit.transform != this.transform&& hit.transform.gameObject.tag != "StartLine")
                {
                    Debug.DrawRay(startPos + new Vector3(xOffset, yOffset, zOffset), transform.TransformDirection(new Vector3(xPar, yPar, zPar)) * rayDistance, Color.red);
                    if (rayName == "LeftHit") {LeftHit=true;}
                    if (rayName == "RightHit") {RightHit=true;}
                    if (rayName == "FrontHit") {FrontHit=true;}
                }
            }
         }



        private void FixedUpdate()
        {


            if ( GetComponent<CarStats>().wrecked)
            {
                m_Target = null;
                m_StopWhenTargetReached = true;
                m_Driving = false;
            }


            if (m_Target)
            {
                Debug.DrawLine(transform.position, m_Target.position, Color.blue);
            }

            if (m_Target == null || !m_Driving)
            {

                // Car should not be moving,
                // use handbrake to stop
                m_CarController.Move(0, 0, -1f, 1f);
            }
            else
            {
                Vector3 fwd = transform.forward;
                if (m_Rigidbody.linearVelocity.magnitude > m_CarController.MaxSpeed*0.1f)
                {
                    fwd = m_Rigidbody.linearVelocity;
                }

                float desiredSpeed = m_CarController.MaxSpeed;

                // now it's time to decide if we should be slowing down...
                switch (m_BrakeCondition)
                {
                    case BrakeCondition.TargetDirectionDifference:
                        {
                            // the car will brake according to the upcoming change in direction of the target. Useful for route-based AI, slowing for corners.

                            // check out the angle of our target compared to the current direction of the car
                            float approachingCornerAngle = Vector3.Angle(m_Target.forward, fwd);

                            // also consider the current amount we're turning, multiplied up and then compared in the same way as an upcoming corner angle
                            float spinningAngle = m_Rigidbody.angularVelocity.magnitude*m_CautiousAngularVelocityFactor;

                            // if it's different to our current angle, we need to be cautious (i.e. slow down) a certain amount
                            float cautiousnessRequired = Mathf.InverseLerp(0, m_CautiousMaxAngle,
                                                                           Mathf.Max(spinningAngle,
                                                                                     approachingCornerAngle));
                            desiredSpeed = Mathf.Lerp(m_CarController.MaxSpeed, m_CarController.MaxSpeed*m_CautiousSpeedFactor,
                                                      cautiousnessRequired);
                            break;
                        }

                    case BrakeCondition.TargetDistance:
                        {
                            // the car will brake as it approaches its target, regardless of the target's direction. Useful if you want the car to
                            // head for a stationary target and come to rest when it arrives there.

                            // check out the distance to target
                            Vector3 delta = m_Target.position - transform.position;
                            float distanceCautiousFactor = Mathf.InverseLerp(m_CautiousMaxDistance, 0, delta.magnitude);

                            // also consider the current amount we're turning, multiplied up and then compared in the same way as an upcoming corner angle
                            float spinningAngle = m_Rigidbody.angularVelocity.magnitude*m_CautiousAngularVelocityFactor;

                            // if it's different to our current angle, we need to be cautious (i.e. slow down) a certain amount
                            float cautiousnessRequired = Mathf.Max(
                                Mathf.InverseLerp(0, m_CautiousMaxAngle, spinningAngle), distanceCautiousFactor);
                            desiredSpeed = Mathf.Lerp(m_CarController.MaxSpeed, m_CarController.MaxSpeed*m_CautiousSpeedFactor,
                                                      cautiousnessRequired);
                            break;
                        }

                    case BrakeCondition.NeverBrake:
                        break;
                }

                // Evasive action due to collision with other cars:

                // our target position starts off as the 'real' target position
                Vector3 offsetTargetPos = m_Target.position;

                // if are we currently taking evasive action to prevent being stuck against another car:
                if (Time.time < m_AvoidOtherCarTime)
                {
                    // slow down if necessary (if we were behind the other car when collision occured)
                    desiredSpeed *= m_AvoidOtherCarSlowdown;

                    // and veer towards the side of our path-to-target that is away from the other car
                    offsetTargetPos += m_Target.right*m_AvoidPathOffset;
                }
                else
                {
                    // no need for evasive action, we can just wander across the path-to-target in a random way,
                    // which can help prevent AI from seeming too uniform and robotic in their driving
                    offsetTargetPos += m_Target.right*
                                       (Mathf.PerlinNoise(Time.time*m_LateralWanderSpeed, m_RandomPerlin)*2 - 1)*
                                       m_LateralWanderDistance;
                }

                // use different sensitivity depending on whether accelerating or braking:
                float accelBrakeSensitivity = (desiredSpeed < m_CarController.CurrentSpeed)
                                                  ? m_BrakeSensitivity
                                                  : m_AccelSensitivity;

                // decide the actual amount of accel/brake input to achieve desired speed.
                float accel = Mathf.Clamp((desiredSpeed - m_CarController.CurrentSpeed)*accelBrakeSensitivity, -1, 1);

                // add acceleration 'wander', which also prevents AI from seeming too uniform and robotic in their driving
                // i.e. increasing the accel wander amount can introduce jostling and bumps between AI cars in a race
                accel *= (1 - m_AccelWanderAmount) +
                         (Mathf.PerlinNoise(Time.time*m_AccelWanderSpeed, m_RandomPerlin)*m_AccelWanderAmount);

                // calculate the local-relative position of the target, to steer towards
                Vector3 localTarget = transform.InverseTransformPoint(offsetTargetPos);

                // work out the local angle towards the target
                float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z)*Mathf.Rad2Deg;

                // get the amount of steering needed to aim the car towards the target
                steer = Mathf.Clamp(targetAngle*m_SteerSensitivity, -1, 1)*Mathf.Sign(m_CarController.CurrentSpeed);


                rayDistance = 5.0f; 
                drawRay(RayCastSource.transform.position, 0, 0, 0, 0, 0.05f, 1, 2.0f, hit, transform,"FrontHit");
                drawRay(RayCastSource.transform.position, -0.125f, 0, 0, -0.25f, 0.05f, 1, rayDistance, hit, transform,"LeftHit");
                drawRay(RayCastSource.transform.position, 0.125f, 0, 0, 0.25f, 0.05f, 1, rayDistance, hit, transform,"RightHit");
                /*
                drawRay(RayCastSource.transform.position, -0.125f, 0, 0, -0.5f, 0.05f, 1, 5.0f, hit, transform, "LeftHit");
                drawRay(RayCastSource.transform.position, 0.125f, 0, 0, 0.5f, 0.05f, 1, 5.0f, hit, transform, "RightHit");
                drawRay(RayCastSource.transform.position, -0.125f, 0, 0, -1.5f, 0.05f, 1, 3.0f, hit, transform, "LeftHit");
                drawRay(RayCastSource.transform.position, 0.125f, 0, 0, 1.5f, 0.05f, 1, 3.0f, hit, transform, "RightHit");
                drawRay(RayCastSource.transform.position, -0.125f, 0, 0, -2.5f, 0.05f, 1, 2.0f, hit, transform, "LeftHit");
                drawRay(RayCastSource.transform.position, 0.125f, 0, 0, 2.5f, 0.05f, 1, 2.0f, hit, transform, "RightHit");
                */
                // if (NotMovieVar)
                // {

                if (!SetThisOnceVar)
                {
                    m_SteerSensitivity = 0.035f + (TiresStat * 0.0005f) + (CarStat * 0.0015f);
                    GetComponent<CarController>().m_SteerHelper = 0.5f + (TiresStat * 0.025f) + (CarStat * 0.025f);
                    SetThisOnceVar = true;
                }
                //}
                    //m_SteerSensitivity = 1.0f;

                //m_CarController.m_FullTorqueOverAllWheels = startTorque + (EngineStat *20) + (CarStat * 50);


                if (torqueBonusCount<300)
                {
                    m_CarController.m_FullTorqueOverAllWheels = startTorque + Random.Range(10,20* CarStat+10*EngineStat);
                    torqueBonusCount++;
                }
                /*
                if (FrontHit)
                {
                    accel = 0.1f;
                    //print("brake something in front");
                    if (Random.Range(0, 10) < 5)
                    {
                        steer = 1.0f;
                    }
                    else
                    {
                        steer = -1.0f;
                    }
                }
                */
                if (FrontHit)
                {
                    accel = 0.1f;
                    //print("brake something in front");
                    /*
                    if (Random.Range(0, 10) < 5)
                    {
                        steer = 1.0f;
                    }
                    else
                    {
                        steer = -1.0f;
                    }
                    */
                }
                if (LeftHit||(LeftHit&&FrontHit)) { steer = 1.0f;
                    accel = 1.0f;
                    torqueBonusCount = 0;
                    // print("steer right");
                    //Debug.Log("" + gameObject + "steer right ");
                }
                if (RightHit || (RightHit && FrontHit)) { steer = -1.0f;
                    accel = 1.0f;
                    torqueBonusCount = 0;
                    //print("steer left");
                    //Debug.Log("" + gameObject + "steer left ");
                }


                if ( (LeftHit&&RightHit && FrontHit)) {
                    reverseRide = true;
                    reverseRideCount = 50;
                    LeftHit = false;
                    RightHit = false;
                    FrontHit = false;
                }


                if (Vector3.Distance(transformOldPosition, transform.position) < 0.05f&& !removeForcedMoveAnimation)
                {
                    realStuck++;
                    //stuck++;
                }
                else
                {
                    realStuck = 0;
                }

                if (cantReachWaypointCounter>5000)
                {
                    realStuck = 300;
                    cantReachWaypointCounter = 0;
                }


                if (realStuck==300)
                {
                    realStuck = 0;


                    if (currentWaypoint > 0) {
                        PreviousWayPoint = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint - 1];
                    }
                    else
                    {
                        PreviousWayPoint = Global.GetComponent<WaypointsList>().listOfWaypoints[Global.GetComponent<WaypointsList>().listOfWaypoints.Count - 1];
                    }

                    transform.position = new Vector3(PreviousWayPoint.transform.position.x, PreviousWayPoint.transform.position.y+50, PreviousWayPoint.transform.position.z);

                    //if (currentWaypoint > 0)
                    //{
                        transform.LookAt(Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform);
                    //}
                    //else
                    //{

                    //}
                    ForcedMoveAnimation = Instantiate(Resources.Load("ETF_M_Holy Shine", typeof(GameObject))) as GameObject;
                    ForcedMoveAnimation.transform.position = new Vector3(transform.position.x-6.85f, transform.position.y+1.03f, transform.position.z-0.89f);
                    ForcedMoveAnimation.transform.parent = transform;

                    removeForcedMoveAnimation = true;




                    stuck = 0;
                    reverseRide = false;
                    reverseRideCount = 0;


                }


                if (removeForcedMoveAnimation)
                {
                    if (removeForcedMoveAnimationIterator<400)
                    {
                        removeForcedMoveAnimationIterator++;
                        accel = 0.0f;
                    }
                    else
                    {
                        Destroy(ForcedMoveAnimation);
                        removeForcedMoveAnimationIterator = 0;
                        removeForcedMoveAnimation = false;
                        stuck = 0;
                        reverseRide = false;
                        reverseRideCount = 0;
                        accel = 1.0f;
                        //SetTarget(Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform);
                    }
                }



                if (Vector3.Distance(transformOldPosition, transform.position) < 0.01f && reverseRide==false && !removeForcedMoveAnimation)
                {
                    //print("Stuck");
                    stuck++;
                    //print(stuck);
                    //print(reverseRide);
                }
                else
                {
                    roadClear++;
                    //print("roadClear");
                    //print(roadClear);
                }
                transformOldPosition = transform.position;

                
                if (roadClear>=50)
                {
                    roadClear = 0;
                    stuck = 0;
                }
                
                if (stuck>=50)
                {
                    reverseRideCount = 150;
                    reverseRide = true;
                    stuck = 0;
                }





                if (!reverseRide)
                {

                   
                    //if ((RaysHit >= 2)|| (RaysHit >= 2))
                    if ((FrontLeftHit&&FrontRightHit) || (RaysHit >= 2))

                    {
                        //steer = 0;
                        accel = -1.0f;
                        reverseRide = true;
                        reverseRideCount = 50;
                        //print("2 rays hit then move back");
                    }
                    RaysHit = 0;
                    LeftHit = false;
                    RightHit = false;
                    FrontHit = false;
                    FrontLeftHit = false;
                    FrontRightHit = false;
                    slowDown = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].GetComponent<WayPointSlowDown>().slowDown;
                    nitro = Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].GetComponent<WayPointSlowDown>().nitro;

                    if (nitro)
                    {
                        if (GetComponent<CarStats>().Nitro > 0)
                        {
                            GetComponent<NitroAi>().nitro = true;
                        }
                    }
                    else
                    {
                        GetComponent<NitroAi>().nitro = false;
                    }

                    /*
                    if (m_CarController.CurrentSpeed > slowDown)
                    {

                        if (m_CarController.CurrentSpeed - slowDown > 10)
                        {
                            accel = -0.1f;
                        }
                        else
                        {
                            if (m_CarController.CurrentSpeed - slowDown > 20)
                            {
                                accel = -0.5f;
                            }
                            else
                            {
                                accel = -1.0f;
                            }
                        }
                    }
                    */
                    /*
                    if ((steer==1.0f || steer==-1.0f)&& m_CarController.CurrentSpeed>35)
                    {
                        accel = -0.1f;
                    }
                    */

                    if (accel>0&&accel<0.9f) { accel = 0.999f; }

                    m_CarController.Move(steer, accel, accel, 0f);
                }
                else
                {
                    //print("reverse ride");
                    accel = -1.0f;
                    
                    if (targetAngle<0)
                    {
                        steer = 0.98f;
                    }
                    else
                    {
                        steer = -0.98f; 
                    }
                    if (accel > 0 && accel < 0.9f) { accel = 0.999f; }

                    m_CarController.Move(steer, accel, accel, 0f);
                }

                if (reverseRideCount>0)
                {
                    reverseRideCount--;
                    //print("Counting reverse");
                    //print(reverseRideCount); 
                } 
                else
                {
                    reverseRide = false;
                }
                // }
                obstacleDetected = false;

                /*
                if (startPhaseCounter < 400)
                {
                    //print("start phase");
                    reverseRide = false;
                    accel = 1.0f;
                    startPhaseCounter++;
                    //m_CarController.m_SlipLimit = 3000.0f;
                    if (accel > 0 && accel < 0.9f) { accel = 0.9f; }
                    m_CarController.Move(steer, accel, accel, 0f);

                }
                else
                {
                    m_CarController.m_SlipLimit = startSlipLimit + (TiresStat*0.05f) + (CarStat * 0.05f);
                    //print("low slip");
                }
                */
/*
                if (m_CarController.CurrentSpeed < 20 + (TiresStat * 1.0f) + (CarStat * 1.0f) )
                {
                    m_CarController.m_MaximumSteerAngle = 50.0f;
                }
                else
                {
                    m_CarController.m_MaximumSteerAngle = 15.0f + (TiresStat * 0.25f) + (CarStat * 0.25f);
                }

                if (m_CarController.CurrentSpeed>60)
                {
                    m_CarController.m_MaximumSteerAngle = 8.0f + (TiresStat * 0.125f) + (CarStat * 0.125f);
                    m_CarController.m_SlipLimit = startSlipLimit + (TiresStat * 0.2f) + (CarStat * 0.1f);

                    if (m_CarController.CurrentSpeed > 100)
                    {
                        m_CarController.m_MaximumSteerAngle = 5.0f + (TiresStat * 0.25f) + (CarStat * 0.25f);
                    }
                    m_CarController.m_SlipLimit = startSlipLimit + (TiresStat * 0.5f) + (CarStat * 0.25f);
                    if (accel > 0 && accel < 0.9f) { accel = 0.9f; }

                    m_CarController.Move(steer, accel, accel, 0f);
                }
                else
                {
                    m_CarController.m_MaximumSteerAngle = 15.0f + (TiresStat * 0.2f) + (CarStat * 0.2f);
                    if (accel > 0 && accel < 0.9f) { accel = 0.9f; }

                    m_CarController.Move(steer, accel, accel, 0f);
                }
*/




                cantReachWaypointCounter++;

                if (Vector3.Distance(m_Target.position, transform.position) < 20.0f)
                    {

                        if (currentWaypoint == 0)
                            {
                                cantReachWaypointCounter = 0;
                                GetComponent<CarStats>().currentLap++;
                                GetComponent<CarStats>().startWaypointReached = true;
                            }

                    reverseRide = false;
                    //print(Global.GetComponent<WaypointsList>().listOfWaypoints.Count);

                    if (GetComponent<CarStats>().startWaypointReached) {

                        if (currentWaypoint < Global.GetComponent<WaypointsList>().listOfWaypoints.Count - 1)
                        {
                            cantReachWaypointCounter = 0;
                            currentWaypoint++;
                            SetTarget(Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform);
                        }
                        else
                        {
                            cantReachWaypointCounter = 0;
                            currentWaypoint = 0;
                            SetTarget(Global.GetComponent<WaypointsList>().listOfWaypoints[currentWaypoint].transform);
                        }

                        GetComponent<CarStats>().waypointsCleared++;

                    }

                    if (GetComponent<CarStats>().raceFinished && wayPointNumberToFinish == 0)
                    {
                        wayPointNumberToFinish = Random.Range(0, 10);
                        if (wayPointNumberToFinish > Global.GetComponent<WaypointsList>().listOfWaypoints.Count)
                        {
                            wayPointNumberToFinish = Global.GetComponent<WaypointsList>().listOfWaypoints.Count - 1;
                        }
                    }


                    if (GetComponent<CarStats>().raceFinished&& currentWaypoint == wayPointNumberToFinish)
                    {
                        m_Target = null;
                        m_StopWhenTargetReached = true;
                        m_Driving = false;

                        //GetComponent<Rigidbody>().mass = 100;
                    }

                }

                //debugAccel = accel;
                //carSpeed = GetComponent<CarController>().m_Rigidbody.velocity.magnitude;

                // if appropriate, stop driving when we're close enough to the target.
                if (m_StopWhenTargetReached && localTarget.magnitude < m_ReachTargetThreshold)
                {
                    m_Driving = false;
                }
            }
        }


 
        private void OnCollisionStay(Collision col)
        {
            // detect collision against other cars, so that we can take evasive action
            if (col.rigidbody != null)
            {
                var otherAI = col.rigidbody.GetComponent<CarAIControl>();
                if (otherAI != null)
                {
                    // we'll take evasive action for 1 second
                    m_AvoidOtherCarTime = Time.time + 1;

                    // but who's in front?...
                    if (Vector3.Angle(transform.forward, otherAI.transform.position - transform.position) < 90)
                    {
                        // the other ai is in front, so it is only good manners that we ought to brake...
                        m_AvoidOtherCarSlowdown = 0.5f;
                    }
                    else
                    {
                        // we're in front! ain't slowing down for anybody...
                        m_AvoidOtherCarSlowdown = 1;
                    }

                    // both cars should take evasive action by driving along an offset from the path centre,
                    // away from the other car
                    var otherCarLocalDelta = transform.InverseTransformPoint(otherAI.transform.position);
                    float otherCarAngle = Mathf.Atan2(otherCarLocalDelta.x, otherCarLocalDelta.z);
                    m_AvoidPathOffset = m_LateralWanderDistance*-Mathf.Sign(otherCarAngle);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            m_Target = target;
            m_Driving = true;
        }
    }



}
