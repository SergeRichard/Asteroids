using UnityEngine;
using System.Collections;

public class SuicideTimer : MonoBehaviour {
	private float startTime;
	public float ExpireTime = 2f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime >= ExpireTime) {
			Destroy (gameObject);
		}
	}
}
