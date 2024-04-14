using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

            print("Triggered Switch Tile");
        }
    }
}
