﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	
	// creates new save file if it doesn't exist
	void Start () {
	    if(PlayerPrefs.GetInt("socks", -1) == -1) {
			Debug.Log("Creating defaults!");
            PlayerPrefs.SetFloat("volume", (float)0.5);

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
		else {
			AudioListener.volume = PlayerPrefs.GetFloat("volume");
		}
	}
	
	// loads game scene
	public void loadGame() {
		SceneManager.LoadScene("main_sceneDEMO");
	}
	
	// loads store scene
	public void loadStore() {
		SceneManager.LoadScene("store_menu");
	}
	
	// loads setting scene
	public void loadSettings() {
		SceneManager.LoadScene("settings_menu");
	}
}
