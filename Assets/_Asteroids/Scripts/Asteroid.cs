using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject AsteroidMedium;

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.tag == "Laser") {
			GameObject asteroid1 = (GameObject)Instantiate (AsteroidMedium, transform.position, Quaternion.identity);
			asteroid1.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity - new Vector3(0,0,Random.Range(0.5f,1.5f));
			asteroid1.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity + new Vector3(0,0,Random.Range(0.5f,1.5f));


			GameObject asteroid2 = (GameObject)Instantiate (AsteroidMedium, transform.position, Quaternion.identity);
			asteroid2.GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity + new Vector3(0,0,Random.Range(0.5f,1.5f));
			asteroid2.GetComponent<Rigidbody> ().angularVelocity = GetComponent<Rigidbody> ().angularVelocity + new Vector3(0,0,Random.Range(0.5f,1.5f));

			Destroy (gameObject);

		}

	}

}
