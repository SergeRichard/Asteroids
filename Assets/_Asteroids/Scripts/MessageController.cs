using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour {
	public delegate void GameOverComplete ();
	public static event GameOverComplete OnGameOverComplete;
	public GameObject FadePlane;
	public GameObject GameOverText;

	public GameObject[] Lives;

	// Use this for initialization
	void Start () {
		FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0);
		GameOverText.GetComponent<TextMesh> ().color = new Color (1, 1, 1, 0);
		DisplayLives ();
	}
	public void DisplayLives() {
		ResetLivesToInActive ();

		for(int i = 0; i < Player.PlayerLife; ++i) {
			Lives [i].SetActive (true);

		}

	}
	void ResetLivesToInActive() {
		foreach (GameObject life in Lives) {
			life.SetActive (false);

		}
	}
	public void ShowGameOver() {
		StartCoroutine (FadeInGameOver ());

	}
	IEnumerator FadeInGameOver() {
		float fadeAmount = 0;

		while (fadeAmount < 1) {
			fadeAmount += .01f;
			FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeAmount);
			GameOverText.GetComponent<TextMesh> ().color = new Color (1, 1, 1, fadeAmount);
			yield return new WaitForSeconds (.01f);
		}
		Invoke ("Complete", 3f);

	}
	void Complete() {
		if (OnGameOverComplete != null)
			OnGameOverComplete ();
	}
}
