using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // For physics movement
    void FixedUpdate() {
        float h = Input.GetAxis("Horizontal"); // a and d buttons
        rb2d.AddForce((Vector2.right * speed) * h); // Moves the players if we press left (>0) or right (<0)
    }
}
