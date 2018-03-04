/**
 * Represent a single node in the pathfinding grid.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/

namespace NesScripts.Controls.PathFind
{
    
    /// <summary>
    /// Represent a single node in the pathfinding grid.
    /// </summary>
    public class Node
    {
        // is this node walkable?
        public bool walkable;
        public int gridX;
        public int gridY;
        public float price;

        // calculated values while finding path
        public int gCost;
        public int hCost;
        public Node parent;

        /// <summary>
        /// Create the grid node.
        /// </summary>
        /// <param name="_price">Price to walk on this node (set to 1.0f to ignore).</param>
        /// <param name="_gridX">Node x index.</param>
        /// <param name="_gridY">Node y index.</param>
        public Node(float _price, int _gridX, int _gridY)
        {
            walkable = _price != 0.0f;
            price = _price;
            gridX = _gridX;
            gridY = _gridY;
        }

        /// <summary>
        /// Create the grid node.
        /// </summary>
        /// <param name="_walkable">Is this tile walkable?</param>
        /// <param name="_gridX">Node x index.</param>
        /// <param name="_gridY">Node y index.</param>
        public Node(bool _walkable, int _gridX, int _gridY)
        {
            walkable = _walkable;
            price = _walkable ? 1f : 0f;
            gridX = _gridX;
			gridY = _gridY;
        }

		/// <summary>
		/// Updates the grid node.
		/// </summary>
		/// <param name="_price">Price to walk on this node (set to 1.0f to ignore).</param>
		/// <param name="_gridX">Node x index.</param>
		/// <param name="_gridY">Node y index.</param>
		public void Update(float _price, int _gridX, int _gridY) {
			walkable = _price != 0.0f;
			price = _price;
			gridX = _gridX;
			gridY = _gridY;
		}

		/// <summary>
		/// Updates the grid node.
		/// </summary>
		/// <param name="_walkable">Is this tile walkable?</param>
		/// <param name="_gridX">Node x index.</param>
		/// <param name="_gridY">Node y index.</param>
		public void Update(bool _walkable, int _gridX, int _gridY) {
			walkable = _walkable;
			price = _walkable ? 1f : 0f;
			gridX = _gridX;
			gridY = _gridY;
		}

        /// <summary>
        /// Get fCost of this node.
        /// </summary>
        public int fCost
        {
            get
            {
                return gCost + hCost;
            }
        }
    }
}