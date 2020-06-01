using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public Vector3 PCPosition;
    public GameObject Player;
    public Vector3 Node1;
    public Vector3 Node9;
    public Vector3 Node4;
    public Vector3 Node3;
    public Vector3 Node8;
    public Vector3 Node5;
    public Vector3 Node6;
    public Vector3 Node;
    public string Posi;
    public string Dest;
    public GameObject Solar;
    public GameObject Shield;
    public GameObject Drone;
    public GameObject Course;
    public GameObject Console;
    public GameObject GM;



    // Start is called before the first frame update
    void Start()
    {
        Posi = "path";
        Dest = "none";
        GM = GameObject.Find("GameManager");
        Solar = GameObject.Find("Solar Panel");
        Shield = GameObject.Find("Shield Control");
        Console = GameObject.Find("Console");
        Course = GameObject.Find("Course Station");
        Drone = GameObject.Find("Drone Control");
        Player = GameObject.Find("PC");
        Node6 = GameObject.Find("Node (6)").transform.position;
        Node = GameObject.Find("Node").transform.position;
        Node1 = GameObject.Find("Node (1)").transform.position;
        Node9 = GameObject.Find("Node (9)").transform.position;
        Node4 = GameObject.Find("Node (4)").transform.position;
        Node3 = GameObject.Find("Node (3)").transform.position;
        Node8 = GameObject.Find("Node (8)").transform.position;
        Node5 = GameObject.Find("Node (5)").transform.position;
        GetPosition();
        if (PCPosition != Node1)
        {
            //print("TRUE");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetPosition();
        OnClick();
        Pace();
    }

    void GetPosition()
    {
        PCPosition = Player.transform.position;
        //print(PCPosition);
        //print(Node1);
    }

    void Pace()
    {
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
        //GetPosition();
        if (Posi == "path" && PCPosition.x != Node1.x)
        {
            agent.destination = Node1;
        }
        else if (Dest == "solar" && PCPosition.z == Node1.z && PCPosition.x != Node4.x)
        {
            agent.destination = Node4;
            Posi = "powerControl";
        }
        else if (Dest == "shield" && PCPosition.z == Node1.z && PCPosition.x != Node3.x)
        {
            agent.destination = Node3;
            Posi = "shieldControl";
        }
        else if ((Dest == "drone" || Dest == "course" || Dest == "console") && PCPosition.z == Node1.z && PCPosition.x > Node1.x)
        {
            agent.destination = Node1;
            if (Dest == "drone")
            {
                Posi = "droneControl";
            }
            if (Dest == "course")
            {
                Posi = "courseControl";
            }
            if (Dest == "console")
            {
                Posi = "consoleControl";
            }
            
        }
        else if (PCPosition.x == Node1.x && Posi == "path" && PCPosition.z == Node1.z)
        {
            agent.destination = Node4;
            Posi = "pathBack";
        }
        else if ((PCPosition.x == Node4.x || PCPosition.x == Node3.x) && Posi == "pathBack" && PCPosition.z == Node4.z)
        {
            Posi = "path";
        }
        else if (PCPosition.z == Node4.z && (Posi == "powerControl") && PCPosition.x == Node4.x)
        {
            agent.destination = Node9;
        }
        else if (PCPosition.z == Node9.z && Posi == "powerControl")
        {
            Posi = "pathBack";
            agent.destination = Node4;
            Dest = "none";
        }
        else if (Posi == "shieldControl" && PCPosition.z == Node4.z && (PCPosition.x == Node4.x || PCPosition.x == Node1.x))
        {
            agent.destination = Node3;
        }
        else if (PCPosition.x == Node3.x && Posi == "shieldControl" && PCPosition.z == Node3.z)
        {
            agent.destination = Node8;
            Posi = "shieldControl";
            Dest = "none";
        }
        else if (PCPosition.z == Node8.z && Posi == "shieldControl")
        {
            agent.destination = Node3;
            Posi = "pathBack";
        }
        else if ((Posi == "droneControl" || Posi == "courseControl" || Posi == "consoleControl") && PCPosition.z == Node4.z && (PCPosition.x == Node4.x || PCPosition.x == Node3.x))
        {
            agent.destination = Node1;
        }
        else if (PCPosition.x == Node1.x && Posi == "droneControl" && PCPosition.z == Node1.z)
        {
            agent.destination = Node5;
            Posi = "droneControl";
        }
        else if (PCPosition.z == Node5.z && Posi == "droneControl" && PCPosition.x == Node5.x)
        {
            agent.destination = Node1;
            Posi = "path";
            Dest = "none";
        }
        else if (PCPosition.x == Node1.x && Posi == "courseControl" && PCPosition.z == Node1.z)
        {
            agent.destination = Node6;
            Posi = "courseControl";
        }
        else if (PCPosition.z == Node6.z && Posi == "courseControl" && PCPosition.x == Node6.x)
        {
            agent.destination = Node1;
            Posi = "path";
            Dest = "none";
        }
        else if (PCPosition.x == Node1.x && Posi == "consoleControl" && PCPosition.z == Node1.z)
        {
            agent.destination = Node;
            Posi = "consoleControl";
        }
        else if (PCPosition.z == Node.z && Posi == "consoleControl" && PCPosition.x == Node.x)
        {
            agent.destination = Node1;
            Posi = "path";
            Dest = "none";
        }
    }
    void backToPath()
    {
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
        if (PCPosition.x == Node4.x)
        {
            agent.destination = Node4;
        }
        if (PCPosition.x == Node3.x)
        {
            agent.destination = Node3;
        }
        if (PCPosition.x == Node1.x)
        {
            agent.destination = Node1;
        }
    }

    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
            //Casts a Ray as hit
            RaycastHit hit;
            //Defines ray as mouse input
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //IF ray hits something in 100f
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //print(hit);
                //If the hit transform is not Null
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject == Solar)
                    {
                        //print("yay");
                        if ((Posi == "path" || Posi == "pathBack") && Dest == "none")
                        {
                            backToPath();
                            //agent.destination = Node4;
                            Posi = "powerControl";
                            Dest = "solar";
                            //print(Posi);
                        }
                        else if (Posi == "shieldControl")
                        {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "solar";
                        }
                        else if (Posi == "droneControl" || Posi == "courseControl" || Posi == "consoleControl")
                        {
                            backToPath();
                            //agent.destination = Node1;
                            Posi = "path";
                            Dest = "solar";
                        }
                    }
                    if (hit.transform.gameObject == Shield)
                    {
                        //print("yay");
                        if (Posi == "path" || Posi == "pathBack")
                        {
                            backToPath();
                            Posi = "shieldControl";
                            Dest = "shield";
                            //print(Posi);
                        }
                        else if (Posi == "powerControl")
                        {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "shield";
                        }
                        else if (Posi == "droneControl" || Posi == "courseControl" || Posi == "consoleControl")
                        {
                            backToPath();
                            Posi = "path";
                            Dest = "shield";
                        }
                    }
                    if (hit.transform.gameObject == Drone)
                    {
                        
                        //print("yay");
                        if (Posi == "path" || Posi == "pathBack")
                        {
                            backToPath();
                            Posi = "droneControl";
                            Dest = "drone";
                            //print(Posi);
                        }
                        else if (Posi == "shieldControl")
                        {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "drone";
                        }
                        else if (Posi == "powerControl")
                        {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "drone";
                        }
                        else if (Posi == "courseControl" || Posi == "consoleControl")
                        {
                            backToPath();
                            Posi = "path";
                            Dest = "drone";
                        }
                        
                    }
                    if (hit.transform.gameObject == Course)
                        {
                        Dest = "course";
                            //print("yay");
                            if (Posi == "path" || Posi == "pathBack")
                            {
                            backToPath();
                            Posi = "courseControl";
                            Dest = "course";
                            //print(Posi);
                        }
                            else if (Posi == "shieldControl")
                            {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "course";
                        }
                            else if (Posi == "powerControl")
                            {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "course";
                        }
                            else if (Posi == "droneControl" || Posi == "consoleControl")
                            {
                            backToPath();
                            Posi = "path";
                            Dest = "course";
                        }
                            

                        }
                        if (hit.transform.gameObject == Console)
                        {
                        
                            //print("yay");
                            if (Posi == "path" || Posi == "pathBack")
                            {
                            backToPath();
                            Posi = "consoleControl";
                            Dest = "console";
                            //print(Posi);
                        }
                            else if (Posi == "shieldControl")
                            {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "console";
                        }
                            else if (Posi == "powerControl")
                            {
                            backToPath();
                            Posi = "pathBack";
                            Dest = "console";
                        }
                            else if (Posi == "droneControl" || Posi == "courseControl")
                            {
                            backToPath();
                            Posi = "path";
                            Dest = "console";
                        }
                        }
                }
            }
        }
    }
}

