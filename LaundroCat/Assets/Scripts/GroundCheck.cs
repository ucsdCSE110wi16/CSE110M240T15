using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;
    private gameMaster gm;
    public Transform laundry_sound;
    public Transform weaponPickup_sound;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();


        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
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
            gm.laundryPoints += 47;

			if (PlayerPrefs.GetInt("socks") < 9999) {
			    PlayerPrefs.SetInt("socks", PlayerPrefs.GetInt("socks")+47);
				if (PlayerPrefs.GetInt("socks") > 9999) {
					PlayerPrefs.SetInt("socks", 9999);
				}
			}
			PlayerPrefs.Save();
        }

        if (col.CompareTag("Weapon"))
        {
			//Commented out bc weapon_beam_pickup(clone) wouldn't be recognized. add additional check if adding more weapons
            //if (col.name == "weapon_beam_pickup")
            //{
                player.weapon_beam = true;
            //}
            Destroy(col.gameObject);
            Instantiate(weaponPickup_sound, transform.position, transform.rotation);
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
