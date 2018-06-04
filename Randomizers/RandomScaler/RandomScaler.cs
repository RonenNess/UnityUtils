/**
 * Randomly scales an object, based on position.
 *   
 * Author: Ronen Ness.
 * Since: 2018. 
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NesScripts.Graphics
{
	/// <summary>
	/// Create random scaling based on object's position.
	/// </summary>
	public class RandomScaler : MonoBehaviour {

		// min scale
		public float MinScale = 0.8f;

		// max scale
		public float MaxScale = 1.2f;

		// Use this for initialization
		void Start () {

			System.Random rand = new System.Random((int)(transform.position.x * 1234.25 + transform.position.z * 97.5 + transform.position.y));
			float factor = MinScale + ((float)rand.NextDouble() * (MaxScale - MinScale));
			transform.localScale = transform.localScale * factor;
			Destroy (this);
		}
	}
}