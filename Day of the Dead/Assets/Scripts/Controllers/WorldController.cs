using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController instance { get; protected set; }

	public World m_world { get; protected set; }

	public Sprite m_tileSprite;

	public SpriteController SP;

	public GameObject Button;

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

	public void CreateWorld ()
	{

		Button.SetActive(false);

		m_world = new World ( 810, 456 );

		for ( int x = 0; x < m_world.m_width; x++ )
		{
			for ( int y = 0; y < m_world.m_height; y++ )
			{
				//GameObject tileGO = new GameObject ();
				//tileGO.transform.position = new Vector3 ( x, y, 0 );
				//SpriteRenderer sr = tileGO.AddComponent<SpriteRenderer> ();
				//sr.sprite = m_tileSprite;
			}
		}

		Debug.Log(m_world.m_width/2 + "," + m_world.m_height/2);

		Camera.main.transform.position = new Vector3(m_world.m_width/2, m_world.m_height/2, Camera.main.transform.position.z);

		SP.SetUpWorld();

		m_world.SpawnCharcater ( m_world.GetTileAt ( m_world.m_width/2, m_world.m_height/2 ) );
	}

}
