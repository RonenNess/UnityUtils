/**
 * Recalculate texture tiling to always tile the texture at the same desired size.
 * This means that if you have bricks texture, for example, you can tile it evenly on all surfaces, no matter their size.
 * Code is based on answer from here and heavily modified: 
 * https://gamedev.stackexchange.com/questions/111060/unity-tiling-of-a-material-independen-of-its-size
 * 
 * Note: this will update material in editor itself.
 * 
 * Author: Ronen Ness.
 * Since: 2018.
 */
using UnityEngine;


namespace NesScripts.Graphics
{
    [ExecuteInEditMode]
    public class RecalcTextureTile : MonoBehaviour
    {
        /// <summary>
        /// Texture scale factor.
        /// </summary>
        public float ScaleFactor = 5.0f;

        /// <summary>
        /// Desired size of a single texture tile, in pixels.
        /// Set to 0,0 to take full texture size.
        /// </summary>
        public Vector2 DesiredTileSizeInPixels = new Vector2(0, 0);

        /// <summary>
        /// If true (default) will use Z axis for y.
        /// </summary>
        public bool UseAxisZ = true;

        /// <summary>
        /// Set this to true to make the object update once (it turns false immediately).
        /// </summary>
        public bool ForceUpdateNow = true;

        // Update is called once per frame
        void Update()
        {
            // update material for when we're in editor mode
            if ((transform.hasChanged || ForceUpdateNow) &&
                (Application.isEditor && !Application.isPlaying))
            {
                // update texture
                UpdateTextureScale();

                // set no changes and update last scale param
                transform.hasChanged = false;
                ForceUpdateNow = false;
            }
        }

        /// <summary>
        /// Recalc and update texture scale.
        /// </summary>
        void UpdateTextureScale()
        {
            // get desired texture size in pixels
            var desiredTextSize = DesiredTileSizeInPixels;

            // set default desired tile size
            if (desiredTextSize.magnitude == 0)
            {
                var text = GetComponent<Renderer>().sharedMaterial.mainTexture;
                desiredTextSize.x = text.width;
                desiredTextSize.y = text.height;
            }

            // calc texture tile
            GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(
                (transform.lossyScale.x / desiredTextSize.x) / ScaleFactor,
                ((UseAxisZ ? transform.lossyScale.z : transform.lossyScale.y) / desiredTextSize.y) / ScaleFactor);
        }
    }
}