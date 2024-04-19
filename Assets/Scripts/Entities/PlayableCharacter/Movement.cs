using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

public class Movement : MonoBehaviour
{
    private CharacterManager characterManager;
    private bool canRotate = true;
    private Vector3 destination;
    private Vector3 attemptedMove;
    
    // Start is called before the first frame update
    void Start()
    {
        characterManager = CharacterManager.Instance;
        characterManager.SetSpawnPoint(gameObject);
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Tile based movement. Valid Movement Check is done by scanning all tile coordinates.
         * Not using collision detection or wall objects ensures ease of use in level design and better scalability
         */
        if (transform.position == destination)
        {
            if (!canRotate)
            {
                canRotate = true;
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                if (IsAttemptedMoveValid(attemptedMove))
                { 
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                attemptedMove = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                attemptedMove = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
        else
        {
            canRotate = false;
            transform.position = Vector3.MoveTowards(transform.position, destination,  characterManager.GetCurrentSpeed() * Time.deltaTime);
        }
    }

    private bool IsAttemptedMoveValid(Vector3 attemptedMoveCoordinates)
    {
        foreach(GameObject tile in LevelManager.AllTiles)
        {
            foreach (string blacklistedTile in Enum.GetNames(typeof(TilesToIgnore)))
            {
                if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMoveCoordinates.x) && 
                    Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMoveCoordinates.z) && !tile.tag.Equals(blacklistedTile))
                {
                    //print("Moving to tile: X = " + Math.Floor(tile.transform.position.x) + " | z = " + Math.Floor(tile.transform.position.z));
                    return true;
                }
            }
        }
        
        print("Invalid destination: x = " + Math.Floor(attemptedMoveCoordinates.x) + " | z = " + Math.Floor(attemptedMoveCoordinates.z));
        return false;
    }

    public bool GetCanRotate()
    {
        return canRotate;
    }
}
