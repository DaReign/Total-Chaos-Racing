using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class LoadScene : MonoBehaviour {
    private AsyncOperation async = null;
    public Texture2D emptyProgressBar; // Set this in inspector.
    public Texture2D fullProgressBar;  // Set this in inspector.
    private GameObject ProgressSlider;
    private bool LoadingStarted = false;
    private bool fadeOut = true;
    private bool fadeIn = false;
    private string SceneToLoad;
    //private GameObject MainCamera;
    // Use this for initialization
    void Start () {
        ProgressSlider = GameObject.Find("ProgressSlider");
        ProgressSlider.SetActive(false);
      //  MainCamera
    }
	
	// Update is called once per frame
	void Update () {
        //        ProgressSlider.SetActive(false);
        /*
        if (LoadingStarted)
        {
            ProgressSlider.SetActive(true);
            ProgressSlider.GetComponent<Slider>().value = async.progress;
        }
        */

        if (fadeOut)
        {
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity > 0.1f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity -= 0.01f;
            }

        }


        if (fadeIn)
        {
            Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
            if (Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity < 1.0f)
            {
                Camera.main.GetComponent<VignetteAndChromaticAberration>().intensity += 0.01f;
            } 
            else
            {
                fadeIn = false;
                LoadingStarted = true;
                ProgressSlider.SetActive(true);
                StartCoroutine(LevelCoroutine(SceneToLoad));
            }
        }


    }

    public void LoadLevel(System.String Scene)
    {
        SceneToLoad = Scene;
        fadeIn = true;
        fadeOut = false;
     //   LoadingStarted = true;
     //   ProgressSlider.SetActive(true);
     //   StartCoroutine(LevelCoroutine("garage"));
    }


    IEnumerator LevelCoroutine(System.String nomScene)
    {
        //LoadingScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(nomScene);

        //Debug.Log(async.progress);
        while (!async.isDone)
        {
            //      LoadingBar.fillAmount = async.progress;
            //Debug.Log(async.progress);
            ProgressSlider.GetComponent<Slider>().value = async.progress;
            //     textPourcentage.text = async.progress + "%";

            yield return null;

        }
    }
    /*
    void OnGUI()
    {
        if (async != null)
        {
            GUI.DrawTexture(Rect(0, 0, 100, 50), emptyProgressBar);
            GUI.DrawTexture(Rect(0, 0, 100 * async.progress, 50), fullProgressBar);
        }
    }
    */
}
