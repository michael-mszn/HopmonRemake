using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Persistence;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public static string selectedLevel;
    public static List<LevelData> levelData;
    public static int highestLevelUnlocked;
    public static bool isPaused;
    
    public GameObject player;
    
    [HideInInspector]
    public int levelCount;
    
    private PlayerData playerData;

    private void Awake()
    {
        Instance = this;
        
        CountLevels();
        playerData = SaveFile.LoadPlayer();
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
        bool newLevelsAdded = playerData.GetLevelData().Count < levelCount;
        if (newLevelsAdded)
        {
            WriteNewlyAddedLevelsToSaveFile();
        }
        levelData = playerData.GetLevelData();
        highestLevelUnlocked = playerData.GetHighestLevelUnlocked();
    }

    void Start()
    {
        isPaused = false;
    }
    
    public void SaveProgress()
    {
        if (player is null)
        {
            return;
        }
        player.SetActive(false);
        LevelData solvedLevel = levelData.FirstOrDefault(ld => string.Equals(""+ld.GetLevelNumber(), selectedLevel));
        if (solvedLevel != null)
        {
            solvedLevel.SetHasSolved(true);
        }

        if (highestLevelUnlocked <= levelData.Count)
        {
            SaveFile.SaveProgress(levelData, Int32.Parse(selectedLevel)+1);
        }

        selectedLevel = ""+(Int32.Parse(selectedLevel)+1);
        UIManager.Instance.ShowLevelCleared();
    }
    
    private PlayerData InitPlayerProfile()
    {               
        levelData = new();
        InitLevelData();
        SaveFile.SaveProgress(levelData, highestLevelUnlocked);
        PlayerData pd = SaveFile.LoadPlayer();
        return pd;

    }

    private void WriteNewlyAddedLevelsToSaveFile()
    {
        for (int i = levelCount - playerData.GetLevelData().Count; i > 0; i--)
        {
            playerData.GetLevelData().Add(new LevelData(playerData.GetLevelData().Count+i, false));
        }
        SaveFile.SaveProgress(playerData.GetLevelData(), playerData.GetHighestLevelUnlocked());
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
    
    private void CountLevels()
    {
        string[] allFiles = Directory.GetFiles(Application.streamingAssetsPath + "/Levels/", "Level*.txt", SearchOption.AllDirectories);
        levelCount = allFiles.Length;
        //For pager test purposes: levelCount = 50;
    }
}
