using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Console : MonoBehaviour
{
    public Transform goal;
    private void start()
    {
        Vector3 PC = GameObject.FindGameObjectWithTag("Player").transform.position;
        //Debug.Log(PC);
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
    // Update is called once per frame
    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    PrintName(hit.transform.gameObject);
                }
            }
        }
     
    }

    private void PrintName(GameObject go)
    {
      print(go.name);
    }

}
