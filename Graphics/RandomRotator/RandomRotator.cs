/**
 * Randomly rotates an object around Y axis, based on X,Z position.
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
	/// Randomly rotate an object around Y axis, based on its X and Z position.
	/// </summary>
	public class RandomRotator : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
			// set rotation and remove self
			transform.Rotate(new Vector3(0, 1, 0), 
				(float)System.Math.Sin(transform.position.x) * 10000 + 
				(float)System.Math.Sin(transform.position.z) * 10000 );
			Destroy (this);
		}
	}
}