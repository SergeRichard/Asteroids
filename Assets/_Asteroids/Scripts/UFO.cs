using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {

	public AudioClip UFOSound;
	public AudioClip UFOExplosionSound;
	public float Speed;
	public float TimeToChangeDirection;
	public GameObject UFOExplosion;
	public GameObject UFOBolt;
	public float intervalShootingTime;

	private float startTimeMoving;
	private float startShootingTime;
	private enum ShootingDirections {up, right, down, left}
	private ShootingDirections shootingDirection = ShootingDirections.up;


	void Start ()
	{
		startTimeMoving = Time.time;
		startShootingTime = Time.time;
		ChangeDirection ();
	}

	void Update() {
		if (GameManager.GameState == GameManager.GameStates.GameOver && GetComponent<AudioSource> ().isPlaying) {
			GetComponent<AudioSource> ().Stop ();
		}

		CheckBoundaries ();
		if (Time.time - startTimeMoving > TimeToChangeDirection) {
			startTimeMoving = Time.time;
			ChangeDirection ();
		}
		if (Time.time - startShootingTime > intervalShootingTime) {
			startShootingTime = Time.time;
			Shoot();
		}
	}
	void Shoot() {
		switch (shootingDirection) {
		case ShootingDirections.up:
			GameObject go = (GameObject)Instantiate (UFOBolt, transform.position, Quaternion.identity);
			go.GetComponent<Rigidbody> ().velocity = new Vector3 (3, 0, 20);
			go.transform.rotation = Quaternion.LookRotation (go.GetComponent<Rigidbody> ().velocity);
			shootingDirection = ShootingDirections.right;
			break;
		case ShootingDirections.right:
			GameObject go2 = (GameObject)Instantiate (UFOBolt, transform.position, Quaternion.identity);
			go2.GetComponent<Rigidbody> ().velocity = new Vector3 (20, 0, -3);
			go2.transform.rotation = Quaternion.LookRotation (go2.GetComponent<Rigidbody> ().velocity);
			shootingDirection = ShootingDirections.down;
			break;
		case ShootingDirections.down:
			GameObject go3 = (GameObject)Instantiate (UFOBolt, transform.position, Quaternion.identity);
			go3.GetComponent<Rigidbody> ().velocity = new Vector3 (-3, 0, -20);
			go3.transform.rotation = Quaternion.LookRotation (go3.GetComponent<Rigidbody> ().velocity);
			shootingDirection = ShootingDirections.left;
			break;
		case ShootingDirections.left:
			GameObject go4 = (GameObject)Instantiate (UFOBolt, transform.position, Quaternion.identity);
			go4.GetComponent<Rigidbody> ().velocity = new Vector3 (-20, 0, 3);
			go4.transform.rotation = Quaternion.LookRotation (go4.GetComponent<Rigidbody> ().velocity);
			shootingDirection = ShootingDirections.up;
			break;

		}

	}
	void ChangeDirection() {
		if (UfoController.UfoDirection == UfoController.UfoDirections.Left) {
			MovingLeft ();

		} else {
			MovingRight ();

		}



	}
	void MovingLeft() {
		switch (Random.Range (0, 3)) {
		case 0:
			// Diagonal up, right
			GetComponent<Rigidbody> ().velocity = new Vector3 (-Speed, 0, Speed);

			break;
		case 1:
			// Diagonal down, right
			GetComponent<Rigidbody> ().velocity = new Vector3 (-Speed, 0, -Speed);
			break;

		case 2:
			GetComponent<Rigidbody>().velocity = -transform.right * Speed;
			break;

		}

	}
	void MovingRight() {
		switch (Random.Range (0, 3)) {
		case 0:
			// Diagonal up, right
			GetComponent<Rigidbody> ().velocity = new Vector3 (Speed, 0, Speed);

			break;
		case 1:
			// Diagonal down, right
			GetComponent<Rigidbody> ().velocity = new Vector3 (Speed, 0, -Speed);
			break;

		case 2:
			GetComponent<Rigidbody>().velocity = transform.right * Speed;
			break;

		}
	}
	void CheckBoundaries() {
		Rigidbody rigidBody = GetComponent<Rigidbody> ();

		if (transform.position.x > 7) {
			rigidBody.MovePosition(new Vector3(-7,transform.position.y, transform.position.z));
		}
		if (transform.position.x < -7) {
			rigidBody.MovePosition(new Vector3(7,transform.position.y, transform.position.z));
		}
		if (transform.position.z > 5.3f) {
			rigidBody.MovePosition(new Vector3(transform.position.x, transform.position.y, -5.3f));
		}
		if (transform.position.z < -5.3f) {
			rigidBody.MovePosition(new Vector3(transform.position.x, transform.position.y, 5.3f));
		}
	}
	void OnCollisionEnter(Collision collision) {
		UfoController.StartTimeSpawn = Time.time;

		if (collision.collider.tag == "Laser" || collision.collider.tag == "Player") {
			GameManager.GameScore += 200;
			FindObjectOfType<MessageController> ().RefreshScore ();
		}

		AudioSource.PlayClipAtPoint (UFOExplosionSound, Vector3.zero,1f);

		Instantiate (UFOExplosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}

