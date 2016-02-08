using UnityEngine;
using System.Collections;

public class enemyMove : MonoBehaviour {

	public float speed = 1f;

	private Rigidbody2D r;

	// Use this for initialization
	void Start () {
		r = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//float h = Input.GetAxis ("Horizontal");
		r.AddForce (Vector2.right * speed * -1);
	}
}
