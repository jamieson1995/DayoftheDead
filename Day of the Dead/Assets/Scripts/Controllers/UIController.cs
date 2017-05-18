using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject P1ScoreChangeGO;

	Vector3 P1ScoreChangeGOOriginalPos;

	public GameObject P2ScoreChangeGO;

	Vector3 P2ScoreChangeGOOriginalPos;

	public Vector3 P1ScoreChangeEndPos;

	public Vector3 P2ScoreChangeEndPos;

	public bool P1MoveScoreChangeText;

	public bool P2MoveScoreChangeText;

	float m_P1ScoreTimerMax = 0.02f;

	float m_P1ScoreTimerCurr = 0.0f;

	float m_P2ScoreTimerMax = 0.02f;

	float m_P2ScoreTimerCurr = 0.0f;

	public GameObject P1ScoreText;

	public GameObject P2ScoreText;

	public GameObject RoundTimerGO;

	Text RoundTimerText;

	public int m_currScoreIncrease;

	void Start ()
	{
		P1ScoreChangeGOOriginalPos = P1ScoreChangeGO.transform.position;
		P2ScoreChangeGOOriginalPos = P2ScoreChangeGO.transform.position;
		P1ScoreChangeEndPos = new Vector3 ( P1ScoreChangeGO.transform.position.x + 30, P1ScoreChangeGO.transform.position.y + 80, P1ScoreChangeGO.transform.position.z );
		P2ScoreChangeEndPos = new Vector3 ( P2ScoreChangeGO.transform.position.x + 30, P2ScoreChangeGO.transform.position.y + 80, P2ScoreChangeGO.transform.position.z );
		RoundTimerText = RoundTimerGO.GetComponent<Text>();
	}

	public void PlayerScoreChange ( int _player, int _amount )
	{
		m_currScoreIncrease = _amount;

		if ( _amount < 0 )
		{
			if ( _player == 1 )
			{
				if (P1MoveScoreChangeText)
				{
					P1ScoreChangeGO.transform.position = P1ScoreChangeGOOriginalPos;
					P1ScoreChangeGO.GetComponent<Text>().fontSize = 30;
					WorldController.instance.m_world.m_P1Score += m_currScoreIncrease;
					P1ScoreText.GetComponent<Text>().text = "Player 1: " + WorldController.instance.m_world.m_P1Score.ToString();
				}
				P1ScoreChangeGO.SetActive(true);
				P1MoveScoreChangeText = true;
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.red;
				P1ScoreChangeGO.GetComponent<Text> ().text = _amount.ToString();
			}
			else
			{
				if (P2MoveScoreChangeText)
				{
					P2ScoreChangeGO.transform.position = P2ScoreChangeGOOriginalPos;
					P2ScoreChangeGO.GetComponent<Text>().fontSize = 30;
					WorldController.instance.m_world.m_P2Score += m_currScoreIncrease;
					P2ScoreText.GetComponent<Text>().text = "Player 2: " + WorldController.instance.m_world.m_P2Score.ToString();
				}
				P2ScoreChangeGO.SetActive(true);
				P2MoveScoreChangeText = true;
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.red;
				P2ScoreChangeGO.GetComponent<Text> ().text = _amount.ToString();
			}
		}
		else if ( _amount > 0 )
		{
			if ( _player == 1 )
			{
				if (P1MoveScoreChangeText)
				{
					P1ScoreChangeGO.transform.position = P1ScoreChangeGOOriginalPos;
					P1ScoreChangeGO.GetComponent<Text>().fontSize = 30;
					WorldController.instance.m_world.m_P1Score += m_currScoreIncrease;
					P1ScoreText.GetComponent<Text>().text = "Player 1: " + WorldController.instance.m_world.m_P1Score.ToString();
				}
				P1ScoreChangeGO.SetActive(true);
				P1MoveScoreChangeText = true;
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.green;
				P1ScoreChangeGO.GetComponent<Text> ().text = "+" + _amount.ToString();
			}
			else
			{
				if (P2MoveScoreChangeText)
				{
					P2ScoreChangeGO.transform.position = P2ScoreChangeGOOriginalPos;
					P2ScoreChangeGO.GetComponent<Text>().fontSize = 30;
					WorldController.instance.m_world.m_P2Score += m_currScoreIncrease;
					P2ScoreText.GetComponent<Text>().text = "Player 2: " + WorldController.instance.m_world.m_P2Score.ToString();
				}
				P2ScoreChangeGO.SetActive(true);
				P2MoveScoreChangeText = true;
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.green;
				P2ScoreChangeGO.GetComponent<Text> ().text = "+" + _amount.ToString();
			}
		}
	}

	void Update ()
	{

		if ( WorldController.instance.m_world != null )
		{
			RoundTimerText.text = "Time: " + WorldController.instance.m_world.m_roundTimerCurr.ToString ();
		}

		if ( P1MoveScoreChangeText )
		{
			if ( P1ScoreChangeGO.transform.position.y >= P1ScoreChangeEndPos.y && P1ScoreChangeGO.transform.position.x >= P1ScoreChangeEndPos.x )
			{
				P1ScoreChangeGO.SetActive ( false );
				P1MoveScoreChangeText = false;
				P1ScoreChangeGO.transform.position = P1ScoreChangeGOOriginalPos;
				P1ScoreChangeGO.GetComponent<Text> ().fontSize = 30;
				WorldController.instance.m_world.m_P1Score += m_currScoreIncrease;
				P1ScoreText.GetComponent<Text>().text = "Player 1: " + WorldController.instance.m_world.m_P1Score.ToString();
			}
			else
			{
				if ( P1ScoreChangeGO.transform.position.x < P1ScoreChangeEndPos.x )
				{
					P1ScoreChangeGO.transform.position = new Vector3 ( P1ScoreChangeGO.transform.position.x + ( 40 * Time.deltaTime ), P1ScoreChangeGO.transform.position.y, P1ScoreChangeGO.transform.position.z );
				}

				if ( P1ScoreChangeGO.transform.position.y < P1ScoreChangeEndPos.y )
				{
					P1ScoreChangeGO.transform.position = new Vector3 ( P1ScoreChangeGO.transform.position.x, P1ScoreChangeGO.transform.position.y + ( 100 * Time.deltaTime ), P1ScoreChangeGO.transform.position.z );
				}

				if ( m_P1ScoreTimerCurr >= m_P1ScoreTimerMax )
				{
					if ( P1ScoreChangeGO.GetComponent<Text> ().fontSize >= 15 )
					{
						P1ScoreChangeGO.GetComponent<Text> ().fontSize -= 1;
					}

					m_P1ScoreTimerCurr = 0.0f;
				}
				else
				{
					m_P1ScoreTimerCurr += Time.deltaTime;
				}
			}
		}

		if ( P2MoveScoreChangeText )
		{
			if ( P2ScoreChangeGO.transform.position.y >= P2ScoreChangeEndPos.y && P2ScoreChangeGO.transform.position.x >= P2ScoreChangeEndPos.x )
			{
				P2ScoreChangeGO.SetActive ( false );
				P2MoveScoreChangeText = false;
				P2ScoreChangeGO.transform.position = P2ScoreChangeGOOriginalPos;
				P2ScoreChangeGO.GetComponent<Text> ().fontSize = 30;
				WorldController.instance.m_world.m_P2Score += m_currScoreIncrease;
				P2ScoreText.GetComponent<Text>().text = "Player 2 : " + WorldController.instance.m_world.m_P2Score.ToString();
			}
			else
			{
				if ( P2ScoreChangeGO.transform.position.x < P2ScoreChangeEndPos.x )
				{
					P2ScoreChangeGO.transform.position = new Vector3 ( P2ScoreChangeGO.transform.position.x + ( 40 * Time.deltaTime ), P2ScoreChangeGO.transform.position.y, P2ScoreChangeGO.transform.position.z );
				}

				if ( P2ScoreChangeGO.transform.position.y < P2ScoreChangeEndPos.y )
				{
					P2ScoreChangeGO.transform.position = new Vector3 ( P2ScoreChangeGO.transform.position.x, P2ScoreChangeGO.transform.position.y + ( 100 * Time.deltaTime ), P2ScoreChangeGO.transform.position.z );
				}

				if ( m_P2ScoreTimerCurr >= m_P2ScoreTimerMax )
				{
					if ( P2ScoreChangeGO.GetComponent<Text> ().fontSize >= 15 )
					{
						P2ScoreChangeGO.GetComponent<Text> ().fontSize -= 1;
					}

					m_P2ScoreTimerCurr = 0.0f;
				}
				else
				{
					m_P2ScoreTimerCurr += Time.deltaTime;
				}


			}


		}
	}

}
