using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public AsteroidSpawnController AsteroidSpawnController;
	public enum GameStates {GameOn, PlayerKilled, Invincible, GameOver};
	public static GameStates GameState;
	public static int GameScore;
	public float TimeToPlayNextBeat = 1f;
	public AudioClip BeatSound1;
	public AudioClip BeatSound2;
	public AudioSource audioSource;

	private bool Beat1 = true;
	private float startTime;




	// Use this for initialization
	void Start () {
		AsteroidSpawnController.SpawnAsteroids (4);
		GameState = GameStates.Invincible;
		GameScore = 0;
		startTime = Time.time;
		if (audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameState != GameStates.GameOver) {
			if (Beat1 && Time.time - startTime >= TimeToPlayNextBeat) {
				Beat1 = false;
				startTime = Time.time;
				audioSource.clip = BeatSound1;
				audioSource.Play ();
			} else if (!Beat1 && Time.time - startTime >= TimeToPlayNextBeat) {
				Beat1 = true;
				startTime = Time.time;
				audioSource.clip = BeatSound2;
				audioSource.Play ();
			}
		}

	}
}
