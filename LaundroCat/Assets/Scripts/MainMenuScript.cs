using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public void loadGame() {
		SceneManager.LoadScene("test_AndyNathan");
	}
	
	public void loadStore() {
		SceneManager.LoadScene("store_menu");
	}
	
	public void loadSettings() {
		SceneManager.LoadScene("settings_menu");
	}
}
