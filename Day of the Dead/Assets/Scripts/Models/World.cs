using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class World {

	public static int ZombieBiteScore = 100;

	public static int SniperHealScore = 100;

	public static int SniperZombieKill = 1000;

	public static int SniperHealMissScore = -100;

	public static int SniperHealZombieScore = -100;

	public static int SniperBulletMiss = -300;

	public static int SniperInfectedKill = -50;

	public static int SniperHumanHealMiss = -50;

	public Tile[,] m_tiles;

	public Character m_player1;

	public List<Character> m_allCharacters;

	public static int m_maxNumberOfAI = 100;

	public int m_currNumberOfAI = 100;

	public float m_percOfAIInfected = 0.0f;

	public int m_width;

	public int m_height;

	public int m_graveyardHeight = 50;

	public int m_mainBullets = 3;

	public int m_healthBullets = 5;

	public bool m_SniperCam;

	public float m_powerUpMaxTimer = 5.0f;

	public float m_powerUpCurrTimer = 0.0f;

	public int m_powerUpNum = 3;

	public int m_powerUpLevel = 0;

	public bool m_powerUpActivatedThisFrame = false;

	public bool m_powerUpCountingUp = false;

	public bool m_partyTime = false;

	public int m_P1Score = 0;

	public int m_P2Score = 0;

	public int m_roundTimerMax = 180;

	public int m_roundTimerCurr = 180;

	public float m_secondTimerCurr = 0.0f;

	public int m_round = 1;

	public float m_AmmoCrateTimerMax = 2.0f;

	public float m_AmmoCrateTimerCurr = 0.0f;

	public bool m_ammoCrateSpawned = false;

	public bool m_smokeActive = false;

	Action<Character> cbCharacterCreated;

	public World ( int _width, int _height )
	{
		m_width = _width;
		m_height = _height;

		m_allCharacters = new List<Character> ();

		m_tiles = new Tile[m_width, m_height];

		for ( int x = 0; x < m_width; x++ )
		{
			for ( int y = 0; y < m_height; y++ )
			{
				m_tiles [ x, y ] = new Tile ( this, x, y );
			}
		}
	}

	public void SpawnPlayer1 ( )
	{

		int randX = UnityEngine.Random.Range ( 0, m_width - Character.m_width);
		int randY = UnityEngine.Random.Range ( m_graveyardHeight, m_height - Character.m_height);

		Character p = new Character ( GetTileAt ( randX, randY ), 1 );

		m_player1 = p;

		m_allCharacters.Add(p);

		if ( cbCharacterCreated != null )
		{
			cbCharacterCreated ( p );
		}

	}

	/// The given tile is the very bottom left of the charcater.
	public void SpawnNPC ( )
	{

		int randX = UnityEngine.Random.Range ( 10, m_width - Character.m_width );
		int randY = UnityEngine.Random.Range ( m_graveyardHeight, m_height - Character.m_height);

		Character c = new Character ( GetTileAt ( randX, randY ), 2 );

		m_allCharacters.Add ( c );

		if ( cbCharacterCreated != null )
		{
			cbCharacterCreated ( c );
		}
	}

	/// The given tile is the very bottom left of the charcater.
	public void SpawnAmmoCrate ( )
	{

		int randX = UnityEngine.Random.Range ( 10, m_width - 119 );
		int randY = UnityEngine.Random.Range ( m_graveyardHeight, m_height - 63);

		Character c = new Character ( GetTileAt ( randX, randY ), 3 );

		m_allCharacters.Add ( c );

		if ( cbCharacterCreated != null )
		{
			cbCharacterCreated ( c );
		}
	
	}

	public void Update ( float _deltaTime )
	{

		if ( m_secondTimerCurr >= 1.0f )
		{
			m_roundTimerCurr -= 1;
			m_secondTimerCurr = 0;
		}
		else
		{
			m_secondTimerCurr += _deltaTime;
		}

		if ( m_smokeActive )
		{
			WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color = new Color ( WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.r, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.g, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.b, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.a + _deltaTime );
		}
		else
		{
			if ( WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.a != 0 )
			{
				WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color = new Color ( WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.r, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.g, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.b, WorldController.instance.Smoke.GetComponent<SpriteRenderer> ().color.a - _deltaTime );
			}
			else if (WorldController.instance.Smoke.activeSelf == true)
			{
				WorldController.instance.Smoke.SetActive(false);
			}
		}

		if ( m_powerUpActivatedThisFrame )
		{
			m_powerUpActivatedThisFrame = false;
			m_powerUpCountingUp = true;
		}

		if ( m_powerUpCountingUp )
		{
			if ( m_powerUpCurrTimer >= m_powerUpMaxTimer )
			{
				Debug.Log ( "Power Up goes off or stops!" );
				m_player1.StopPowerUp ();
				m_powerUpCurrTimer = 0;
				m_powerUpCountingUp = false;
			}
			else
			{
				m_powerUpCurrTimer += _deltaTime;
			}
		}

		if ( m_mainBullets == 0 )
		{
			if ( m_AmmoCrateTimerCurr >= m_AmmoCrateTimerMax )
			{
				if ( m_ammoCrateSpawned == false )
				{
					SpawnAmmoCrate();
					m_ammoCrateSpawned = true;
				}
			}
			else
			{
				m_AmmoCrateTimerCurr += Time.deltaTime;
			}
		}

		List<Character> TempList = new List<Character>();

		foreach ( Character c in m_allCharacters.ToArray() )
		{
			TempList.Add(c);
		}

		foreach ( Character c in TempList.ToArray() )
		{
			c.Update ( _deltaTime );
		}
	}

	public void SniperShot ( int _bullet, Tile _tile )
	{
		if ( _bullet == 1 )
		{
			if ( m_mainBullets <= 0 )
			{
				//Check to see if ammo crate was shot.
				foreach ( Character c in m_allCharacters.ToArray() )
				{
					if ( c.isAmmoCrate == false )
					{
						continue;
					}
					if ( c.IsTileUnderCharacter ( _tile ) )
					{
						//Ammo crate was shot
						m_mainBullets++;
						WorldController.instance.UIC.GainBullet();
						GameObject.Destroy ( WorldController.instance.SC.m_characterGameObjectMap [ c ] );
						m_ammoCrateSpawned = false;
						m_AmmoCrateTimerCurr = 0.0f;
					}
			}
				return;
			}
			WorldController.instance.SM.PlaySniperShotAS();
			m_mainBullets--;
			WorldController.instance.UIC.LoseBullet(1);
			foreach ( Character c in m_allCharacters.ToArray() )
			{
				if ( c.IsTileUnderCharacter ( _tile ) )
				{
					if ( c.isPlayer )
					{
						Debug.Log ( "player was shot with left button." );	
						WorldController.instance.UIC.PlayerScoreChange ( 2, SniperZombieKill );
						WorldController.instance.SM.PlayZombieDeathAS();
					}
					else if ( c.isInfected )
					{
						Debug.Log ( "Infected was shot with left button." );
						WorldController.instance.UIC.PlayerScoreChange ( 2, SniperInfectedKill );
						m_currNumberOfAI--;
						GameObject.Destroy ( WorldController.instance.SC.m_characterGameObjectMap [ c ] );
						m_allCharacters.Remove ( c );
					}
					else
					{
						Debug.Log ( "Human was shot with left button." );
						WorldController.instance.UIC.PlayerScoreChange ( 2, SniperBulletMiss );
						m_currNumberOfAI--;
						GameObject.Destroy ( WorldController.instance.SC.m_characterGameObjectMap [ c ] );
						m_allCharacters.Remove ( c );
					}
				}
			}
		}
		else if ( _bullet == 2 )
		{	
			if ( m_healthBullets <= 0 )
			{
				return;
			}
			WorldController.instance.UIC.LoseBullet(2);
			WorldController.instance.SM.PlaySniperShotAS();
			m_healthBullets--;
			bool infectedWasHit = false;
			int numberOfHumansHit = 0;
			foreach ( Character c in m_allCharacters.ToArray() )
			{
				if ( c.IsTileUnderCharacter ( _tile ) )
				{
					if ( c.isPlayer )
					{
						WorldController.instance.UIC.PlayerScoreChange ( 2, SniperHealZombieScore );

					}
					else if ( c.isInfected )
					{
						infectedWasHit = true;
						c.isInfected = false;
						c.m_toTile = c.m_currTile;
						c.m_originalTile = c.m_currTile;
						c.m_movementPercentage = 0.0f;
						c.wasShot = true;
					}
					else
					{
						numberOfHumansHit++;
						c.wasShot = true;
						m_currNumberOfAI--;
						GameObject.Destroy ( WorldController.instance.SC.m_characterGameObjectMap [ c ] );
						m_allCharacters.Remove ( c );
					}
				}
			}

			if ( infectedWasHit )
			{
				WorldController.instance.UIC.PlayerScoreChange ( 2, SniperHealScore );
			}
			else
			{
				for ( int i = 0; i < numberOfHumansHit; i++ )
				{
					WorldController.instance.UIC.PlayerScoreChange ( 2, SniperHumanHealMiss );
				}
			}
		}

		return;
	}

	public void SwitchSniperCameraMode ( int _type )
	{
		if ( _type == 1 )
		{
			WorldController.instance.m_mainCamera.gameObject.SetActive ( true );
			WorldController.instance.m_sniperCamera.gameObject.SetActive ( false );
			m_SniperCam = false;
		}
		else
		{
			WorldController.instance.m_mainCamera.gameObject.SetActive ( false );
			WorldController.instance.m_sniperCamera.gameObject.SetActive ( true );
			m_SniperCam = true;
		}
	}

	public Tile GetTileAt ( int _x, int _y )
	{

		if ( _x >= m_width || _x < 0 || _y >= m_height || _y < 0 )
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
