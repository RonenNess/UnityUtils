/**
 * Implements a texture quad fan from a single quad, useful to represent semi-3d trees in old game graphics.
 *   
 * Author: Ronen Ness.
 * Since: 2018. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NesScripts.Graphics
{
	/// <summary>
	/// Turn a textured quad into a textured fan (useful for trees, for example).
	/// </summary>
	public class TextureFan : MonoBehaviour {

		/// <summary>
		/// How many sides this textured fan should have.
		/// </summary>
		public int NumberOfSides = 2;

		// Use this for initialization
		void Start () {

			// first remove self, so we won't create endless fan leafs
			Destroy(this);

			// get prototype to clone next type
			GameObject toClone = gameObject;

			// now clone based on number of leafs
			for (int i = 0; i < NumberOfSides * 2; ++i) {

				// clone self
				var newSide = Object.Instantiate(toClone);
				Destroy (newSide.GetComponent<TextureFan> ());

				// rotate it
				newSide.transform.parent = transform;
				newSide.transform.localRotation = Quaternion.identity;
				newSide.transform.Rotate(new Vector3(0, 1, 0), (180 / NumberOfSides) * i);
				newSide.transform.localScale = Vector3.one;
				newSide.transform.localPosition = Vector3.zero;

				// set this side as the next object to clone
				toClone = newSide;
			}

			// remove original mesh renderer
			Destroy(GetComponent<MeshRenderer>());
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}