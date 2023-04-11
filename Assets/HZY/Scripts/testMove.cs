using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testMove : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform destination;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = GameObject.FindGameObjectWithTag("1111").transform;

        MoveToDestination(destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToDestination(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
