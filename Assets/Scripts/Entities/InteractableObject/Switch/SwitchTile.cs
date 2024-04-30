using System;
using System.Collections;
using System.Collections.Generic;
using Entities.InteractableObject;
using Entities.Monster;
using UnityEngine;

public class SwitchTile : MonoBehaviour
{
    public SwitchTileColor switchColor;
    private Material _material;
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchButton.Switch += OnSwitch;
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

    public void OnCollisionStay(Collision entity)
    {
        if (tag.Equals("DeactivatedTile") && entity.gameObject.tag.Equals("Monster"))
        {
            entity.gameObject.tag = "Falling";
        }
    }

    private void OnSwitch()
    {
        if (tag.Equals("ActivatedTile"))
        {
            tag = "DeactivatedTile";
            RenderOpacitySwitch(0.25f);
        }
        else if (tag.Equals("DeactivatedTile"))
        {
            tag = "ActivatedTile";
            RenderOpacitySwitch(1.0f);
        }
    }

    private void OnDestroy()
    {
        SwitchButton.Switch -= OnSwitch;
    }
}
