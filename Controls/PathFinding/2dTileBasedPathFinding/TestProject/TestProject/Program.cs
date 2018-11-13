using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesScripts.Controls.PathFind
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // create the tiles map
            int width = 100;
            int height = 100;
            float[,] tilesmap = new float[width, height];

            System.Random _rand = new Random();
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    tilesmap[i, j] = (float)_rand.NextDouble();
                }
            }

            // create a grid
            PathFind.Grid grid = new PathFind.Grid(tilesmap);

            // create source and target points
            PathFind.Point _from = new PathFind.Point(1, 1);
            PathFind.Point _to = new PathFind.Point(90, 90);

            // get path
            // path will either be a list of Points (x, y), or an empty list if no path is found.
            List<PathFind.Point> path1 = PathFind.Pathfinding.FindPath(grid, _from, _to);

            // for Manhattan distance
            List<PathFind.Point> path2 = PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan);
        }
    }
}
