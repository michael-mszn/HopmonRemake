using System;
using System.Collections;
using System.Collections.Generic;
using Setup;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI crystalProgressText;
    public TextMeshProUGUI crystalCarriedText;
    public GameObject gameOverScreen;
    public static UIManager Instance;
    private List<IInitializedFlag> WaitForScriptsList;
    
    private bool hasFinishedLoading;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        WaitForScriptsList = new();
        hasFinishedLoading = false;
        gameOverScreen.SetActive(false);
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

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }
    
    public void InitializeUI()
    {
        UpdateHpText();
        UpdateCrystalCarriedText();
        UpdateCrystalsLeftText();
    }
}
