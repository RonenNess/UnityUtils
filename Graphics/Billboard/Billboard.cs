/**
 * Implement a basic billboard component.
 * Author: Ronen Ness.
 * Since: 2017.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NesScripts.Graphics
{
    /// <summary>
    /// Different ways to rotate billboard.
    /// </summary>
    public enum BillboardRotationType
    {
        /// <summary>
        /// Always face the camera on all axis.
        /// </summary>
        Absolute,

        /// <summary>
        /// Rotate horizontally only, based on camera angle.
        /// </summary>
        HorizontalCameraDirection,

        /// <summary>
        /// Rotate horizontally only, based on camera position.
        /// </summary>
        HorizontalCameraPosition,

        /// <summary>
        /// Rotate horizontally only, based on camera angle and position.
        /// This yield the best results, but have the worse performance. Use this on large billboards.
        /// </summary>
        HorizontalMixed,
    }

    /// <summary>
    /// Make the entity always face camera, either with one axis locked, or with all axis.
    /// This basically implements a simple billboard.
    /// </summary>
    public class Billboard : MonoBehaviour
    {

        // how to rotate the billboard
        public BillboardRotationType RotationType = BillboardRotationType.HorizontalMixed;

        // caching the camera object
        Camera cam;

        /// <summary>
        /// Get angle between two points.
        /// </summary>
        private float Angle(Vector3 from, Vector3 to)
        {
            Vector2 p1 = new Vector2(from.x, from.z);
            Vector2 p2 = new Vector2(to.x, to.z);
            float ret = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
            return ret;
        }

        /// <summary>
        /// Get the angle needed for rotation to camera position.
        /// </summary>
        private float AbsAngleToCam(Vector3 camPos)
        {
            float ret = -Angle(cam.transform.position, transform.position) + 90;
            return ret;
        }

        /// <summary>
        /// Get avarage between two angles.
        /// </summary>
        private float AvgAngles(float a, float b)
        {
            return Mathf.LerpAngle(a, b, 0.5f);
        }

        /// <summary>
        /// Init the component.
        /// </summary>
        void Start()
        {
            cam = Camera.main;
        }

        /// <summary>
        /// Make object face camera.
        /// </summary>
        void Update()
        {
            // rotate billboard based on rotation type
            switch (RotationType)
            {

                // absolute rotation
                case BillboardRotationType.Absolute:
                    transform.rotation = cam.transform.rotation;
                    break;

                // horizontal angle-based
                case BillboardRotationType.HorizontalCameraDirection:
                    Vector3 rot = cam.transform.rotation.eulerAngles;
                    rot.x = 0; rot.z = 0;
                    transform.rotation = Quaternion.Euler(rot);
                    break;

                // horizontal position-based
                case BillboardRotationType.HorizontalCameraPosition:
                    transform.rotation = Quaternion.Euler(new Vector3(0, AbsAngleToCam(cam.transform.position), 0));
                    break;

                // horizontal mixed (position + rotation)
                case BillboardRotationType.HorizontalMixed:

                    // get camera-based rotation
                    Vector3 camRot = cam.transform.rotation.eulerAngles;
                    camRot.x = 0;
                    camRot.z = 0;

                    // get angle-based rotation
                    float angle = AbsAngleToCam(cam.transform.position);

                    // avarage results
                    camRot.y = AvgAngles(camRot.y, angle);

                    // apply rotation
                    transform.rotation = Quaternion.Euler(camRot);
                    break;

            }
        }

    }
}