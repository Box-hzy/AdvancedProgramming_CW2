using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireStationManagement : MonoBehaviour
{
    public List<GameObject> FireStationList = new List<GameObject>();
    Dictionary<GameObject,GameObject> FireEngineAppear = new Dictionary<GameObject,GameObject>();
    
    public GameObject FireEngine;//need add
    
    GameObject firehouse;

    public List<GameObject> FireEngineGenerationList= new List<GameObject>();

    public bool dispatch = false;

    public float truckStoppingDistance = 3.0f;

    
    GameManagement gameManagement;
    HouseManager houseManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        for (int i = 0; i < FireEngineGenerationList.Count; i++) {
            FireEngineAppear.Add(FireStationList[i], FireEngineGenerationList[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagement.getFireAlarm()) {
            
            firehouse = houseManager.getCurrentBurningHouse();
            Debug.Log(firehouse);
            GameObject firestation = GetClosestFireStation(firehouse);
            DispatchFireEngines(firestation, firehouse);
            

        }
    }

    void DispatchFireEngines(GameObject firestation, GameObject firehouse) { //instantiate a fire engine which will drive to the burning house
        Debug.Log("dispatch");

        //GameObject thisFireEngine = FireEngineAppear[firestation];

        //thisFireEngine.SetActive(true);

        GameObject thisFireEngine = Instantiate(FireEngine, FireEngineAppear[firestation].transform.position, Quaternion.identity);
        FireEngine fireEngineScript = thisFireEngine.GetComponent<FireEngine>();
        
        
        
        //Caculate the parking point near the burning house
        Vector3 truckDirection = (firehouse.transform.position - thisFireEngine.transform.position).normalized;
        Vector3 truckStopPosition = firehouse.transform.position - truckDirection * truckStoppingDistance;

        fireEngineScript.setFirehouseDestination(truckStopPosition);
        fireEngineScript.setFireStationDestination(FireEngineAppear[firestation].transform.position);
        fireEngineScript.firehouse = firehouse;

        gameManagement.setFireAlarm(false);
    }

    GameObject GetClosestFireStation(GameObject firehouse) {
        GameObject closestFireStation = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject firestation in FireStationList) {
            float distance = Vector3.Distance(firestation.transform.position, firestation.transform.position);
            if (minDistance > distance) { 
                minDistance= distance;
                closestFireStation = firestation;
            }
        }
        return closestFireStation;
    }
}
