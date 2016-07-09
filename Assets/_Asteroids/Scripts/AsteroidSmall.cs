using UnityEngine;
using System.Collections;

public class AsteroidSmall : MonoBehaviour {

	public AudioClip SmallBang;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser" || collision.collider.tag == "UfoLaser") {	

			if (collision.collider.tag == "Laser") {
				GameManager.GameScore += 100;
				FindObjectOfType<MessageController> ().RefreshScore ();
			}

			if (GameManager.GameState != GameManager.GameStates.GameOver)
				AudioSource.PlayClipAtPoint (SmallBang, Vector3.zero,1f);

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
