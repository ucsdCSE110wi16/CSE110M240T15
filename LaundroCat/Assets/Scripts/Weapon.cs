﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    private Player player;
    private Transform weapon_beam;
    private Animator weapon_beam_anim;
    private BoxCollider2D weapon_beam_collider;
    private GameObject[] enemy;
    private gameMaster gm;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon_beam = GameObject.Find("weapon_beam").GetComponent<Transform>();
        weapon_beam_anim = GameObject.Find("weapon_beam").GetComponent<Animator>();
        weapon_beam_collider = GameObject.Find("weapon_beam").GetComponent<BoxCollider2D>();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        weapon_beam.transform.position = player.transform.position;
        if (player.facingRight)
            weapon_beam.transform.position = new Vector3(player.transform.position.x + 4,
                                                     player.transform.position.y,
                                                     player.transform.position.z);
        else
            weapon_beam.transform.position = new Vector3(player.transform.position.x - 4,
                                                     player.transform.position.y,
                                                     player.transform.position.z);
		// Shooter on MouseDown
		Rect bounds = new Rect(Screen.width - Screen.width/4, 0, Screen.width, Screen.height/4);

		if (Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetMouseButtonDown (1) && player.weapon_beam) {
				UsePowerUp ();
			}
		}
		else {
			if ((Input.GetMouseButtonDown (0) || Input.GetMouseButtonDown (1))
			&& (bounds.Contains (Input.GetTouch(0).position) || bounds.Contains(Input.GetTouch(1).position))
			&& player.weapon_beam) {
				UsePowerUp ();
			}
		}
    }

    void UsePowerUp()
    {
        if (player.weapon_beam)
        {
            gm.useWeapon();
            weapon_beam_anim.enabled = true;
            if (player.facingRight)
                weapon_beam.transform.localScale = new Vector3(-12, 5, 1);
            else
                weapon_beam.transform.localScale = new Vector3(12, 5, 1);
            player.weapon_beam = false;
        }
        for (int i = 0; i < enemy.Length; i++)
            DestroyObject(enemy[i]);
    }
}
