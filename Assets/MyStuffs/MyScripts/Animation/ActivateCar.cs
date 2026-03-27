using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCar : MonoBehaviour {
    public GameObject CarObj;
	// Use this for initialization
	void Start () {
        CarObj.SetActive(true);
        QualitySettings.SetQualityLevel(0, true);
        Debug.Log("quality " + QualitySettings.GetQualityLevel());
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10;
    }
}
