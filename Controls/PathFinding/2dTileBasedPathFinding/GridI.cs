/**
 * Represent a grid of nodes we can search paths on.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/
using System.Collections.Generic;

namespace NesScripts.Controls.PathFind {

    /// <summary>
    /// A 2D grid of nodes we use to find paths with the PathFinding class.
    /// The grid supplies Nodes which mark tiles with traversal costs.
    /// The grid can be square or hexagonal (using offset coordinate system).
    /// </summary>
    public interface GridI {
        Node GetNode (int x, int y);
        IEnumerable<Node> GetNeighbours (Node currentNode, Pathfinding.DistanceType distance);
    }
}