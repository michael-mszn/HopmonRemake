using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static List<GameObject> AllTiles = new();
    
    private void Awake()
    {
        Instance = this;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTileCoordinates();
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
}
