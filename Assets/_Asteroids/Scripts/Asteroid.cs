using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject AsteroidMedium;
	public AudioClip LargeBang;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser" || collision.collider.tag == "UfoLaser") {
			GameObject asteroid1 = (GameObject)Instantiate (AsteroidMedium, transform.position, Quaternion.identity);
			asteroid1.transform.parent = transform.parent;
			asteroid1.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity - new Vector3 (0, 0, Random.Range (0.5f, 1.5f));
			asteroid1.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity * Random.Range(1f,3f);


			GameObject asteroid2 = (GameObject)Instantiate (AsteroidMedium, transform.position, Quaternion.identity);
			asteroid2.transform.parent = transform.parent;
			asteroid2.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity + new Vector3(0,0,Random.Range(0.5f,1.5f));
			asteroid2.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity * -Random.Range(1f,3f);

			if (collision.collider.tag == "Laser") {
				GameManager.GameScore += 20;
				FindObjectOfType<MessageController> ().RefreshScore ();
			}
			if (GameManager.GameState != GameManager.GameStates.GameOver)
				AudioSource.PlayClipAtPoint (LargeBang, Vector3.zero,1f);

			Destroy (gameObject);

		}

	}

}
