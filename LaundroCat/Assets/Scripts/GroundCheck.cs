using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    // Gets called when the groundCheck enters something
    void OnTriggerEnter2D(Collider2D col)
    {
        player.grounded = true;
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
