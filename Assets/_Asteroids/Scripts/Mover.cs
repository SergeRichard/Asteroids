using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;

	void Start ()
	{
		transform.rotation = Random.rotation;
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
}
