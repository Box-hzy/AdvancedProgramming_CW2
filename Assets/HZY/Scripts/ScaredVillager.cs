using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScaredVillager : MonoBehaviour
{

    float escapeRange = 200;
    float angle = 30;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("DestroyVillager", 5);
    }

    public void SetEscapePoint()
    {
        float randomAngle = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 direction = rotation * transform.parent.forward;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction * escapeRange, out hit, randomAngle, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }

    void DestroyVillager()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

    }
}
