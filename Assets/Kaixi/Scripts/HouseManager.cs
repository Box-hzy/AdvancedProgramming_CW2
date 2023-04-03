using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject closestHouse;
    public GameObject[] buildings;
    public float minDistance;

    public void ClosetHouse() {
        minDistance = Mathf.Infinity;

        buildings = GameObject.FindGameObjectsWithTag("Buildings");

        foreach (GameObject building in buildings)
        {
            float distance = Vector3.Distance(playerTransform.position, building.transform.position);
            if (distance < minDistance)
            {
                closestHouse = building;
                minDistance = distance;

            }
        }
    }
    
    public GameObject getClosestHouse() //return a transform with the nearest house
    {
        
        return closestHouse;
    }

    public float getMinDistance() {
        return minDistance;
    }

    private void Update()
    {
        ClosetHouse();
    }


}
