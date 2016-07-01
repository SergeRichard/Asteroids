using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody rigidBody;
	Transform transform;
	public GameObject Laser;
	public Transform ShotSpawn;
	public Transform PlayerLazerSpawn;
	public float RotationSpeed;
	public float thrust;
	public float MaximumSpeed;
	public GameObject[] Thrusters;
	public float ShotDelay = .5f;

	private bool blinkingCoroutine = false;
	private float currentTime;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		transform = GetComponent<Transform> ();
		currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.down * RotationSpeed * Time.deltaTime);

		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);

		}
		if ((Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow))) {
			rigidBody.AddForce (transform.forward * thrust * Time.deltaTime);

		} 
		if (Input.GetKey (KeyCode.Space) && (Time.time - currentTime > ShotDelay)) {
			currentTime = Time.time;
			InstantiateLaser ();
		}
		if (GameManager.GameState == GameManager.GameStates.Invincible) {
			if (!blinkingCoroutine) {
				GetComponent<BoxCollider> ().enabled = false;
				StartCoroutine (playerShipBlinking ());
			}
		}
		CheckBoundaries ();
		CheckSpeed ();
	}
	IEnumerator playerShipBlinking() {
		blinkingCoroutine = true;

		while (true) {
			GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds (.1f);
			GetComponent<MeshRenderer> ().enabled = true;
			yield return new WaitForSeconds (.1f);
		}
	}
	void InstantiateLaser() {
		GameObject laser = (GameObject)Instantiate (Laser, ShotSpawn.position, Quaternion.identity);
		laser.transform.parent = PlayerLazerSpawn;
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
	void CheckSpeed() {
		float speed = Vector3.Magnitude (rigidBody.velocity);

		if (speed > MaximumSpeed) {
			float brakeSpeed = speed - MaximumSpeed;  // calculate the speed decrease

			Vector3 normalisedVelocity = rigidBody.velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

			rigidBody.AddForce(-brakeVelocity);  // apply opposing brake force
		}
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log (collision.collider.name);

	}
}
