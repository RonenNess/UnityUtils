/**
 * Script for levels editor to hold and place objects.
 *   
 * Author: Ronen Ness.
 * Since: 2017. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NesScripts.Controls.GameEditor
{
    /// <summary>
    /// A script to hold objects "in hands" and place them around the level using the mouse controls.
    /// </summary>
    public class ObjectsPlacer : MonoBehaviour
    {
        // The object currently held in hands (eg the object we are about to place down).
        private GameObject ObjectInHands;

        // If not set to 0, will snap objects position to this grid size
        public float SnapToGrid = 0.25f;

        // If true, will debug-draw ray from camera to target.
        public bool ShowDebugRay = true;

        // If true, will clone object when placing it. If false, once placing object we will no longer hold anything until we pickup a new object.
        public bool CloneObjectOnPlacement = true;

        // Which mouse key will place the object
        public int MouseKeyToPlaceObject = 0;

        // Special layer we use to prevent collision and interaction with other objects while we hold this object in hands.
        // If you use layer 31 for something else, please pick a different layer.
        public int NoCollisionLayerId = 31;

		// Set the object to pickup at next Update() call.
		// For code you can just call PickupObject() directly, for Unity editor you can use this member.
		public GameObject ObjectToPickup = null;

        // original layer of the object-in-hands
        private int prevLayer = 0;

		// Filter with which layers we are allowed to collide with when placing the object.
		// For example, if you only want to place objects on floor and not on each other, set this to the floor layer id.
		public int CollisionLayersFilter = int.MaxValue;

		// The max renderers extent height of the object in hand.
		private float targetMaxExtentHeight = 0f;

        // original detect collision state of the object's rigid body
        private bool _prevRigidBodyDetectCollision = false;

		// Where to hold the object on Y axis, in percents, when playing it.
		// If equals 1f will hold the object from its bottom point.
		// If 0.5 from center.
		// If 0.0, from its top.
		public float PivotY = 1f;

        /// <summary>
        /// Set a new object-in-hands to place in level, eg the object we are currently setting.
        /// </summary>
        /// <param name="target">New object to place.</param>
        public void PickupObject(GameObject target)
        {
			// set object to pickup to new target
			ObjectToPickup = target;

            // set the object-in-hands pointer
            ObjectInHands = target;

            // set the object to the unused layer so it won't interact with stuff
            prevLayer = ObjectInHands.layer;
            ObjectInHands.layer = NoCollisionLayerId;
            
			// get the extent height of the object based on its renderer and child renderers
			targetMaxExtentHeight = 0f;
			var childRenderers = ObjectInHands.GetComponentsInChildren<Renderer>();
			foreach (var renderer in childRenderers) {
				var curr = renderer.bounds.extents.y;
				targetMaxExtentHeight = Mathf.Max (targetMaxExtentHeight, curr);
			}

            // disable rigid body
            var rigidBody = ObjectInHands.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
				_prevRigidBodyDetectCollision = rigidBody.detectCollisions;
				rigidBody.detectCollisions = false;
            }

            // disable all object colliders
			foreach (Collider collider in ObjectInHands.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }

        /// <summary>
        /// Place down the object currently held in hands.
        /// </summary>
        /// <param name="clone">If true, will clone the object-in-hand but keep on holding it. If false, will just place down the object we held.</param>
        public void PlaceObject(bool clone = false)
        {
            // store object we previously held. this is the object we will now place down
            GameObject objectWeJustPutDown = ObjectInHands;

            // if we are cloning this object, pickup its clone. if not, will hold null
            ObjectInHands = clone ? GameObject.Instantiate(ObjectInHands) : null;

			// set object to pickup to new object in hand
			ObjectToPickup = ObjectInHands;

            // restore original layer
			objectWeJustPutDown.layer = prevLayer;

            // restore original rigid body collision detection state
			var rigidBody = objectWeJustPutDown.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
				rigidBody.detectCollisions = _prevRigidBodyDetectCollision;
            }

            // enable all object colliders
			foreach (Collider collider in objectWeJustPutDown.GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
        }

        /// <summary>
        /// Handle objects placement.
        /// </summary>
        void Update()
        {
			// check if we should pickup an object
			if (ObjectToPickup != ObjectInHands) {
				PickupObject (ObjectToPickup);
			}
			
            // if no object in hands - nothing to do.
            if (ObjectInHands == null)
            {
                return;
            }

            // get ray from camera to mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // debug draw ray
            if (ShowDebugRay)
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
            }

            // for collision detection
            RaycastHit hitInfo;

			// if we hit a valid target we can place object on:
			if (Physics.Raycast(ray, out hitInfo, 1000f, ~(1 << NoCollisionLayerId) & CollisionLayersFilter))
            {
                // get collision point
                Vector3 collisionPoint = hitInfo.point;

                // snap to grid
                if (SnapToGrid > 0)
                {
                    collisionPoint = new Vector3(
                        Mathf.Round(collisionPoint.x / SnapToGrid) * SnapToGrid,
                        collisionPoint.y,
                        Mathf.Round(collisionPoint.z / SnapToGrid) * SnapToGrid);
                }

                // update the position of the object-in-hand, to make it show where you are going to place it
				ObjectInHands.transform.position = collisionPoint + new Vector3(0, -targetMaxExtentHeight + targetMaxExtentHeight * (PivotY * 2f), 0);
                ObjectInHands.transform.rotation = Quaternion.identity;

                // if click, leave object where we placed it
				if (Input.GetMouseButtonUp(MouseKeyToPlaceObject))
                {
                    PlaceObject(CloneObjectOnPlacement);
                }
            }
        }
    }
}