using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject P1ScoreChangeGO;

	Vector3 P1ScoreChangeGOOriginalPos;

	public GameObject P2ScoreChangeGO;

	Vector3 P2ScoreChangeGOOriginalPos;

	void Start()
	{
		P1ScoreChangeGOOriginalPos = P1ScoreChangeGO.transform.position;
		P2ScoreChangeGOOriginalPos = P2ScoreChangeGO.transform.position;
	}

	public void PlayerScoreChange ( int _player, int _amount )
	{
		if ( _amount < 0 )
		{
			if ( _player == 1 )
			{
				P1ScoreChangeGO.SetActive(true);
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.red;
			}
			else
			{
				P2ScoreChangeGO.SetActive(true);
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.red;
			}
		}
		else if ( _amount > 0 )
		{
			if ( _player == 1 )
			{
				P1ScoreChangeGO.SetActive(true);
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.green;
			}
			else
			{
				P2ScoreChangeGO.SetActive(true);
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.green;
			}
		}
	}

	void Update()
	{

	}

}
