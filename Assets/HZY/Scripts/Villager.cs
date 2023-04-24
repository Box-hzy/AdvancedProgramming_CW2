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

    public GameObject testPrefab;

    NavMeshAgent agent;
    HouseManager houseManager;
    Animator animator;

    //basic property
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    public float detectRadius = 20;
    public float maxWalkDistance = 200;
    public float onLookRange = 20;
    public Vector2 runawayRange = new Vector2(70, 100);
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

    [SerializeField]private House firedHouse;
    public float distanceFromFiredHouse;

    private void Start()
    {
        ChangeSkin();
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

    void ChangeSkin()
    {   
        List<GameObject> list = new List<GameObject>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer mesh))
            {
                list.Add(transform.GetChild(i).gameObject);
            }
        }

        int randomSkinIndex = Random.Range(0, list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            if (i == randomSkinIndex)
            {
                list[i].SetActive(true);
            }
            else
            {
                list[i].SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        //physics raycast should be in the fixedupdate
        CurrentStateOnStayFixedUpdate();
    }

    private void Update()
    {
        CurrentStateOnStay();

        //check house state
        if (firedHouse != null)
        {
            if (firedHouse.GetComponent<House>().getState() != 1 && firedHouse.GetComponent<House>().getState() != 2)
            {
                firedHouse = null;
                Debug.Log("house is not on fire");
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
                //Debug.Log("Villager Walking");
                animator.SetBool("Walk", true);
                agent.speed = walkSpeed;
                SetRandomDestination();
                break;
            case State.Escape:
                //Debug.Log("Villager Escape");
                animator.SetBool("Run", true);
                agent.speed = runSpeed;
                SetEscapePointAndEscape();
                break;
            case State.Onlook:
                SetOnLookPoint();
                break;
            case State.Investigate:
                //Debug.Log("Villager Investigate");
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
    bool CheckIfReachDestination(Vector3 destination, float distance)
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

    public Collider[] detectedCollider;

    //OnStay
    void DetectFiredHouse()
    {
        detectedCollider = new Collider[30];
        Physics.OverlapSphereNonAlloc(transform.position, detectRadius, detectedCollider, detectableLayer);
        for (int i = 0; i < detectedCollider.Length; i++)
        {
            if (detectedCollider[i] == null) return;
            //if the house is on fire, start escape mode
            if (detectedCollider[i].TryGetComponent<House>(out House house))
            {
                if (house.getState() == 1)
                {
                    Debug.Log("house state = 1!");
                    firedHouse = house;
                    Debug.Log(firedHouse.name);
                    ChangeStateAndEnter(State.Escape);
                    break;
                }
                //if the house is being puting out on fire, villager will stay around;
                else if (house.getState() == 2)
                {
                    firedHouse = house;
                    ChangeStateAndEnter(State.Onlook);
                }

            }


            //chain reaction, the escape villager also effect the other villager
            if (detectedCollider[i].TryGetComponent<Villager>(out Villager villager))
            {
                if (detectedCollider[i].gameObject == gameObject) return;

                if (villager.currentState == State.Escape && villager.distanceFromFiredHouse < 10)
                {
                    ChangeStateAndEnter(State.Escape);
                }
            }



        }
    }

    //OnStay
    void CheckIfArrive()
    {
        if (CheckIfReachDestination(targetDestination, 5))
        {
            ChangeStateAndEnter(State.Idle);
        }
    }



    #endregion
    #region Escape
    //OnEnter
    void SetEscapePointAndEscape()
    {
        //Vector3 direction = (firedHouse.transform.position - transform.position).normalized;
        float randomRange = Random.Range(runawayRange.x, runawayRange.y);
        //Vector3 destination = VillagerManager.Instance.SamplePositionOnNavMesh(direction * randomRange,20);
        //Instantiate(testPrefab, destination, Quaternion.identity);
        //Debug.Log(destination);

        Vector3 randomRun = RandomNavmeshLocation(firedHouse.transform.position, randomRange);
        agent.SetDestination(randomRun);

    }

    //OnStay
    void DetectFiredHouseDistance()
    {
        if (firedHouse == null)
        {
            ChangeStateAndEnter(State.Walk);
        }
        //if the house is being putting out on fire, then change to onlook state
        else if (CheckIfReachDestination(firedHouse.transform.position, 80) && firedHouse.getState() == 2)
        {
            ChangeStateAndEnter(State.Onlook);
        }

        //else if (CheckIfReachDestination(firedHouse.transform.position, 80) && firedHouse.getState() == 1)
        //{
        //    ChangeStateAndEnter(State.Escape);
        //}
        //if run too far (>80), then change back to walk state 
        else if (!CheckIfReachDestination(firedHouse.transform.position, 80))
        {
            firedHouse = null;
        }
    }

    #endregion
    #region Idle
    //OnEnter
    void IdleOnEnter()
    {
        
        //pretent enter to the building
        if (hasTargetHouse)
        {
            transform.LookAt(targetDestination);
            float randomWaitTime = Random.Range(5f, 10f);
            StartCoroutine(SetDeactiveAndActive(randomWaitTime));
        }
        else
        {
            float randomWaitTime = Random.Range(2f, 4f);
            StartCoroutine(StopForSeconds(randomWaitTime));
        }

    }

    IEnumerator SetDeactiveAndActive(float randomWaitTime)
    {
        //wait for 2s and then "enter" the building
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        Invoke("SetActive", randomWaitTime);
    }

    //"exit" from the building
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }


}

