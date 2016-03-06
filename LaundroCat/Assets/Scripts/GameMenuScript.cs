using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenuScript : MonoBehaviour {
	
	private bool paused = false;
	public Button pause_b, resume_b;
	
	/* pauses game, quits if game is already paused */
	public void pause() {
		if (!paused) {
			Time.timeScale = 0;
			pause_b.GetComponentInChildren<Text>().text =
					"Exit Game";
			resume_b.gameObject.SetActive(true);
			paused = true;
		}
		else {
			resume();
			SceneManager.LoadScene("main_menu");
		}
	}
	
	/* resumes game when it is paused */
	public void resume() {
		pause_b.GetComponentInChildren<Text>().text =
				"Pause";
		resume_b.gameObject.SetActive(false);
		Time.timeScale = 1;
		paused = false;
	}
}