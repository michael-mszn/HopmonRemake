using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    private CharacterManager characterManager;

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
            if (Input.GetKey(KeyCode.W))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                if (IsAttemptedMoveValid(attemptedMove))
                { 
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                attemptedMove = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                attemptedMove = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination,  characterManager.getSpeed() * Time.deltaTime);
        }
    }

    private bool IsAttemptedMoveValid(Vector3 attemptedMoveCoordinates)
    {
        foreach(GameObject tile in LevelManager.AllTiles)
        {
            if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMoveCoordinates.x) && 
                Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMoveCoordinates.z) && tile.tag != "DeactivatedTile")
            {
                //print("Moving to tile: X = " + Math.Floor(tile.transform.position.x) + " | z = " + Math.Floor(tile.transform.position.z));
                return true;
            }
        }
        
        //print("Invalid destination: x = " + Math.Floor(attemptedMoveCoordinates.x) + " | z = " + Math.Floor(attemptedMoveCoordinates.z));
        return false;
    }
    
}
