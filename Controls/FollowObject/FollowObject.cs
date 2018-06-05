/**
 * Makes object follow another object while looking at it.
 * Useful as a top-down camera controller or following effects (like a cloud above the player etc).
 * Author: Ronen Ness.
 * Since: 2017.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NesScripts.Controls
{
    /// <summary>
    /// Make the object follow another object and look at it, with optional offsets and smooth movement.
    /// </summary>
    public class FollowObject : MonoBehaviour
    {
        /// <summary>
        /// Target to follow.
        /// </summary>
        public GameObject Target;

        /// <summary>
        /// Position offset to keep from target.
        /// </summary>
        public Vector3 PositionOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// Lookat offset from target (will look at target position + this offset).
        /// </summary>
        public Vector3 LookatOffset = new Vector3(0, 0, 0);

        /// <summary>
        /// If true, will rotate to lookat target (turn to false to disable rotation)
        /// </summary>
        public bool ControlRotation = true;

        /// <summary>
        /// If true, will move to target position (turn to false to disable movement)
        /// </summary>
        public bool ControlPosition = true;

        /// <summary>
        /// If value > 0f, will lerp toward target position with this factor as damping time.
        /// </summary>
        public float PositionDampingTime = 0.25f;

        /// <summary>
        /// If value > 0f, will lerp rotation to look at target with this factor as damping time.
        /// </summary>
        public float RotationDampingTime = 0.25f;

        /// <summary>
        /// If true, we will init offset and rotation from starting transform (will override PositionOffset).
        /// </summary>
        public bool SetOffsetFromTransform = true;

        // movement velocity for damping
        Vector3 moveVelocity = new Vector3(0, 0, 0);

        /// <summary>
        /// Initialize controller.
        /// </summary>
        void Start()
        {
            // init offset and lookat offset from starting transform
            if (SetOffsetFromTransform)
            {
                PositionOffset = transform.position - Target.transform.position;
            }
        }

        /// <summary>
        /// Follow target
        /// </summary>
        void FixedUpdate()
        {
            // control position
            if (ControlPosition)
            {
                // get target position
				var targetPos = Target.transform.position + PositionOffset;

                // move to target
                transform.position = PositionDampingTime > 0 ? 
                    Vector3.SmoothDamp(transform.position, targetPos, ref moveVelocity, PositionDampingTime) : 
                    targetPos;

                // to prevent shaking
                if ((transform.position - targetPos).magnitude < 0.05f)
                    transform.position = targetPos;
            }

            // control rotation
            if (ControlRotation)
            {
                // get target rotation
				var targetRotation =  Quaternion.LookRotation((Target.transform.position + LookatOffset) - transform.position );

                // rotate to look at target
                transform.rotation = RotationDampingTime > 0 ? 
                    Quaternion.Slerp(transform.rotation, targetRotation, (1f / RotationDampingTime) * Time.deltaTime) : 
                    targetRotation;
            }
        }
    }
}