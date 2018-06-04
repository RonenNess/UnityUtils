/**
 * Basic FPS-like walking, running, and jumping using Rigid Body (instead of controller).
 * This is just a modified version of the following sources:
 *   - http://wiki.unity3d.com/index.php/RigidbodyFPSWalker
 *   
 * Author: Ronen Ness.
 * Since: 2018. 
*/
using UnityEngine;
using System.Collections;

namespace NesScripts.Controls
{

	/// <summary>
	/// An FPS walker to work with Rigid Body.
	/// </summary>
	public class FPSRigidWalker : MonoBehaviour
    {
        /// <summary>
        /// Player walking speed.
        /// </summary>
		public float WalkSpeed = 10.0f;

        /// <summary>
        /// Max movement speed.
        /// </summary>
		public float MaxVelocityChange = 10.0f;

        /// <summary>
        /// Can this player jump?
        /// </summary>
		public bool CanJump = true;

        /// <summary>
        /// Jumping force.
        /// </summary>
        public float JumpForce = 5.0f;

        // the rigid body we control
        private Rigidbody rigidbody;

        // distance from ground to calculate if we are touching ground or in air.
        private float distToGround;

        // time passed since we last jumped, to prevent multi-jumping while holding jump button.
        private float timeSinceLastJump;

        // are we walking forward due to someone calling Walk from outside?
        // this value represent the time we need to walk forward
        float forceWalkingForward = 0f;

        /// <summary>
        /// Init on start
        /// </summary>
        public void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.freezeRotation = true;
            distToGround = GetComponent<Collider>().bounds.extents.y;
            timeSinceLastJump = 1f;
        }

        /// <summary>
        /// Do physics and movement
        /// </summary>
        void FixedUpdate ()
        {

            // calculate moving velocity
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (forceWalkingForward > 0f)
            {
                targetVelocity.z = 0.1f;
                forceWalkingForward -= Time.deltaTime;
            } 
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity.y = 0;
            targetVelocity *= WalkSpeed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // increase time since last jump
            if (timeSinceLastJump < 1000f)
                timeSinceLastJump += Time.deltaTime;

            // jump
            if (CanJump && Input.GetButton("Jump") && IsGrounded) {
				rigidbody.velocity = new Vector3(velocity.x, JumpForce, velocity.z);
                timeSinceLastJump = 0.0f;
            }
		}

        /// <summary>
        /// Make this walker move forward.
        /// You must call this every frame while you want to walk.
        /// </summary>
        public void WalkForward(float duration)
        {
            forceWalkingForward = duration;
        }

        /// <summary>
        /// Get if character is currently standing on floor
        /// </summary>
        public bool IsGrounded
        {
            get
            {
                return System.Math.Abs(rigidbody.velocity.y) <= 0.01f && 
                    (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f)) &&
                    timeSinceLastJump > 0.025f;
            }
        }

    }
}