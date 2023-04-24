using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PolicemanInvInCar : MonoBehaviour
{
    enum State
    {
        FindVillager,
        Investigate,
        Chase,
        BackToCar
    }

    Vector3 PolicecarVector3;
    LayerMask villagerMask;
    [SerializeField] State policeState;
    public NavMeshAgent agent;
    public bool backToCar = false;
    public float findRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        villagerMask = 1 << 11;
    }

    private void Awake()
    {
        backToCar = false;
    }
    // Update is called once per frame
    void Update()
    {
        switch (policeState)
        {
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

}
