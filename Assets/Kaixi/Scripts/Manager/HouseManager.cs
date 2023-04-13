using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public GameObject closestHouse;
    //public GameObject[] buildings;

    public House[] houses;

    public float minDistance;
    
    //public Dictionary<GameObject,int> HouseState = new Dictionary<GameObject,int>(); //Housename,house state(0 for nothing, 1 for burning,2 for fire get extinguished)
    
    public GameObject testObject;
    GameObject currentBuringHouse;

    public Material burningMaterial;
    public Material ruinMaterial;

    public Transform LargeHouses;
    GameManagement gameManagement;

    private void Start()
    {
        
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        
        //buildings = GameObject.FindGameObjectsWithTag("Buildings");
        

    }

    void ClosestHouse(GameObject gameObject) {
        minDistance = Mathf.Infinity;
        closestHouse = null;

        

        foreach (House house in houses)
        {
            float distance = Vector3.Distance(gameObject.transform.position, house.gameObject.transform.position);
            if (distance < minDistance)
            {
                closestHouse = house.gameObject;
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



    public void setHouseState(GameObject house,int stateNum) {
        house.GetComponent<House>().setState(stateNum);
        
    }


    public void ClosestHouseWithState(GameObject gameObject,int state)
    {
        minDistance = Mathf.Infinity;
        closestHouse = null;


        foreach (House house in houses)
        {
            float distance = Vector3.Distance(gameObject.transform.position, house.gameObject.transform.position);
            if (distance < minDistance && house.getState() == state)
            {
                closestHouse = house.gameObject;
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
