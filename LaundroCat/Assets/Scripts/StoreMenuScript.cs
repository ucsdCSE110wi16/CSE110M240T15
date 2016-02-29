using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System.Collections;

public class StoreMenuScript : MonoBehaviour {

	public string chosen = null;
	public Button cat, dino, duck, elephant, sheep, turtle;
	public Button multiuse;
	
	public void Start () {
		if (PlayerPrefs.GetString("cat") == "true") {
			cat.image.sprite =
					Resources.Load<Sprite>("Sprites/cat_spritesheet");
		}
		if (PlayerPrefs.GetString("dino") == "true") {
			dino.image.sprite =
					Resources.Load<Sprite>("Sprites/dino_spritesheet");
		}
		if (PlayerPrefs.GetString("duck") == "true") {
			duck.image.sprite =
					Resources.Load<Sprite>("Sprites/duck_spritesheet");
		}
		if (PlayerPrefs.GetString("elephant") == "true") {
			elephant.image.sprite =
					Resources.Load<Sprite>("Sprites/elephant_spritesheet");
		}
		if (PlayerPrefs.GetString("sheep") == "true") {
			sheep.image.sprite =
					Resources.Load<Sprite>("Sprites/sheep_spritesheet");
		}
		if (PlayerPrefs.GetString("turtle") == "true") {
			turtle.image.sprite =
					Resources.Load<Sprite>("Sprites/turtle_spritesheet");
		}
	}
	
	public void goBack() {
		SceneManager.LoadScene("main_menu");
	}
	
	public void chooseAnimal(string animal) {
		if (this.chosen != animal) {
			this.chosen = animal;
			multiuse.gameObject.SetActive(true);
		}
		else {
			this.chosen = null;
			multiuse.gameObject.SetActive(false);
		}
	}
	
	public void onMultiuse() {
		
	}
}