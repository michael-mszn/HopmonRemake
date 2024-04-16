using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject passenger;
    
    // Start is called before the first frame update
    void Start()
    { 
        /*
         * All Tiles, Interactables and Monsters must be the child of a Map GameObject
         */
        GameObject map = GameObject.FindWithTag("Map");
        passenger = PassengerManager.Instance.SetPassengerCrystal(gameObject);
        passenger.transform.parent = map.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
