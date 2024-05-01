using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class LevelGenerator : MonoBehaviour
{
    public GameObject map;
    public GameObject player;
    public GameObject baseTilePrefab;
    public GameObject floorTilePrefab;
    public GameObject bridgeTilePrefab;
    public GameObject spikeTilePrefab;
    public GameObject switchTilePrefab;
    public GameObject cyanSwitchTilePrefab;
    public GameObject orangeSwitchTilePrefab;
    public GameObject obstacleTilePrefab;
    public GameObject onewayTilePrefab;
    public GameObject wallTilePrefab;
    public GameObject largeWallTilePrefab;

    void Awake()
    {
        GenerateLevel();
        SetSpawnPoint();
    }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
       
    }
    

    private void GenerateLevel()
    {
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(0, 0, i);
            GameObject tile = Instantiate(floorTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(-10, 4, i);
            GameObject tile = Instantiate(wallTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(-20, 8, i);
            GameObject tile = Instantiate(largeWallTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(i, 0, 0);
            GameObject tile = Instantiate(onewayTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(i, 0, 50);
            GameObject tile = Instantiate(bridgeTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(50, 0, i);
            GameObject tile = Instantiate(spikeTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(20, 0, i);
            GameObject tile = Instantiate(cyanSwitchTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }
        
        for(int i = 10; i <= 100; i += 10)
        {
            Vector3 coordinate = new Vector3(30, 0, i);
            GameObject tile = Instantiate(orangeSwitchTilePrefab, coordinate, Quaternion.identity);
            tile.transform.parent = map.transform;
        }

        Instantiate(switchTilePrefab, new Vector3(10, 0, 20), Quaternion.identity);
        Instantiate(obstacleTilePrefab, new Vector3(10, 0, 10), Quaternion.identity);
    }
    
    /*
     * Properly centers the playable character on the tile grid
     */
    private void SetSpawnPoint()
    {
        GameObject baseTile = Instantiate(baseTilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        baseTile.transform.parent = map.transform;
        player.transform.position = new Vector3(baseTile.transform.position.x,
            baseTile.transform.position.y + 7,
            baseTile.transform.position.z);
        player.GetComponent<Movement>().InitDestination();
    }
}
