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
    public float invulnerabilitySeconds;
    public float fireCooldown;
    private float currentSpeed;
    private float invulnerabilityTimer;
    private Transform baseTile;
    private float lowestSpeedLimit;
    private int hp;
    private int crystalCarried;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        lowestSpeedLimit = maximumSpeed * lowestSpeedPercentage;
        currentSpeed = maximumSpeed;
        hp = 2;
        crystalCarried = 0;
        invulnerabilityTimer = 0;
        UIManager.Instance.InitializeUI();
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
    
    public void AddCrystal()
    {
        crystalCarried += 1;
        UIManager.Instance.UpdateCrystalCarriedText();
        if (currentSpeed >= lowestSpeedLimit)
        {
            currentSpeed -= maximumSpeed*0.1f;
        }
    }

    public void IncreaseHP()
    {
        hp += 1;
        UIManager.Instance.UpdateHpText();
    }

    public void SecureCrystal()
    {
        while (crystalCarried != 0)
        {
            crystalCarried -= 1;
            currentSpeed += maximumSpeed*0.1f;
            LevelManager.Instance.DecrementCrystalsLeft();
            UIManager.Instance.UpdateCrystalCarriedText();
            UIManager.Instance.UpdateCrystalsLeftText();
        }

        if (LevelManager.Instance.GetCrystalsLeft() == 0)
        {
            //TODO: Proper transition to next level
            print("Level solved!");
        }
    }
    
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public int GetHp()
    {
        return hp;
    }
    
    public int GetCrystalCarried()
    {
        return crystalCarried;
    }

    public void TakeDamage()
    {
        if (invulnerabilityTimer <= 0)
        {
            if (hp == 0)
            {
                //Game over 
            }
            else
            {
                hp -= 1;
                invulnerabilityTimer = invulnerabilitySeconds;
                UIManager.Instance.UpdateHpText();
            }
        }
    }
    
}
