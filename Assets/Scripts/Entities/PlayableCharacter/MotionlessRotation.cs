using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A rotation is sometimes necessary despite no movement being executed
 * (i.e. when the player tries to move into a wall).
 * This file is responsible for handling this case.
 */
public class MotionlessRotation : MonoBehaviour
{
        private Movement characterRotation;  
        
    // Start is called before the first frame update
    void Start()
    {
            /*
             * Caching reference to character's rotation property for performance
             */
            characterRotation = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
            if (characterRotation.GetCanRotate())
            {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                    }

                    if (Input.GetKeyDown(KeyCode.S))
                    {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                            transform.rotation = Quaternion.Euler(0, -90, 0);
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                            transform.rotation = Quaternion.Euler(0, 90, 0);
                    }
            }
    }
}
