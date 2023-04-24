using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class Villager : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walk,
        Escape,
        Onlook,
        Investigate
    }


    NavMeshAgent agent;
    HouseManager houseManager;
    Animator animator;

    //basic property
    public float speed = 5;
    public float detectRadius = 10;
    public float maxWalkDistance = 200;
    public float onLookRange = 20;
    public Vector2 runawayRange = new Vector2(20, 100);
    public bool isBeingInvestigate;

    //ray detection
    //private Collider[] detectedCollider;
    public LayerMask detectableLayer;

    [SerializeField] private State currentState;
    //private State previousState;

    //random choose a house as a destination or just roam without house as a target
    public Transform targetHouse;
    private Vector3 targetDestination;
    bool hasTargetHouse;

    private House firedHouse;
    public float distanceFromFiredHouse;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        houseManager = GameObject.FindObjectOfType<HouseManager>();
        //detectedCollider = new Collider[30];
        Init();
        ChangeStateAndEnter(currentState);
    }

    private void Init()
    {
        currentState = State.Idle;
    }


    private void FixedUpdate()
    {
        //physics raycast should be in the fixedupdate
        CurrentStateOnStayFixedUpdate();
    }

    private void Update()
    {
        //check house state
        if (firedHouse != null)
        {
            if (firedHouse.GetComponent<House>().getState() != 1 || firedHouse.GetComponent<House>().getState() != 2)
            {
                firedHouse = null;
                return;
            }
            distanceFromFiredHouse = Vector3.Distance(transform.position, firedHouse.transform.position);
        }

        //check if the villager is being investigated by the police
        if (isBeingInvestigate)
        {
            if (currentState == State.Investigate) return;
            ChangeStateAndEnter(State.Investigate);
        }


        CurrentStateOnStay();
    }

    //OnStay - excute every fixedUpdate
    void CurrentStateOnStayFixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                DetectFiredHouse();
                break;
            case State.Walk:
                DetectFiredHouse();
                CheckIfArrive();
                break;
            case State.Escape:
                break;
            case State.Onlook:
                break;
            case State.Investigate:
                break;
            default:
                break;
        }
    }

    //OnStay - excute every Update
    void CurrentStateOnStay()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Walk:
                break;
            case State.Escape:
                DetectFiredHouseDistance();
                break;
            case State.Onlook:
                CheckFiredHouseState();
                CheckFiredHouseDistance();
                break;
            case State.Investigate:
                break;
            default:
                break;

        }
    }


    //OnEnter - excute once
    void ChangeStateAndEnter(State newState)
    {
        ExitCurrentState();

        //previousState = currentState;
        currentState = newState;
        switch (currentState)
        {
            case State.Idle:
                agent.isStopped = true;
                IdleOnEnter();
                break;
            case State.Walk:
                Debug.Log("Villager Walking");
                SetRandomDestination();
                animator.SetBool("Walk", true);
                break;
            case State.Escape:
                Debug.Log("Villager Escape");
                SetEscapePointAndEscape();
                animator.SetBool("Run", true);
                break;
            case State.Onlook:
                SetOnLookPoint();
                break;
            case State.Investigate:
                Debug.Log("Villager Investigate");
                break;
            default:
                break;

        }

    }

    //OnExit - excute once
    void ExitCurrentState()
    {
        switch (currentState)
        {
            case State.Idle:
                agent.isStopped = false;
                break;
            case State.Walk:
                if (firedHouse != null)
                {
                    LookAtFirePoint(firedHouse.transform);
                }
                animator.SetBool("Walk", false);
                break;
            case State.Escape:
                animator.SetBool("Run", false);
                break;
            case State.Onlook:
                agent.isStopped = false;
                break;
            case State.Investigate:
                animator.SetBool("Talk", false);
                break;
            default:
                break;
        }
    }

    //common function
    bool CheckDistanceFromDestination(Vector3 destination, float distance)
    {
        if (Vector3.Distance(transform.position, destination) < distance)
        {
            return true;
        }
        return false;
    }

    #region Walk
    //OnExit - if there is firedHouse 
    private void LookAtFirePoint(Transform firedHouse)
    {
        StartCoroutine(RotateToTargetPoint(firedHouse));
    }

    IEnumerator RotateToTargetPoint(Transform target)
    {
        transform.rotation = Quaternion.LookRotation(target.position);
        yield return null;
    }

    //OnEnter
    void SetRandomDestination()
    {
        //set random bool, the villager either has target or roam around
        int randomNumber = Random.Range(0, 2);
        hasTargetHouse = randomNumber == 0;
        if (hasTargetHouse)
        {
            int random = Random.Range(0, houseManager.houses.Length);
            targetHouse = houseManager.houses[randomNumber].transform;
            targetDestination = targetHouse.transform.position + transform.forward;
        }
        else
        {
            targetDestination = RandomNavmeshLocation(transform.position, maxWalkDistance);
        }
        agent.SetDestination(targetDestination);
    }

    private Vector3 RandomNavmeshLocation(Vector3 origin, float distance)
    {
        // Random generate a new location
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);
        return navHit.position;
    }

    //OnStay
    void DetectFiredHouse()
    {
        Collider[] detectedCollider = new Collider[30];
        Physics.OverlapSphereNonAlloc(transform.position, detectRadius, detectedCollider, detectableLayer);
        for (int i = 0; i < detectedCollider.Length; i++)
        {
            //if the house is on fire, start escape mode
            detectedCollider[i].TryGetComponent<House>(out House house);
            if (house == null)
            {
                return;
            }
            else if (house.houseState == 1)
            {
                firedHouse = house;
                ChangeStateAndEnter(State.Escape);
                break;
            }
            //if the house is being puting out on fire, villager will stay around;
            else if (house.houseState == 2)
            {
                firedHouse = house;
                ChangeStateAndEnter(State.Onlook);
            }

            //chain reaction, the escape villager also effect the other villager
            detectedCollider[i].TryGetComponent<Villager>(out Villager villager);
            if (villager == null)
            {
                return;
            }
            else if (villager.currentState == State.Escape && villager.distanceFromFiredHouse < 10)
            {
                ChangeStateAndEnter(State.Escape);
            }


        }
    }

    //OnStay
    void CheckIfArrive()
    {
        if (CheckDistanceFromDestination(targetDestination, 5))
        {
            ChangeStateAndEnter(State.Idle);
        }
    }



    #endregion
    #region Escape
    //OnEnter
    void SetEscapePointAndEscape()
    {
        Vector3 direction = (firedHouse.transform.position - transform.position).normalized;
        float randomRange = Random.Range(runawayRange.x, runawayRange.y);
        Vector3 destination = direction * randomRange;
        agent.SetDestination(destination);

    }

    //OnStay
    void DetectFiredHouseDistance()
    {
        if (firedHouse == null)
        {
            ChangeStateAndEnter(State.Walk);
        }
        //if the house is being putting out on fire, then change to onlook state
        else if (CheckDistanceFromDestination(firedHouse.transform.position, 80) && firedHouse.getState() == 2)
        {
            ChangeStateAndEnter(State.Onlook);
        }
        //if run too far (>80), then change back to walk state 
        else if (!CheckDistanceFromDestination(firedHouse.transform.position, 80))
        {
            firedHouse = null;
        }
    }

    #endregion
    #region Idle
    //OnEnter
    void IdleOnEnter()
    {
        float randomWaitTime = Random.Range(2f, 4f);

        //pretent enter to the building
        if (hasTargetHouse)
        {
            gameObject.SetActive(false);
            Invoke("SetActive", randomWaitTime);
        }
        else
        {
            StartCoroutine(StopForSeconds(randomWaitTime));
        }
        
    }

    void SetActive()
    {
        gameObject.SetActive(true);
        ChangeStateAndEnter(State.Walk);
    }

    IEnumerator StopForSeconds(float second)
    {
        yield return new WaitForSeconds(second);
        if (firedHouse == null)
        {
            ChangeStateAndEnter(State.Walk);
        }
    }
    #endregion
    #region OnLook
    //onEnter
    void SetOnLookPoint()
    {
        if (firedHouse == null) return;
        Vector3 destination = (transform.position - firedHouse.transform.position).normalized * onLookRange;
        agent.SetDestination(destination);
    }

    //OnStay
    void CheckFiredHouseState()
    {
        if (firedHouse == null)
        {
            ChangeStateAndEnter(State.Walk);
        }
        else if (firedHouse.GetComponent<House>().getState() != 2)
        {
            firedHouse = null;
            ChangeStateAndEnter(State.Walk);
        }

    }

    //OnStay
    void CheckFiredHouseDistance()
    {
        if (distanceFromFiredHouse < onLookRange)
        {
            agent.isStopped = true;
        }
    }

    #endregion





}

