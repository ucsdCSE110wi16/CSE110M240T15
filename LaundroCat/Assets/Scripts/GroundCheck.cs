using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;
    private gameMaster gm;

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
    }

    void OnTriggerStay2D(Collider2D col)
    {
        player.grounded = true;
    }

    // Gets called when the groundCheck exits something
    void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
        player.canDoubleJump = true;
    }
}
