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
        // target to follow.
        public GameObject Target;

        // position offset from target.
        public Vector3 PositionOffset = new Vector3(0, 0, 0);

        // lookat offset from target.
        public Vector3 LookatOffset = new Vector3(0, 0, 0);

        // if true, will rotate to lookat target (turn to false to disable rotation)
        public bool ControlRotation = true;

        // if true, will move to target position (turn to false to disable movement)
        public bool ControlPosition = true;

        // if > 0.0f, will follow / lookat target with damping, eg smoothly
        public float DampingTime = 0.25f;

        // movement velocity for damping
        Vector3 moveVelocity = new Vector3(0, 0, 0);

        // update the follower
        void Update()
        {
            // control position
            if (ControlPosition)
            {
                // get target position
				var targetPos = Target.transform.position + PositionOffset;

                // move to target
                transform.position = DampingTime > 0 ? Vector3.SmoothDamp(transform.position, targetPos, ref moveVelocity, DampingTime) : targetPos;
            }

            // control rotation
            if (ControlRotation)
            {
                // get target rotation
				var targetRotation =  Quaternion.LookRotation((Target.transform.position + LookatOffset) - transform.position );

                // rotate to look at target
                transform.rotation = DampingTime > 0 ? Quaternion.Slerp(transform.rotation, targetRotation, (1f / DampingTime) * Time.deltaTime) : targetRotation;
            }
        }
    }
}