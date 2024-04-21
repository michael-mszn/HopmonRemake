using System;
using System.Collections;
using System.Collections.Generic;
using Entities.InteractableObject;
using UnityEngine;

public class SwitchTile : MonoBehaviour
{
    public SwitchTileColor switchColor;
    private Material _material;
    
    // Start is called before the first frame update
    void Start()
    {
            _material = gameObject.GetComponent<Renderer>().material;
            if (switchColor == SwitchTileColor.ORANGE)
            {
                gameObject.tag = "ActivatedTile";
                RenderOpacitySwitch(1.0f);
            }
            else
            {
                gameObject.tag = "DeactivatedTile";
                RenderOpacitySwitch(0.25f);
            }
        
    }

    public void RenderOpacitySwitch(float opacity)
    {
        Color currentColor = _material.color;
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, opacity);
        _material.SetColor("_Color", newColor);
    }
    
}
