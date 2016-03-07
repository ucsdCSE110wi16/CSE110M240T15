using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuScript : MonoBehaviour {
	
	private bool paused = false;
	public Button pause_b, resume_b;
	
	public GameObject cat, dino, duck, elephant, sheep, turtle;
	
	public void Start() {
		cat.active = false;
		dino.active = false;
		duck.active = false;
		elephant.active = false;
		sheep.active = false;
		turtle.active = false;
		switch(PlayerPrefs.GetString("current")) {
			case "cat":
				cat.active = true;
				return;
			case "dino":
				dino.active = true;
				return;
			case "duck":
				duck.active = true;
				return;
			case "elephant":
				elephant.active = true;
				return;
			case "sheep":
				sheep.active = true;
				return;
			case "turtle":
				turtle.active = true;
				return;
		}
	}
	
	/* pauses game, quits if game is already paused */
	public void pause() {
		if (!paused) {
			Time.timeScale = 0;
//			pause_b.GetComponentInChildren<Text>().text =
//					"resume";
			resume_b.gameObject.SetActive(true);
			paused = true;
		}
		else {
			resume();
		}
	}
	
	/* resumes game when it is paused */
	public void resume() {
//		pause_b.GetComponentInChildren<Text>().text =
//				"Pause";
		resume_b.gameObject.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}
	public void quit() {
		resume ();
		SceneManager.LoadScene("main_menu");
	}
}