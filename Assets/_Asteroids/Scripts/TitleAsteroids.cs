using UnityEngine;
using System.Collections;

public class TitleAsteroids : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
		if (transform.position.y > 5.3f) {
			rigidBody.MovePosition(new Vector3(transform.position.x, -5.3f, transform.position.z));
		}
		if (transform.position.y < -5.3f) {
			rigidBody.MovePosition(new Vector3(transform.position.x, 5.3f, transform.position.z));
		}
	}
}
