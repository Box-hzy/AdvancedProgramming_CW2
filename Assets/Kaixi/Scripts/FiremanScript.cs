using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiremanScript : MonoBehaviour
{
    HouseManager houseManager;
    GameObject ClosestFireHouse;
    public NavMeshAgent FirefighterAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Extinguishing();
    }

    void Extinguishing() {
        ClosestFireHouse = houseManager.getClosestHouseWithState(this.gameObject, 1);
        //Debug.Log(ClosestFireHouse);
        if (ClosestFireHouse != null)
        {
            FirefighterAgent.SetDestination(ClosestFireHouse.transform.position);
        } 
    }
}
