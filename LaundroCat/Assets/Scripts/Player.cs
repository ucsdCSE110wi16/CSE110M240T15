using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;

    private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        // Moving to left change direction
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Moving to right change direction
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // The button jump is space
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
        }
    }

    // For physics movement
    void FixedUpdate() {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f; // Don't use z-axis for 3D things
        easeVelocity.x *= 0.85f; // Multiplies easevelocity to reduce it

        float h = Input.GetAxis("Horizontal"); // a and d buttons or <- and -> buttons
        rb2d.AddForce((Vector2.right * speed) * h); // Moves the player if we press left (>0) or right (<0)        
        // fake friction / Easing the x speed of our player
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        // Make player stop at a certain speed instead of speeding up infinitely
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        // Same as above but for going left
        if (rb2d.velocity.x < -maxSpeed)
        {
			rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }
}
