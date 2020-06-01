using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}
