using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI crystalProgressText;
    public TextMeshProUGUI crystalCarriedText;
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

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

    /*
     * This can not be done in the Start() function of the UIManager due to a race condition,
     * so it needs to be invoked in the ChracterManager Start() function after all values have been
     * initialized.
     */
    public void InitializeUI()
    {
        UpdateHpText();
        UpdateCrystalCarriedText();
        UpdateCrystalsLeftText();
    }
}
