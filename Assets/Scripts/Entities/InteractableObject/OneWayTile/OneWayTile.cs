using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTile : MonoBehaviour
{
    public GameObject lowerBlock;
    public GameObject upperBlock;

    public float fallSpeed;
    
    private Vector3 fallPosition;
    private bool hasLeftTile;

    private bool hasFinishedCollapsing;
    // Start is called before the first frame update
    void Start()
    {
        hasLeftTile = false;
        hasFinishedCollapsing = false;
        fallPosition = new Vector3(transform.position.x, transform.position.y + 8, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLeftTile)
        {
            if (upperBlock.transform.position.y!= fallPosition.y)
            {
                BecomeWall();
            }
            else if (!hasFinishedCollapsing)
            {
                ChangeColor(upperBlock);
                ChangeColor(lowerBlock);
                hasFinishedCollapsing = true;
                //print("Has collapsed");
            }
        }
    }
    
    public void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag.Equals("Character"))
        {
            hasLeftTile = true;
            LevelManager.AllTiles.Remove(gameObject);
            tag = "Wall";
        }
    }

    private void BecomeWall()
    {
        upperBlock.transform.position = Vector3.MoveTowards(upperBlock.transform.position, fallPosition,  fallSpeed * Time.deltaTime);
        //Smooth Collapse Animation
        fallSpeed *= 0.9955f;
    }

    private void ChangeColor(GameObject block)
    {
        /*
         *  Non-Problematic GetComponent<>() call as it only gets invoked once in the Update function.
         *  Preferably done here without caching to keep namespace tidy as there is no performance gain
         */
        Material _material = block.GetComponent<Renderer>().material;
        _material.SetColor("_Color", Color.white);
    }
}
