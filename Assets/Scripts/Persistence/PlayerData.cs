using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Persistence;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public List<LevelData> levelData;
    private int highestLevelUnlocked;

    public PlayerData(List<LevelData> levelData, int highestLevelUnlocked)
    {
        this.levelData = levelData;
        this.highestLevelUnlocked = highestLevelUnlocked;
    }

    public List<LevelData> GetLevelData()
    {
        return levelData;
    }

    public void SetLevelCleared(int levelIndex)
    {
        LevelData entry = levelData.Find(ld => ld.GetLevelNumber() == levelIndex);
        entry.SetHasSolved(true);
    }
    
    public int GetHighestLevelUnlocked()
    {
        return highestLevelUnlocked;
    }

    public void SetHighestLevelUnlocked(int levelNumber)
    {
        highestLevelUnlocked = levelNumber;
    }
}
