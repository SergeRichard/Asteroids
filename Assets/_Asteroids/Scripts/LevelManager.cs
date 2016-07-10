using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public GameObject FadePlane;
	private string _name;

	void Start() {
		if (FadePlane != null) {
			StartCoroutine (FadeIn ());
		}
	}

	public void LoadLevel(string name)
	{
		//Application.LoadLevel (name);
		_name = name;
		SceneManager.LoadScene (name);
	}
	public void FadeOutThenLoadLevel(string name) {
		_name = name;
		StartCoroutine (FadeOut ());

	}
	IEnumerator FadeOut() {
		float fadeAmount =0;

		while (fadeAmount <= 1) {
			fadeAmount += .02f;
			FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeAmount);
			yield return new WaitForSeconds (.01f);
		}
		SceneManager.LoadScene (_name);

	}
	IEnumerator FadeIn() {
		float fadeAmount = 1;

		while (fadeAmount >= 0) {
			fadeAmount -= .02f;
			FadePlane.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, fadeAmount);
			yield return new WaitForSeconds (.01f);
		}

	}
	public void QuitGame()
	{
		Application.Quit ();
	}
}
