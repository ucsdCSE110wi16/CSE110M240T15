using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    public bool weapon_beam = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Which powerup is being used;
	    if (weapon_beam)
        {
            GetComponent<Animation>().Play();
        }
	}   
}
