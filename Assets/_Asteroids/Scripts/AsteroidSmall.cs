using UnityEngine;
using System.Collections;

public class AsteroidSmall : MonoBehaviour {


	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser") {			

			Destroy (gameObject);

		}

	}
	void Update() {
		CheckBoundaries ();

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
}
