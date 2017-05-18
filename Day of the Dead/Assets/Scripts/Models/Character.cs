using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character {

	public Tile m_originalTile;

	public Tile m_currTile;

	public Tile m_toTile;

	public static int m_width = 32;

	public static int m_height = 64;

	public bool player = false;

	public bool infected = false;

	public float m_movementPercentage = 0.0f;

	public float m_playerSpeed = 2.0f;

	public float m_speed = 1.0f;	

	public float m_movementCooldown;

	public int m_speedMultiplier;

	public int m_currX;

	public int m_currY;

	public bool m_biteThisFrame;

	public bool m_canBite;

	public float m_biteMaxTimer = 3.0f;

	public float m_biteCurrTimer;

	public float X
	{
		get
		{
			return Mathf.Lerp(m_originalTile.X, m_toTile.X, m_movementPercentage);
		}
	}

	public float Y
	{
		get
		{
			return Mathf.Lerp(m_originalTile.Y, m_toTile.Y, m_movementPercentage);
		}
	}

	World m_world;

	Action<Character> cbCharacterCreated;
	Action<Character> cbCharacterMoved;

	public Character ( Tile _tile )
	{
		m_world = WorldController.instance.m_world;
		m_originalTile = m_currTile = m_toTile = _tile;
		m_currX = m_originalTile.X;
		m_currY = m_originalTile.Y;
		m_movementCooldown = UnityEngine.Random.Range ( 0.0f, 5.0f );
		if ( player )
		{
			m_biteCurrTimer = m_biteMaxTimer;
		}
	}

	//If the given tile is under the character, return true.
	public bool IsTileUnderCharacter ( Tile _tile )
	{
		if ( _tile == null )
		{
			return false;
		}
		if ( _tile.X > m_world.m_width )
		{
			return false;
		}
		if ( _tile.Y > m_world.m_height )
		{
			return false;
		}

		if ( _tile.X > m_currX && _tile.X < m_currX + m_width )
		{
			if ( _tile.Y > m_currY && _tile.Y < m_currY + m_height )
			{
				return true;
			}
		}

		return false;
	}

	public Tile GetTileUnderCharacter ()
	{
		return ( WorldController.instance.m_world.GetTileAt (
			Mathf.FloorToInt ( X + 0.5f ),
			Mathf.FloorToInt ( Y + 0.5f ) ) );
	}

	public void Update ( float _deltaTime )
	{
		Update_Movement ( _deltaTime );

		if ( player == false )
		{
			if ( infected )
			{
				Update_AIInfected ( _deltaTime );
			}
			else
			{
				Update_AIAlive ( _deltaTime );
			}
		}

		if ( player )
		{
			if ( m_biteCurrTimer >= m_biteMaxTimer )
			{
				m_canBite = true;
			}
			else
			{
				m_biteCurrTimer += _deltaTime;
			}
		}

	}

	public void Bite ()
	{

		bool killedThisFrame = false;

		foreach ( Character c in m_world.m_allCharacters.ToArray() )
		{
			if ( c.player || c.infected )
			{
				continue;
			}

			for ( int x = m_currTile.X; x < m_currTile.X + m_width; x++ )
			{
				if ( c.IsTileUnderCharacter ( m_world.GetTileAt ( x, m_currTile.Y ) ) )
				{
					c.infected = true;
					killedThisFrame = true;
					WorldController.instance.SC.ChangeCharacterSprite ( c );
					break;
				}

				if ( c.IsTileUnderCharacter ( m_world.GetTileAt ( x, m_currTile.Y + m_height ) ) )
				{
					c.infected = true;
					killedThisFrame = true;
					WorldController.instance.SC.ChangeCharacterSprite ( c );
					break;
				}
			}

			for ( int y = m_currTile.Y; y < m_currTile.Y + m_width; y++ )
			{
				if ( c.IsTileUnderCharacter ( m_world.GetTileAt ( m_currTile.X, y ) ) )
				{
					c.infected = true;
					killedThisFrame = true;
					WorldController.instance.SC.ChangeCharacterSprite ( c );
					break;
				}

				if ( c.IsTileUnderCharacter ( m_world.GetTileAt ( m_currTile.X + m_width, y ) ) )
				{
					c.infected = true;
					killedThisFrame = true;
					WorldController.instance.SC.ChangeCharacterSprite ( c );
					break;
				}
			}

			if ( killedThisFrame )
			{
				m_world.m_powerUpLevel++;

				if ( m_world.m_powerUpLevel >= 4 )
				{
					m_world.m_powerUpNum++;

					m_world.m_powerUpLevel = 0;

					if ( m_world.m_powerUpNum > 0 )
					{
						Debug.Log("Power-up Level - " + m_world.m_powerUpNum);
					}
				}
	
				break;
			}
		}

		m_biteCurrTimer = 0;
		m_canBite = false;
	}

	void Update_Movement ( float _deltaTime )
	{
		float distToTravel = Mathf.Sqrt (
			                     Mathf.Pow ( m_originalTile.X - m_toTile.X, 2 ) +
			                     Mathf.Pow ( m_originalTile.Y - m_toTile.Y, 2 )
		                     );

		
		float distThisFrame;

		if ( player )
		{
			distThisFrame = 100 * _deltaTime;
		}
		else
		{
			distThisFrame = m_speed * 100 * _deltaTime;
		}

		float percThisFrame = distThisFrame / distToTravel;

		m_movementPercentage += percThisFrame;

		if ( GetTileUnderCharacter () != m_currTile )
		{
			m_currTile = GetTileUnderCharacter ();
			m_currX = m_currTile.X;
			m_currY = m_currTile.Y;
		}

		if ( m_movementPercentage >= 1 )
		{
			m_originalTile = m_toTile;
			m_movementPercentage = 0;
		}

		if ( cbCharacterMoved != null )
		{
			cbCharacterMoved ( this );
		}
	}

	public void Update_AIAlive ( float _deltaTime )
	{
		if ( m_originalTile != m_toTile )
		{
			return;
		}

		if ( m_movementCooldown > 0 )
		{
			m_movementCooldown -= _deltaTime;
			return;
		}

		if ( m_world.m_partyTime == false )
		{
			m_movementCooldown = UnityEngine.Random.Range ( 2.0f, 5.0f );
		}
		else
		{
			m_movementCooldown = 0.0f;
		}

		MoveSomewhere();
	}

	public void MoveSomewhere()
	{
		int randX = UnityEngine.Random.Range ( 5, m_world.m_width - m_width );
		int randY = UnityEngine.Random.Range ( m_world.m_graveyardHeight, m_world.m_height - m_height);

		m_toTile = m_world.GetTileAt(randX, randY);
	}

	public void Update_AIInfected ( float _deltaTime )
	{
		//Go to graveyard
		if ( m_toTile.Y == 0 )
		{
			return;
		}
		m_originalTile = m_currTile;
		m_toTile = m_currTile;
		m_currX = m_originalTile.X;
		m_currY = m_originalTile.Y;
		m_movementPercentage = 0;
		m_toTile = m_world.GetTileAt ( UnityEngine.Random.Range ( 5, m_world.m_width - m_width ), 0 );
	}

	public bool MoveEast ( float amount )
	{
		if ( amount == m_speed )
		{
			return false;
		}

		if ( m_world.GetTileAt ( m_toTile.X + m_width, m_toTile.Y ) == null )
		{
			return false;
		}

		if ( m_world.GetTileAt(m_toTile.X + 1, m_toTile.Y) == null )
		{
			return false;
		}

		m_toTile = m_world.GetTileAt(m_toTile.X + 1, m_toTile.Y);

		MoveEast(amount + 1);

		return true;
	}

	public bool MoveWest ( float amount )
	{
		if ( amount == m_speed )
		{
			return false;
		}

		if ( m_world.GetTileAt(m_toTile.X - 1, m_toTile.Y) == null )
		{
			return false;
		}

		m_toTile = m_world.GetTileAt(m_toTile.X - 1, m_toTile.Y);

		MoveWest(amount + 1);

		return true;
	}

	public bool MoveNorth ( float amount )
	{
		if ( amount == m_speed )
		{
			return false;
		}

		if ( m_world.GetTileAt ( m_toTile.X, m_toTile.Y + m_height) == null )
		{
			return false;
		}

		if ( m_world.GetTileAt(m_toTile.X, m_toTile.Y + 1) == null )
		{
			return false;
		}

		m_toTile = m_world.GetTileAt(m_toTile.X, m_toTile.Y + 1);

		MoveNorth(amount + 1);

		return true;
	}

	public bool MoveSouth ( float amount )
	{
		if ( amount == m_speed )
		{
			return false;
		}

		if ( m_world.GetTileAt ( m_toTile.X, m_toTile.Y - 1 ) == null )
		{
			return false;
		}

		if ( m_toTile.Y - 1 < m_world.m_graveyardHeight)
		{
			return false;
		}

		m_toTile = m_world.GetTileAt(m_toTile.X, m_toTile.Y - 1);

		MoveSouth(amount + 1);

		return true;
	}

	public void SmokeBomb ()
	{
		m_world.m_powerUpCountingUp = true;
		WorldController.instance.Smoke.SetActive(true);
		WorldController.instance.Smoke.transform.position = WorldController.instance.SC.m_characterGameObjectMap[this].transform.position;
	}

	public void Zoom()
	{
		m_world.m_powerUpCountingUp = true;
		m_world.SwitchSniperCameraMode ( 2 );
	}

	public void Alarm ()
	{
		m_world.m_powerUpCountingUp = true;
		m_world.m_partyTime = true;
		foreach ( Character c in m_world.m_allCharacters.ToArray() )
		{
			if ( c.player == false )
			{
				c.m_originalTile = c.m_currTile;
				c.m_toTile = c.m_currTile;
				c.m_currX = c.m_originalTile.X;
				c.m_currY = c.m_originalTile.Y;
				c.m_movementPercentage = 0;
				c.m_movementCooldown = 0.0f;
				c.MoveSomewhere();
			}
		}
	}

	public void InfectionBombDropped ()
	{
		m_world.m_powerUpCountingUp = true;
		WorldController.instance.Bomb.SetActive(true);
		WorldController.instance.Bomb.transform.position = new Vector3 (WorldController.instance.IC.m_mousePos.x, WorldController.instance.IC.m_mousePos.y, WorldController.instance.Bomb.transform.position.z);
	}

	public void InfectionBombActivated ()
	{
		int width = 60;
		int height = 60;

		foreach ( Character c in m_world.m_allCharacters.ToArray() )
		{
			if ( c.player )
			{
				continue;
			}

			bool bombHitThisCharacter = false;

			for ( int x = (int)( WorldController.instance.Bomb.transform.position.x - ( width / 2 ) ); x < (int)( WorldController.instance.Bomb.transform.position.x + ( width / 2 ) ); x++ )
			{
				for ( int y = (int)( WorldController.instance.Bomb.transform.position.y - ( height / 2 ) ); y < (int)( WorldController.instance.Bomb.transform.position.y + ( height / 2 ) ); y++ )
				{
					if ( c.IsTileUnderCharacter ( m_world.GetTileAt ( x, y ) ) )
					{
						bombHitThisCharacter = true;
					}
				}
			}

			if ( bombHitThisCharacter )
			{
				c.infected = true;
				WorldController.instance.SC.ChangeCharacterSprite(c);
			}
		}
	}

	public void StopPowerUp ()
	{
		if ( WorldController.instance.Bomb.activeSelf == true )
		{
			InfectionBombActivated ();
			WorldController.instance.Bomb.SetActive ( false );
		}

		m_world.m_partyTime = false;
		WorldController.instance.m_smallCrosshair.SetActive(false);
		WorldController.instance.Smoke.SetActive ( false );
		m_world.SwitchSniperCameraMode ( 1 );
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
