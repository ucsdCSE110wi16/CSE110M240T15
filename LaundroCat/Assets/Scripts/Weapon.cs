using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private Player player;
    private Transform weapon_beam;
    private Animator weapon_beam_anim;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon_beam = GameObject.Find("weapon_beam").GetComponent<Transform>();
        weapon_beam_anim = GameObject.Find("weapon_beam").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        if (player.weapon_beam)
        {
            player.Stop();
            weapon_beam_anim.enabled = true;
            weapon_beam.Translate(player.transform.position);
            player.weapon_beam = false;
        }
    }
}
