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
        
        if (!isCameraRotating && !GameManager.isPaused)
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
        else if (isCameraRotating)
        {
            
            transform.RotateAround(playableCharacter.transform.position, Vector3.up, rotateByDegrees * Time.deltaTime);
            degreesRotated += rotateByDegrees * Time.deltaTime;
            if (Math.Abs(degreesRotated) >= 90)
            {
                /*
                 * Ensures that after the camera rotation transition completed, the final rotation is divisible by 90.
                 * Otherwise, the camera can slowly get out of position if multiple camera rotations happen.
                 */
                transform.RotateAround(playableCharacter.transform.position, Vector3.up, rotateByDegrees - degreesRotated
                );
                
                cameraFollowScript.SetDistanceCameraToPlayer(transform.position - playableCharacter.transform.position);
                CameraRotationCompletion?.Invoke();
                characterMovement.SetIsCameraRotating(false);
                isCameraRotating = false;
            }
        }
    }

    private void InitiateCameraMotion(float deg)
    {
        isCameraRotating = true;
        characterMovement.SetIsCameraRotating(true);
        rotateByDegrees = deg;
        degreesRotated = 0;
    }
}
