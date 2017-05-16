using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour {

	Dictionary<Character, GameObject> m_characterGameObjectMap;

	public Sprite[] m_sprites;

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
		GameObject char_GO = new GameObject ();

		m_characterGameObjectMap.Add ( _char, char_GO );
		char_GO.transform.position = new Vector3 ( _char.X, _char.Y, 0 );
		SpriteRenderer sr = char_GO.AddComponent<SpriteRenderer>();
		sr.sprite = m_sprites[0];

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

	}
}
