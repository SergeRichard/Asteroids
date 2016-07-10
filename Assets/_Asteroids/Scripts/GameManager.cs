using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public AsteroidSpawnController AsteroidSpawnController;
	public enum GameStates {GameOn, PlayerKilled, Invincible, GameOver, NextLevel};
	public static GameStates GameState;
	public static int GameScore;
	public float TimeToPlayNextBeat = 1f;
	public AudioClip BeatSound1;
	public AudioClip BeatSound2;
	public AudioSource audioSource;
	public MessageController MessageController;

	private bool Beat1 = true;
	private float startTime;
	private int NumOfAsteroids = 2;
	private int NextScoreForLife = 10000;


	// Use this for initialization
	void Start () {
		AsteroidSpawnController.SpawnAsteroids (NumOfAsteroids);
		GameState = GameStates.Invincible;
		GameScore = 0;
		startTime = Time.time;
		if (audioSource == null) {
			audioSource = GetComponent<AudioSource> ();
		}
		//PlayerPrefs.DeleteKey ("High Score");
		MessageController.HighScoreValue.text = PlayerPrefs.GetInt ("High Score").ToString();
	}
	void StartNextLevel() {
		AsteroidSpawnController.SpawnAsteroids (++NumOfAsteroids);

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
		if (GameState == GameStates.GameOn && Player.PlayerLife > 0) {
			if (AsteroidSpawnController.AsteroidCount() == 0) {
				StartNextLevel ();
			}
		}
		if (GameScore >= NextScoreForLife) {
			Player.PlayerLife++;
			MessageController.DisplayLives ();
			NextScoreForLife += 10000;
		}
	}
}
