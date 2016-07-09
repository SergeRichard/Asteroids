using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Rigidbody rigidBody;

	public AudioClip PlayerExplosionSound;
	public AudioClip LaserSound;
	public GameObject Laser;
	public Transform ShotSpawn;
	public Transform PlayerLazerSpawn;
	public float RotationSpeed;
	public float thrust;
	public float MaximumSpeed;
	public GameObject[] Thrusters;
	public float ShotDelay = .5f;
	public GameObject PlayerExplosion;
	public static int PlayerLife = 3;
	public MessageController MessageController;
	public LevelManager LevelManager;

	private bool blinkingCoroutine = false;
	private bool playerKilledCoroutine = false;
	private bool fadeGameOverCoroutine = false;
	private float currentTime;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();

		currentTime = Time.time;

		MessageController.DisplayLives ();
		PlayerLife = 3;
	}
	
	// Update is called once per frame
	void Update () {
		switch (GameManager.GameState) {
		case GameManager.GameStates.GameOn:
			CheckInput ();
			break;
		case GameManager.GameStates.Invincible:
			CheckInput ();
			if (!blinkingCoroutine) {
				GetComponent<BoxCollider> ().enabled = false;

				if (PlayerLife == 0) {
					GameManager.GameState = GameManager.GameStates.GameOver;
				} else {
					StartCoroutine (PlayerShipBlinking ());
				}
			}
			break;
		case GameManager.GameStates.PlayerKilled:
			if (!playerKilledCoroutine) {
				StartCoroutine (PlayerKilledCoroutine ());
				PlayerLife--;
				MessageController.DisplayLives ();

			}

			break;
		case GameManager.GameStates.GameOver:
			if (!fadeGameOverCoroutine) {
				fadeGameOverCoroutine = true;
				MessageController.ShowGameOver ();
				MessageController.OnGameOverComplete += GameOverComplete;
			}
			break;

		}

		CheckBoundaries ();
		CheckSpeed ();
	}
	void GameOverComplete() {
		fadeGameOverCoroutine = false;
		LevelManager.LoadLevel ("TitleScene");

	}
	void CheckInput() {
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

	}
	IEnumerator PlayerKilledCoroutine() {
		playerKilledCoroutine = true;

		Instantiate (PlayerExplosion, transform.position, Quaternion.identity);

		GetComponent<MeshRenderer> ().enabled = false;

		yield return new WaitForSeconds (2f);

		GameManager.GameState = GameManager.GameStates.Invincible;

		playerKilledCoroutine = false;
		ResetToCenter ();
	}
	IEnumerator PlayerShipBlinking() {
		blinkingCoroutine = true;

		float startTime = Time.time;

		while (Time.time - startTime < 4f) {
			GetComponent<MeshRenderer> ().enabled = false;
			yield return new WaitForSeconds (.1f);
			GetComponent<MeshRenderer> ().enabled = true;
			yield return new WaitForSeconds (.1f);
		}
		GameManager.GameState = GameManager.GameStates.GameOn;

		GetComponent<BoxCollider> ().enabled = true;

		blinkingCoroutine = false;
	}
	void InstantiateLaser() {
		AudioSource.PlayClipAtPoint (LaserSound, Vector3.zero,1f);
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
		AudioSource.PlayClipAtPoint (PlayerExplosionSound, Vector3.zero,1f);

		GameManager.GameState = GameManager.GameStates.PlayerKilled;
	}
	void ResetToCenter() {
		GetComponent<BoxCollider> ().enabled = false;


		rigidBody.velocity = new Vector3 (0, 0, 0);
		rigidBody.angularVelocity = new Vector3 (0, 0, 0);

		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
		transform.position = new Vector3 (0, 0, 0);


	}
}
