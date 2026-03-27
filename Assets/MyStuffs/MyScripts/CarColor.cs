using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Networking; // UNet removed in Unity 6

public class CarColor : MonoBehaviour {
    // [SyncVar(hook = "OnChangeColor")] // UNet removed in Unity 6
    public Color color;
    bool canChange = true;
    MeshRenderer[] rends;
    // Use this for initialization
    void OnChangeColor(Color color)
    {
        print("color changed");
        rends = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.color = color;
        }
    }


    void Start () {

        // UNet isServer removed in Unity 6 - color now set locally
        // if (!isServer)
        //     return;

        switch (Random.Range(0,5))
        {
            case 0: color = new Color(0, 0, 0, 1.0F); break;
            case 1: color = new Color(0, 0, 1.0F, 1.0F); break;
            case 2: color = new Color(0, 1.0F, 1.0F, 1.0F); break;
            case 3: color = new Color(0.5F, 0.5F, 0.5F, 1.0F); break;
            case 4: color = new Color(0.0F, 1.0F, 0.0F, 1.0F); break;
            case 5: color = new Color(1.0F, 0.0F, 0.4F, 0.5F); break;
            default: color = color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); break;
        }




        rends = GetComponentsInChildren<MeshRenderer>();
        for (int i =0;i<rends.Length;i++)
        {
            rends[i].material.color = color;
        }
	}
	
	// Update is called once per frame
	void Update () {


        // UNet isServer removed in Unity 6
        // if (isServer)
        // {
            if (canChange)
            {
                StartCoroutine("generate");
            }
        // }
    }

    IEnumerator generate()
    {
        canChange = false;
        yield return new WaitForSeconds(35.0f);
        switch (Random.Range(0, 5))
        {
            case 0: color = new Color(0, 0, 0, 1.0F); break;
            case 1: color = new Color(0, 0, 1.0F, 1.0F); break;
            case 2: color = new Color(0, 1.0F, 1.0F, 1.0F); break;
            case 3: color = new Color(0.5F, 0.5F, 0.5F, 1.0F); break;
            case 4: color = new Color(0.0F, 1.0F, 0.0F, 1.0F); break;
            case 5: color = new Color(1.0F, 0.0F, 0.4F, 0.5F); break;
            default: color = color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)); break;
        }




        rends = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.color = color;
        }
    }


}
