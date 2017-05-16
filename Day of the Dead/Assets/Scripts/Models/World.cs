using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class World {

	public Tile[,] m_tiles;

	public List<Character> m_allCharacters;

	public int m_width;

	public int m_height;

	Action<Character> cbCharacterCreated;

	public World (int _width, int _height)
	{
		m_width = _width;
		m_height = _height;

		m_allCharacters = new List<Character>();

		m_tiles = new Tile[m_width, m_height];

		for ( int x = 0; x < m_width; x++ )
		{
			for ( int y = 0; y < m_height; y++ )
			{
				m_tiles [ x, y ] = new Tile ( this, x, y );
			}
		}
	}

	/// The given tile is the very bottom left of the charcater.
	public void SpawnCharcater ( Tile _tile )
	{
		Character c = new Character ( _tile );

		m_allCharacters.Add(c);

		if ( cbCharacterCreated != null )
		{
			cbCharacterCreated ( c );
		}
	}

	public Tile GetTileAt ( int _x, int _y )
	{

		if ( _x > m_width || _x < 0 || _y > m_height || _y < 0 )
		{
			return null;
		}

		return m_tiles[_x, _y];
	}	

	public void RegisterCharacterCreated(Action<Character> _callbackFunc)
	{
		cbCharacterCreated += _callbackFunc;	
	}

}
