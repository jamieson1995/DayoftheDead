using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController instance { get; protected set; }

	public World m_world { get; protected set; }

	public Sprite m_tileSprite;

	public SpriteController SC;

	public GameObject Button;

	public Texture2D m_crossHair;

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

		Button.SetActive ( false );

		Cursor.SetCursor(m_crossHair, Vector2.zero, CursorMode.Auto);

		m_world = new World ( 810, 400 );

		Camera.main.transform.position = new Vector3 ( m_world.m_width / 2, m_world.m_height / 2 + (m_world.m_height - 386) , Camera.main.transform.position.z );

		SC.SetUpWorld ();

		for ( int x = 0; x < m_world.m_width; x++ )
		{
			GameObject tile_go1 = new GameObject ();
			tile_go1.transform.position = new Vector3 ( x, 0, 0 );
			SpriteRenderer sr1 = tile_go1.AddComponent<SpriteRenderer> ();
			sr1.sprite = m_tileSprite;
			tile_go1.layer = 10;

			GameObject tile_go2 = new GameObject ();
			tile_go2.transform.position = new Vector3 ( x, m_world.m_height, 0 );
			SpriteRenderer sr2 = tile_go2.AddComponent<SpriteRenderer> ();
			sr2.sprite = m_tileSprite;
			tile_go2.layer = 10;
		}

		for ( int y = 0; y < m_world.m_height; y++ )
		{
			GameObject tile_go1 = new GameObject ();
			tile_go1.transform.position = new Vector3 ( 0, y, 0 );
			SpriteRenderer sr1 = tile_go1.AddComponent<SpriteRenderer> ();
			sr1.sprite = m_tileSprite;
			tile_go1.layer = 10;

			GameObject tile_go2 = new GameObject ();
			tile_go2.transform.position = new Vector3 ( m_world.m_width, y, 0 );
			SpriteRenderer sr2 = tile_go2.AddComponent<SpriteRenderer> ();
			sr2.sprite = m_tileSprite;
			tile_go2.layer = 10;
		}

		m_world.SpawnPlayer1 ();

		for ( int i = 0; i < 5; i++ )
		{
			m_world.SpawnNPC ();
		}

	}

}
