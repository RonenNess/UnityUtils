/**
 * 3D tilemap, made of game objects with tile components, arranged in a grid.
 *   
 * Author: Ronen Ness.
 * Since: 2017. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NesScripts.Tilemap
{
	/// <summary>
	/// Represent a 3d tilemap, made of tile components.
	/// Note: see Tile component for more info.
	/// </summary>
	public class TileMap : MonoBehaviour {

		// Prototype of the tile to set when creating an empty map.
		// Note: must contain a Tile component.
		public GameObject DefaultTilePrefab;

		// tilemap width and height
		public int Width = 100;
		public int Height = 100;

		// if true, will create empty tilemap from prototype on Start event.
		public bool CreateEmptyOnStart = true;

		// tile size (if 0, will be calculated on start based on tile renderers)
		public Vector2 TileSize = new Vector2(0, 0);

		// array of tiles
		Tile[,] _tiles;

		/// <summary>
		/// Creates empty tilemap.
		/// </summary>
		/// <param name="width">Map width.</param>
		/// <param name="height">Map height.</param>
		/// <param name="defaultTileprefab">Default prototype to set for all tiles.</param>
		public void CreateEmpty(int width, int height, GameObject defaultTileprefab)
		{
			// set params
			Width = width;
			Height = height;
			DefaultTilePrefab = defaultTileprefab;

			if (DefaultTilePrefab == null) {
				throw new UnityException ("You must set a default tile prefab when creating empty tilemap!");
			}

			// make sure tile prototype got Tile component
			if (DefaultTilePrefab.GetComponent<Tile>() == null) {
				throw new UnityException ("Tile prototype must contain a 'Tile' component.");
			}

			// if there's a previous tilemap, destroy it first
			DestroyTilemap ();

			// create tiles array
			_tiles = new Tile[Width, Height];

			// if no tile size provided, calculate it
			if (TileSize.x == 0 && TileSize.y == 0) {
				var renderers = DefaultTilePrefab.GetComponentsInChildren<Renderer> ();
				foreach (var renderer in renderers) {
					TileSize.x = Mathf.Max (TileSize.x, renderer.bounds.extents.x * 2);
					TileSize.y = Mathf.Max (TileSize.y, renderer.bounds.extents.z * 2);
				}
			}

			// create the tilemap
			for (int i = 0; i < Width; ++i) {
				for (int j = 0; j < Height; ++j) {

					// set the tile
					SetTile (DefaultTilePrefab, new Vector2 (i, j), false);

				}
			}

			// build all tiles
			for (int i = 0; i < Width; ++i) {
				for (int j = 0; j < Height; ++j) {
					_tiles [i, j].Build (false);
				}
			}
		}

		/// <summary>
		/// Destroy the tilemap.
		/// </summary>
		public void DestroyTilemap()
		{
			// if there's a previous tilemap, destroy it first
			if (_tiles != null) {
				foreach (var tile in _tiles) {
					Destroy (tile.gameObject);
				}
				_tiles = null;
			}
		}

		/// <summary>
		/// Set a tile.
		/// </summary>
		/// <param name="tilePrefab">Tile to set. May be null to remove the tile without substitute.</param>
		/// <param name="index">Index to put this tile.</param>
		/// <param name="build">If true, will also build the tile and its immediate neighbors.</param>
		public void SetTile(GameObject tilePrefab, Vector2 index, bool build = true)
		{
			// destroy previous tile, if exists
			if (_tiles [(int)index.x, (int)index.y] != null) {
				Destroy (_tiles[(int)index.x, (int)index.y].gameObject);
			}

			// if only destroying tile, stop here
			if (tilePrefab == null) {

				// remove the tile component
				_tiles [(int)index.x, (int)index.y] = null;

				// rebuild neighbors
				for (int i = -1; i <= 1; ++i) {
					for (int j = -1; j <= 1; ++j) {
						if (i != 0 || j != 0) {
							var ntile = GetTile (index + new Vector2 (i, j));
							if (ntile != null) {
								ntile.Build (false);
							}
						}
					}
				}
				return;
			}

			// create tile and set its position and parent
			GameObject tile = GameObject.Instantiate(tilePrefab);
			tile.transform.position = new Vector3 (index.x * TileSize.x, 0, index.y * TileSize.y);
			tile.transform.parent = transform;

			// init tile component
			Tile tileComponent = tile.GetComponent<Tile>();
			tileComponent.Init (this, index);

			// add to tiles array
			_tiles[(int)index.x, (int)index.y] = tileComponent;

			// if build is true, build tile
			if (build) {
				tileComponent.Build (true);
			}
		}
		
		// get tile by index (or null if out of boundaries / not set)
		public Tile GetTile(Vector2 index)
		{
			try {
				return _tiles [(int)index.x, (int)index.y];
			}
			catch (System.IndexOutOfRangeException ) {
				return null;
			}
		}
			
		// Use this for initialization
		void Start () {

			// create empty map on start
			if (CreateEmptyOnStart) {
				CreateEmpty (Width, Height, DefaultTilePrefab);
			}
		}
	}
}