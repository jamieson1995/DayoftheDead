using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController instance { get; protected set; }

	public World m_world { get; protected set; }

	public Sprite m_tileSprite;

	public SpriteController SC;

	public InputController IC;

	public UIController UIC;

	public SoundManager SM;

	public GameObject Button;

	public Sprite m_crossHairSp;

	public GameObject m_smallCrosshair;

	public GameObject m_background;

	public GameObject m_foreground;

	public Camera m_mainCamera;

	public Camera m_sniperCamera;

	public GameObject Smoke;

	public GameObject Bomb;

	public GameObject m_cursor;

	public bool P1Ready = false;

	public bool P2Ready = false;

	public bool RoundActive = false;

	public GameObject RoundStartMenu;

	public bool RoundEndedThisFrame = false;

	void Awake ()
	{
		if ( instance == null )
		{
			instance = this;
		}
		else
		{
			//Already have an instance.
		}

	}

	void Update ()
	{
		if ( RoundEndedThisFrame )
		{

		}

		if ( P1Ready && P2Ready )
		{
			RoundActive = true;
			RoundStartMenu.SetActive(false);
			CreateWorld ();
			P1Ready = false;
			P2Ready = false;
		}

		if ( m_world == null )
		{
			return;
		}

		m_world.Update ( Time.deltaTime );
	}

	public void CreateWorld ()
	{
		if ( m_world != null )
		{
			if ( m_world.m_allCharacters.Count > 0 )
			{
				foreach ( Character c in m_world.m_allCharacters.ToArray() )
				{
					GameObject.Destroy ( SC.m_characterGameObjectMap [ c ] );
				}
			}
			m_world.m_allCharacters = new List<Character> ();
			UIC.SetRoundUI();
		}
		else
		{
			m_world = new World ( 810, 400 );
		}

		SM.PlayCrowdSoundsAS ();

		Button.SetActive ( false );

		Cursor.visible = false;

		m_mainCamera = Camera.main;

		m_mainCamera.transform.position = new Vector3 ( m_world.m_width / 2, m_world.m_height / 2 + ( m_world.m_height - 386 ), Camera.main.transform.position.z );

		m_background.transform.position = new Vector3 ( Camera.main.transform.position.x, Camera.main.transform.position.y, m_background.transform.position.z );

		m_foreground.transform.position = new Vector3 ( m_background.transform.position.x - (int)(m_world.m_width/2), m_background.transform.position.y - (int)(m_world.m_height/2) - 28, m_foreground.transform.position.z );

		SC.SetUpWorld ();

		m_world.SpawnPlayer1 ();

		for ( int i = 0; i < World.m_maxNumberOfAI; i++ )
		{
			m_world.SpawnNPC ();
		}

	}

}
