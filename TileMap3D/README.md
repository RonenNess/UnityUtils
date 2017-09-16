# TileMap

Define a TileMap and Tile components.

## What's a Tilemap

In this case, a TileMap is a 3d grid of 'Tile' game objects that compose a level. Think of games like Dungeon Keeper 2 - every floor tile, wall, gold tile etc is a single tile from the tilemap that is the level.

This component helps us implement something similar to that.

## How to use

### Create TileMap

First, create a GameObject that will be the root of your tilemap, and attach the ```TileMap``` script component to it.
Configure tilemap size (Width and Height) and mark "Create Empty On Start" so it will generate an empty tilemap when scene loads.

### Create Tile Prefabs

Next, you need to define a basic tile prefab. For example, create a 3d Plane object in the size of 5, and attach the ```Tile``` script component to it.
Don't change its "Tile Type Name" property for now.

Now you need to set your tile prefab as the tilemap's ```Default Tile Prefab``` property.

If you run the game now, you would see that the Tilemap duplicated the tile prefab based on your tilemap size, and placed the tiles in a grid. The grid size is based on your tile prefab renderers size, but if you want to set an alternative size you can set the ```TileMap's``` Tile Size property.

After you got a single tile prefab and tilemap running, you can start creating other tile prefabs to use with the tilemap editor script. 
But before you do, you might want to read about tile types first.

### Change Tiles

To change a specific tile via code, use the ```TileMap.SetTile()``` function.

If you want to edit tiles from the scene like in a levels editor, check out the ```TilemapEditor``` script component.

### Tile Types

A ```TileType``` is a class that describe the tile behavior, and how it interacts with other neighbor tiles.

For example imagine a "forst" tile that have thick group of trees at the center, and tall grass / short few trees when it merges with other tile types. How would you implement the merging parts?

The answer is via ```TileType```. To define a new tile type simply inherit from the ```TileType``` class and implement the following (or leave them to the default if that's fitting for your type):

#### TileCategory

The category of the tile - is it wall? water? endless pit?

#### UseDynamicPartsBuild

Set to true if you want to dynamically build tile merging parts (described ahead). If not, leave this as false.

#### Get<xxx>Part

This is the part that allow you to implement merging. There's a function like this for every neighbor direction of the tile - front, back, left, etc.

If this function returns null, nothing would happen if the neighbor is different. But if it returns a prefab of any sort, it will create this prefab, place and rotate it based on direction to build this part automatically.

Note that the function also get the tile itself and the relevant neighbor, so you can apply some internal logic (for example use one type of merging for water tiles and different type of merging for floor).

### How to use custom tile types

To use a custom tile type simply set the ```Tile Type Name``` property of your prefab to match the custom tile class name.

Note that this property nullify once init, so don't use it afterwards. It only exists to allow you to set tile types via the inspector.


## TilemapEditor

A script component you can attach to any GameObject that will provide an editor-like functionality.

Basically it will allow you to hold a tile prefab type will cast rays from main camera to mouse trying to hit objects with ```Tile``` component.

When hit a valid tile object and mouse button is pressed, it will replace the tile in the tilemap with the tile prefab currently "in hands".

This will provide a basic, easy levels editor to use with ```TileMaps```.