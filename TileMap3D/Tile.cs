/**
 * Represent a single tile in the 3d tilemap.
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
	/// Base tile component to be put in the tilemap.
	/// All tile GameObjects must have this component or a component derived from it.
	/// </summary>
	public class Tile : MonoBehaviour {

		/// <summary>
		/// The tile type of this tile instance.
		/// </summary>
		private TileType TileType;

		/// <summary>
		/// Tile type to load when this tile spawns.
		/// You must set this from the inspector.
		/// </summary>
		public string TileTypeName = "TileType_Null";

		/// <summary>
		/// Parts created dynamically.
		/// </summary>
		List<GameObject> _parts = new List<GameObject>();

		// tile index in tilemap
		public Vector2 Index {
			get;
			private set;
		}

		// tilemap this tile belongs to
		public TileMap Tilemap {
			get;
			private set;
		}

		// set tile type
		void Start ()
		{
			if (TileType == null) 
			{
				TileType = TileType.GetType (TileTypeName);
				TileTypeName = null;
			}
		}

		/// <summary>
		/// Update the tile index in tilemap.
		/// </summary>
		/// <param name="index">New tile index.</param>
		public void Init(TileMap tilemap, Vector2 index)
		{
			Index = index;
			Tilemap = tilemap;
			Start ();
		}

		/// <summary>
		/// Build this tile and notify its neighbors.
		/// </summary>
		/// <param name="buildNeighbors">If true, will also rebuild all the neighbors of this tile.</param>
		public void Build(bool buildNeighbors = false)
		{
			// build tile
			OnBuild ();

			// notify neighbors
			if (buildNeighbors) {
				for (int i = -1; i <= 1; ++i) {
					for (int j = -1; j <= 1; ++j) {

						// skip self
						if (i == 0 && j == 0)
							continue;

						// get neighbor and relative index
						Vector2 neighborIndex = Index + new Vector2 (i, j);

						// get neighbor and notify it
						var neighbor = Tilemap.GetTile (neighborIndex);
						if (neighbor != null) neighbor.Build (false);
					}
				}
			}
		}

		/// <summary>
		/// Instanciate given prefab and add to parts.
		/// This also set self as parent.
		/// </summary>
		/// <param name="prefab">Prefab to add.</param>
		/// <param name="rotateY">Rotate new part on Y axis.</param>
		/// <returns>>Newly created instance.</returns>
		private GameObject AddPart(GameObject prefab, float rotateY = 0)
		{
			GameObject newPart = GameObject.Instantiate (prefab) as GameObject;
			newPart.transform.parent = gameObject.transform;
			newPart.transform.localRotation = Quaternion.Euler(new Vector3 (0, rotateY, 0));
			newPart.transform.localPosition = Vector3.zero;
			_parts.Add (newPart);
			return newPart;
		}

		/// <summary>
		/// Called when the tile should be built.
		/// This actually implements the building logic.
		/// </summary>
		virtual protected void OnBuild()
		{
			// first destroy all previously-built parts
			foreach (var part in _parts) {
				Destroy(part);
			}
			_parts.Clear ();

			// if tiletype don't support dynamic build, skip
			if (!TileType.UseDynamicPartsBuild) {
				return;
			}

			// get all neighbors
			var front = Tilemap.GetTile(Index + Vector2.up);
			var back = Tilemap.GetTile(Index + Vector2.down);
			var left = Tilemap.GetTile(Index + Vector2.left);
			var right = Tilemap.GetTile(Index + Vector2.right);
			var frontLeft = Tilemap.GetTile(Index + Vector2.up + Vector2.left);
			var frontRight = Tilemap.GetTile(Index + Vector2.up + Vector2.right);
			var backLeft = Tilemap.GetTile(Index + Vector2.down + Vector2.left);
			var backRight = Tilemap.GetTile(Index + Vector2.down + Vector2.right);

			// get which neighbors are different
			bool frontDiff = front == null || front.TileType != TileType;
			bool backDiff = back == null || back.TileType != TileType;
			bool leftDiff = left == null || left.TileType != TileType;
			bool rightDiff = right == null || right.TileType != TileType;
			bool frontLeftDiff = frontLeft == null || frontLeft.TileType != TileType;
			bool frontRightDiff = frontRight == null || frontRight.TileType != TileType;

			// now create new parts

			// build front type
			if (frontDiff) {
				var prefab = TileType.GetFrontPart (this, front);
				if (prefab != null) {
					AddPart (prefab, 180);
				}
			}

			// build back type
			if (backDiff) {
				var prefab = TileType.GetBackPart (this, back);
				if (prefab != null) {
					AddPart (prefab, 0);
				}
			}

			// build left type
			if (leftDiff) {
				var prefab = TileType.GetLeftPart (this, left);
				if (prefab != null) {
					AddPart (prefab, 90);
				}
			}

			// build right type
			if (rightDiff) {
				var prefab = TileType.GetRightPart (this, right);
				if (prefab != null) {
					AddPart (prefab, -90);
				}
			}
		}
	}
}