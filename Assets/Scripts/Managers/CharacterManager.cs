using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Persistence;
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
    public float currentSpeed;
    [Header("Do not add a prefab of the character but an instantiated object")]
    public GameObject character;
    
    private float invulnerabilityTimer;
    private Transform baseTile;
    private float lowestSpeedLimit;
    private int hp;
    private int crystalCarried;
    private Material characterMaterial;
    private List<Vector3> playerTileLog;

    private bool isInitialized;

    private void Awake()
    {
            Instance = this;
            Movement.PlayerMovement += OnPlayerMovement;
    }

    // Start is called before the first frame update
    void Start()
    {
        isInitialized = false;
        playerTileLog = new();
        lowestSpeedLimit = maximumSpeed * lowestSpeedPercentage;
        currentSpeed = maximumSpeed;
        hp = 2;
        crystalCarried = 0;
        invulnerabilityTimer = 0;
        characterMaterial = character.GetComponent<Renderer>().material;
        isInitialized = true;
    }
    
    void Update()
    {
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
            DoInvulnerabilityFrameFlickering();
        }
    }
    
    public void AddCrystal()
    {
        crystalCarried += 1;
        UIManager.Instance.UpdateCrystalCarriedText();
        if (currentSpeed >= lowestSpeedLimit)
        {
            currentSpeed -= maximumSpeed*0.075f;
        }
    }

    public void IncreaseHP()
    {
        if (hp != 9)
        {
            hp += 1;
            UIManager.Instance.UpdateHpText();
        }
    }

    public void SecureCrystal()
    {
        while (crystalCarried != 0)
        {
            crystalCarried -= 1;
            LevelManager.Instance.DecrementCrystalsLeft();
            UIManager.Instance.UpdateCrystalCarriedText();
            UIManager.Instance.UpdateCrystalsLeftText();
        }
        currentSpeed = maximumSpeed;
        if (LevelManager.Instance.GetCrystalsLeft() == 0)
        {
            SaveProgress();
        }
    }

    private void SaveProgress()
    {
        LevelData solvedLevel = SceneHandler.levelData.FirstOrDefault(ld => string.Equals(""+ld.GetLevelNumber(), SceneHandler.selectedLevel));
        if (solvedLevel != null)
        {
            solvedLevel.SetHasSolved(true);
        }
        PersistPlayerData.SaveProgress(SceneHandler.levelData, Int32.Parse(SceneHandler.selectedLevel)+1);
        SceneHandler.selectedLevel = ""+(Int32.Parse(SceneHandler.selectedLevel)+1);
        UIManager.Instance.ShowLevelCleared();
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

    public void TakeDamage(int damage)
    {
        if (!PauseMenu.isPaused)
        {
            if (invulnerabilityTimer <= 0)
            {
                hp -= damage;
                UIManager.Instance.UpdateHpText();
                if (hp <= 0)
                {
                    hp = 0;
                    UIManager.Instance.UpdateHpText();
                    UIManager.Instance.ShowGameOver();
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
    
    /*
     * Player coordinates are saved with y = 0 because the y-level is trivial for equal position
     * comparisons.  
     */
    private void OnPlayerMovement()
    {
        Vector3 playerPosition = new Vector3((float)Math.Floor(character.transform.position.x), 0,
            (float)Math.Floor(character.transform.position.z));
        playerTileLog.Add(playerPosition);
    }

    private void OnDestroy()
    {
        Movement.PlayerMovement -= OnPlayerMovement;   
    }

    public List<Vector3> GetPlayerTileLog()
    {
        return playerTileLog;
    }
    
}
