using System;
using System.Collections.Generic;
using System.IO;
using Persistence;
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
    
    private List<GameObject> levelButtonsList;
    private int pages;
    private int currentPage;

        
    void Start() 
    { 
        levelButtonsList = new();
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
        float xPosition = -730;
        float yPosition = -280;
        for (int i = currentPage*24+1; i <= currentPage*24+24; i++)
        {
            if (i <= GameManager.Instance.levelCount)
            {
                GameObject button = Instantiate(levelButtonPrefab, new Vector3(xPosition, yPosition, 0),
                    Quaternion.identity);
                button.gameObject.GetComponent<LevelButton>().SetLevelText("" + i);
                xPosition += 200;
                if (xPosition >= 800)
                {
                    xPosition = -730;
                    yPosition -= 200;
                }

                if (i < GameManager.highestLevelUnlocked)
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
                }
                else if (i == GameManager.highestLevelUnlocked)
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
                }

                button.transform.SetParent(levelSelectScreen.transform, false);
                levelButtonsList.Add(button);
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

    /*
     * Todo: Not performant, use object pooling
     */
    private void ResetLevelUIElements()
    {
        foreach (var UIElement in levelButtonsList)
        {
            Destroy(UIElement);
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
