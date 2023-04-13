using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddHouseScript : MonoBehaviour
{
    private void Awake()
    {

        int childCout = transform.childCount;

        for (int i = 0; i < childCout; i++)
        {
            var house = transform.GetChild(i).AddComponent<House>();
            if (transform.CompareTag("BigHouse"))
            {
                house.setScore(200);
            }
            else if (transform.CompareTag("SmallHouse"))
            {
                house.setScore(100);
            }
        }

    }

    public float radius;
    public Collider[] results;
    private void AddNeighbour()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, results);
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].TryGetComponent<House>(out House house))
            { 
                
            }
        }

       
    }
}
