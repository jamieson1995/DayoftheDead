using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public Vector3 m_mousePos;

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

		m_mousePos = WorldController.instance.m_mainCamera.ScreenToWorldPoint ( Input.mousePosition );

		WorldController.instance.m_cursor.transform.position = new Vector3 ( m_mousePos.x, m_mousePos.y, WorldController.instance.m_cursor.transform.position.z );

		m_tileUnderMouse = GetTileUnderMouse ();

		ProcessKeyboardInput ();

		if ( m_world.m_SniperCam )
		{
			CameraFollowMouse ();

			WorldController.instance.m_cursor.SetActive ( false );

			GameObject crossHair = WorldController.instance.m_smallCrosshair;

			crossHair.SetActive ( true );

			crossHair.transform.position = new Vector3 ( WorldController.instance.IC.m_mousePos.x, WorldController.instance.IC.m_mousePos.y, crossHair.transform.position.z );

		}
		else
		{
			if ( WorldController.instance.m_cursor.activeSelf == false )
			{
				WorldController.instance.m_cursor.SetActive(true);
			}
		}

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
		if ( Input.GetKey ( KeyCode.Space ) ) //FIXME
		{
			if ( m_world.m_player1.m_canBite )
			{
				m_world.m_player1.Bite ();
			}
		}
		if ( Input.GetKey ( KeyCode.LeftShift ) )
		{
			m_world.m_powerUpActivatedThisFrame = true;
			if ( m_world.m_powerUpNum > 0 )
			{
				switch ( m_world.m_powerUpNum )
				{

					case 1:
						m_world.m_player1.SmokeBomb ();
						break;

					case 2:
						m_world.m_player1.Zoom ();
						break;

					case 3:
						m_world.m_player1.Alarm ();
						break;

					case 4:
						m_world.m_player1.InfectionBombDropped ();
						break;

				}
				m_world.m_powerUpNum = 0;
			}
			else
			{
				Debug.Log("No power-up to activate!");
			}
		}
	}

	void CameraFollowMouse()
	{
		WorldController.instance.m_sniperCamera.transform.position = m_mousePos;
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
