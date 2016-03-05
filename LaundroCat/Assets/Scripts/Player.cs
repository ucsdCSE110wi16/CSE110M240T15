using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Floats
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;
	public float timer = 4f;

    // Booleans
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    public bool weapon_beam = false;
	public bool invincible = false;

    // Reference
    private Rigidbody2D rb2d;
    private Animator anim;
    private float h; // a and d buttons or <- and -> buttons
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
	public Color32 c;


    // Stats
    public int currHealth;
    public int maxHealth = 3;

    //for handling sounds
    private gameMaster gm;

    // Used for mobile inputs
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        // For health vars
        currHealth = maxHealth;

		c = this.GetComponent<Renderer>().material.color;

        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
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
		// Player becomes damage-proof for four(ish) seconds after being damaged
		if (this.invincible == true) {
			timer -= Time.deltaTime;
			if (timer > 0)
				this.invincible = true;
			else
				this.invincible = false;

			if (((int)timer % 2) != 0) {
				this.GetComponent<Renderer> ().material.color = Color.yellow;
			} else {
				this.GetComponent<Renderer> ().material.color = c;
			}
		} else {
			timer = 4f;
			this.GetComponent<Renderer>().material.color = c;
		}

        anim.SetBool("grounded", grounded);
        anim.SetFloat("speed", Mathf.Abs(rb2d.velocity.x));

        Swipe(); // Calls above function
        h = currentSwipe.x;

        // Moving to left change direction | left swipe
        if (Input.GetAxis("Horizontal") < -0.1f || (currentSwipe.x < 0)) //&& currentSwipe.y > -0.1f && currentSwipe.y < 0.1f))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        // Moving to right change direction | right swipe
        if (Input.GetAxis("Horizontal") > 0.1f || (currentSwipe.x > 0)) //&& currentSwipe.y > -0.1f && currentSwipe.y < 0.1f))
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        // Where jumping is. The button jump is space || it is a tap on the right side of the screen either 2 fingers or 1
        if ((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1) ||
           (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2)) && !wallSliding) // Stopped and press button on right side
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower / 1.50f); //1st jump power
                canDoubleJump = true;
                gm.playerJump();
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0); // x and y, new resets
                    rb2d.AddForce(Vector2.up * jumpPower); // 2nd jump power
                    gm.playerJump();
                }
            }
        }

        // Make character die when below camera
        if (transform.position.y < -6)
        {
            currHealth = 0;
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

        // Wall Jump
        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.12f, wallLayerMask);
            if (facingRight && (Input.GetAxis("Horizontal") > 0.1f || currentSwipe.x > 0)
                || !facingRight && (Input.GetAxis("Horizontal") < -0.1f || currentSwipe.x < 0))
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
            }
        }

        if (wallCheck == false || grounded)
        {
            wallSliding = false;
        }
    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f); // hardcoded, fix later just for testing
        wallSliding = true;

        // Input jump
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(1) ||
           (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1, 1) * jumpPower * 1.3f);
            }
            else
            {
				rb2d.AddForce (new Vector2 (1, 1) * jumpPower * 1.3f);
            }
        }
    }

    // For physics movement
    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f; // Don't use z-axis for 3D things
        easeVelocity.x *= 0.85f; // Multiplies easevelocity to reduce it

        if (Application.platform == RuntimePlatform.WindowsEditor)
            h = Input.GetAxis("Horizontal");

        if (grounded)
        {
            rb2d.AddForce((Vector2.right * speed) * h); // Moves the player if we press left (>0) or right (<0)
        }
        else
        {
            // If not grounded then add twice force we normally do so it's not too floaty
            rb2d.AddForce((Vector2.right * speed * 2f) * h);
        }


        // fake friction / Easing the x speed of our player
//        if (grounded) COMMENTING THIS OUT REMOVES AIR FRICTION
 //       {
            rb2d.velocity = easeVelocity;
//        }

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

    public void Die()
    {
        currHealth--;
        if (currHealth <= 0)
        {
          gm.playerDeathSound();
			    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
			    for (int i = 0; i < objects.Length; i++) {
				        Destroy(objects[i]);
			    }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Loads current scene over again (restarts)
        }
        else {
          gm.playerHurtSound();
        }
    }

    // Freezes player
    public void Stop()
    {
        rb2d.velocity = Vector3.zero;
        rb2d.Sleep();
    }

	// Enemy knockback
	public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir) {
		float timer = 0;

		while (knockDur > timer) {
			timer += Time.deltaTime;
			rb2d.AddForce (new Vector3 (knockbackDir.x * -100, knockbackDir.y * knockbackPwr, transform.position.z));
		}

		yield return 0;
	}

	/*
	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag("Enemy"))
			StartCoroutine (Knockback (0.02f, 50, transform.position));
	}
	*/
}
