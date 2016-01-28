using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    // Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    // Booleans
    public bool grounded;
    public bool canDoubleJump;

    // Reference
    private Rigidbody2D rb2d;
	private Animator anim;
    float h; // a and d buttons or <- and -> buttons

    //private Vector2 touchOrigin = -Vector2.one; //initialize touch input to off the screen

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        // Moving to left change direction | left click which is 0, 1 = right click
        if (Input.GetAxis("Horizontal") < -0.1f) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Moving to right change direction
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }

        // Where jumping is. The button jump is space || it is a second tap on screen
        if (Input.GetButtonDown("Jump") || (Input.GetMouseButtonDown(1)))
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower / 1.50f); //1st jump power
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0); // x and y, new resets
                    rb2d.AddForce(Vector2.up * jumpPower); // 2nd jump power
                }
            }
        }
        
    }

    // For physics movement
    void FixedUpdate() {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f; // Don't use z-axis for 3D things
        easeVelocity.x *= 0.85f; // Multiplies easevelocity to reduce it

        h = Input.GetAxis("Horizontal");

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
