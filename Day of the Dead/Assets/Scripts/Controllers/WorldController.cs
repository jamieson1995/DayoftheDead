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
		if ( m_world == null )
		{
			return;
		}

		m_world.Update ( Time.deltaTime );
	}

	public void CreateWorld ()
	{

		SM.PlayCrowdSoundsAS();

		Button.SetActive ( false );

		Cursor.visible = false;

		m_world = new World ( 810, 400 );

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
