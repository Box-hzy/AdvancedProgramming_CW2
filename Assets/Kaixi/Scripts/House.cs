using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class House : MonoBehaviour
{


    public int houseState;
    public List<House> neighbourHouses = new List<House>();
    public int score;
    public Material defaultMaterial;
    public Material burningMaterial;
    public Material destroiedMaterial;

    public float radius = 10;    //raycast radius
    Collider[] results = new Collider[10];
    LayerMask layerMask;
    float RecoverTime = 180;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        houseState = 0;
        AddNeighbour();
        layerMask = 1 << 10;
        defaultMaterial = GetComponent<MeshRenderer>().material;
        gameObject.AddComponent<NavMeshObstacle>();
    }

    // Update is called once per frame
    void Update()
    {

        if (houseState == 3)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                setState(0);
            }
        }




    }


    private void AddNeighbour()
    {
        int hits = Physics.OverlapSphereNonAlloc(transform.position, radius, results, layerMask);
        for (int i = 0; i < hits; i++)
        {
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

        switch (houseState)
        {
            case 0://original state
                GetComponent<Renderer>().material = defaultMaterial; 
                break;
            case 1://fire is burning
                GetComponent<Renderer>().material = burningMaterial; 
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


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
