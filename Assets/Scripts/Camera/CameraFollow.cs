using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject playableCharacter;

    public Vector3 cameraOffset;

    public Vector3 cameraRotation;

    private Movement player;

    private Vector3 distanceCameraToPlayer;

    private Vector3 targetPosition;
    private Vector3 velocity;

    private float cameraTimer;
    private float smoothCameraTransitionWaitTime;

    private bool enableCameraQERotation;
    
    // Start is called before the first frame update
    void Start()
    {
        smoothCameraTransitionWaitTime = 0.3f;
        velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(cameraRotation);
        player = playableCharacter.GetComponent<Movement>();
        enableCameraQERotation = true;
        transform.position = playableCharacter.transform.position + cameraOffset;
        distanceCameraToPlayer = cameraOffset;
        cameraTimer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTimer > 0)
        {
            cameraTimer -= Time.deltaTime;
        }

        if (!player.GetIsStandingStill())
        {
            player.SetCanRotate(false);
            enableCameraQERotation = false;
            SmoothCameraTransition(smoothCameraTransitionWaitTime);
        }
        else if (cameraTimer > 0)
        {
            SmoothCameraTransition(0f);
            player.SetCanRotate(true);
        }
        else
        {
            enableCameraQERotation = true;
        }
    }

    IEnumerator MoveCamera(float time)
    {
        cameraTimer = Math.Min(cameraTimer + time, smoothCameraTransitionWaitTime);
        targetPosition = playableCharacter.transform.position + distanceCameraToPlayer;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.17f);
        yield break;
    }
    
    private void SmoothCameraTransition(float time)
    {
        StartCoroutine(MoveCamera(time));
    }
    
    public void SetDistanceCameraToPlayer(Vector3 distanceVector)
    {
        distanceCameraToPlayer = distanceVector;
    }

    public bool IsCameraQERotationEnabled()
    {
        return enableCameraQERotation;
    }
}
