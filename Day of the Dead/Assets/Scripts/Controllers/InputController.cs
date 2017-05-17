using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	Vector3 m_mousePos;

	Tile m_tileUnderMouse;

	bool ignoreMouse;

	World m_world;

	void Update ()
	{

		ignoreMouse = false;

		if ( WorldController.instance.m_world == null )
		{
			return;
		}
		else if ( m_world == null )
		{
			m_world = WorldController.instance.m_world;
		}

		m_mousePos = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		m_tileUnderMouse = GetTileUnderMouse ();

		ProcessKeyboardInput ();

		if ( m_tileUnderMouse == null )
		{
			ignoreMouse = true;
		}

		if ( ignoreMouse )
		{
			return;
		}

		if ( Input.GetMouseButtonDown ( 0 ) )
		{
			m_world.SniperShot ( 1, m_tileUnderMouse );
		}

		if ( Input.GetMouseButtonDown ( 1 ) )
		{
			m_world.SniperShot ( 2, m_tileUnderMouse );
		}
	}

	void ProcessKeyboardInput ()
	{
		if ( Input.GetKey ( KeyCode.D ) )
		{
			m_world.m_player1.MoveEast ( 0 );
		}
		if ( Input.GetKey ( KeyCode.W ) )
		{
			m_world.m_player1.MoveNorth ( 0 );
		}
		if ( Input.GetKey ( KeyCode.S ) )
		{
			m_world.m_player1.MoveSouth ( 0 );
		}
		if ( Input.GetKey ( KeyCode.A ) )
		{
			m_world.m_player1.MoveWest ( 0 );
		}
		if ( Input.GetKey ( KeyCode.Space ) )
		{
			if ( m_world.m_player1.m_canBite )
			{
				m_world.m_player1.Bite();
			}
		}
	}

	public Tile GetTileUnderMouse ()
	{
		if ( m_world == null )
		{
			return null;
		}

		return m_world.GetTileAt (
			Mathf.FloorToInt ( m_mousePos.x + 0.5f ),
			Mathf.FloorToInt ( m_mousePos.y + 0.5f ) );
	}
}
