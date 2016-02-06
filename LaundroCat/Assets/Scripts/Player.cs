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
    private float h; // a and d buttons or <- and -> buttons

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    public void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

    public void Swipe()
    {

        // Handle swiping
        if ((Input.GetMouseButtonDown(0)) && Mathf.Abs(Input.mousePosition.x) < Screen.width / 2)
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if ((Input.GetMouseButton(0) && !Input.GetMouseButton(1)) && Mathf.Abs(Input.mousePosition.x) < Screen.width / 2)
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();
        }
       if (Input.GetMouseButtonUp(0))
        {
            currentSwipe = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        Swipe(); // Calls above function
        h = currentSwipe.x;

        // Moving to left change direction | left swipe
        if (Input.GetAxis("Horizontal") < -0.1f || (currentSwipe.x < 0)) //&& currentSwipe.y > -0.1f && currentSwipe.y < 0.1f)) 
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Moving to right change direction | right swipe
        if (Input.GetAxis("Horizontal") > 0.1f || (currentSwipe.x > 0)) //&& currentSwipe.y > -0.1f && currentSwipe.y < 0.1f))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Where jumping is. The button jump is space || it is a tap on the right side of the screen either 2 fingers or 1
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1) ||
           (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2)) // Stopped and press button on right side
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
    public void FixedUpdate() {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f; // Don't use z-axis for 3D things
        easeVelocity.x *= 0.85f; // Multiplies easevelocity to reduce it

        if (Application.platform == RuntimePlatform.WindowsEditor)
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
