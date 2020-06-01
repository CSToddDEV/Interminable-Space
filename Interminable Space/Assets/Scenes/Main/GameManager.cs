using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    //managed values
    private static float courseValue, powerValue, hullValue;

    //game object references
    private Slider courseSlider, powerSlider, hullSlider;
    private Button solarButton, shieldButton, droneButton, thrusterButton, consoleButton;
    private static GameObject solarStation, shieldStation, droneStation, thrusterStation, consoleStation;

    public static Material onMat, offMat;

    //Station Status
    private static bool solarPanel, shield, drone, thruster, console, inMain;

    //Rates
    //  Rates of Managed values 
    private float courseRate = 0.0f;
    private float powerRate = 0.0f;
    private float hullRate = 0.0f;

    //  Base Deterioration
    private float courseDecay = -0.01f;
    private float powerDecay = -0.01f;
    private float hullDecay = -0.01f;

    //  Solar Panel Rates
    private float solarOnTime = 0.0f;   //Time it takes to turn on the Solar Panels
    private float solarOffTime = 0.0f;  //Time it takes to turn off the Solar Panels
    private float solarOnPowerGrowth = 0.10f;    //Rate at which the Solar Panels provide power
    private float solarOnCourseDrain = -0.05f;   //Rate at which the Solar Panels cause the ship to lose course

    //  Shield Rates
    private float shieldOnTime = 0.0f;  //Time it takes to turn on the Shield
    private float shieldOffTime = 0.0f; //time it takes to turn off the Shield
    private float shieldOnPowerDrain = -0.01f;   //Rate at which the Shield drains power

    //  Drone Rates
    private float droneOnTime = 0.0f;   //Time it takes to turn on the Drones
    private float droneOffTime = 0.0f;  //Time it takes to turn off the Drones
    private float droneOnCost = 0.1f;  //Base cost for turning on the Drones
    private float droneOnPowerDrain = -0.025f;   //Rate at which the Drones drain power
    private float droneOnHullGrowth = 0.10f; //Rate at which the drones repair hull

    //  Thruster Rates
    private float thrustOnTime = 0.0f;  //Time it takes to turn on the control Thrusters
    private float thrustOffTime = 0.0f; //Time it takes to turn off the control Thrusters
    private float thrustOnPowerDrain = -0.05f;   //Rate at which the thrusters drain power
    private float thrustOnCourseGrowth = 0.10f;  //Rate at which the thrusters improve course

    //  Length the disaster lasts
    private float disasterTime = 10.0f;

    //  Set Colors
    private Color32 red = new Color32(255, 0, 0, 255);
    private Color32 green = new Color32(0, 255, 0, 255);

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
        onMat = Resources.Load<Material>("ButtonOn");
        offMat = Resources.Load<Material>("ButtonOff");
    }

    // Update is called once per frame
    void Update()
    {
        //only execute if the MainScene is loaded
        if (inMain)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit clickedObject;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out clickedObject))
                {
                    if (clickedObject.collider != null)
                    {
                        switch (clickedObject.collider.gameObject.name)
                        {
                            case "Solar Panel":
                                Debug.Log("Solar Panel");
                                if (solarPanel)
                                {
                                    solarPanel = false;
                                } else
                                {
                                    solarPanel = true;
                                }
                                break;
                            case "Shield Control":
                                Debug.Log("Shield control");
                                if (shield)
                                {
                                    shield = false;
                                } else
                                {
                                    shield = true;
                                }
                                break;
                            case "Hull Display":
                                Debug.Log("Hull Display");
                                break;
                            case "Drone Control":
                                Debug.Log("Drone Control");
                                if (drone)
                                {
                                    drone = false;
                                } else
                                {
                                    drone = true;
                                }
                                break;
                            case "Course Station":
                                Debug.Log("Course Station");
                                if (thruster)
                                {
                                    thruster = false;
                                } else
                                {
                                    thruster = true;
                                }
                                break;
                            case "Console":
                                Debug.Log("Console");
                                break;
                            default:
                                Debug.Log("NA");
                                break;
                        }
                }
                }
            }

            //Color buttons based on off or on
            //  for solar panel
            if (solarPanel)
            {
                solarButton.GetComponent<Image>().color = green;
            }
            else
            {
                solarButton.GetComponent<Image>().color = red;
            }

            //  for shield
            if (shield)
            {
                shieldButton.GetComponent<Image>().color = green;
            } else
            {
                shieldButton.GetComponent<Image>().color = red;
            }

            //  for drone
            if (drone)
            {
                droneButton.GetComponent<Image>().color = green;
            }
            else
            {
                droneButton.GetComponent<Image>().color = red;
            }

            //  for thrusters
            if (thruster)
            {
                thrusterButton.GetComponent<Image>().color = green;
            } else
            {
                thrusterButton.GetComponent<Image>().color = red;
            }

            //  for console
            if (console)
            {
                consoleButton.GetComponent<Image>().color = green;
            } else
            {
                consoleButton.GetComponent<Image>().color = red;
            }
        }
    }

    private void FixedUpdate()
    {
        if (inMain)
        {
            //  update managed resource displays
            courseSlider.value = courseValue;
            powerSlider.value = powerValue;
            hullSlider.value = hullValue;

            //  Base values
            courseValue += courseDecay;
            powerValue += powerDecay;
            hullValue += hullDecay;

            //  change rate of managed resources
            if (solarPanel)
            {
                powerValue += solarOnPowerGrowth;
                courseValue += solarOnCourseDrain;
            }
            if (shield)
            {
                powerValue += shieldOnPowerDrain;
            }
            if (drone)
            {
                powerValue += droneOnPowerDrain;
                hullValue += droneOnHullGrowth;
            }
            if (thruster)
            {
                powerValue += thrustOnPowerDrain;
                courseValue += thrustOnCourseGrowth;
            }

            //  maintain boundaries of managed values at 0.0f and 100.0f
            if (courseValue <= 0.0f)
            {
                courseValue = 0.0f;
            }
            if (courseValue >= 100.0f)
            {
                courseValue = 100.0f;
            }
            if (powerValue <= 0.0f)
            {
                powerValue = 100.0f;
            }
            if (powerValue >= 100.0f)
            {
                powerValue = 100.0f;
            }
            if (hullValue <= 0.0f)
            {
                hullValue = 0.0f;
            }
            if (hullValue >= 100.0f)
            {
                hullValue = 100.0f;
            }
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
        Debug.Log("Restart");

        //Reset managed values to full
        courseValue = 100.0f;
        powerValue = 100.0f;
        hullValue = 100.0f;

        //Get reference to Display Buttons
        GameObject DisplayList = GameObject.Find("Station Interaction");
        solarButton = DisplayList.transform.Find("Solar Panel Button").gameObject.GetComponent<Button>();
        shieldButton = DisplayList.transform.Find("Shield Button").gameObject.GetComponent<Button>();
        droneButton = DisplayList.transform.Find("Drones Button").gameObject.GetComponent<Button>();
        thrusterButton = DisplayList.transform.Find("Thrusters Button").gameObject.GetComponent<Button>();
        consoleButton = DisplayList.transform.Find("Asteroid Button").gameObject.GetComponent<Button>();

        //Get reference to Display Sliders
        GameObject SliderList = GameObject.Find("Readouts");
        courseSlider = SliderList.transform.Find("CourseSlider").gameObject.GetComponent<Slider>();
        powerSlider = SliderList.transform.Find("PowerSlider").gameObject.GetComponent<Slider>();
        hullSlider = SliderList.transform.Find("HullSlider").gameObject.GetComponent<Slider>();

        //Get reference to Station gameObjects
        solarStation = GameObject.Find("Solar Panel");
        shieldStation = GameObject.Find("Shield Control");
        droneStation = GameObject.Find("Drone Control");
        thrusterStation = GameObject.Find("Course Station");
        consoleStation = GameObject.Find("Console");

        //Reset Station Status
        solarPanel = false;
        shield = false;
        drone = false;
        thruster = false;
        console = false;

        //Reset Indicators
        solarStation.transform.Find("Indicator").GetComponent<Renderer>().material = offMat;
        shieldStation.transform.Find("Indicator").GetComponent<Renderer>().material = offMat;
        droneStation.transform.Find("Indicator").GetComponent<Renderer>().material = offMat;
        thrusterStation.transform.Find("Indicator").GetComponent<Renderer>().material = offMat;

    }

    public static void toggleStation(string station) 
    { 
        switch (station)
        {
            case "Solar Panel":
                if (solarPanel)
                {
                    solarStation.transform.Find("Indicator").GetComponent<Renderer>().material = offMat;
                    solarPanel = false;
                } else
                {
                    solarStation.transform.Find("Indicator").GetComponent<Renderer>().material = onMat;
                    solarPanel = true;
                }
                break;
            default:
                break;

        }
        Debug.Log( station + " Station: ");
    }

}
