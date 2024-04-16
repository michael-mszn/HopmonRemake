using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance;
    
    public Passenger crystal;
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject SetPassengerCrystal(GameObject tile)
    {
        GameObject passenger = Instantiate(crystal.prefab, tile.transform.position, Quaternion.identity);
        PositionPassengerOnTile(tile, passenger, crystal.yOffset);
        return passenger;
    }
    
    public void PositionPassengerOnTile(GameObject tile, GameObject passenger, int yOffset)
    {
        passenger.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + yOffset,
            tile.transform.position.z); 
    }
}
