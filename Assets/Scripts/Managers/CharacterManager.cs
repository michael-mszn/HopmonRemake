using System;
using System.Collections;
using System.Collections.Generic;
using Setup;
using UnityEngine;

public class CharacterManager : MonoBehaviour, IInitializedFlag
{

    public static CharacterManager Instance;
    public float maximumSpeed;
    [Range(0, 1)]
    public float lowestSpeedPercentage;
    public float invulnerabilitySeconds;
    public float fireCooldown;
    public GameObject character;
    public float invulnerabilityTimer;
    private float currentSpeed;
    private Transform baseTile;
    private float lowestSpeedLimit;
    private int hp;
    private int crystalCarried;
    private Material characterMaterial;

    private bool isInitialized;

    private void Awake()
    {
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isInitialized = false;
        SetSpawnPoint();
        lowestSpeedLimit = maximumSpeed * lowestSpeedPercentage;
        currentSpeed = maximumSpeed;
        hp = 2;
        crystalCarried = 0;
        invulnerabilityTimer = 0;
        isInitialized = true;
        characterMaterial = character.GetComponent<Renderer>().material;
    }
    
    void Update()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
            DoInvulnerabilityFrameFlickering();
        }
    }

    /*
     * Properly centers the playable character on the tile grid
     */
    public void SetSpawnPoint()
    {
        baseTile = GameObject.FindWithTag("Map").transform.Find("BaseTile").gameObject.GetComponent<Transform>();
        character.transform.position = new Vector3(baseTile.transform.position.x, baseTile.transform.position.y + 7,
            baseTile.transform.position.z);
        character.GetComponent<Movement>().InitDestination();
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
        if (!PauseMenu.isPaused)
        {
            if (invulnerabilityTimer <= 0)
            {
                hp -= 1;
                UIManager.Instance.UpdateHpText();
                if (hp == 0)
                {
                    hp = 0;
                    //UIManager.Instance.ShowGameOver();
                }
                else
                {
                    invulnerabilityTimer = invulnerabilitySeconds;
                    ChangeMaterial(Color.red, 1f);
                }
            }
        }
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    IEnumerator ShowInvulnerabilityFrame()
    {
        yield return new WaitForSeconds(0.05f);
        ChangeMaterial(Color.white, 1f);
        yield return new WaitForSeconds(0.3f);
        /*
         * The repeated if check here ensures that the flickering animation matches the actual
         * i-frame cooldown in the code
         */
        if (invulnerabilityTimer > 0)
        {
            ChangeMaterial(Color.white, 0.25f);
        }
    }

    private void ChangeMaterial(Color color, float opacity)
    {
        Color currentColor = color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, opacity);
        characterMaterial.SetColor("_Color", newColor);
    }
    
    private void DoInvulnerabilityFrameFlickering()
    {
        StartCoroutine(ShowInvulnerabilityFrame());
    }
    
}
