using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{   
    //managed values
    private float courseValue, powerValue, hullValue;

    //game object references
    private Slider courseSlider, powerSlider, hullSlider;
    private Button solarStationButton, shieldStationButton, droneStationButton, thrusterStationButton, consoleStationButton;

    //Station Status
    private bool solarPanelStation, shieldStation, droneStation, thrusterStation, console, inMain;

    //Rates
    //  Base Deterioration
    private float powerDecay = -0.1f;
    private float solarDecay = -0.1f;
    private float hullDecay = -0.1f;

    //  Solar Panel Rates
    private float solarOnTime = 0.0f;   //Time it takes to turn on the Solar Panels
    private float solarOffTime = 0.0f;  //Time it takes to turn off the Solar Panels
    private float solarOnPowerGrowth = 1.0f;    //Rate at which the Solar Panels provide power
    private float solarOnCourseDrain = -0.5f;   //Rate at which the Solar Panels cause the ship to lose course

    //  Shield Rates
    private float shieldOnTime = 0.0f;  //Time it takes to turn on the Shield
    private float shieldOffTime = 0.0f; //time it takes to turn off the Shield
    private float shieldOnPowerDrain = -1.0f;   //Rate at which the Shield drains power

    //  Drone Rates
    private float droneOnTime = 0.0f;   //Time it takes to turn on the Drones
    private float droneOffTime = 0.0f;  //Time it takes to turn off the Drones
    private float droneOnCost = 10.0f;  //Base cost for turning on the Drones
    private float droneOnPowerDrain = -0.25f;   //Rate at which the Drones drain power
    private float droneOnHullGrowth = 1.0f; //Rate at which the drones repair hull

    //  Thruster Rates
    private float thrustOnTime = 0.0f;  //Time it takes to turn on the control Thrusters
    private float thrustOffTime = 0.0f; //Time it takes to turn off the control Thrusters
    private float thrustOnPowerDrain = -0.5f;   //Rate at which the thrusters drain power
    private float thrustOnCourseGrowth = 1.0f;  //Rate at which the thrusters improve course

    //  Length the disaster lasts
    private float disasterTime = 10.0f;

    public void Awake()
    {
        //Do not Destroy this object when loading a new scene
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {  
        //Add delegate to sceneLoaded function that calls OnSceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //only execute if the MainScene is loaded
        if (inMain)
        {
            solarStationButton.clicked += stationClicked;
        }
    }

    private void onDisable()
    {  
        //remove delegate when GameManager is disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //called everytime the scene changes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //check to see if current scene is the MainScene
        if (scene.name == "MainScene")
        {
            inMain = true;  //Flag indicating we are currently in the MainScene
            startGame();    //Call game reset functions
        } else
        {
            inMain = false; //Flag indicating that we are not in the MainScene
        }
    }

    //Function for changing scenes on a timer.  Used for splash screens.
    IEnumerator LoadLevelAfterDelay(float delay, string nextScene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }

    //Reset game values to start game
    private void startGame()
    {
        //Reset managed values to full
        courseValue = 100.0f;
        powerValue = 100.0f;
        hullValue = 100.0f;

        //Get reference to Display Sliders
        GameObject SliderList = GameObject.Find("Readouts");
        courseSlider = SliderList.transform.Find("CourseSlider").gameObject.GetComponent<Slider>();
        powerSlider = SliderList.transform.Find("PowerSlider").gameObject.GetComponent<Slider>();
        hullSlider = SliderList.transform.Find("HullSlider").gameObject.GetComponent<Slider>();

        //Get reference to testing buttons
        GameObject StationList = GameObject.Find("Station Interaction");
        solarStationButton = StationList.transform.Find("Solar Panel Button").gameObject.GetComponent<Button>();
        shieldStationButton = StationList.transform.Find("Shield Button").gameObject.GetComponent<Button>();
        droneStationButton = StationList.transform.Find("Drones Button").gameObject.GetComponent<Button>();
        thrusterStationButton = StationList.transform.Find("Thrusters Button").gameObject.GetComponent<Button>();
        consoleStationButton = StationList.transform.Find("Asteroid Button").gameObject.GetComponent<Button>();

        //Reset Station Status
        solarPanelStation = false;
        shieldStation = false;
        droneStation = false;
        thrusterStation = false;
        console = false;
    }

    private void stationClicked(GameObject gObject) 
    { 
        Debug.Log("Clicked Station: " + gObject.name);
    }

    private void stationclicked(Action act)
    {

    }
}
