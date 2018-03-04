# 2dTileBasedPathFinding

A very simple 2d tile-based pathfinding for unity, with tiles price supported.

## About

This code is mostly based on the code from [this tutorial](https://www.youtube.com/watch?v=mZfyt03LDH4), with the following modifications:

- Removed all rendering and debug components.
- Converted it into a script-only solution, that relay on grid input via code.
- Separated into files, some docs, minor refactoring.
- Added a more simple, straight-forward API.
- Added support in tile prices, eg tiles that cost more to walk on.
- Added support for Manhattan distance.

But overall most of the work is done by [Sebastian Lague](https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ).

Note: originally this was hosted here: https://github.com/RonenNess/Unity-2d-pathfinding
But the code in this repo is newer and will be maintained.

## How to use

First, copy the folder 'PathFinding' to anywhere you want your asset scripts folder. Once you have it in your project, use the pathfinding like this:

```C#
// create the tiles map
float[,] tilesmap = new float[width, height];
// set values here....
// every float in the array represent the cost of passing the tile at that position.
// use 0.0f for blocking tiles.

// create a grid
PathFind.Grid grid = new PathFind.Grid(tilesmap);

// create source and target points
PathFind.Point _from = new PathFind.Point(1, 1);
PathFind.Point _to = new PathFind.Point(10, 10);

// get path
// path will either be a list of Points (x, y), or an empty list if no path is found.
List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to);

// for Manhattan distance
List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan);

```

If you don't care about price of tiles (eg tiles can only be walkable or blocking), you can also pass a 2d array of *booleans* when creating the grid:
```C#
// create the tiles map
bool[,] tilesmap = new bool[width, height];
// set values here....
// true = walkable, false = blocking

// create a grid
PathFind.Grid grid = new PathFind.Grid(tilesmap);

// rest is the same..
```

After creating the grid with a tilemap, you can update the grid using:
```C#
// create a grid
PathFind.Grid grid = new PathFind.Grid(tilesmap);

// change the tilemap here

// update later
grid.UpdateGrid (tilesmap);
```