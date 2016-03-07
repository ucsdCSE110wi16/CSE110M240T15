using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour {

    public Button backButton;
	public Button musicButton;
	public Slider musicSlider;

	public void Start() {
		musicSlider.value = PlayerPrefs.GetFloat("volume");
	}
	
	public void goBack() {
		//Debug.Log(creditsPanel.activeSelf);
		//if (creditsPanel.activeSelf == true) {
		//	creditsPanel.SetActive(false);
		//}
		//else {
			SceneManager.LoadScene("main_menu");
		//}
	}

    public void setMusicVolume()
    {
		AudioListener.volume = musicSlider.value;
        PlayerPrefs.SetFloat("volume", musicSlider.value);
        PlayerPrefs.Save();
    }
	
	public void muteAndUnMute() {
		if (PlayerPrefs.GetFloat("volume") == 0) {
			AudioListener.volume = (float)0.5;
			PlayerPrefs.SetFloat("volume", (float)0.5);
		}
		else {
			AudioListener.volume = (float)0;
			PlayerPrefs.SetFloat("volume", (float)0);
		}
		PlayerPrefs.Save();
	}

	
	public void reset() {
		Debug.Log("Creating defaults!");
        PlayerPrefs.SetFloat("music", (float)1.0);
        PlayerPrefs.SetFloat("sound", (float)1.0);

        PlayerPrefs.SetInt("socks", 0);

		PlayerPrefs.SetString("current", "cat");
        PlayerPrefs.SetString("cat", "true");
        PlayerPrefs.SetString("dino", "false");
        PlayerPrefs.SetString("duck", "false");
        PlayerPrefs.SetString("elephant", "false");
        PlayerPrefs.SetString("sheep", "false");
        PlayerPrefs.SetString("turtle", "false");
        PlayerPrefs.Save();
	}
	
	public void openCredits() {
		//creditsPanel.SetActive(true);
	}
}