using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScaredVillagers : MonoBehaviour
{

    public GameObject scaredVillager;
    private House thisHouse;
    bool isSpawn;
    private void Awake()
    {
        thisHouse = GetComponent<House>();
    }

    private void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        switch (thisHouse.getState())
        {
            case 0:
                isSpawn = false;
                break;
            case 1:
                if (!isSpawn)
                {
                    Spawn();
                }
                break;
        }
    }

    void Spawn()
    {
        isSpawn = true;
        Debug.Log("spawn scared villagers");
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(scaredVillager, transform.position, Quaternion.identity, VillagerManager.Instance.scaredVillagerParent);
            go.GetComponent<ScaredVillager>().origin = transform;
            go.GetComponent<ScaredVillager>().SetEscapePoint();
        }
      
    }
}
