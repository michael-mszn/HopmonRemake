using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public static event Action CameraRotationCompletion;
    public GameObject playableCharacter;
    private Movement characterMovement;
    private CameraFollow cameraFollowScript;
    private bool isCameraRotating;
    private float rotateByDegrees;
    private float degreesRotated;
    /*
     * Unfortunately, transform.rotation for the Camera is always reset to 0 by the
     * Unity engine once a change is completed, so it needs to be tracked separately.
     */
    private float previousRotationY;
    
    // Start is called before the first frame update
    void Start()
    {
        characterMovement = playableCharacter.GetComponent<Movement>();
        cameraFollowScript = GetComponent<CameraFollow>();
        isCameraRotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraFollowScript.IsCameraQERotationEnabled())
        {
            return;
        }
        
        if (characterMovement.GetIsStandingStill() && !isCameraRotating && !PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                InitiateCameraMotion(90);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                InitiateCameraMotion(-90);
            }
        }
        else if (characterMovement.GetIsStandingStill() && isCameraRotating)
        {
            
            transform.RotateAround(playableCharacter.transform.position, Vector3.up, rotateByDegrees * Time.deltaTime);
            degreesRotated += Math.Abs(rotateByDegrees) * Time.deltaTime;
            if (degreesRotated >= 90)
            {
                /*
                 * Ensures that after the camera rotation transition completed, the final rotation is divisible by 90.
                 * Otherwise, the camera can slowly get out of position if multiple camera rotations happen.
                 */
            
                transform.rotation = Quaternion.Euler(cameraFollowScript.cameraRotation.x, previousRotationY + rotateByDegrees, cameraFollowScript.cameraRotation.z);

                cameraFollowScript.SetDistanceCameraToPlayer(transform.position - playableCharacter.transform.position);
                CameraRotationCompletion?.Invoke();
                characterMovement.SetIsCameraMoving(false);
                isCameraRotating = false;
                previousRotationY += rotateByDegrees;
            }
        }
    }

    private void InitiateCameraMotion(float deg)
    {
        isCameraRotating = true;
        characterMovement.SetIsCameraMoving(true);
        rotateByDegrees = deg;
        degreesRotated = 0;
    }
}
