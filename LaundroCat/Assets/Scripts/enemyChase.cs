using UnityEngine;
using System.Collections;

public class enemyChase : MonoBehaviour {

    Transform target;
	private gameMaster gm;
	private Rigidbody2D r;

    public float speed = 2f;
	public int moveSpeed = 2;
    
    public bool chasingPlayer;
	public bool spawnStuff = true;    

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }

    // Update is called once per frame
    void Update() {
		if (gameObject == null) {
			return;
		}

		if (transform.position.y < -10f) {
			spawnStuff = false;
			Destroy (this.gameObject);
		}
		else
			spawnStuff = true;

        target = GameObject.FindWithTag("Player").transform;

        Vector3 forwardAxis = new Vector3(0, 0, -1);



        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > 5) {
            chasingPlayer = false;
            return;
        }
        chasingPlayer = true;

        transform.LookAt(target.position, forwardAxis);
        Debug.DrawLine(transform.position, target.position);
        transform.eulerAngles = new Vector3(1, 0, 0);

        int pos = Mathf.Abs((int) (transform.position.y - target.position.y));
        //if (pos < 0.5) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * 2 * Time.deltaTime);
        //}

        /*Vector2 velocity = new Vector2((transform.position.x - target.position.x) * speed, 0);
        Vector3 v = r.velocity;
        v.x = -velocity.x;
        r.velocity = v;*/

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
