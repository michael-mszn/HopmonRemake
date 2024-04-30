using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{

    public static event Action Switch;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    /*
     *todo:  Very expensive (O(n) and n times GetComponent calls).
     * Refactor to use Events
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Character"))
        {
            foreach (GameObject tile in LevelManager.AllTiles)
            {
                if (tile.gameObject.tag.Equals("ActivatedTile"))
                {
                    tile.tag = "DeactivatedTile";
                    tile.GetComponent<SwitchTile>().RenderOpacitySwitch(0.25f);
                }
                else if (tile.gameObject.tag.Equals("DeactivatedTile"))
                {
                    tile.tag = "ActivatedTile";
                    tile.GetComponent<SwitchTile>().RenderOpacitySwitch(1.0f);
                }
            }
            Switch?.Invoke();
            print("Triggered Switch Tile");
        }
    }
}
