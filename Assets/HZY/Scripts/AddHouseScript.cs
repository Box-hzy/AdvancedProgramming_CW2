using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class AddHouseScript : MonoBehaviour
{
    public Material burningMat;
    public Material destoryMat;

    private void Awake()
    {

        int childCout = transform.childCount;

        for (int i = 0; i < childCout; i++)
        {
            var house = transform.GetChild(i).AddComponent<House>();
            house.burningMaterial = burningMat;
            house.destroiedMaterial = destoryMat;

            if (transform.CompareTag("BigHouse"))
            {
                house.setScore(200);
            }
            else if (transform.CompareTag("SmallHouse"))
            {
                house.setScore(100);
            }
        }

    }

   
}
