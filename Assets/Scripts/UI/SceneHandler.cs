using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static string selectedLevel;

    public GameObject levelSelectScreen;

    public GameObject mainMenuScreen;

    public GameObject levelButtonPrefab;

    public GameObject leftPageArrow;
    public GameObject rightPageArrow;
    
    
    private string[] allFiles;
    private List<GameObject> levelButtonsList;
    private float levelCount;
    private int pages;
    private int currentPage;
    
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "LevelScene")
        {
            levelButtonsList = new();
            CountLevels();
            DeterminePageCount();
            currentPage = 0;
            GenerateLevelUIElements();
            UpdatePageIcons();
            GetMainMenu();
        }
    }
    
    public void GetMainMenu()
    {
        levelSelectScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }
    
    public void GetLevelSelection()
    {
        mainMenuScreen.SetActive(false);
        levelSelectScreen.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    private void CountLevels()
    {
        allFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Levels/", "Level*.txt", SearchOption.AllDirectories);
        levelCount = allFiles.Length;
    }

    private void GenerateLevelUIElements()
    {
        ResetLevelUIElements();
        float xPosition = -730;
        float yPosition = 195;
        for (int i = currentPage*24+1; i <= currentPage*24+24; i++)
        {
            if (i <= levelCount)
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
        pages = (int) Math.Floor(levelCount / 24);
        pages = levelCount % 24 == 0 ? pages -= 1 : pages;   
    }
}
