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

	public bool P1MoveScoreText;

	public bool P2MoveScoreText;

	float m_timerMax = 0.02f;

	float m_timerCurr = 0.0f;

	void Start ()
	{
		P1ScoreChangeGOOriginalPos = P1ScoreChangeGO.transform.position;
		P2ScoreChangeGOOriginalPos = P2ScoreChangeGO.transform.position;
		P1ScoreChangeEndPos = new Vector3 ( P1ScoreChangeGO.transform.position.x + 30, P1ScoreChangeGO.transform.position.y + 80, P1ScoreChangeGO.transform.position.z );
		P2ScoreChangeEndPos = new Vector3 ( P2ScoreChangeGO.transform.position.x + 30, P2ScoreChangeGO.transform.position.y + 80, P2ScoreChangeGO.transform.position.z );
	}

	public void PlayerScoreChange ( int _player, int _amount )
	{
		if ( _amount < 0 )
		{
			if ( _player == 1 )
			{
				P1ScoreChangeGO.SetActive(true);
				P1MoveScoreText = true;
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.red;
				P1ScoreChangeGO.GetComponent<Text> ().text = _amount.ToString();
			}
			else
			{
				P2ScoreChangeGO.SetActive(true);
				P2MoveScoreText = true;
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.red;
				P2ScoreChangeGO.GetComponent<Text> ().text = _amount.ToString();
			}
		}
		else if ( _amount > 0 )
		{
			if ( _player == 1 )
			{
				P1ScoreChangeGO.SetActive(true);
				P1MoveScoreText = true;
				P1ScoreChangeGO.GetComponent<Text> ().color = Color.green;
				P1ScoreChangeGO.GetComponent<Text> ().text = "+" + _amount.ToString();
			}
			else
			{
				P2ScoreChangeGO.SetActive(true);
				P2MoveScoreText = true;
				P2ScoreChangeGO.GetComponent<Text> ().color = Color.green;
				P2ScoreChangeGO.GetComponent<Text> ().text = "+" + _amount.ToString();
			}
		}
	}

	void Update ()
	{
		if ( P1MoveScoreText )
		{
			if ( P1ScoreChangeGO.transform.position.y >= P1ScoreChangeEndPos.y && P1ScoreChangeGO.transform.position.x >= P1ScoreChangeEndPos.x )
			{
				P1ScoreChangeGO.SetActive ( false );
				P1MoveScoreText = false;
				P1ScoreChangeGO.transform.position = P1ScoreChangeGOOriginalPos;
				P1ScoreChangeGO.GetComponent<Text> ().fontSize = 30;
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

				if ( m_timerCurr >= m_timerMax )
				{
					if ( P1ScoreChangeGO.GetComponent<Text> ().fontSize >= 15 )
					{
						P1ScoreChangeGO.GetComponent<Text> ().fontSize -= 1;
					}

					m_timerCurr = 0.0f;
				}
				else
				{
					m_timerCurr += Time.deltaTime;
				}


			}


		}

		if ( P2MoveScoreText )
		{
			if ( P2ScoreChangeGO.transform.position.y >= P2ScoreChangeEndPos.y && P2ScoreChangeGO.transform.position.x >= P2ScoreChangeEndPos.x )
			{
				P2ScoreChangeGO.SetActive ( false );
				P2MoveScoreText = false;
				P2ScoreChangeGO.transform.position = P2ScoreChangeGOOriginalPos;
				P2ScoreChangeGO.GetComponent<Text> ().fontSize = 30;
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

				if ( m_timerCurr >= m_timerMax )
				{
					if ( P2ScoreChangeGO.GetComponent<Text> ().fontSize >= 15 )
					{
						P2ScoreChangeGO.GetComponent<Text> ().fontSize -= 1;
					}

					m_timerCurr = 0.0f;
				}
				else
				{
					m_timerCurr += Time.deltaTime;
				}


			}


		}
	}

}
