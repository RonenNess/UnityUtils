/**
 * Flying camera for levels editor.
 * See origin and credits in class top comment.
 *   
 * Author: Ronen Ness.
 * Since: 2017. 
*/
using UnityEngine;
using System.Collections;

namespace NesScripts.Controls.GameEditor
{
    /// <summary>
    /// Fly camera controller for editor-like apps.
    /// Attach this script to the GameObject holding the main camera.
    /// </summary>
    public class FlyCameraController : MonoBehaviour
    {
        /**
         * Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.
         * Converted to C# 27-02-13 - no credit wanted.
         * Added resetRotation, RF control, improved initial mouse position, 2015-03-11 - Roi Danton.
         * Remaded camera rotation - now cursor is locked, added "Walker Mode", 25-09-15 - LookForward.
         * Simple flycam I made, since I couldn't find any others made public.
         * Made simple to use (drag and drop, done) for regular keyboard layout
         * wasdrf : Basic movement
         * shift : Makes camera accelerate
         * space : Moves camera on X and Z axis only.  So camera doesn't gain any height
         * q : Change mode
         */

        // Mouse rotation sensitivity.
        public float mouseSensitivity = 5.0f;

        // Regular speed.
        public float speed = 10.0f;

        // Gravity force.
        public float gravity = 20.0f;

        // Multiplied by how long shift is held.  Basically running.
        public float shiftAdd = 25.0f;

        // Maximum speed when holding shift.
        public float maxShift = 100.0f;

		// Which mouse key invoke camera rotation (0 = left, 1 = right)
		public int MouseKeyForRotation = 1;

        // total move, rotation y, and limit vertical rotation.
        private float totalRun = 1.0f;
        private float rotationY = 0.0f;
        private float maximumY = 90.0f;
        private float minimumY = -90.0f;

        /// <summary>
        /// Do camera controls.
        /// </summary>
        void FixedUpdate()
        {
            // rotate camera
			if (Input.GetMouseButton(MouseKeyForRotation))
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
                rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0.0f);
            }

            // get movement direction
			Vector3 p = getMovementDirection();

			// if shift is held down, move faster
			if (shiftAdd != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
			// else, fly at normal speed
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1.0f, 1000.0f);
                p = p * speed;
            }

			// now actually set new position
            p = p * Time.deltaTime;
            transform.Translate(p);
        }

        /// <summary>
        /// Get movement direction.
        /// </summary>
        private Vector3 getMovementDirection()
        {
			// get direction from input
            Vector3 p_Velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            // Strifing
            if (Input.GetKey(KeyCode.F))
            {
                p_Velocity += new Vector3(0.0f, -1.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.R))
            {
                p_Velocity += new Vector3(0.0f, 1.0f, 0.0f);
            }
            
			// return movement direction
            return p_Velocity;
        }

        /// <summary>
        /// Reset camera rotation to a given look-at point.
        /// </summary>
        public void resetRotation(Vector3 lookAt)
        {
            transform.LookAt(lookAt);
        }
    }
}