using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {
	public float Speed;
	public float TimeToChangeDirection;

	private float startTimeMoving;

	public GameObject UFOExplosion;


	void Start ()
	{
		startTimeMoving = Time.time;
		ChangeDirection ();
	}

	void Update() {
		CheckBoundaries ();
		if (Time.time - startTimeMoving > TimeToChangeDirection) {
			startTimeMoving = Time.time;
			ChangeDirection ();
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

		Instantiate (UFOExplosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}

