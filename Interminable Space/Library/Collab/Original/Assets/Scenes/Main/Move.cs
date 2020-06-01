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
    public Vector3 Node2;
    public Vector3 Node7;
    public int Posi = 0;
    public GameObject Solar;
    public GameObject Hull;



    // Start is called before the first frame update
    void Start()
    {
        Solar = GameObject.Find("Solar Panel");
        Hull = GameObject.Find("Hull Display");
        Player = GameObject.Find("PC");
        Node1 = GameObject.Find("Node (1)").transform.position;
        Node9 = GameObject.Find("Node (9)").transform.position;
        Node4 = GameObject.Find("Node (4)").transform.position;
        Node2 = GameObject.Find("Node (2)").transform.position;
        Node7 = GameObject.Find("Node (7)").transform.position;
        GetPosition();
        if(PCPosition != Node1)
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
        if (Posi == 0 && PCPosition.x != Node1.x)
        {
            agent.destination = Node1;
            //GetPosition();
            //print(PCPosition.x);
        }
        else if (PCPosition.x == Node1.x)
        {
            agent.destination = Node4;
            Posi = 1;
            //GetPosition();
            //print(PCPosition.x);
        }
        else if (PCPosition.x == Node4.x && Posi == 1 && PCPosition.z == Node4.z)
        {
            Posi = 0;
        }
        else if (PCPosition.z == Node4.z && Posi == -1 && PCPosition.x == Node4.x)
        {
            agent.destination = Node9;
        }
        else if (PCPosition.z == Node9.z)
        {
            Posi = 1;
            agent.destination = Node4;
        }
        else if (Posi == -5 && PCPosition.z == Node4.z)
        {
            agent.destination = Node2;
        }
        else if (PCPosition.x == Node2.x && (Posi == -3 || Posi == -5))
        {
            agent.destination = Node7;
            Posi = -15;

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
                        if (Posi > -1)
                        {
                            agent.destination = Node4;
                            Posi = -1;
                            //print(Posi);
                        }
                        else if (Posi == -3)
                        {
                            agent.destination = Node2;
                            Posi = 1;
                        }
                    }
                    if (hit.transform.gameObject == Hull)
                    {
                        //print("yay");
                        if (Posi > -1)
                        {
                            agent.destination = Node2;
                            Posi = -3;
                            //print(Posi);
                        }
                        else if (Posi == -1)
                        {
                            agent.destination = Node4;
                            Posi = -5;
                        }
                    }
                }
            }
        }
    }
}
