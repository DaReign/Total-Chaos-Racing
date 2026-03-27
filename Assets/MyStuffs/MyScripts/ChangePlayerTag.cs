using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6
using MyNamespace;

public class ChangePlayerTag : MonoBehaviour
{
    public GameObject StageCanvasNetwork;
    public bool NetworkPlayer = false;
    // Use this for initialization
    void Start () {

        NetworkPlayer = GetComponent<IsNetworkPlayer>().NetworkPlayer;


        if (NetworkPlayer)
        {
            StageCanvasNetwork = Instantiate(Resources.Load("StageCanvas", typeof(GameObject))) as GameObject;
            // UNet isLocalPlayer removed in Unity 6
            // if (!isLocalPlayer)
            // {
            //     gameObject.tag = "Enemy";
            // }
        }        

    }
}
