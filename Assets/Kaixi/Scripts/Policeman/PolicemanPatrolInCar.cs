using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PolicemanPatrolInCar : MonoBehaviour
{
    GameObject player;
    Vector3 PolicecarVector3;
    float patrolSpeed;
    float chaseSpeed;
    enum State { 
        Patrol,
        Chase,
        BackToCar
    }
    [SerializeField]State policeState;

    public NavMeshAgent agent;
    public float maxPatrolDistance;
    public float viewDistance = 10f; // view distance
    public float viewAngle = 90f; // view angle

    public List<Collider> targetsInViewRadius = new List<Collider>();
    public MeshFilter meshFilter;
    Mesh mesh;

    public bool backToCar = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mesh = CreateMesh();
        meshFilter.mesh = mesh;



    }
    private void Awake()
    {
        backToCar = false;
    }
    // Update is called once per frame
    void Update()
    {
        targetsInViewRadius = new List<Collider>(Physics.OverlapSphere(transform.position, viewDistance));
        
        switch (policeState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.BackToCar:
                agent.SetDestination(PolicecarVector3);
                if (Vector3.Distance(transform.position, PolicecarVector3) <= 2.0f) { 
                    backToCar= true;
                    gameObject.SetActive(false);
                }
                break;
        }

        if (targetsInViewRadius.Count > 0)
        {
            if (targetsInViewRadius.Contains(player.GetComponent<CapsuleCollider>()))
            {

                Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f)
                {
                    policeState = State.Chase;
                    //Debug.Log("Player is in view!");
                }
                else
                {
                    policeState = State.Patrol;
                }
            }
            else
            {
                policeState = State.Patrol;
            }


        }
    }


    void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)//if police is not walking
        {
            Vector3 randomPoint = RandomNavmeshLocation(transform.position, maxPatrolDistance);
            agent.speed = patrolSpeed;
            agent.SetDestination(randomPoint);
            StartCoroutine(StopForSeconds(2));
        }
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

    private IEnumerator StopForSeconds(float seconds)
    {
        // stop moving for seconds.
        agent.isStopped = true;
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;

    }


    private Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        int vertexCount = 16;
        Vector3[] vertices = new Vector3[vertexCount + 1];
        int[] triangles = new int[vertexCount * 3];

        vertices[0] = Vector3.zero;
        float angleStep = viewAngle / (vertexCount - 1);
        for (int i = 0; i < vertexCount; i++)
        {
            float angle = -viewAngle / 2f + angleStep * i;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad);
            float z = Mathf.Cos(angle * Mathf.Deg2Rad);
            vertices[i + 1] = new Vector3(x, 0f, z) * viewDistance;
        }

        for (int i = 0; i < vertexCount - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    public void SetPoliceCar(Vector3 vector3)
    {
        PolicecarVector3 = vector3;
    }

    public bool getBackToCar() { 
        return backToCar;
    }

}
