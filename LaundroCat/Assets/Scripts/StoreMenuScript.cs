using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System.Collections;

public class StoreMenuScript : MonoBehaviour {

	public string chosen = null;
	public Sprite cat_s, dino_s, duck_s,
			elephant_s, sheep_s, turtle_s;
	public Sprite thousand, hundred, ten, one;
	public Sprite[] digits;
	
	public Button cat_b, dino_b, duck_b,
			elephant_b, sheep_b, turtle_b;
	public Text socks;
	public Button multiuse;
	
	private int[] costs = {0, 250, 250, 500, 500, 1000};
	
	public void Start () {
		update();
	}
	
	public void goBack() {
		SceneManager.LoadScene("main_menu");
	}
	
	public void chooseAnimal(string animal) {
		if (this.chosen != animal) {
			this.chosen = animal;
			update();
			multiuse.gameObject.SetActive(true);
		}
		else {
			this.chosen = null;
			update();
			multiuse.gameObject.SetActive(false);
		}
	}
	
	public void onMultiuse() {
		if (PlayerPrefs.GetString(chosen) == "true") {
			PlayerPrefs.SetString("current", chosen);
			PlayerPrefs.Save();
			update();
		}
		else {
			PlayerPrefs.SetString(chosen, "true");
			PlayerPrefs.SetInt("socks",
					PlayerPrefs.GetInt("socks")-getCost());
			PlayerPrefs.Save();
			update();
		}
	}
	
	public void update() {
		if (PlayerPrefs.GetString("cat") == "true") {
			cat_b.image.sprite = cat_s;
		}
		if (PlayerPrefs.GetString("dino") == "true") {
			dino_b.image.sprite = dino_s;
		}
		if (PlayerPrefs.GetString("duck") == "true") {
			duck_b.image.sprite = duck_s;
		}
		if (PlayerPrefs.GetString("elephant") == "true") {
			elephant_b.image.sprite = elephant_s;
		}
		if (PlayerPrefs.GetString("sheep") == "true") {
			sheep_b.image.sprite = sheep_s;
		}
		if (PlayerPrefs.GetString("turtle") == "true") {
			turtle_b.image.sprite = turtle_s;
		}
		
		socks.text = ""+PlayerPrefs.GetInt("socks");
		
		if (PlayerPrefs.GetString("current") == chosen) {
			multiuse.GetComponentInChildren<Text>().text =
					"Equipped!";
			multiuse.interactable = false;
		}
		else if (PlayerPrefs.GetString(chosen) == "true") {
			multiuse.GetComponentInChildren<Text>().text =
					"Equip";
			multiuse.interactable = true;
		}
		else {
			multiuse.GetComponentInChildren<Text>().text =
					"Buy: "+getCost();
			if (getCost() < PlayerPrefs.GetInt("socks")) {
				multiuse.interactable = true;
			}
			else {
				multiuse.interactable = false;
			}
		}
	}
	
	public int displaySocks() {
		int socks = PlayerPrefs.GetInt("socks");
		thousand.image.sprite = digits[socks%10000];
		hundred.image.sprite = digits[socks%1000];
		ten.image.sprite = digits[socks%100];
		one.image.sprite = digits[socks%10];
	}
	
	public int getCost() {
		switch(chosen) {
			case "cat": return costs[0];
			case "dino": return costs[1];
			case "duck": return costs[2];
			case "elephant": return costs[3];
			case "sheep": return costs[4];
			case "turtle": return costs[5];
		}
		return -1;
	}
}