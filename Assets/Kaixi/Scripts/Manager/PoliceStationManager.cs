using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStationManager : MonoBehaviour
{
    public List<GameObject> PoliceStationList = new List<GameObject>();
    Dictionary<GameObject, GameObject> PoliceEngineAppear = new Dictionary<GameObject, GameObject>();

    public GameObject PoliceEngine;//need add

    GameObject firehouse;

    public List<GameObject> PoliceEngineGenerationList = new List<GameObject>();

    public bool dispatch = false;

    public float truckStoppingDistance = 3.0f;


    GameManagement gameManagement;
    HouseManager houseManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        for (int i = 0; i < PoliceEngineGenerationList.Count; i++)
        {
            PoliceEngineAppear.Add(PoliceStationList[i], PoliceEngineGenerationList[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagement.getFireAlarm())
        {

            firehouse = houseManager.getCurrentBurningHouse();
            Debug.Log(firehouse);
            GameObject Policestation = GetClosestPoliceStation(firehouse);
            DispatchPoliceEngines(Policestation, firehouse);


        }
    }

    void DispatchPoliceEngines(GameObject Policestation, GameObject firehouse)
    { //instantiate a Police engine which will drive to the burning house
        Debug.Log("dispatch");


        //thisPoliceEngine.SetActive(true);

        GameObject thisPoliceEngine = Instantiate(PoliceEngine, PoliceEngineAppear[Policestation].transform.position, Quaternion.identity);
        PoliceCar PoliceEngineScript = thisPoliceEngine.GetComponent<PoliceCar>();



        //Caculate the parking point near the burning house
        Vector3 truckDirection = (firehouse.transform.position - thisPoliceEngine.transform.position).normalized;
        Vector3 truckStopPosition = firehouse.transform.position - truckDirection * truckStoppingDistance;

        PoliceEngineScript.setPolicehouseDestination(truckStopPosition);
        PoliceEngineScript.setPoliceStationDestination(PoliceEngineAppear[Policestation].transform.position);
        PoliceEngineScript.firehouse = firehouse;


        gameManagement.setFireAlarm(false);
    }

    GameObject GetClosestPoliceStation(GameObject firehouse)
    {
        GameObject closestPoliceStation = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject firestation in PoliceStationList)
        {
            float distance = Vector3.Distance(firestation.transform.position, firestation.transform.position);
            if (minDistance > distance)
            {
                minDistance = distance;
                closestPoliceStation = firestation;
            }
        }
        return closestPoliceStation;
    }
}
