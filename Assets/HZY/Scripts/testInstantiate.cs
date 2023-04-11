using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testInstantiate : MonoBehaviour
{
    public GameObject test;
    public Transform spawnPont;
    public Transform destination;

    GameObject testCube;


    bool canBeBurn;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        { 
            testCube =  Instantiate(test,spawnPont.position,Quaternion.identity);
            Debug.Log("aaaaa");
        }

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Debug.Log(testCube.transform.position);
        //    testCube.GetComponent<testMove>().MoveToDestination(destination.position);
        //}

        if(Input.GetKey(KeyCode.E))
        {
            
        }

    }


}
