using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour {




	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene ("Scene1", LoadSceneMode.Single);
			Debug.Log ("Scene loaded");
		}

	}
}
