using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour {

	public GameObject background, alt_background;

	public Button musicButton;
	public Slider musicSlider;
	public Button credit_b, reset_b;
	
	public Image credits, reset_warning;
	
	public bool alt_mode = false;

	public void Start() {
		musicSlider.value = PlayerPrefs.GetFloat("volume");
		credits.enabled = false;
		reset_warning.enabled = false;
	}
	
	public void goBack() {
		if (alt_mode) {
			alt_mode = false;
			showSettings();
		}
		else {
			SceneManager.LoadScene("main_menu");
		}
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
		musicSlider.value = PlayerPrefs.GetFloat("volume");
		PlayerPrefs.Save();
	}
	
	public void hideSettings() {
		background.active = false;
		alt_background.active = true;
		musicButton.gameObject.SetActive(false);
		musicSlider.gameObject.SetActive(false);
		credit_b.gameObject.SetActive(false);
		reset_b.gameObject.SetActive(false);
	}
	
	public void showSettings() {
		background.active = true;
		alt_background.active = false;
		musicButton.gameObject.SetActive(true);
		musicSlider.gameObject.SetActive(true);
		credit_b.gameObject.SetActive(true);
		reset_b.gameObject.SetActive(true);
		credits.enabled = false;
		reset_warning.enabled = false;
	}
	
	public void reset() {
		Debug.Log("Creating defaults!");
        PlayerPrefs.SetFloat("volume", (float)1.0);

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
		alt_mode = true;
		hideSettings();
		credits.enabled = true;
	}
	
	public void onReset() {
		if (!alt_mode) {
			alt_mode = true;
			hideSettings();
			reset_warning.enabled = true;
			reset_b.gameObject.SetActive(true);
		}
		else {
			reset();
			SceneManager.LoadScene("main_menu");
		}
	}
}