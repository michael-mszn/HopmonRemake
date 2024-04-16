using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public static CharacterManager Instance;
    public float maximumSpeed;
    [Range(0, 1)]
    public float lowestSpeedPercentage;
    public int hp;
    public int crystalCarried;
    public float currentSpeed;
    private Transform baseTile;
    private float lowestSpeedLimit;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        lowestSpeedLimit = maximumSpeed * lowestSpeedPercentage;
        currentSpeed = maximumSpeed;
        hp = 2;
        crystalCarried = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Properly centers the playable character on the tile grid
     */
    public void SetSpawnPoint(GameObject character)
    {
        baseTile = GameObject.FindWithTag("Map").transform.Find("BaseTile").gameObject.GetComponent<Transform>();
        character.transform.position = new Vector3(baseTile.transform.position.x, baseTile.transform.position.y + 7,
            baseTile.transform.position.z);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void AddCrystal()
    {
        crystalCarried += 1;
        if (currentSpeed >= lowestSpeedLimit)
        {
            currentSpeed -= maximumSpeed*0.1f;
        }
    }

    public void SecureCrystal()
    {
        while (crystalCarried != 0)
        {
            crystalCarried -= 1;
            currentSpeed += maximumSpeed*0.1f;
            LevelManager.Instance.crystalsLeft -= 1;
        }

        if (LevelManager.Instance.crystalsLeft == 0)
        {
            //TODO: Proper transition to next level
            print("Level solved!");
        }
    }
    
}
