/**
 * Add random doodads on tile.
 *   
 * Author: Ronen Ness.
 * Since: 2018. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ness.Graphics {

	/// <summary>
	/// Add random doodads on tile.
	/// </summary>
	public class RandomDoodadsGenerator : MonoBehaviour {

		// size of a single tile square
		public static readonly float TileSize = 2;

		/// <summary>
		/// Doodad type and chance to appear.
		/// </summary>
		[System.Serializable]
		public struct DoodadType
		{
			// doodad prefab
			public GameObject Prefab;

			// occurance chance in percent
			public float Chance;
		}

		// different types and their chances
		public DoodadType[] Types;

		// Use this for initialization
		void Start () {

			// create random and seed based on position
			System.Random rand = new System.Random((int)(transform.position.x * 12.3f * transform.position.z / 1.234f));

			// spawn doodads
			for (int i = 0; i < Types.Length; ++i) {
				if ((float)rand.NextDouble() < Types[i].Chance) {
					GameObject newObj = Object.Instantiate (Types [i].Prefab);
					newObj.transform.parent = transform;
					newObj.transform.position = new Vector3(
						transform.position.x + -TileSize + (float)rand.NextDouble () * TileSize, 
						0, 
						transform.position.z + -TileSize + (float)rand.NextDouble () * TileSize);
				}
			}

			// destroy self
			Destroy(this);

		}
	}
}