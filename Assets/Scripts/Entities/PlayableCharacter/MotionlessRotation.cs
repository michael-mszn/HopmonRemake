using Entities.PlayableCharacter;
using UnityEngine;

/*
 * A rotation is sometimes necessary despite no movement being executed
 * (i.e. when the player tries to move into a wall).
 * This file is responsible for handling this case.
 */
public class MotionlessRotation : Controls
{
        private Movement characterRotation;
        
    // Start is called before the first frame update
    void Start()
    {
            characterRotation = gameObject.GetComponent<Movement>();
            /*
             * Camera is not allowed by Unity to be instantiated inside an abstract class
             */
            _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
            if (characterRotation.GetCanPlayerRotate())
            {
                    if (Input.GetKey(forwardKeyCode))
                    {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                    }

                    if (Input.GetKey(backKeyCode))
                    {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                    }

                    if (Input.GetKey(leftKeyCode))
                    {
                            transform.rotation = Quaternion.Euler(0, -90, 0);
                    }

                    if (Input.GetKey(rightKeyCode))
                    {
                            transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
            }
    }
}
