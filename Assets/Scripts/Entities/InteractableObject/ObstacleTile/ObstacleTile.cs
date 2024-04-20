using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTile : MonoBehaviour
{
    public GameObject obstacle;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Wall";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DestroyObstacle()
    {
        gameObject.tag = "Tile";
        LevelManager.AllTiles.Add(gameObject);
        Destroy(obstacle);
    }
}
