using UnityEngine;
using System.Collections;

public class UFOLazer : MonoBehaviour {
	Rigidbody rigidBody;
	public float speed;
	public float TimeToLive = 1f;

	private float currentTime;

	void Start ()
	{
		//GetComponent<Rigidbody>().velocity = transform.forward * speed;
		currentTime = Time.time;
		rigidBody = GetComponent<Rigidbody> ();
	}
	void Update() {
		float totalTime = Time.time - currentTime;

		if (totalTime > TimeToLive) {
			Destroy (gameObject);
		}

		CheckBoundaries ();
	}
	void OnCollisionEnter(Collision collision) {

		Destroy (gameObject);


	}
	void CheckBoundaries() {
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
}

