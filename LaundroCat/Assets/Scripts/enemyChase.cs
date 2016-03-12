using UnityEngine;
using System.Collections;

public class enemyChase : MonoBehaviour {

    Transform target;
	private gameMaster gm;
	private Rigidbody2D r;

    public float speed;
	public float dampingFactor;
	public float dampingThreshold;
	public float maxSpeed;
	//private float distToGround;
    
	public bool chasingPlayer = false;
	public bool spawnStuff = true;  
	public bool grounded = false;

    // Use this for initialization
    void Start () {
		speed = 4f;
		dampingFactor = 0.93f;
		dampingThreshold = 0.1f;
		maxSpeed = 6f;
        r = gameObject.GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

		target = GameObject.FindWithTag("Player").transform;
    }

	void SlowDown() {
		if (r.velocity.magnitude > dampingThreshold)
			r.velocity *= dampingFactor;
		else
			r.velocity = Vector3.zero;
	}

    // Update is called once per frame
    void FixedUpdate() {
		if (transform.position.y < -6f) {
			spawnStuff = false;
			Destroy (this.gameObject);
		}
		else
			spawnStuff = true;

		r.velocity = Vector3.ClampMagnitude (r.velocity, maxSpeed);

        Vector3 forwardAxis = new Vector3(0, 0, -1);

        float distance = Vector3.Distance(target.position, transform.position);
		//float distance = Mathf.Abs(target.position.x - transform.position.x);
		if (distance > 6) {
			chasingPlayer = false;
			SlowDown ();
		}
		else {
			chasingPlayer = true;
		}
			
		if (chasingPlayer) {
			transform.LookAt (target.position, forwardAxis);
			Debug.DrawLine (transform.position, target.position);
			transform.eulerAngles = new Vector3 (1, 0, 0);

			//int pos = Mathf.Abs((int) (transform.position.y - target.position.y));
			//transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * 2 * Time.deltaTime);
			Vector3 dir = target.position - transform.position;
			dir.Normalize ();
			dir.y = 0;
			float currSpeed = speed;
			if (!grounded)
				currSpeed /= 2;
			transform.position += dir * currSpeed * Time.deltaTime;
		} //else if (!grounded) SlowDown ();

        var relativePoint = transform.InverseTransformPoint(target.position);
        if (relativePoint.x < 0.0) {
            Vector3 flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }
    }

	void OnDestroy() {
		float willSpawnLaundry = Random.Range (0, 2); //50% chance
		float willSpawnWeapon = Random.Range (0, 25); //2% chance

		if (spawnStuff && (willSpawnLaundry == 1)) {
			Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y);
			Instantiate (GameObject.FindWithTag ("laundry"), newPos, gameObject.transform.rotation);
		} else if (spawnStuff && (willSpawnWeapon == 1) && gameMaster.spawnWeapon) {
			gameMaster.spawnWeapon = false;
			Vector3 newPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y);

            if (GameObject.FindWithTag("Weapon") != null)
                Instantiate (GameObject.FindWithTag ("Weapon"), newPos, gameObject.transform.rotation);
		}

        if (gm != null)
    	    gm.enemyDeathSound();
	}

}
