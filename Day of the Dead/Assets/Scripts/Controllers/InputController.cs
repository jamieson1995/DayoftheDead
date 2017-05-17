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

		m_tileUnderMouse = GetTileUnderMouse ();

		ProcessKeyboardInput ();

		if ( m_world.m_SniperCam )
		{
			CameraFollowMouse ();

			Cursor.visible = false;
			GameObject crossHair;
			if ( GameObject.Find ( "Cross Hair" ) == null )
			{
				crossHair = new GameObject ();
				crossHair.name = "Cross Hair";
				SpriteRenderer sr = crossHair.AddComponent<SpriteRenderer> ();
				sr.sprite = WorldController.instance.m_crossHairSp;
			}
			else
			{
				crossHair = GameObject.Find ( "Cross Hair" );

			}

			crossHair.transform.position = new Vector3 ( WorldController.instance.IP.m_mousePos.x, WorldController.instance.IP.m_mousePos.y, crossHair.transform.position.z );

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
		if ( Input.GetKey ( KeyCode.Space ) )
		{
			//if ( m_world.m_player1.m_canBite )
			//{
			//	m_world.m_player1.Bite();
			//}
			m_world.SwitchSniperCameraMode ( 2 );
		}
		if ( Input.GetKey ( KeyCode.LeftShift ) )
		{
			m_world.m_powerUpActivatedThisFrame = true;
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
