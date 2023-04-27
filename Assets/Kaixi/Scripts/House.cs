using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class House : MonoBehaviour
{

    public GameObject scaredVillager;
    public int houseState;
    public List<House> neighbourHouses = new List<House>();
    public int score;
    public Material defaultMaterial;
    public Material burningMaterial;
    public Material destroiedMaterial;
    //public Transform escapePoint;
    public float radius = 10;    //raycast radius
    Collider[] results = new Collider[10];
    [SerializeField] LayerMask layerMask;
    float RecoverTime = 180;
    float timer;
    Vector3 centrePoint; //transform.position is based on Pivot point, however the pivot point of the model is too faraway
    float FireTimer = 0;
    public VisualEffect fireVFX;
    float putoffFire = 180;

    // Start is called before the first frame update
    void Start()
    {
        fireVFX = GetComponentInChildren<VisualEffect>();
        houseState = 0;
        layerMask = 1 << 10;
        defaultMaterial = GetComponent<MeshRenderer>().material;
        gameObject.AddComponent<NavMeshObstacle>().carving = true;
        centrePoint = GetComponent<MeshRenderer>().bounds.center;
        AddNeighbour();
        //putoffFire = fireVFX.GetFloat("MaxSize");
        //SetEscapePoint();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (houseState == 1)
        { //fire will get big with the time increase;
            FireTimer += Time.deltaTime;
            if (FireTimer <= fireVFX.GetFloat("MaxSize"))
            {
                fireVFX.SetFloat("FireSize", FireTimer);
            }
            else
            {
                fireVFX.SetFloat("FireSize", fireVFX.GetFloat("MaxSize"));
            }


        }
        if (houseState == 2)
        {

            putoffFire -= Time.deltaTime;
            if (putoffFire > 0)
            {
                fireVFX.SetFloat("FireSize", putoffFire);
            }
            else
            {
                fireVFX.SetFloat("FireSize", 0);
            }


        }



        if (houseState == 3)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                setState(0);
            }
        }


    }

    void SpawnScaredVillagers()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go =   Instantiate(scaredVillager, transform.position, Quaternion.identity, transform);
        }
       
    }


    private void AddNeighbour()
    {
        int hits = Physics.OverlapSphereNonAlloc(centrePoint, radius, results, layerMask);
        for (int i = 0; i < hits; i++)
        {
            //Debug.Log("111");
            if (results[i].TryGetComponent<House>(out House house))
            {

                if (house != this)
                    neighbourHouses.Add(house);
            }
        }


    }

    public int getState()
    {
        return houseState;
    }

    public void setState(int thisState)
    {
        houseState = thisState;
        fireVFX.SetInt("HouseState", houseState);

        switch (houseState)
        {
            case 0://original state
                GetComponent<Renderer>().material = defaultMaterial;
                break;
            case 1://fire is burning
                GetComponent<Renderer>().material = burningMaterial;
                SpawnScaredVillagers();
                break;
            case 2:// fireman is putting off the fire
                break;
            case 3://house into ruin
                GetComponent<Renderer>().material = destroiedMaterial;
                timer = RecoverTime;

                break;
        }
    }

    public void setScore(int thisScore)
    {
        score = thisScore;
    }
    public int getScore()
    {
        return score;
    }

    public Vector3 getCentre()
    {
        return centrePoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centrePoint, radius);
    }
}
