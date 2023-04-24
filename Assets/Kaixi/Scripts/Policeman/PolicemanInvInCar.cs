using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanInvInCar : MonoBehaviour
{
    enum State { 
        FindVillager,
        Investigate,
        Chase,
        BackToCar
    }
    
    Vector3 PolicecarVector3;

    [SerializeField] State policeState;
    public NavMeshAgent agent;
    public bool backToCar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        backToCar = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (policeState) {
            case State.FindVillager:
                break;
            case State.Investigate:
                break;
            case State.Chase:
                break;
            case State.BackToCar:
                agent.SetDestination(PolicecarVector3);
                if (Vector3.Distance(transform.position, PolicecarVector3) <= 2.0f)
                {
                    backToCar = true;
                    gameObject.SetActive(false);
                }
                break;
        
        }
    }

    public void SetPoliceCar(Vector3 vector3)
    {
        PolicecarVector3 = vector3;
    }

    public bool getBackToCar()
    {
        return backToCar;
    }

    Vector3 getClosetInvVillager() { 
         Vector3 thisVec = Vector3.zero;
        return thisVec;
    
    }
}
