using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Sprite[] HeartSprites;
    public Image HeartUI;
    private Player player;
    public Sprite[] WeaponToggle;
    public Image PowerUpUI;
	public Sprite[] controls_sprite;
	public Image controls_image;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		controls_image = GameObject.Find("controls_UI").GetComponent<Image> ();
    }

    void Update()
    {
        if (player.currHealth <= 3 )
            HeartUI.sprite = HeartSprites[player.currHealth];
        
        // Weapon UI - Change array to each power-up
        if (player.weapon_beam)
        {
            PowerUpUI.sprite = WeaponToggle[1];
        }
        else
        {
            PowerUpUI.sprite = WeaponToggle[0];
        }

		// For the controls display
		if (Input.mousePresent || Input.touchCount > 0) {
			if (player.facingRight) {
				controls_image.sprite = controls_sprite [0];
			} else {
				controls_image.sprite = controls_sprite [1];
			}
		} else {
			controls_image.sprite = null;
		}
		/*
		this.transform.position = new Vector3 (player.transform.position.x,
			player.transform.position.y + 1f , player.transform.position.z);
*/
		this.transform.position = new Vector3(Input.mousePosition.x/Screen.width, 
			Input.mousePosition.y/Screen.height, 0);
    }
}
