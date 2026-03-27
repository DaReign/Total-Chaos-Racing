using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6
using UnityStandardAssets.Vehicles.Car;


namespace MyNamespace
{
    public class IsNetworkPlayer : MonoBehaviour
    {
        public bool NetworkPlayer;
        public bool Player;
        private bool firstPass=true;
        // Use this for initialization
        void Start()
        {
            if (!NetworkPlayer)
            {
                // UNet components removed in Unity 6
                // GetComponent<NetworkIdentity>().enabled = false;
                // GetComponent<NetworkTransform>().enabled = false;
            }
            /*
            if (!Player)
            {
                GetComponent<CarAIControl>().enabled = true;
            }
            */
        }

        // Update is called once per frame
        void Update()
        {

            if (firstPass)
            {
                this.gameObject.SetActive(true);
                firstPass = false;
            }

        }
    }
}
