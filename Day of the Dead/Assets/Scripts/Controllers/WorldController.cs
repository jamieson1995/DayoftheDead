using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

	public static WorldController instance { get; protected set; }

	public World m_world { get; protected set; }

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

	void Start () {
		m_world = new World();
	}
	
	void Update () {
		
	}
}
