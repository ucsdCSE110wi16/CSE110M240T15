using UnityEngine;
using UnityEngine.SceneManagement;
// using System.Collections;

public class SettingsMenuScript : MonoBehaviour {

    public GameObject backButton;
	public GameObject musicButton;
	public GameObject musicSlider;
	public GameObject soundsButton;
	public GameObject soundsSlider;
	public GameObject creditsPanel;

	public void goBack() {
		Debug.Log(creditsPanel.activeSelf);
		if (creditsPanel.activeSelf == true) {
			creditsPanel.SetActive(false);
		}
		else {
			SceneManager.LoadScene("main_menu");
		}
	}

    public void setMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
        PlayerPrefs.Save();
    }

    public void setSoundsVolume(float value)
    {
        PlayerPrefs.SetFloat("Sounds", value);
        PlayerPrefs.Save();
    }
	
	public void openCredits() {
		creditsPanel.SetActive(true);
	}
}
