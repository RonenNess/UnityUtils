/**
 * Represent a tile type you can attach to tiles.
 * Mostly contain metadata about a tile and how to build it.
 *   
 * Author: Ronen Ness.
 * Since: 2017. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


namespace NesScripts.Tilemap
{
	/// <summary>
	/// Different tile categories.
	/// </summary>
	public enum TileCategory
	{
		/// <summary>
		/// Regular, walkable, floor tiles.
		/// </summary>
		Floor,

		/// <summary>
		/// Walls / blocking tiles.
		/// </summary>
		Wall,

		/// <summary>
		/// Water / other liquid tiles you can swim in.
		/// </summary>
		Water,

		/// <summary>
		/// Endless pit tiles.
		/// </summary>
		Pit,

		/// <summary>
		/// 'Null' tiles, eg nothing there. Use this for internal stuff and editor.
		/// </summary>
		Null,
	}
	
	/// <summary>
	/// Define the API for a tile type.
	/// </summary>
	[System.Serializable]
	public class TileType 
	{

		// a dictionary with all tile type instances
		static internal Dictionary<string, TileType> _types = new Dictionary<string, TileType>();

		/// <summary>
		/// Get tile type by identifier (must match class name).
		/// </summary>
		/// <param name="typeName">Tile type identifier.</param>
		public static TileType GetType(string typeName) {

			// try to get if already loaded
			TileType instance;
			if (_types.TryGetValue (typeName, out instance)) 
			{
				return instance;
			}

			// if got here it means this tile type is not loaded yet. get type from name.
			var assembly = Assembly.GetExecutingAssembly();
			var type = Array.Find(assembly.GetTypes(), t => t.Name == typeName);

			// instanciate it
			instance = Activator.CreateInstance(type) as TileType;

			// add to dictionary and return
			_types[typeName] = instance;
			return instance;
		}

		/// <summary>
		/// Gets the tile category.
		/// </summary>
		/// <value>The tile type category.</value>
		virtual public TileCategory TileCategory { get {return TileCategory.Null;} }

		/// <summary>
		/// Only if true will try to build other tile parts, using the GetXxxPart() functions.
		/// </summary>
		/// <value><c>true</c> if use dynamic parts build; otherwise, <c>false</c>.</value>
		virtual public bool UseDynamicPartsBuild { get { return false; } }

		/// <summary>
		/// When a tile is built and its front (positive Z) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "front part" of this tile.
		/// For example, if your tile is a wall, this will be its front.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The front part for this tile. Note: will be cloned, not used directly.</value>
		virtual public GameObject GetFrontPart(Tile self, Tile neighbor) {
			return null;
		}

		/// <summary>
		/// When a tile is built and its back (negative Z) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "back part" of this tile.
		/// For example, if your tile is a wall, this will be its back.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The back part for this tile. Note: will be cloned, not used directly.</value>
		virtual public GameObject GetBackPart(Tile self, Tile neighbor) {
			return null;
		}

		/// <summary>
		/// When a tile is built and its left (negative X) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "left part" of this tile.
		/// For example, if your tile is a wall, this will be its left size.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The left part for this tile. Note: will be cloned, not used directly.</value>
		virtual public GameObject GetLeftPart(Tile self, Tile neighbor) {
			return null;
		}

		/// <summary>
		/// When a tile is built and its right (positive X) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "right part" of this tile.
		/// For example, if your tile is a wall, this will be its right size.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The right part for this tile. Note: will be cloned, not used directly.</value>
		virtual public GameObject GetRightPart(Tile self, Tile neighbor) {
			return null;
		}
	}

	/// <summary>
	/// Define a basic null tile type for testing.
	/// </summary>
	public class TileType_Null : TileType
	{
	}

	/// <summary>
	/// An basic tile with a constant type of walls to merge.
	/// </summary>
	public class TileType_WallsExample : TileType
	{
		/// <summary>
		/// Only if true will try to build other tile parts, using the GetXxxPart() functions.
		/// </summary>
		/// <value>true</value>
		override public bool UseDynamicPartsBuild { get { return true; } }

		/// <summary>
		/// The path of the prefab we use for walls for this tile type (under Resources folder).
		/// </summary>
		virtual protected string WallResourcePath { get { return "World/Prefabs/Tiles/Wall"; } }

		/// <summary>
		/// When a tile is built and its front (positive Z) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "front part" of this tile.
		/// For example, if your tile is a wall, this will be its front.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The front part for this tile. Note: will be cloned, not used directly.</value>
		override public GameObject GetFrontPart(Tile self, Tile neighbor) {
			return (GameObject)Resources.Load(WallResourcePath, typeof(GameObject));
		}

		/// <summary>
		/// When a tile is built and its back (negative Z) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "back part" of this tile.
		/// For example, if your tile is a wall, this will be its back.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The back part for this tile. Note: will be cloned, not used directly.</value>
		override public GameObject GetBackPart(Tile self, Tile neighbor) {
			return (GameObject)Resources.Load(WallResourcePath, typeof(GameObject));
		}

		/// <summary>
		/// When a tile is built and its left (negative X) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "left part" of this tile.
		/// For example, if your tile is a wall, this will be its left size.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The left part for this tile. Note: will be cloned, not used directly.</value>
		override public GameObject GetLeftPart(Tile self, Tile neighbor) {
			return (GameObject)Resources.Load(WallResourcePath, typeof(GameObject));
		}

		/// <summary>
		/// When a tile is built and its right (positive X) neighbor is of a different type, this function will be called.
		/// If returns a GameObject and not null, will use this prefab as the "right part" of this tile.
		/// For example, if your tile is a wall, this will be its right size.
		/// 
		/// Note: handle positioning and rotation. Your prefab is assumed to be facing forward.
		/// </summary>
		/// <value>The right part for this tile. Note: will be cloned, not used directly.</value>
		override public GameObject GetRightPart(Tile self, Tile neighbor) {
			return (GameObject)Resources.Load(WallResourcePath, typeof(GameObject));
		}
	}
}
