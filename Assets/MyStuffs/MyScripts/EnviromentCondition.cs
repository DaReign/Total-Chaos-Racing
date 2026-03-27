using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;


public class EnviromentCondition : MonoBehaviour {

    public GameObject Sun;
    public int DayState;
    public int Weather;
    public int Skybox;

    private int DrawVar;

    public Material Skybox1;
    public Material Skybox2;
    public Material Skybox3;
    public Material Skybox3b;
    public Material Skybox4;
    public Material Skybox5;
    public Material Skybox6;
    public Material Skybox7;
    public Material Skybox7b;
    public Material SunnySkybox1;
    public Material SunnySkybox2;
    public Material SunnySkybox3;
    public Material DawnDuskSkybox;
    public Material EarieSkybox;
    public Material Overcast1;
    public Material Overcast2;
    public Material StarryNight;
    public Material MoonShine;

    public GameObject Sun1;
    public GameObject Sun2;
    public GameObject Sun3;
    public GameObject Sun4;
    public GameObject Sun5;
    public GameObject Sun6;

    public GameObject EnviromentConditionSetup;
    public GameObject DayStateInput;
    public GameObject WeatherInput;
    public GameObject SkyboxInput;
    private bool start = true;
    // Use this for initialization
    void Start () {
        // Time.timeScale = 0.0F;
        setUp();
        EnviromentConditionSetup.SetActive(false);

    }

    void DeactivateSuns ()
    {
        Sun1.SetActive(false);
        Sun2.SetActive(false);
        Sun3.SetActive(false);
        Sun4.SetActive(false);
        Sun5.SetActive(false);
        Sun6.SetActive(false);
    }

    void setSkybox()
    {
        DeactivateSuns();
        if (DayState==0&&Weather<2)
        {
            
            DrawVar = Random.Range(0,9);
            switch (DrawVar)
             {
                 case 0: Sun = Sun3; Sun3.SetActive(true); RenderSettings.skybox = Skybox1; break;
                 case 1: Sun = Sun3; Sun3.SetActive(true); RenderSettings.skybox = Skybox2; break;
                 case 2: Sun = Sun3; Sun3.SetActive(true); RenderSettings.skybox = Skybox3; break;
                 case 3: Sun = Sun3; Sun3.SetActive(true); RenderSettings.skybox = Skybox3b; break;
                 case 4: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = Skybox7; break;
                 case 5: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = Skybox7b; break;
                 case 6: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = SunnySkybox1; break;
                 case 7: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = SunnySkybox2; break;
                 case 8: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = SunnySkybox3; break;
                 default: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = Skybox7; break;
             }
        }

        if (DayState == 1 && Weather < 2)
        {

            DrawVar = Random.Range(0, 5);
            switch (DrawVar)
            {
                case 0: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = Skybox4; break;
                case 1: Sun = Sun1; Sun1.SetActive(true); RenderSettings.skybox = Skybox5; break;
                case 2: Sun = Sun2; Sun2.SetActive(true); RenderSettings.skybox = Skybox6; break;
                case 3: Sun = Sun4; Sun4.SetActive(true); RenderSettings.skybox = DawnDuskSkybox; break;
                case 4: Sun = Sun5; Sun5.SetActive(true); RenderSettings.skybox = EarieSkybox; break;
                default: Sun = Sun1; Sun1.SetActive(true); Sun1.GetComponent<Light>().intensity = 0.4f; RenderSettings.skybox = Skybox4; break;
            }
        }

        if (DayState < 2 && Weather > 1)
        {
            DrawVar = Random.Range(0, 2);
            switch (DrawVar)
            {
                case 0: Sun = Sun6; Sun6.SetActive(true); RenderSettings.skybox = Overcast1; break;
                case 1: Sun = Sun6; Sun6.SetActive(true); RenderSettings.skybox = Overcast2; break;
                default: Sun = Sun6; Sun6.SetActive(true); RenderSettings.skybox = Overcast1; break;
            }
        }

        if (DayState==2)
        {
            DrawVar = Random.Range(0, 2);
            switch (DrawVar)
            {
                case 0: Sun6.SetActive(true); RenderSettings.skybox = StarryNight; break;
                case 1: Sun6.SetActive(true); RenderSettings.skybox = MoonShine; break;
                default: Sun6.SetActive(true); RenderSettings.skybox = StarryNight; break;
            }

        }

        if (DayState<2&&Weather<2) {
            Camera.main.GetComponent<SunShafts>().sunTransform = Sun.transform;
        }
        else
        {
            Camera.main.GetComponent<SunShafts>().enabled = false;
        }
        /*       switch (Skybox)
        {
            case 0: RenderSettings.skybox = Skybox1; break;
            case 1: RenderSettings.skybox = Skybox2; break;
            case 2: RenderSettings.skybox = Skybox3; break;
            case 3: RenderSettings.skybox = Skybox4; break;
            case 4: RenderSettings.skybox = Skybox5; break;
            case 5: RenderSettings.skybox = Skybox6; break;
            case 6: RenderSettings.skybox = Skybox7; break;
            default: RenderSettings.skybox = Skybox1; break;
        }
*/
    }

