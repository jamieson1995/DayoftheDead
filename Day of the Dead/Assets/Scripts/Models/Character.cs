using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character {

	public Tile m_tile { get; protected set; }

	public int X;
	public int Y;

	World m_world;

	Action<Character> cbCharacterCreated;
	Action<Character> cbCharacterMoved;

	public Character ( Tile _tile )
	{
		m_world = WorldController.instance.m_world;
		m_tile = _tile;

		X = _tile.x;
		Y = _tile.y;

	}

	//If the given tile is under the character, return true.
	public bool IsTileUnderCharacter ( Tile _tile )
	{
		if ( _tile.x > m_world.m_width - 64 )
		{
			_tile = m_world.GetTileAt(m_world.m_width - 64, _tile.y);
		}
		if ( _tile.y > m_world.m_height - 64 )
		{
			_tile = m_world.GetTileAt(_tile.x, m_world.m_height - 64);
		}


		for ( int x = 0; x < 64; x++ )
		{
			for ( int y = 0; y < 64; y++ )
			{
				if ( _tile != null && _tile == m_world.GetTileAt ( X + x, Y + y ) )
				{
					return true;
				}
			}
		}

		return false;
	}

	public void RegisterOnCreatedCallback( Action<Character> _callbackFunc)
	{
		cbCharacterCreated += _callbackFunc;
	}

	public void UnRegisterOnCreatedCallback( Action<Character> _callbackFunc)
	{
		cbCharacterCreated -= _callbackFunc;
	}

	public void RegisterOnMovedCallback( Action<Character> _callbackFunc)
	{
		cbCharacterMoved += _callbackFunc;
	}

	public void UnRegisterOnMovedCallback( Action<Character> _callbackFunc)
	{
		cbCharacterMoved -= _callbackFunc;
	}
}
