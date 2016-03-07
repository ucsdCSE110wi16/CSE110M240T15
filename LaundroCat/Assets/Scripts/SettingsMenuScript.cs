using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour {

	public Sprite regular_back;
	public Sprite other_back;
	public Image background;

	public Button musicButton;
	public Slider musicSlider;
	public Button reset_b;
	public Button credit_b;
	
	public bool credit_mode = false;

	public void Start() {
		musicSlider.value = PlayerPrefs.GetFloat("volume");
	}
	
	public void goBack() {
		if (credit_mode) {
			credit_mode = false;
		//	background.sprite = regular_back;
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
		credit_mode = true;
	//	background.sprite = other_back;
		//creditsPanel.SetActive(true);
	}
}