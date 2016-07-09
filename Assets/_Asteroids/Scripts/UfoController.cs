using UnityEngine;
using System.Collections;

public class UfoController : MonoBehaviour {
	public float TimeToSpawn = 8f;
	public enum UfoDirections { Right, Left};
	public static UfoDirections UfoDirection;
	public static float StartTimeSpawn;
	public GameObject UFO;


	void Start ()
	{
		StartTimeSpawn = Time.time;

		SetMoveDirection ();
	}
	
	void Update() {
		if (transform.childCount == 0) {
			SetMoveDirection ();
			if (Time.time - StartTimeSpawn >= TimeToSpawn) {

				float vertical = Random.Range (-6.5f, 6.5f);

				if (UfoDirection == UfoDirections.Left) {
					// Instantiate the UFO to the right side of the screen
					GameObject go = (GameObject)Instantiate(UFO,new Vector3(6.5f,0,vertical), Quaternion.Euler(new Vector3(90,0,0)));
					go.transform.parent = transform;
				} else {
					// Instantiate the UFO to the right side of the screen
					GameObject go = (GameObject)Instantiate(UFO,new Vector3(-6.5f,0,vertical), Quaternion.Euler(new Vector3(90,0,0)));
					go.transform.parent = transform;
				}
			}

		}

	}
	void SetMoveDirection() {
		if (Random.Range (0, 2) == 0) {
			UfoDirection = UfoDirections.Left;
		} else {
			UfoDirection = UfoDirections.Right;
		}

	}
}
