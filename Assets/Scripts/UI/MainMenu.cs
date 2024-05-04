using System;
using System.Collections.Generic;
using System.IO;
using Persistence;
using Setup;
using TMPro;
using UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectScreen;
    public GameObject mainMenuScreen;
    public GameObject levelButtonPrefab;
    public GameObject leftPageArrow;
    public GameObject rightPageArrow;

    private ObjectPool levelUIElementPool;
    private int pages;
    private int currentPage;
        
    void Start() 
    { 
        levelUIElementPool = new ObjectPool(levelButtonPrefab, 30);
        DeterminePageCount();
        currentPage = 0;
        UpdatePageIcons();
        ShowMainMenu();
        GenerateLevelUIElements();
    }
    
    public void ShowMainMenu()
    {
        levelSelectScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    
    public void ShowLevelSelection()
    {
        mainMenuScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    private void GenerateLevelUIElements()
    {
        ResetLevelUIElements();
        float xStartPosition = 225;
        float yStartPosition = 785;
        float xPosition = xStartPosition;
        float yPosition = yStartPosition;
        for (int i = currentPage*24+1; i <= currentPage*24+24; i++)
        {
            if (i <= GameManager.Instance.levelCount)
            {
                GameObject button = levelUIElementPool.GetPoolItem();
                button.transform.SetParent(levelSelectScreen.transform, false);
                Vector3 screenPosition = new Vector3(xPosition, yPosition, 0);
                button.transform.position = screenPosition;
                button.GetComponent<LevelButton>().SetLevelText("" + i);
                xPosition += 200;
                if (xPosition >= 1755)
                {
                    xPosition = xStartPosition;
                    yPosition -= 200;
                }

                if (i < GameManager.highestLevelUnlocked)
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(88, 70, 245, 255);
                }
                else if (i == GameManager.highestLevelUnlocked)
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                }
                else
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
                }
            }
        }
    }
    
    public void GetNextLeftPage()
    {
        currentPage -= 1;
        GenerateLevelUIElements();
        UpdatePageIcons();
    }
    
    public void GetNextRightPage()
    {
        currentPage += 1;
        GenerateLevelUIElements();
        UpdatePageIcons();
    }

    /*
     * When player clicks to load a new page
     */
    private void UpdatePageIcons()
    {
        if (currentPage != 0)
            leftPageArrow.SetActive(true);
        else leftPageArrow.SetActive(false);
        
        if (currentPage != pages)
            rightPageArrow.SetActive(true);
        else rightPageArrow.SetActive(false);
    }
    
    private void ResetLevelUIElements()
    {
        foreach (var UIElement in levelUIElementPool.GetPool())
        {
            UIElement.SetActive(false);
        }
    }

    /*
     * If there are no items to expect on the next page, then the
     * GetRightPage UI Element should not be loaded. This happens
     * when the levelCount is dividable by 24.
     */
    private void DeterminePageCount()
    {
        pages = (int) Math.Floor((float)GameManager.Instance.levelCount / 24);
        pages = GameManager.Instance.levelCount % 24 == 0 ? pages -= 1 : pages;
    }
}
