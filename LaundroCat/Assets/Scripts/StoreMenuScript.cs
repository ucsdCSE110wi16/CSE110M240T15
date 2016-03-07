using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreMenuScript : MonoBehaviour {

	public string chosen = null;
	public Sprite cat_s, dino_s, duck_s,
			elephant_s, sheep_s, turtle_s;
	public Image thousand, hundred, ten, one;
	public Image thousand_c, hundred_c, ten_c, one_c;
	public Sprite[] digits;
	
	public Button cat_b, dino_b, duck_b,
			elephant_b, sheep_b, turtle_b;
	public Button multiuse;
	public Sprite equipped, equip, buy;
	
	private int[] costs = {0, 250, 250, 500, 500, 1000};
	
	public void Start () {
		update();
		hideCost();
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
			//update();
			multiuse.gameObject.SetActive(false);
			hideCost();
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
		
		displaySocks();
		
		if (PlayerPrefs.GetString("current") == chosen) {
			multiuse.image.sprite = equipped;
			multiuse.interactable = false;
			hideCost();
		}
		else if (PlayerPrefs.GetString(chosen) == "true") {
			multiuse.image.sprite = equip;
			multiuse.interactable = true;
			hideCost();
		}
		else {
			multiuse.image.sprite = buy;
			displayCost();
			if (getCost() < PlayerPrefs.GetInt("socks")) {
				multiuse.interactable = true;
			}
			else {
				multiuse.interactable = false;
			}
		}
	}
	
	public void displaySocks() {
		int socks = PlayerPrefs.GetInt("socks");
		thousand.sprite = digits[(socks%10000)/1000];
		hundred.sprite = digits[(socks%1000)/100];
		ten.sprite = digits[(socks%100)/10];
		one.sprite = digits[socks%10];
	}
	
	public void displayCost() {
		int cost = getCost();
		thousand_c.sprite = digits[(cost%10000)/1000];
		hundred_c.sprite = digits[(cost%1000)/100];
		ten_c.sprite = digits[(cost%100)/10];
		one_c.sprite = digits[cost%10];
		
		thousand_c.enabled = true;
		hundred_c.enabled = true;
		ten_c.enabled = true;
		one_c.enabled = true;
	}
	
	public void hideCost() {
		thousand_c.enabled = false;
		hundred_c.enabled = false;
		ten_c.enabled = false;
		one_c.enabled = false;
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
		return 0;
	}
}