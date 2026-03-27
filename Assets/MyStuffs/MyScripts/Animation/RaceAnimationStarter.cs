using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceAnimationStarter : MonoBehaviour {
    public GameObject CarObj;
    public GameObject StageCanvasObj;
    public GameObject Camera1Obj;
    private bool FirstPassVar = true;

    // Use this for initialization
    void Start () {
        // StageCanvasObj.SetActive(false);
        QualitySettings.SetQualityLevel(5, true);
        Debug.Log("quality " + QualitySettings.GetQualityLevel());
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 8;
        //Time.timeScale = 0.5F;
        //QualitySettings.vSyncCount = 2;
    }

    // Update is called once per frame
    void Update () {
		
        if (FirstPassVar)
        {
            StageCanvasObj.SetActive(false);
            FirstPassVar = false;
            StartCoroutine("generate");
        }

    }


    IEnumerator generate()
    {
        yield return new WaitForSeconds(5.0f);
        CarObj.SetActive(true);
        Camera1Obj.GetComponent<Animator>().enabled = true;
    }

}
