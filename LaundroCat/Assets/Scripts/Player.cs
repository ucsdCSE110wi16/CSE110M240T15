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

    private Vector2 touchOrigin = -Vector2.one; //initialize touch input to off the screen

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        int horizontal = 0;
        
        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if(myTouch.phase == TouchPhase.Began) //check if touch begins
            {
                //set origin point of touch
                touchOrigin = myTouch.position;
            }

            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;

                float x = touchEnd.x - touchOrigin.x;

                touchOrigin.x = -1; //reset touchOrigin

                horizontal = x > 0 ? 1 : -1;
            }

            if(horizontal != 0)
            {
                transform.localScale = new Vector3(horizontal, 1, 1);
            }
        }

        /*// Moving to left change direction
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Moving to right change direction
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }

        // Where jumping is. The button jump is space
        if (Input.GetButtonDown("Jump"))
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
        */
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
