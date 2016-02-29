using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Controls_UI : MonoBehaviour {

	public Sprite[] controls_sprite;
	public SpriteRenderer controls_image;
	private Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		controls_image = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePresent || Input.touchCount > 0) {
			if (player.facingRight) {
				controls_image.sprite = controls_sprite [0];
			} else {
				controls_image.sprite = controls_sprite [1];
			}
		} else {
			controls_image.sprite = null;
		}

		this.transform.position = new Vector3 (player.transform.position.x,
			player.transform.position.y + 1f , player.transform.position.z);
	}
}
