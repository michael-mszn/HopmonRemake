using System;
using System.Collections.Generic;
using System.IO;
using Persistence;
using TMPro;
using UI;
using UnityEngine;


//todo: Refactor some game flow logic into a game manager
public class MainMenu : MonoBehaviour
{
    public static string selectedLevel;
    public static List<LevelData> levelData;
    public static int highestLevelUnlocked;
    private PlayerData playerData;

    public GameObject levelSelectScreen;
    public GameObject mainMenuScreen;
    public GameObject levelButtonPrefab;
    public GameObject leftPageArrow;
    public GameObject rightPageArrow;
    
    private string[] allFiles;
    private List<GameObject> levelButtonsList;
    private int levelCount;
    private int pages;
    private int currentPage;

        
    void Start() 
    { 
        levelButtonsList = new();
        CountLevels();
        DeterminePageCount();
        currentPage = 0;
        UpdatePageIcons();
        ShowMainMenu();
        playerData = PersistPlayerData.LoadPlayer();
                
        /*
         * If playerdata.hop has not been created and initialized yet
         */
        if (playerData is null)
        {
            playerData = InitPlayerProfile();
        }
                
        /*
        * If a developer or player added new levels in StreamableAssets
        */
        else if (playerData.GetLevelData().Count < levelCount)
        {
            for (int i = levelCount - playerData.GetLevelData().Count; i > 0; i--)
            {
                playerData.GetLevelData().Add(new LevelData(playerData.GetLevelData().Count+i, false));
            }
            PersistPlayerData.SaveProgress(playerData.GetLevelData(), playerData.GetHighestLevelUnlocked());
        }
        levelData = playerData.GetLevelData();
        highestLevelUnlocked = playerData.GetHighestLevelUnlocked();
        GenerateLevelUIElements();
    }
    
    private PlayerData InitPlayerProfile()
    {               
        levelData = new();
        InitLevelData();
        PersistPlayerData.SaveProgress(levelData, highestLevelUnlocked);
        PlayerData pd = PersistPlayerData.LoadPlayer();
        return pd;

    }
    
    private void InitLevelData()
    {
        for(int i = 1; i <= levelCount; i++)
        {
            LevelData ld = new LevelData(i, false);
            levelData.Add(ld);
        }

        highestLevelUnlocked = 1;
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
    
       private void CountLevels()
    {
        allFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Levels/", "Level*.txt", SearchOption.AllDirectories);
        levelCount = allFiles.Length;
    }

    private void GenerateLevelUIElements()
    {
        ResetLevelUIElements();
        float xPosition = -730;
        float yPosition = -280;
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

                if (i < highestLevelUnlocked)
                {
                    button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.green;
                }
                else if (i == highestLevelUnlocked)
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
        pages = (int) Math.Floor((float)levelCount / 24);
        pages = levelCount % 24 == 0 ? pages -= 1 : pages;
    }
        
}
