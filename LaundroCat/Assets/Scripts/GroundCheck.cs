using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;
    private gameMaster gm;
    public Transform laundry_sound;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    // Gets called when the groundCheck enters something
    void OnTriggerEnter2D(Collider2D col)
    {
		if (col.CompareTag("Ground") || col.CompareTag("Platform") || col.CompareTag("Enemy"))
        	player.grounded = true;

        if (col.CompareTag("laundry"))
        {
            Instantiate(laundry_sound, transform.position, transform.rotation);
            Destroy(col.gameObject);
            gm.laundryPoints += 1;
        }

        if (col.CompareTag("Weapon"))
        {
			//Commented out bc weapon_beam_pickup(clone) wouldn't be recognized. add additional check if adding more weapons
            //if (col.name == "weapon_beam_pickup")
            //{
                player.weapon_beam = true;
            //}
            Destroy(col.gameObject);
        }

		if (col.CompareTag ("Enemy")) {
			//StartCoroutine (player.Knockback (0.02f, 25, player.transform.position));
			//player.canDoubleJump = true;
			//Destroy (col.gameObject, 0.1f);
			if (player.invincible == true)
				return;
			player.Die();
			player.invincible = true;
		}

		if (col.CompareTag ("Bounce")) {
			//StartCoroutine (player.Knockback (0.02f, 25, player.transform.position));
			player.canDoubleJump = true;
			Destroy (col.gameObject.transform.parent.gameObject, 0.1f);

		}
    }

    void OnTriggerStay2D(Collider2D col)
    {
        player.grounded = true;
    }

    // Gets called when the groundCheck exits something
    void OnTriggerExit2D(Collider2D col)
    {
       	player.grounded = false;

		if (col.CompareTag("Ground") || col.CompareTag("Platform"))
        	player.canDoubleJump = true;
    }
}
