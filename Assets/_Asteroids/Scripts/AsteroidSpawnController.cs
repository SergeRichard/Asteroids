using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawnController : MonoBehaviour {

	public GameObject[] AsteroidSpawnPoints;

	public GameObject Asteroid1;
	public GameObject Asteroid2;
	public GameObject Asteroid3;
	public GameObject Asteroids;

	private List<GameObject> asteroids = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}

	public void SpawnAsteroids(int numOfAsteroids) {
		for (int t = 0; t < numOfAsteroids; t++) {
			
			GameObject asteroid = (GameObject)Instantiate (Asteroid1, AsteroidSpawnPoints [Random.Range (0, 13)].transform.position, Quaternion.identity);
			asteroid.transform.parent = Asteroids.transform;
			asteroids.Add (asteroid);
		}

	}

	// Update is called once per frame
	void Update () {	

		foreach (var asteroid in asteroids) {
			if (asteroid != null)
				CheckBoundaries (asteroid);
		}
	}

	public int AsteroidCount() {
		return Asteroids.transform.childCount;
	}

	void CheckBoundaries(GameObject asteroid) {
		Rigidbody rigidBody = asteroid.GetComponent<Rigidbody> ();

		if (asteroid.transform.position.x > 7) {
			rigidBody.MovePosition(new Vector3(-7,asteroid.transform.position.y, asteroid.transform.position.z));
		}
		if (asteroid.transform.position.x < -7) {
			rigidBody.MovePosition(new Vector3(7,asteroid.transform.position.y, asteroid.transform.position.z));
		}
		if (asteroid.transform.position.z > 5.3f) {
			rigidBody.MovePosition(new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, -5.3f));
		}
		if (asteroid.transform.position.z < -5.3f) {
			rigidBody.MovePosition(new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, 5.3f));
		}
	}
}
