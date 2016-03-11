using UnityEngine;
using System.Collections;

public class damagePlayer : MonoBehaviour {

	private Player player;
	private enemyChase poop;
	private float distToGround;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		poop = gameObject.GetComponentInParent<enemyChase>();
		distToGround = gameObject.GetComponent<Collider2D> ().bounds.extents.y;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag ("Player")) {
			if (player.invincible == true)
				return;
			player.Die();
			player.invincible = true;
		}

	}

	void IsGrounded() {
		//print ("tpy = " + transform.position.y + "dtg0.2 = " + (distToGround + 0.2f));
		//Vector2 pos = new Vector2(gameObject.GetComponent<Collider2D> ().bounds.center.x, gameObject.GetComponent<Collider2D> ().bounds.center.y);
		Vector2 pos = new Vector2(transform.position.x, transform.position.y-distToGround-0.1f);
		RaycastHit2D hit = Physics2D.Raycast (pos, -Vector2.up, 0.1f);
		poop.grounded = false;
		if (hit.collider != null) {	
			//print (hit.collider.name);
			if (hit.collider.CompareTag("Ground") || hit.collider.CompareTag("Platform") || hit.collider.name == "Ramp(Clone)")
				poop.grounded = true;
		}
	}

	// Update is called once per frame
	void Update () {
		IsGrounded ();
	}
}
