using System.Collections;
using System.Collections.Generic;
using Entities.Monster.AI;
using UnityEngine;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance;
    
    public Passenger crystal;
    public Passenger bonusHeart;
    public Passenger bird;
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public GameObject SetPassengerCrystal(GameObject tile)
    {
        GameObject passenger = Instantiate(crystal.prefab, tile.transform.position, Quaternion.identity);
        PositionPassengerOnTile(tile, passenger, crystal.yOffset);
        return passenger;
    }
    
    public GameObject SetPassengerBonusHeart(GameObject tile)
    {
        GameObject passenger = Instantiate(bonusHeart.prefab, tile.transform.position, bonusHeart.prefab.transform.rotation);
        PositionPassengerOnTile(tile, passenger, bonusHeart.yOffset);
        return passenger;
    }
    
    public GameObject SetPassengerBird(GameObject tile)
    {
        GameObject passenger = Instantiate(bird.prefab, tile.transform.position, Quaternion.identity);
        PositionPassengerOnTile(tile, passenger, bird.yOffset);
        return passenger;
    }
    
    /*
     *  Aligns the passenger on the tile-grid 
     */
    public void PositionPassengerOnTile(GameObject tile, GameObject passenger, int yOffset)
    {
        passenger.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + yOffset,
            tile.transform.position.z); 
    }
}
