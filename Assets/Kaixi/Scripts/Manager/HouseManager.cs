using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public GameObject closestHouse;
    public GameObject[] buildings;
    public float minDistance;
    public Dictionary<GameObject,int> HouseState = new Dictionary<GameObject,int>(); //Housename,house state(0 for nothing, 1 for burning,2 for fire get extinguished)
    public GameObject testObject;
    GameObject currentBuringHouse;

    public Material burningMaterial;
    public Material ruinMaterial;
    
    GameManagement gameManagement;

    private void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        
        //buildings = GameObject.FindGameObjectsWithTag("Buildings");
        
        foreach (GameObject building in buildings) { 
            HouseState.Add(building, 0); 
        }

    }

    void ClosestHouse(GameObject gameObject) {
        minDistance = Mathf.Infinity;
        closestHouse = null;

        

        foreach (GameObject building in buildings)
        {
            float distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            if (distance < minDistance)
            {
                closestHouse = building;
                minDistance = distance;

            }
        }
    }
    
    public GameObject getClosestHouse(GameObject gameObject) //return a transform with the nearest house
    {
        ClosestHouse(gameObject);

        return closestHouse;
    }

    public float getMinDistance(GameObject gameObject) {
        ClosestHouse(gameObject);

        return minDistance;
    }

    public int getHouseState(GameObject house) { 
        return HouseState[house];
    }

    public void setHouseState(GameObject house,int stateNum) {
        HouseState[house] = stateNum;
    }


    public void ClosestHouseWithState(GameObject gameObject,int state)
    {
        minDistance = Mathf.Infinity;
        closestHouse = null;


        foreach (GameObject building in buildings)
        {
            float distance = Vector3.Distance(gameObject.transform.position, building.transform.position);
            if (distance < minDistance && getHouseState(building) == state)
            {
                closestHouse = building;
                minDistance = distance;

            }
        }
  
    }


    public GameObject getClosestHouseWithState(GameObject gameObject, int state) //return a transform with the nearest house
    {
        ClosestHouseWithState(gameObject,state);

        return closestHouse;
    }

    public float getMinDistanceWithState(GameObject gameObject, int state)
    {
        ClosestHouseWithState(gameObject, state);

        return minDistance;
    }

    public void setCurrentBurningHouse(GameObject thisGamObject) {
        currentBuringHouse = thisGamObject;
    }

    public GameObject getCurrentBurningHouse() {
        return currentBuringHouse;
    }

    public Material BurningMaterial(){
        return burningMaterial;
    }

    public Material RuinMaterial() {
        return ruinMaterial; 
    }
}
