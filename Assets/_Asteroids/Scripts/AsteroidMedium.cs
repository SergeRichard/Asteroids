using UnityEngine;
using System.Collections;

public class AsteroidMedium : MonoBehaviour {

	public GameObject AsteroidSmall;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser") {
			GameObject asteroid1 = (GameObject)Instantiate (AsteroidSmall, transform.position, Quaternion.identity);
			asteroid1.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity - new Vector3(0,0,Random.Range(0.5f,1.5f));
			asteroid1.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity + new Vector3(0,0,Random.Range(0.5f,1.5f));


			GameObject asteroid2 = (GameObject)Instantiate (AsteroidSmall, transform.position, Quaternion.identity);
			asteroid2.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity + new Vector3(0,0,Random.Range(0.5f,1.5f));
			asteroid2.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity + new Vector3(0,0,Random.Range(0.5f,1.5f));

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
