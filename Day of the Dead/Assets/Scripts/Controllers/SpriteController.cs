using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	public Dictionary<Character, GameObject> m_characterGameObjectMap;

	public GameObject m_character;

	public GameObject m_ammoCrate;

	public bool m_maskChanging;

	public float m_maskChangePerc = 0.0f;

	void Start()
	{
		m_characterGameObjectMap = new Dictionary<Character, GameObject>();
	}

	public void SetUpWorld()
	{
		World world = WorldController.instance.m_world;
		world.RegisterCharacterCreated(OnCharacterCreated);
	}

	void OnCharacterCreated ( Character _char )
	{
		if ( _char.isAmmoCrate )
		{
			GameObject Ammo_GO = (GameObject)Instantiate(m_ammoCrate);
			m_characterGameObjectMap.Add ( _char, Ammo_GO );
			Ammo_GO.transform.position = new Vector3 ( _char.X, _char.Y, 0 );
			Ammo_GO.GetComponent<SpriteRenderer>().sortingOrder = (Mathf.FloorToInt(_char.Y) - WorldController.instance.m_world.m_height) * -1;
			return;
		}

		GameObject char_GO = (GameObject)Instantiate(m_character);
		m_characterGameObjectMap.Add ( _char, char_GO );

		_char.RegisterOnMovedCallback( OnCharacterMoved );

	}

	void OnCharacterMoved ( Character _char )
	{

		if ( m_characterGameObjectMap.ContainsKey ( _char ) == false )
		{
			Debug.LogError("Trying to change visuals of a character not in our map.");
		}

		GameObject char_GO = m_characterGameObjectMap[_char];

		char_GO.transform.position = new Vector3 ( _char.X, _char.Y, 0 );

		char_GO.GetComponent<SpriteRenderer>().sortingOrder = (Mathf.FloorToInt(_char.Y) - WorldController.instance.m_world.m_height) * -1;
		char_GO.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = char_GO.GetComponent<SpriteRenderer>().sortingOrder + 1;
	}

	void Update ()
	{
		if ( WorldController.instance.m_world == null )
		{
			return;
		}

		if ( WorldController.instance.m_world.m_player1.m_changeMask )
		{
			m_maskChanging = true;
		}
		else
		{
			m_maskChanging = false;
		}

		if ( m_maskChanging )
		{
			m_characterGameObjectMap [ WorldController.instance.m_world.m_player1 ].transform.GetChild ( 0 ).gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp ( Color.white, new Color ( 0.0f, 255.0f, 0.0f, 255.0f ), m_maskChangePerc );
			m_maskChangePerc += 0.1f * Time.deltaTime;
			if ( m_maskChangePerc >= 100 )
			{
				m_maskChangePerc = 100;
			}
		}
		else
		{
			m_characterGameObjectMap [ WorldController.instance.m_world.m_player1 ].transform.GetChild ( 0 ).gameObject.GetComponent<SpriteRenderer> ().color = Color.Lerp ( m_characterGameObjectMap [ WorldController.instance.m_world.m_player1 ].transform.GetChild ( 0 ).gameObject.GetComponent<SpriteRenderer> ().color, Color.white, m_maskChangePerc );
			m_maskChangePerc -= 0.1f * Time.deltaTime;
			if ( m_maskChangePerc <= 0 )
			{
				m_maskChangePerc = 0;
			}
		}
	}

	public void ChangeMaskColour()
	{
		m_maskChanging = true;
	}
}
