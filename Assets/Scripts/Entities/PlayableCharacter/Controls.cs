using System;
using UnityEngine;

namespace Entities.PlayableCharacter
{
    /*
     * Both player movement and rotation controls need a common understanding of what KeyCode
     * represents what direction, therefore they inherit this class.
     */
    public abstract class Controls : MonoBehaviour
    {
        protected KeyCode forwardKeyCode;
        protected KeyCode backKeyCode;
        protected KeyCode leftKeyCode;
        protected KeyCode rightKeyCode;
        
        protected Camera _camera;

        void Awake()
        {
            RotateCamera.CameraRotationCompletion += OnCameraRotationCompletion;
            forwardKeyCode = KeyCode.W;
            backKeyCode = KeyCode.S;
            leftKeyCode = KeyCode.A;
            rightKeyCode = KeyCode.D;
        }

        private void OnCameraRotationCompletion()
        {
            AssignKeys();
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
        
        private void OnDestroy()
        {
            RotateCamera.CameraRotationCompletion -= OnCameraRotationCompletion; 
        }
    }
}