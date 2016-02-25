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
        	player.grounded = true;

        if (col.CompareTag("laundry"))
        {
            Instantiate(laundry_sound, transform.position, transform.rotation);
            Destroy(col.gameObject);
            gm.laundryPoints += 1;
        }

        if (col.CompareTag("Weapon"))
        {
            if (col.name == "weapon_beam_pickup")
            {
                player.weapon_beam = true;
            }
            Destroy(col.gameObject);
        }

		if (col.CompareTag ("Enemy")) {
//			StartCoroutine (player.Knockback (0.02f, 25, player.transform.position));
			Destroy (col.gameObject, 0.1f);
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
