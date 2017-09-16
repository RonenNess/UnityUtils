/**
 * Rotate camera using the mouse, in an FPS-game style.
 *   
 * Author: Ronen Ness.
 * Since: 2017. 
*/
using UnityEngine;
using System.Collections;

namespace NesScripts.Controls
{
    /// <summary>
    /// Rotate the game object (assumed to hold the camera) based on mouse movement, in an FPS-game style.
    /// Note: attach this script to the object holding the camera, but NOT to the same object holding the character controller / FPS walker script.
    /// Best way to use this script is to attach it to a child of the FPS walker / character controller object, and set the CharacterControllerObj to point on it.
    /// Then attach the camera under this object.
    ///
    /// Use this script with the FPS walker to implement full FPS controls.
    /// </summary>
    public class FPSMouseLooking : MonoBehaviour
    {
        // mouse sensitivity factor
        public float mouseSensitivity = 175.0f;

        // clamp to min / max angle when looking up / down
        public float clampAngle = 85.0f;

        // delay factor to smooth rotation
        public float RotationSmoothingDelay = 0.075f;

        // current rotation
        private float rotY = 0.0f;
        private float rotX = 0.0f;

		// target rotation
		private float targetRotY = 0.0f;
		private float targetRotX = 0.0f;

        /// <summary>
        /// Should point on the GameObject that have the FPSWalker / Character Controller component, to rotate it around Y axis.
        /// </summary>
        public GameObject CharacterControllerObj = null;

        // velocity for smooth damping
        float velocityX = 0f;
        float velocityY = 0f;

        /// <summary>
        /// Init the controller.
        /// </summary>
        void Start()
        {
            // get starting rotation
            Vector3 rot = transform.localRotation.eulerAngles;
			rotY = targetRotY = rot.y;
			rotX = targetRotX = rot.x;

            // lock mouse to screen center
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        /// <summary>
        /// Lock / unlock cursor.
        /// This will hide the curser + lock it in the center.
        /// </summary>
        /// <param name="lockCurser">Should we lock cursor or not.</param>
        public void LockCursor(bool lockCurser)
        {
            Cursor.lockState = lockCurser ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !lockCurser;
        }

        /// <summary>
        /// Do rotation.
        /// </summary>
        void FixedUpdate()
        {
            // get mouse rotation
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

			// update target rotation x and y
			targetRotX += mouseY * mouseSensitivity * Time.deltaTime;
			targetRotY += mouseX * mouseSensitivity * Time.deltaTime;

            // rotate using smooth damp
			rotX = Mathf.SmoothDamp(rotX, targetRotX, ref velocityX, RotationSmoothingDelay);
			rotY = Mathf.SmoothDamp(rotY, targetRotY, ref velocityY, RotationSmoothingDelay);

            // clamp rotation on up / down angle
            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            // update transform
            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;

            // if we have character controller attached, update its rotation around y axis
            if (CharacterControllerObj)
            {
                CharacterControllerObj.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);
            }
        }
    }
}