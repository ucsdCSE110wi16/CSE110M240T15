﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private gameMaster gm;

    // Stats
    public int currHealth;
    public int maxHealth = 3;

    // Used for mobile inputs
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();

        // For health vars
        currHealth = maxHealth;

        // for the gameMaster
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
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
    void Update()
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

        //TODO THIS DOESN"T WORK ON THE BOUNCY PLATFORMS
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

        // Make character die (health mechanics)
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
        if (currHealth <= 0)
        {
            Die();
        }
    }

    // For physics movement
    void FixedUpdate() {
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

    void Die()
    {
        currHealth--;
        if (currHealth == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Loads current scene over again (restarts)
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Laundry"))
        {
            Destroy(col.gameObject);
            gm.laundryPoints += 1;
        }
    }
}
