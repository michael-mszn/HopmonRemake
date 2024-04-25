using System.Collections.Generic;
using Setup;
using UnityEngine;

public class LevelManager : MonoBehaviour, IInitializedFlag
{
    public static LevelManager Instance;
    public static List<GameObject> AllTiles;
    private int crystalsInLevel;
    private int crystalsLeft;

    private bool isInitialized;
    
    private void Awake()
    {
        Instance = this;
        Crystal.CrystalSpawn += OnCrystalSpawn;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isInitialized = false;
        AllTiles = new();
        UpdateTileCoordinates("Tile", AllTiles);
        isInitialized = true;
    }

    public void UpdateTileCoordinates(string tileTag, List<GameObject> tileList)
    {
        GameObject map = GameObject.FindWithTag("Map");
        foreach (Transform child in map.transform)
        {
            /*
             * All objects that can be walked on have a tag that contains the word "Tile".
             */
            if (child.tag.Contains(tileTag))
            {
                tileList.Add(child.gameObject);
            }
        }
    }

    private void OnCrystalSpawn()
    {
        crystalsInLevel += 1;
        crystalsLeft += 1;
    }

    public int GetCrystalsInLevel()
    {
        return crystalsInLevel;
    }

    public int GetCrystalsLeft()
    {
        return crystalsLeft;
    }

    public void DecrementCrystalsLeft()
    {
        crystalsLeft -= 1;
    }
    
    public bool IsInitialized()
    {
        return isInitialized;
    }
    
    private void OnDestroy()
    {
        Crystal.CrystalSpawn -= OnCrystalSpawn;
    }
}
