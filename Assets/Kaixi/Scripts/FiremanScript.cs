using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiremanScript : MonoBehaviour
{
    
    HouseManager houseManager;
    FireStationManagement fireStationManagement;
    public GameObject ClosestFireHouse;
    public NavMeshAgent FirefighterAgent;
    public Vector3 FirefighterOriginPostion;
    public FireEngine fireEngineScript;
    public GameObject fireEngine;

    public int state = 0; //0 for go to fire building, 1 for put off fire, 2 for get back to fire engine
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        houseManager = GameObject.Find("HouseManager").GetComponent<HouseManager>();
        fireStationManagement = GameObject.Find("FireStationManagement").GetComponent<FireStationManagement>();
        FirefighterAgent = GetComponent<NavMeshAgent>();
        
        FirefighterOriginPostion = this.transform.position;
    }

    private void OnEnable()
    {
        fireEngine = transform.parent.gameObject;
        fireEngineScript = fireEngine.GetComponent<FireEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log(Vector3.Distance(transform.position, FirefighterOriginPostion));
        switch (state) {
            case 0:
                
                FirefighterAgent.SetDestination(ClosestFireHouse.transform.position);
                if (Vector3.Distance(transform.position, ClosestFireHouse.transform.position) < 3f) {
                    
                    state = 1;
                }
                break;
            case 1:
                Debug.Log("put off fire");
                StartCoroutine(putoffFire());
                break;
            case 2:
                Debug.Log("Go Back");
                GoBackToFireEngine();
                break;
        }

    }


    

    IEnumerator putoffFire()
    {
        yield return new WaitForSeconds(3f);

       
        houseManager.setHouseState(ClosestFireHouse, 2); //set house state into ruin.
        state = 2;
    }

    void GoBackToFireEngine() {
        FirefighterAgent.SetDestination(FirefighterOriginPostion);
        if (Vector3.Distance(transform.position,FirefighterOriginPostion)<= 1.5f)
        {
            
            transform.SetParent(fireEngine.transform);
            fireEngineScript = fireEngine.GetComponent<FireEngine>();
            fireEngineScript.changeState(2);
            this.gameObject.SetActive(false);
        }
    }

    public void SetFireEnginePostion(Vector3 postion) { 
        FirefighterOriginPostion = postion;

    }
}
