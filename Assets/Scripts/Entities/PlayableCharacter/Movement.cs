using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Movement : MonoBehaviour
{
    private bool canRotate = true;
    private Vector3 destination;
    private Vector3 attemptedMove;
    private bool isCameraMoving;
    private Camera _camera;

    private KeyCode forwardKeyCode;
    private KeyCode backKeyCode;
    private KeyCode leftKeyCode;
    private KeyCode rightKeyCode;
    
    // Start is called before the first frame update
    void Start()
    {
        isCameraMoving = false;
        _camera = Camera.main;

        forwardKeyCode = KeyCode.W;
        backKeyCode = KeyCode.S;
        leftKeyCode = KeyCode.A;
        rightKeyCode = KeyCode.D;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Tile based movement. Valid Movement Check is done by scanning all tile coordinates.
         * Not using collision detection or wall objects ensures ease of use in level design and better scalability
         */
        if (GetIsStandingStill() && !isCameraMoving && !PauseMenu.isPaused)
        {
            if (!canRotate)
            {
                canRotate = true;
            }
            
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
        else if(!isCameraMoving)
        {
            canRotate = false;
            transform.position = Vector3.MoveTowards(transform.position, destination,  CharacterManager.Instance.GetCurrentSpeed() * Time.deltaTime);
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
        
        //print("Invalid destination: x = " + Math.Floor(attemptedMoveCoordinates.x) + " | z = " + Math.Floor(attemptedMoveCoordinates.z));
        return false;
    }

    /*
     * What is understood as "forward", "back", "left", "right" changes when the camera view
     * gets rotated. Therefore, the interpretation of a keystroke has to update every single time
     * the camera is rotated so WASD-movement is according to expectations regardless of camera view.
     */
    public void AssignKeys()
    {
        if (Math.Round(_camera.transform.forward.z) == 1)
        {
            forwardKeyCode = KeyCode.W;
            backKeyCode = KeyCode.S;
            leftKeyCode = KeyCode.A;
            rightKeyCode = KeyCode.D;
        }
        
        if (Math.Round(_camera.transform.forward.z) == -1)
        {
            forwardKeyCode = KeyCode.S;
            backKeyCode = KeyCode.W;
            leftKeyCode = KeyCode.D;
            rightKeyCode = KeyCode.A;
        }
        
        if (Math.Round(_camera.transform.forward.x) == -1)
        {
            forwardKeyCode = KeyCode.D;
            backKeyCode = KeyCode.A;
            leftKeyCode = KeyCode.W;
            rightKeyCode = KeyCode.S;
        }
        
        if (Math.Round(_camera.transform.forward.x) == 1)
        {
            forwardKeyCode = KeyCode.A;
            backKeyCode = KeyCode.D;
            leftKeyCode = KeyCode.S;
            rightKeyCode = KeyCode.W;
        }
    }

    public bool GetCanRotate()
    {
        return canRotate;
    }

    public bool GetIsStandingStill()
    {
        return transform.position == destination;
    }

    public void SetIsCameraMoving(bool v)
    {
        isCameraMoving = v;
    }

    public void InitDestination()
    {
        destination = transform.position;
    }
}
