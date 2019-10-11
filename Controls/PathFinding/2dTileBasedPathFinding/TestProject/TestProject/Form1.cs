using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NesScripts.Controls.PathFind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // draw map from a timer every interval
        private void DrawMapWhenReady_Tick(object sender, EventArgs e)
        {
            // create the tiles map
            int width = 100;
            int height = 100;
            float[,] tilesmap = new float[width, height];

            // create random map
            System.Random _rand = new Random();
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    tilesmap[i, j] = (float)_rand.NextDouble();
                }
            }

            // create a grid
            PathFind.GridI grid = new PathFind.SquareGrid(tilesmap);

            // create source and target points
            PathFind.Point _from = new PathFind.Point(1, 1);
            PathFind.Point _to = new PathFind.Point(90, 90);

            // get path
            // path will either be a list of Points (x, y), or an empty list if no path is found.
            List<PathFind.Point> path1 = PathFind.Pathfinding.FindPath(grid, _from, _to);

            // for Manhattan distance
            List<PathFind.Point> path2 = PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan);

            // get graphics context
            Graphics g = ShowMap.CreateGraphics();

            // draw map background
            var tileSize = 4;
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    var colVal = (int)(tilesmap[i, j] * 255);
                    Brush aBrush = new SolidBrush(Color.FromArgb(colVal, colVal, colVal));
                    g.FillRectangle(aBrush, i * tileSize, j * tileSize, tileSize, tileSize);
                }
            }

            // draw paths
            Brush p1Brush = new SolidBrush(Color.Red);
            foreach (var n in path1)
            {
                g.FillRectangle(p1Brush, n.x * tileSize, n.y * tileSize, tileSize, tileSize);
            }
            Brush p2Brush = new SolidBrush(Color.Blue);
            foreach (var n in path2)
            {
                g.FillRectangle(p2Brush, n.x * tileSize, n.y * tileSize, tileSize, tileSize);
            }

            // set interval until next calculation
            DrawMapWhenReady.Interval = 500;
        }
    }
}
