using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Setup;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI crystalProgressText;
    public TextMeshProUGUI crystalCarriedText;
    public TextMeshProUGUI fireCooldownText;
    public GameObject gameOverScreen;
    public GameObject energyBar;
    public Color unloadedColor;
    public Color loadedColor;
    public static UIManager Instance;
    private List<IInitializedFlag> WaitForScriptsList;
    private List<Image> energyBarSquares;
    
    
    private bool hasFinishedLoading;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        WaitForScriptsList = new();
        energyBarSquares = new();
        hasFinishedLoading = false;
        gameOverScreen.SetActive(false);
        InitializeColors();
        WaitForScriptsList.Add(CharacterManager.Instance.GetComponent<CharacterManager>());
        WaitForScriptsList.Add(LevelManager.Instance.GetComponent<LevelManager>());
    }

    void Update()
    {
        /*
         * This can not be done in the Start() function of the UIManager due to a race condition,
         * so it needs to be invoked after all values have been initialized by scripts the UIManager
         * depends on.
         */
        if (!hasFinishedLoading)
        {
            foreach (IInitializedFlag script in WaitForScriptsList)
            {
                if (!script.IsInitialized())
                {
                    return;
                }
            }
            InitializeUI();
            hasFinishedLoading = true;
        }
    }
    
    public void UpdateHpText()
    {
        hpText.text = "" + CharacterManager.Instance.GetHp();
    }

    public void UpdateCrystalCarriedText()
    {
        crystalCarriedText.text = "" + CharacterManager.Instance.GetCrystalCarried();
    }

    public void UpdateCrystalsLeftText()
    {
        crystalProgressText.text =
            "" + (LevelManager.Instance.GetCrystalsInLevel() - LevelManager.Instance.GetCrystalsLeft()) + "/" +
            LevelManager.Instance.GetCrystalsInLevel();
    }

    public void UpdateFireCooldownText(float cooldown)
    {
        if (cooldown != 0)
        {
            fireCooldownText.text = "" + cooldown.ToString("#0.0", CultureInfo.InvariantCulture) + "s";
        }
        else
        {
            fireCooldownText.text = "READY";
        }
    }

    public void ShowGameOver()
    {
        PauseMenu.isPaused = true;
        gameOverScreen.SetActive(true);
    }
    
    public void InitializeUI()
    {
        UpdateHpText();
        UpdateCrystalCarriedText();
        UpdateCrystalsLeftText();
        UpdateFireCooldownText(0);
    }
    
    public void SetEnergySquareLoaded(int index)
    {
        energyBarSquares[index].color = loadedColor;
    }
    
    public void SetEnergySquareUnloaded(int index)
    {
        energyBarSquares[index].color = unloadedColor;
    }

    private void InitializeColors()
    {
        unloadedColor.a = 1;
        loadedColor.a = 1;
        
        foreach (Transform child in energyBar.transform)
        {
            Image energySquare = child.gameObject.GetComponent<Image>();
            energySquare.color = loadedColor;
            energyBarSquares.Add(energySquare);
        }
    }
    
}
