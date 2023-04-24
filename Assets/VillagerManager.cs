using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VillagerManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform villagerGroup;
    public int numVillager = 15;
    BoxCollider spawnArea;

    private void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        

        for (int i = 0; i < numVillager; i++)
        {
            Vector3 randomPosition = SampleRandomPositionOnNavMesh(GetRandomPosition());
            Instantiate(prefab, randomPosition, Quaternion.identity, villagerGroup);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
        return new Vector3(x, y, z);
    }

    Vector3 SampleRandomPositionOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 100, NavMesh.AllAreas))
        { 
            position = hit.position;
        }
        return position;
    }

}
