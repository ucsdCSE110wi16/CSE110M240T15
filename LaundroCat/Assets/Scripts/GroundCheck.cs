﻿ using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;
    private gameMaster gm;
    private Weapon weapon;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
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

        if (col.gameObject.name == "weapon_beam")
        {
            Destroy(col.gameObject);
            weapon.weapon_beam = true;
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
    }
}
