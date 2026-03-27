using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour {
    public List<GameObject> scoresList = new List<GameObject>();
    private GameObject ScoresObject;
    public GameObject Content;
    // Use this for initialization
    void Start () {
        for (var i=0;i<100;i++)
        {
            ScoresObject = Instantiate(Resources.Load("Line", typeof(GameObject))) as GameObject;
            ScoresObject.transform.parent = Content.transform;
            scoresList.Add(ScoresObject);
            //scoresList[i].transform.Find("Points/Text").GetComponent<UnityEngine.UI.Text>().text = "aa";

            scoresList[i].transform.Find("Position/Text").GetComponent<UnityEngine.UI.Text>().text = ""+(i+1);
            //scoresList[i].transform.Find("Position/Text").GetComponent<UnityEngine.UI.Text>().text = "" + (i + 1);

            //            GUILayout.Box("Player id " + i + "got " + PlayerPrefs.GetString("GetPointsP" + i) + " points and " + PlayerPrefs.GetString("GetMoneyP" + i) + " money", GUILayout.Width(400));


            //print(scoresList[i].transform.Find("Points/Text"));
            //print(scoresList[i]);
            //UnityEngine.Random.Range(0, 3);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
