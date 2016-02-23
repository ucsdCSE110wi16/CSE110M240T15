using UnityEngine;

public class LoadData : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if (!PlayerPrefs.HasKey("Socks"))
        {
            PlayerPrefs.SetFloat("Music", (float)1.0);
            PlayerPrefs.SetFloat("Sound", (float)1.0);

            PlayerPrefs.SetInt("Socks", 0);

            PlayerPrefs.SetString("Cat", "true");
            PlayerPrefs.SetString("Dino", "false");
            PlayerPrefs.SetString("Duck", "false");
            PlayerPrefs.SetString("Elephant", "false");
            PlayerPrefs.SetString("Sheep", "false");
            PlayerPrefs.SetString("Turtle", "false");
            PlayerPrefs.Save();
        }
	}
}