    public void setCameraWeather ()
    {
        switch (Weather)
        {
            case 0: break;
            case 1: Camera.main.GetComponent<CameraWheather>().LightRain.SetActive(true);break;
            case 2: Camera.main.GetComponent<CameraWheather>().HeavyRain.SetActive(true);break;
            case 3: Camera.main.GetComponent<CameraWheather>().LightRain.SetActive(true); Camera.main.GetComponent<CameraWheather>().Thunder.SetActive(true); break;
            case 4: Camera.main.GetComponent<CameraWheather>().HeavyRain.SetActive(true); Camera.main.GetComponent<CameraWheather>().Thunder.SetActive(true);  break;
            default: break;
        }

    }

    public void enviromentSetUp()
    {
        Time.timeScale = 0.0F;
        EnviromentConditionSetup.SetActive(true);
    }


    public void setUp ()
    {
        /*
        DayState = int.Parse(DayStateInput.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text);
        Weather = int.Parse(WeatherInput.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text);
        Skybox = int.Parse(SkyboxInput.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text);
        */

        DayState = int.Parse(PlayerPrefs.GetString("RaceDayState"));
        Weather = int.Parse(PlayerPrefs.GetString("RaceWeather"));

        DayState = 0;
        Weather = 0;
        //Skybox = 1;
        /*
        if (DayState == 0)
        {
            Skybox = 7;
        }
        */


        setSkybox();
        setCameraWeather();

        Sun = GameObject.Find("Sun");
        //print("Sun");
        //print(Sun.GetComponent<Light>());


        /* 
        if (DayState == 0)
        {
            Sun.GetComponent<Light>().intensity = 0.0f;
            Sun.GetComponent<Light>().color = new Color(0.17f, 0.17f, 0.17f, 1);
            //Sun.SetActive(false);
            //RenderSettings.skybox = Skybox8;
            RenderSettings.ambientIntensity = 0.1f;
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0, 0, 1);
            Camera.main.farClipPlane = 300f;
            //Camera.main.GetComponent<CameraWheather>().Thunder.SetActive(true);
            //Camera.main.GetComponent<CameraWheather>().HeavyRain.SetActive(true);
            //Camera.main.GetComponent<GlobalFog>().enabled = true;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 0;
            RenderSettings.fogEndDistance = 300;
            //Camera.main.GetComponent<GlobalFog>().globalFogColor = Color.black;
            //Screen.SetResolution(1024, 600, true);
        }
        */

        //Time.timeScale = 1.0F;
        EnviromentConditionSetup.SetActive(false);


    }

    // Update is called once per frame
    /*
    void Update () {
		if (start)
        {
            setUp();
            start = false;
        }
	}
    */
}
