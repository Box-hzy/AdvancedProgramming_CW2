using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireStationManagement : MonoBehaviour
{
    public List<GameObject> FireStationList = new List<GameObject>();
    Dictionary<GameObject,GameObject> FireEngineAppear = new Dictionary<GameObject,GameObject>();
    
    public GameObject FireEngine;
    GameObject firehouse;

    public List<GameObject> FireEngineList= new List<GameObject>();

    GameManagement gameManagement;
    HouseManager houseManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        for (int i = 0; i < FireEngineList.Count; i++) {
            FireEngineAppear.Add(FireStationList[i], FireEngineList[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagement.getFireAlarm()) {
            firehouse = houseManager.getClosestHouseWithState(this.gameObject, 1);
            Debug.Log(firehouse);
            GameObject firestation = GetClosestFireStation(firehouse);
            DispatchFireEngines(firestation,firehouse);  
        }
    }

    void DispatchFireEngines(GameObject firestation,GameObject firehouse) { //instantiate a fire engine which will drive to the burning house
        
        GameObject thisFireEngine = FireEngineAppear[firestation];
        //GameObject thisFireEngine = Instantiate(FireEngine, FireEngineAppear[firestation].transform);
        thisFireEngine.SetActive(true);
        NavMeshAgent agent = thisFireEngine.GetComponent<NavMeshAgent>();
        agent.SetDestination(firehouse.transform.position);
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
