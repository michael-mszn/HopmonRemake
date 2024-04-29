using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject passenger;

    void Awake()
    {
        LevelManager.LevelManagerIsReady += OnLevelManagerIsReady;
    }
    
    // Start is called before the first frame update
    void Start()
    { 
        
        /*
         * All Tiles, Interactables and Monsters must be the child of a Map GameObject
         */
        GameObject map = GameObject.FindWithTag("Map");
        
        //todo: this is just for test cases and will be replaced with a dynamic passenger-setter
        if (UnityEngine.Random.Range(0f, 1.0f) <= 0.23f)
        {
            passenger = PassengerManager.Instance.SetPassengerCrystal(gameObject);
        }
        
        else if(UnityEngine.Random.Range(0f, 1.0f) > 0.33f && UnityEngine.Random.Range(0f, 1.0f) <= 0.45f)
        {
            passenger = PassengerManager.Instance.SetPassengerBonusHeart(gameObject);
        }
        else if (UnityEngine.Random.Range(0f, 1.0f) > 0.23f && UnityEngine.Random.Range(0f, 1.0f) <= 0.28f)
        {
            passenger = PassengerManager.Instance.SetPassengerDragon(gameObject);
        }
        
        else if (UnityEngine.Random.Range(0f, 1.0f) > 0.45f && UnityEngine.Random.Range(0f, 1.0f) <= 0.66f  && tag.Equals("SpikeTile"))
        {
            passenger = PassengerManager.Instance.SetPassengerSpike(gameObject);
        }
        else if (UnityEngine.Random.Range(0f, 1.0f) > 0.28f && UnityEngine.Random.Range(0f, 1.0f) <= 0.33f)
        {
            passenger = PassengerManager.Instance.SetPassengerStalker(gameObject);
        }
        
        passenger.transform.parent = map.transform;
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
