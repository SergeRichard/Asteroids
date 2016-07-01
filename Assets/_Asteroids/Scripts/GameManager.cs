using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public AsteroidSpawnController AsteroidSpawnController;

	public enum GameStates {GameOn, PlayerKilled, Invincible, GameOver};

	public static GameStates GameState;



	// Use this for initialization
	void Start () {
		AsteroidSpawnController.SpawnAsteroids (4);
		GameState = GameStates.Invincible;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
