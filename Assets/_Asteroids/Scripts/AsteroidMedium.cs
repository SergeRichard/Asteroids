using UnityEngine;
using System.Collections;

public class AsteroidMedium : MonoBehaviour {

	public GameObject AsteroidSmall;
	public AudioClip MediumBang;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser" || collision.collider.tag == "UfoLaser") {
			GameObject asteroid1 = (GameObject)Instantiate (AsteroidSmall, transform.position, Quaternion.identity);
			asteroid1.GetComponent<Rigidbody> ().velocity = (GetComponent<Rigidbody> ().velocity - new Vector3(0,0,Random.Range(0.5f,1.5f))) * Random.Range(1f,-1.25f);
			asteroid1.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity * Random.Range(1f,3f);


			GameObject asteroid2 = (GameObject)Instantiate (AsteroidSmall, transform.position, Quaternion.identity);
			asteroid2.GetComponent<Rigidbody> ().velocity = (GetComponent<Rigidbody> ().velocity + new Vector3(0,0,Random.Range(0.5f,1.5f))) * Random.Range(1f,-1.25f);
			asteroid2.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity * -Random.Range(1f,3f);

			if (collision.collider.tag == "Laser") {
				GameManager.GameScore += 50;
				FindObjectOfType<MessageController> ().RefreshScore ();
			}

			if (GameManager.GameState != GameManager.GameStates.GameOver)
				AudioSource.PlayClipAtPoint (MediumBang, Vector3.zero,1f);

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
