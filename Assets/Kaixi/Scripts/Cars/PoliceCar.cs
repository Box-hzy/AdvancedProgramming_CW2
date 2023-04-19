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
    // Start is called before the first frame update
    void Start()
    {
        
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
                    //Policeman.SetActive(true);
                    //Policeman.transform.SetParent(null);
                    //PolicemanScript.SetFireEnginePostion(transform.position);
                    changeState(1);
                }

                break;
            case 1:
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
