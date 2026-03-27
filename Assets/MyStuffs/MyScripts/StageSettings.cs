using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSettings : MonoBehaviour {

    private GameObject Global;
    public GameObject MainSettings;
    public GameObject GraphicQuality;
    public GameObject CameraRange;
    public GameObject SettingsButton;
    public GameObject GraphicQualityButton;
    public GameObject CameraRangeButton;
    public GameObject CameraRangeValue;
    public GameObject CameraRangeSlider;
    public GameObject SettingsBackButton;
    public GameObject CurrentGraphicLevel;
    public GameObject ReturnToGameButton;
    public GameObject StaticObjectsObj;
    public GameObject MusicButton;
    public GameObject MainMenuObj;

    public Slider mainSlider;

    private string CurrentQualityLevel;

    public bool MainMenu = false;


    // Use this for initialization
    void Start () {
        Global = GameObject.Find("Global");
        StaticObjectsObj = GameObject.Find("Static");
        getQuality();
        Debug.Log("Graphic LEVEL " + QualitySettings.GetQualityLevel());
        if (QualitySettings.GetQualityLevel() == 0)
        {
            if (StaticObjectsObj) {
                StaticObjectsObj.SetActive(false);
                Camera.main.GetComponent<CameraWheather>().LightRain.SetActive(false);
                Camera.main.GetComponent<CameraWheather>().HeavyRain.SetActive(false);
                Camera.main.GetComponent<CameraWheather>().Thunder.SetActive(false);
            }
        }
        else
        {
            if (StaticObjectsObj) { StaticObjectsObj.SetActive(true); }
        }
    }
	
    public void Music ()
    {
        string MusicVar = "";

        MusicVar= PlayerPrefs.GetString("Music");

        if (MusicVar == "Off")
        {
            MusicButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music On";
            PlayerPrefs.SetString("Music", "On");
            MainMenuObj.GetComponent<MainMenu>().AudioSourceObj.SetActive(true);
        }
        else
        {
            MusicButton.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Music Off";
            PlayerPrefs.SetString("Music", "Off");
            MainMenuObj.GetComponent<MainMenu>().AudioSourceObj.SetActive(false);
        }
    }


    public void clearViews () {
        GraphicQuality.SetActive(false);
        CameraRange.SetActive(false);
        MainSettings.SetActive(false);
    }

    public void ReturnToGameButtonFunction()
    {
        clearViews();
        SettingsBackButton.SetActive(false);
        ReturnToGameButton.SetActive(false);
        Time.timeScale = 1.0F;
    }


    public void SettingsButtonFunction()
    {
        clearViews();
        SettingsBackButton.SetActive(false);
        MainSettings.SetActive(true);
        ReturnToGameButton.SetActive(true);
        Time.timeScale = 0.0F;
    }
    public void GraphicQualityButtonFunction()
    {
        SettingsBackButton.SetActive(true);
        clearViews();
        GraphicQuality.SetActive(true);
        getQuality();
        CurrentGraphicLevel.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current Quality"+ CurrentQualityLevel;
    }
    public void setLowGraphic ()
    {
        if (!MainMenu)
        {
            if (StaticObjectsObj) { StaticObjectsObj.SetActive(false); }
            Camera.main.GetComponent<CameraWheather>().LightRain.SetActive(false);
            Camera.main.GetComponent<CameraWheather>().HeavyRain.SetActive(false);
            Camera.main.GetComponent<CameraWheather>().Thunder.SetActive(false);
        }
        QualitySettings.SetQualityLevel(0, true);
        getQuality();
        CurrentGraphicLevel.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current Quality" + CurrentQualityLevel;
    }
    public void setMediumGraphic()
    {
        if (!MainMenu)
        {
            if (StaticObjectsObj) { StaticObjectsObj.SetActive(true); }
            Global.GetComponent<EnviromentCondition>().setCameraWeather();
        }
        QualitySettings.SetQualityLevel(1, true);
        getQuality();
        CurrentGraphicLevel.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current Quality" + CurrentQualityLevel;
    }
    public void setHighGraphic()
    {
        if (!MainMenu)
        {
            if (StaticObjectsObj) { StaticObjectsObj.SetActive(true); }
            Global.GetComponent<EnviromentCondition>().setCameraWeather();
        }
        QualitySettings.SetQualityLevel(2, true);
        getQuality();
        CurrentGraphicLevel.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Current Quality" + CurrentQualityLevel;
    }

    private void getQuality()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0: CurrentQualityLevel = "Low"; break;
            case 1: CurrentQualityLevel = "Medium"; break;
            case 2: CurrentQualityLevel = "High"; break;
            default: CurrentQualityLevel = "None"; break;
        } 
    }

    public void CameraRangeButtonFunction()
    {
        SettingsBackButton.SetActive(true);
        clearViews();
        CameraRange.SetActive(true);
        mainSlider.value = Camera.main.farClipPlane;
        CameraRangeValue.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Camera.main.farClipPlane;
    }
    public void OnSliderValueChanged ()
    {
        Camera.main.farClipPlane = mainSlider.value;
        CameraRangeValue.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + Camera.main.farClipPlane;
    }
}
