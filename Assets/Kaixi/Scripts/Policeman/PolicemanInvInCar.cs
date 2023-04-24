using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PolicemanInvInCar : MonoBehaviour
{
    public enum State
    {
        FindVillager,
        Investigate,
        Chase,
        BackToCar
    }

    GameObject player;
    Vector3 PolicecarVector3;
    LayerMask villagerMask;
    public State policeState;
    public NavMeshAgent agent;
    public bool backToCar = false;
    public float findRadius = 10;
    Vector3 villagerVector3;


    // Start is called before the first frame update
    void Start()
    {
        villagerMask = 1 << 11;
        player = GameObject.FindWithTag("Player");
    }

    private void Awake()
    {
        backToCar = false;
        villagerVector3 = getClosetInvVillager().transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        switch (policeState)
        {
            case State.FindVillager:
                agent.SetDestination(villagerVector3);
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

    public void setState(State state) { 
        policeState= state;
    }


    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }
}
