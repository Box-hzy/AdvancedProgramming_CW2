using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : MonoBehaviour
{
    public Vector3 firehouseDestination;
    public Vector3 PoliceStationDestination;
    public GameObject firehouse;
    float stopDistance = 3.0f;
    GameObject TalkingPoliceman;
    GameObject PatrolPoliceman;
    public int state = 0;
    public NavMeshAgent navMeshAgent;
    PolicemanInvInCar policemanInv;
    PolicemanPatrolInCar policemanPatrol;
    // Start is called before the first frame update
    void Start()
    {
        policemanInv = GetComponentInChildren<PolicemanInvInCar>();
        policemanPatrol = GetComponentInChildren<PolicemanPatrolInCar>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case 0:
                navMeshAgent.SetDestination(firehouseDestination);
                if (Vector3.Distance(transform.position, firehouseDestination) <= stopDistance)
                {

                    navMeshAgent.isStopped = true;
                    
                    policemanPatrol.gameObject.SetActive(true);
                    policemanPatrol.SetPoliceCar(transform.position);

                    policemanInv.gameObject.SetActive(true);
                    policemanInv.SetPoliceCar(transform.position);
                    changeState(1);
                }

                break;
            case 1:
                if (policemanInv.getBackToCar() && policemanPatrol.getBackToCar()) {
                    changeState(2);
                }
                break;
            case 2:
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(PoliceStationDestination);
                if (Vector3.Distance(transform.position, PoliceStationDestination) <= 1.0f)
                {
                    Destroy(this.gameObject);
                }
                break;

        }
    }

    public void changeState(int thisState)
    {
        state = thisState;
    }

    public void setPolicehouseDestination(Vector3 thisFirehouse)
    {
        firehouseDestination = thisFirehouse;
    }
    public void setPoliceStationDestination(Vector3 thisFireStation)
    {
        PoliceStationDestination = thisFireStation;
    }
}
