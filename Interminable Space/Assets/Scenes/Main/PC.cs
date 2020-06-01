using UnityEngine.AI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PC : MonoBehaviour
{
    // Sets definable move goal for testing
    public Transform goal;
    // Following Path Variables
    public GameObject[] PathNode;
    public GameObject Player;
    public float MoveSpeed;
    float Timer;
    static Vector3 CurrentPositionHolder;
    int CurrentNode;
    private Vector3 startPosition;
   
    void Start()
    {
        // Finds PC and loads the NetMeshAgent
       //NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
        //Moves PC to location
        //agent.destination = goal.position;
        //PathNode = GetComponentInChildren<>();
        CheckNode();
    }

    void CheckNode()
    {
        Timer = 0;
        startPosition = Player.transform.position;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime * MoveSpeed;
        if(Player.transform.position != CurrentPositionHolder)
        {
            Player.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
        }
        else
        {
            if(CurrentNode<PathNode.Length-1)
            {
                CurrentNode++;
                CheckNode();
            }
        }
        
        //If Left Clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Casts a Ray as hit
            RaycastHit hit;
            //Defines ray as mouse input
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //IF ray hits something in 100f
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                print(hit);
                //If the hit transform is not Null
                if (hit.transform != null)
                {
                    print(hit.transform.gameObject);
                }
            }
        }
        
    }
}
