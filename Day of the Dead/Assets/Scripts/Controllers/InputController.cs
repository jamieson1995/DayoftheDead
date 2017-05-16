using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	Vector3 m_mousePos;

	void Update ()
	{

		if ( WorldController.instance.m_world == null )
		{
			return;
		}

		m_mousePos = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		Tile t = GetTileUnderMouse();

		if ( t == null )
		{
			Debug.Log("Tile is null.");
			return;
		}

		if ( WorldController.instance.m_world.m_allCharacters [ 0 ].IsTileUnderCharacter ( t ) )
		{
			Debug.Log("Mouse is over charcater");
		}

	}

	public Tile GetTileUnderMouse ()
	{
		if ( WorldController.instance.m_world == null )
		{
			return null;
		}

		return WorldController.instance.m_world.GetTileAt (
			Mathf.FloorToInt ( m_mousePos.x + 0.5f ),
			Mathf.FloorToInt ( m_mousePos.y + 0.5f ) );
	}
}
