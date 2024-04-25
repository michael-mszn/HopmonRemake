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
    public Passenger dragon;
    public Passenger spike;
    
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    //todo: refactor this to become one method once dynamic map generator was implemented
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
    
    public GameObject SetPassengerDragon(GameObject tile)
    {
        GameObject passenger = Instantiate(dragon.prefab, tile.transform.position, Quaternion.identity);
        PositionPassengerOnTile(tile, passenger, dragon.yOffset);
        return passenger;
    }
    
    public GameObject SetPassengerSpike(GameObject tile)
    {
        GameObject passenger = Instantiate(spike.prefab, tile.transform.position, Quaternion.identity);
        PositionPassengerOnTile(tile, passenger, spike.yOffset);
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
