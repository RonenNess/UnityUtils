/**
 * Provide editor functionality for tilemaps (eg changing tiles at runtime using mouse input and main camera).
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
	/// Provide editor functionality to the tilemaps.
	/// </summary>
	public class TilemapEditor : MonoBehaviour {

		/// <summary>
		/// Tile to set on click.
		/// </summary>
		public GameObject TilePrefab;

		/// <summary>
		/// Which mouse button will set tile when clicked.
		/// </summary>
		public int MouseKeyToPlaceTiles = 0;

		/// <summary>
		/// If true, will "paint" tiles while mouse button is held down. If false, will only place a single tile when mouse button is release.
		/// </summary>
		public bool BrushMode = false;

		/// <summary>
		/// Layer ids to query for tiles (if you put all your tiles under a specific layer, use this to make sure you won't collide with other things in the way).
		/// </summary>
		public int TilesLayersFilter = int.MaxValue;

		/// <summary>
		/// If true, will render query ray in gizmos.
		/// </summary>
		public bool ShowDebugRay = true;

		// Update is called once per frame
		void Update () {

			// get ray from camera to mouse position
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// debug draw ray
			if (ShowDebugRay)
			{
				Debug.DrawRay(ray.origin, ray.direction, Color.green);
			}

			// for collision detection
			RaycastHit hitInfo;

			// if we hit a valid target we can place object on:
			if (Physics.Raycast(ray, out hitInfo, 1000f, TilesLayersFilter))
			{
				// get tile component from collision object
				Tile tile = hitInfo.collider.gameObject.GetComponent<Tile>();

				// if not valid tile, skip
				if (tile == null) return;

				// if click, leave object where we placed it
				if ((BrushMode && Input.GetMouseButton(MouseKeyToPlaceTiles)) || Input.GetMouseButtonUp(MouseKeyToPlaceTiles))
				{
					tile.Tilemap.SetTile (TilePrefab, tile.Index, true);
				}
			}

		}
	}

}