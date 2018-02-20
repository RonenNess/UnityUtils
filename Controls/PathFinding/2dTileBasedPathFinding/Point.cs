/**
 * Represent a point on the path-finding grid.
 * Based on code and tutorial by Sebastian Lague (https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).
 *   
 * Author: Ronen Ness.
 * Since: 2016. 
*/

namespace NesScripts.Controls.PathFind
{
    /// <summary>
    /// A 2d point on the grid
    /// </summary>
    public class Point
    {
        // point X
        public int x;

        // point Y
        public int y;

        /// <summary>
        /// Init the point with zeros.
        /// </summary>
        public Point()
        {
            x = 0;
            y = 0;
        }

        /// <summary>
        /// Init the point with values.
        /// </summary>
        public Point(int iX, int iY)
        {
            this.x = iX;
            this.y = iY;
        }

        /// <summary>
        /// Init the point with a single value.
        /// </summary>
        public Point(Point b)
        {
            x = b.x;
            y = b.y;
        }

        /// <summary>
        /// Get point hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return x ^ y;
        }

        /// <summary>
        /// Compare points.
        /// </summary>
        public override bool Equals(System.Object obj)
        {
            // check type
            if (!(obj.GetType() == typeof(PathFind.Point)))
                 return false;

            // check if other is null
            Point p = (Point)obj;
            if (ReferenceEquals(null, p))
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        /// <summary>
        /// Compare points.
        /// </summary>
        public bool Equals(Point p)
        {
            // check if other is null
            if (ReferenceEquals(null, p))
            {
                return false;
            }

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }

        /// <summary>
        /// Check if points are equal in value.
        /// </summary>
        public static bool operator ==(Point a, Point b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            if (ReferenceEquals(null, a))
            {
                return false;
            }
            if (ReferenceEquals(null, b))
            {
                return false;
            }
            // Return true if the fields match:
            return a.x == b.x && a.y == b.y;
        }

        /// <summary>
        /// Check if points are not equal in value.
        /// </summary>
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Set point value.
        /// </summary>
        public Point Set(int iX, int iY)
        {
            this.x = iX;
            this.y = iY;
            return this;
        }
    }
}
