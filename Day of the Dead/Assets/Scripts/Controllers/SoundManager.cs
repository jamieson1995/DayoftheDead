using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	public AudioClip SmokeSoundAC;

	public AudioClip SniperShotAC;

	public AudioClip ZombieBiteAC;

	public AudioClip ZombieDeathAC;

	public AudioClip PartyMusicAC;

	public AudioClip CrowdSoundsAC;

	public AudioClip ZoomInAC;

	public AudioClip ZoomOutAC;

	public AudioClip BombAC;

	AudioSource SmokeSoundAS;

	AudioSource SniperShotAS;

	AudioSource ZombieBiteAS;

	AudioSource ZombieDeathAS;

	AudioSource PartyMusicAS;

	AudioSource CrowdSoundsAS;

	AudioSource ZoomInAS;

	AudioSource ZoomOutAS;

	AudioSource BombAS;

	void Start()
	{
		SmokeSoundAS = gameObject.AddComponent<AudioSource>();
		SmokeSoundAS.clip = SmokeSoundAC;

		SniperShotAS = gameObject.AddComponent<AudioSource>();
		SniperShotAS.clip = SniperShotAC;

		ZombieBiteAS = gameObject.AddComponent<AudioSource>();
		ZombieBiteAS.clip = ZombieBiteAC;

		ZombieDeathAS = gameObject.AddComponent<AudioSource>();
		ZombieDeathAS.clip = ZombieDeathAC;

		PartyMusicAS = gameObject.AddComponent<AudioSource>();
		PartyMusicAS.clip = PartyMusicAC;

		CrowdSoundsAS = gameObject.AddComponent<AudioSource>();
		CrowdSoundsAS.clip = CrowdSoundsAC;
		CrowdSoundsAS.loop = true;

		ZoomInAS = gameObject.AddComponent<AudioSource>();
		ZoomInAS.clip = ZoomInAC;

		ZoomOutAS = gameObject.AddComponent<AudioSource>();
		ZoomOutAS.clip = ZoomOutAC;

		BombAS = gameObject.AddComponent<AudioSource>();
		BombAS.clip = BombAC;

	}

	void Update ()
	{
		if ( SmokeSoundAS.isPlaying )
		{
			SmokeSoundAS.volume -= 0.3f * Time.deltaTime;
		}

		if ( CrowdSoundsAS.isPlaying )
		{
			CrowdSoundsAS.volume = (float)WorldController.instance.m_world.m_currNumberOfAI / 100.0f;
		}
	}

	public void PlaySmokeSoundAS ()
	{
		SmokeSoundAS.volume = 1.0f;
		SmokeSoundAS.Play();
	}

	public void PlaySniperShotAS()
	{
		SniperShotAS.volume = 0.5f;
		SniperShotAS.Play();
	}

	public void PlayZombieBiteAS()
	{
		ZombieBiteAS.Play();
	}

	public void PlayZombieDeathAS()
	{
		ZombieDeathAS.Play();
	}

	public void PlayPartyMusicAS()
	{
		PartyMusicAS.Play();
	}

	public void PlayCrowdSoundsAS()
	{
		CrowdSoundsAS.Play();
	}

	public void PlayZoomInAS()
	{
		ZoomInAS.Play();
	}

	public void PlayZoomOutAS()
	{
		ZoomOutAS.Play();
	}

	public void PlayBombAS()
	{
		BombAS.Play();
	}

	
}
