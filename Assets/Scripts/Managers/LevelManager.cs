using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static List<GameObject> AllTiles = new();
    public int crystalsInLevel;
    public int crystalsLeft;
    
    private void Awake()
    {
        Instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTileCoordinates();
        CountCrystalsInLevel();
        crystalsLeft = crystalsInLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTileCoordinates()
    {
        GameObject map = GameObject.FindWithTag("Map");
        foreach (Transform child in map.transform)
        {
            /*
             * All objects that can be walked on have a tag that contains the word "Tile".
             */
            if (child.tag.Contains("Tile"))
            {
                AllTiles.Add(child.gameObject);
                print("Added Tile");
            }
        }
    }

    public void CountCrystalsInLevel()
    {
        GameObject map = GameObject.FindWithTag("Map");
        foreach (Transform child in map.transform)
        {
            if (child.tag.Contains("Crystal"))
            {
                crystalsInLevel += 1;
            }
        }
    }
    
}
