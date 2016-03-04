using UnityEngine;

public class LoadData : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    //if (!PlayerPrefs.HasKey("Socks"))
        //{
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
        //}
	}
}