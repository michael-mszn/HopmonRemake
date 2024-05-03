using System;
using System.Collections.Generic;
using System.IO;
using LevelGen;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject gameMap;
    public GameObject player;

    private LevelGenDictionary levelGenDictionary;
    
    /*
     * The Level Generator operates in a single scene. It loads the .txt file with the corresponding
     * level number that gets set by the SceneHandler.
     */
    void Awake()
    {
        string level = File.ReadAllText(Application.streamingAssetsPath + "/Levels/" + "Level" + MainMenu.selectedLevel + ".txt");
        levelGenDictionary = gameObject.GetComponent<LevelGenDictionary>();
        levelGenDictionary.InitTileMap();
        levelGenDictionary.InitPassengerMap();
        GenerateLevel(level);
        SetSpawnPoint();
    }

    private void GenerateLevel(String level)
    {
        string[] rows = level.Split("\n");
        int zCoordinate = 0;
        foreach (string row in rows)
        {
            GenerateRow(row, zCoordinate);
            zCoordinate -= 10;
        }
    }

    /*
     * Responsible for generating all tiles + passenger of an individual row (z-axis)
     */
    private void GenerateRow(string row, int zCoordinate)
    {
        string[] tiles = row.Split(" ");
        int xCoordinate = 0;
        foreach (string tile in tiles)
        {
            string[] tileItems = tile.Split("|");
            GameObject tileGameObject = null;
            foreach(var entry in levelGenDictionary.GetTileMap())
            {
                if (string.Equals(tileItems[0], entry.Value.Item1) && !string.Equals(tileItems[0], "EMPTY")) 
                {
                    tileGameObject = Instantiate(entry.Value.Item2, new Vector3(xCoordinate, 
                        entry.Value.Item2.gameObject.transform.position.y,
                        zCoordinate), Quaternion.identity);
                    tileGameObject.transform.parent = gameMap.transform;
                }
            }

            if (tileItems.Length == 2)
            {
                foreach (var entry in levelGenDictionary.GetPassengerMap())
                {
                    if (string.Equals(tileItems[1], entry.Value.Item1) && !string.Equals(tileItems[0], "NP"))
                    {
                        GameObject passengerGameObject = SetPassenger(tileGameObject, entry.Value.Item2);
                        passengerGameObject.transform.parent = gameMap.transform;
                    }
                }
            }
            xCoordinate += 10;
        }
    }
    
    private GameObject SetPassenger(GameObject tile, Passenger passenger)
    {
        GameObject p = Instantiate(passenger.prefab, tile.transform.position, passenger.prefab.gameObject
            .transform.rotation);
        PositionPassengerOnTile(tile, p, passenger.GetOffset());
        return p;
    }
    
    /*
     *  Aligns the passenger on the tile-grid
     */
    private void PositionPassengerOnTile(GameObject tile, GameObject passenger, int yOffset)
    {
        passenger.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + yOffset,
            tile.transform.position.z); 
    }
        
    /*
     * Properly centers the playable character on the tile grid
     */
    private void SetSpawnPoint()
    {
        GameObject baseTile = gameMap.transform.Find("BaseTile(Clone)").gameObject;
        player.transform.position = new Vector3(baseTile.transform.position.x,
            baseTile.transform.position.y + 7,
            baseTile.transform.position.z);
        player.GetComponent<Movement>().InitDestination();
    }
}
