using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private List<GameObject> allTiles = new();
    
    private void Awake()
    {
        Instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTileCoordinates()
    {
        GameObject map = GameObject.FindWithTag("Map");
        foreach (Transform child in map.transform)
        {
            if (child.tag == "Tile")
            {
                allTiles.Add(child.gameObject);
                print("Added Tile");
            }
        }
    }

    public List<GameObject> getAllTiles()
    {
        if (!allTiles.Any())
        {
            LoadTileCoordinates();
        }
        
        return allTiles;
    }
}
