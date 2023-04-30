using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PolicemanInvInCar : MonoBehaviour
{
    public enum State
    {
        FindVillager,
        Investigate,
        BackToCar
    }

    Vector3 PolicecarVector3;
    public LayerMask villagerMask;
    public State policeState;
    public NavMeshAgent agent;
    public bool backToCar = false;
    float findRadius = 20;
    public Vector3 villagerVector3;
    public GameObject villager;

    public float policeTime;
    public float thisTime;


    GameManagement gameManagement;

    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        villagerMask = 1 << 11;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = gameManagement.getPolicePatrolSpeed();
        policeTime = gameManagement.getPoliceTime();
        
    }

    private void Awake()
    {
        backToCar = false;
        villager = getClosetInvVillager();
        if (villager != null)
        {
            villagerVector3 = villager.transform.position;
        }
        
        

        thisTime = policeTime;
    }

    private void OnEnable()
    {
        policeState = State.FindVillager;
    }
    // Update is called once per frame
    void Update()
    {
        thisTime -= Time.deltaTime;
        if (thisTime <= 0) {
            setState(State.BackToCar);
        }
        
        switch (policeState)
        {
            case State.FindVillager:
                villager = getClosetInvVillager();


                if (villager != null) {
                    villagerVector3 = villager.transform.position;
                    agent.SetDestination(villagerVector3);
                    if (Vector3.Distance(transform.position, villagerVector3) <= 2.0f)
                    {
                        setState(State.Investigate);

                    }
                }
                
                break;
            case State.Investigate:

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

    GameObject getClosetInvVillager()
    {
        Collider[] villagers = new Collider[20];

        int hits = Physics.OverlapSphereNonAlloc(transform.position, findRadius, villagers, villagerMask);

        
        if (villagers[0] != null) {
            Debug.Log("Yeha");
            GameObject target = villagers[0].gameObject;
            for (int i = 1; i < hits; i++)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > Vector3.Distance(transform.position, villagers[i].transform.position))
                {
                    target = villagers[i].gameObject;
                }

            }
            return target;
        }
        

        return null;
        
    }

    public void setState(State state) { 
        policeState= state;
    }


    public State getState()
    {
        return policeState;
    }
}
