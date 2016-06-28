using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public AsteroidSpawnController AsteroidSpawnController;

	// Use this for initialization
	void Start () {
		AsteroidSpawnController.SpawnAsteroids (4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
