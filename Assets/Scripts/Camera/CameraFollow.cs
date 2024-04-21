using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject playableCharacter;

    public Vector3 cameraOffset;

    public Vector3 cameraRotation;

    private Movement characterRotation;

    private Vector3 distanceCameraToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(cameraRotation);
        transform.position = playableCharacter.transform.position + cameraOffset;
        characterRotation = playableCharacter.GetComponent<Movement>();
        distanceCameraToPlayer = transform.position - playableCharacter.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * If the player can not change their rotation right now, then it means a movement command is being
         * processed right now. Therefore, the camera needs to adjust its position as long as the command is
         * still executing
         */ 
        if (!characterRotation.GetCanRotate())
        {
            MoveCamera();
        }
    }

    public void MoveCamera()
    {
        transform.position = playableCharacter.transform.position + distanceCameraToPlayer;
    }
    
    public void SetDistanceCameraToPlayer(Vector3 distanceVector)
    {
        distanceCameraToPlayer = distanceVector;
    }
}
