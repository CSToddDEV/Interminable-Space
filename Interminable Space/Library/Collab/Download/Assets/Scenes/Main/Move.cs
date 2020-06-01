using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    public Vector3 PCPosition;
    public GameObject Player;
    public Vector3 Node1;
    public Vector3 Node4;
    public int Posi = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.Find("PC");
        Node1 = GameObject.Find("Node (1)").transform.position;
        Node4 = GameObject.Find("Node (4)").transform.position;
        GetPosition();
        if(PCPosition != Node1)
        {
            print("TRUE");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pace();
    }

    void GetPosition()
    {
        PCPosition = Player.transform.position;
    }

    void Pace()
    {
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
        GetPosition();
        if (PCPosition.x != Node1.x && Posi == 0)
        {
            agent.destination = Node1;
            //GetPosition();
        }
        else if (PCPosition.x == Node1.x)
        {
            Posi = 1;
            agent.destination = Node4;
            //GetPosition();
        }
        else if (PCPosition.x == Node4.x)
        {
            Posi = 0;
        }

    }
}
