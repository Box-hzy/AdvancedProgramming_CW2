﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInteract : MonoBehaviour
{
    public Material grassmat;
    public Transform player;
    public Terrain terrain;
    TerrainData tData;
    void Start()
    {
      
        
    }

    void Update()
    {
        
            
        Shader.SetGlobalVector("_PlayerPosition", player.position);
        
    }
}
