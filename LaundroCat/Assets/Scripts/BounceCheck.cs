using UnityEngine;
using System.Collections;

public class BounceCheck : MonoBehaviour {

	//private Player player;
	//private gameMaster gm;
	Transform parent;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindWithTag ("Player");
		//gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
		parent = transform.parent;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.CompareTag ("Player")) {
			print ("test");
			Destroy(parent, 0.1f);
		}
	}

	void OnTriggerExit2D(Collider2D col) {

	}

	void OnTriggerStay2D(Collider2D col) {

	}
}
