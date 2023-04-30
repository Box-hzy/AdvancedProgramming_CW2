using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AddHouseScript : MonoBehaviour
{
    public Vector2Int randomScore;
    public Material burningMat;
    public Material destoryMat;
    
    private void Awake()
    {

        int childCout = transform.childCount;

        for (int i = 0; i < childCout; i++)
        {
            if (transform.GetChild(i).GetComponent<MeshRenderer>() == null) return; //ignore EscapePoint empty obj
            
            Transform child = transform.GetChild(i);
            var house = child.AddComponent<House>();
            child.AddComponent<SpawnScaredVillagers>();

            house.tag = "Buildings";

            int random = Random.Range(randomScore.x, randomScore.y);

            house.setScore(random);

            //if (transform.CompareTag("BigHouse"))
            //{
            //    house.setScore(200);
            //}
            //else if (transform.CompareTag("SmallHouse"))
            //{
            //    house.setScore(100);
            //}
        }

    }

   
}
