using System;
using Entities.PlayableCharacter;
using UnityEngine;

public class Movement : Controls
{
    private bool canPlayerRotate;
    private Vector3 destination;
    private Vector3 attemptedMove;
    private bool isCameraRotating;

    
    // Start is called before the first frame update
    void Start()
    {
        canPlayerRotate = true;
        isCameraRotating = false;
        /*
         * Camera is not allowed by Unity to be instantiated inside an abstract class
         */
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Tile based movement. Valid Movement Check is done by scanning all tile coordinates.
         * Not using collision detection or wall objects ensures ease of use in level design and better scalability
         */
        if (GetIsStandingStill() && !isCameraRotating && !PauseMenu.isPaused)
        {
            if (Input.GetKey(forwardKeyCode))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 10);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            if (Input.GetKey(backKeyCode))
            {
                attemptedMove = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                if (IsAttemptedMoveValid(attemptedMove))
                { 
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 10);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }

            if (Input.GetKey(leftKeyCode))
            {
                attemptedMove = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }

            if (Input.GetKey(rightKeyCode))
            {
                attemptedMove = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                if (IsAttemptedMoveValid(attemptedMove))
                {
                    destination = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
        }
        else if(!isCameraRotating)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination,  CharacterManager.Instance.GetCurrentSpeed() * Time.deltaTime);
        }
    }

    private bool IsAttemptedMoveValid(Vector3 attemptedMoveCoordinates)
    {
        foreach(GameObject tile in LevelManager.AllTiles)
        {
            if (!Enum.IsDefined(typeof(TilesToIgnore), tile.gameObject.tag))
            {
                if (Math.Floor(tile.transform.position.x) == Math.Floor(attemptedMoveCoordinates.x) &&
                    Math.Floor(tile.transform.position.z) == Math.Floor(attemptedMoveCoordinates.z))
                {
                    //print("Moving to tile: X = " + Math.Floor(tile.transform.position.x) + " | z = " + Math.Floor(tile.transform.position.z));
                    return true;
                }
            }
        }
        
        //print("Invalid destination: x = " + Math.Floor(attemptedMoveCoordinates.x) + " | z = " + Math.Floor(attemptedMoveCoordinates.z));
        return false;
    }
    
    public bool GetCanPlayerRotate()
    {
        return canPlayerRotate;
    }

    public void SetCanRotate(bool v)
    {
        canPlayerRotate = v;
    }
    

    public bool GetIsStandingStill()
    {
        return transform.position == destination;
    }

    public void SetIsCameraMoving(bool v)
    {
        isCameraRotating = v;
    }

    public void InitDestination()
    {
        destination = transform.position;
    }
}
