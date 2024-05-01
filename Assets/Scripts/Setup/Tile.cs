using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    void Awake()
    {
        LevelManager.LevelManagerIsReady += OnLevelManagerIsReady;
    }
    
    private void OnLevelManagerIsReady()
    {
        if (tag.Contains("Tile"))
        {
            LevelManager.AllTiles.Add(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        LevelManager.LevelManagerIsReady -= OnLevelManagerIsReady;
    }
}
