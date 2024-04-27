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
    private float currentSpeed;
    public float invulnerabilityTimer;
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
            DoInvulnerabilityFrameFlickering(0.25f, 0.10f, Color.red);
            DoInvulnerabilityFrameFlickering(1f, 0.4f, Color.white);
        }
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
            hp -= 1;
            UIManager.Instance.UpdateHpText();
            if (hp == 0)
            {
                hp = 0;
                UIManager.Instance.ShowGameOver();
            }
            else
            {
                invulnerabilityTimer = invulnerabilitySeconds;
                DoInvulnerabilityFrameFlickering(1f, 0f, Color.red);
            }
        }
    }

    public bool IsInitialized()
    {
        return isInitialized;
    }

    IEnumerator ShowInvulnerabilityFrame(float opacity, float delayTime, Color color)
    {
        yield return new WaitForSeconds(delayTime);
        /*
         * The repeated if check here ensures that the flickering animation matches the actual
         * i-frame cooldown in the code
         */
        if (invulnerabilityTimer > 0)
        {
            Color currentColor = color;
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, opacity);
            characterMaterial.SetColor("_Color", newColor);
        }
        else
        {
            characterMaterial.SetColor("_Color", Color.white);
        }
    }
    
    private void DoInvulnerabilityFrameFlickering(float opacity, float delayTime, Color color)
    {
        StartCoroutine(ShowInvulnerabilityFrame(opacity, delayTime, color));
    }
    
}
