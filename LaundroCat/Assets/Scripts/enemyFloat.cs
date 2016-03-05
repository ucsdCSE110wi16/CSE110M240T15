using UnityEngine;
using System.Collections;

public class enemyFloat : MonoBehaviour {

	public float speed = 1f;
    public float moveSpeed = 0.01f;

    private float time = 3f;
    private float toAdd = 3f;

    private int toGo = -1;

	//private Rigidbody2D r;
	private gameMaster gm;
	public LayerMask mask = -1;

	public bool spawnStuff = true;

    // Use this for initialization
    void Start()
    {
        //r = gameObject.GetComponent<Rigidbody2D>();
        //r.gravityScale = 0;
        if (GameObject.FindGameObjectWithTag("GameMaster") != null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 100, Vector3.down);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            if (hit.collider != null)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + 20f, transform.position.z);
            }
        }

        

        /* float newYPos = Terrain.activeTerrain.SampleHeight (transform.position) + 20f;
		transform.position = new Vector3 (transform.position.x, newYPos, transform.position.z);*/
    }
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = new Ray (transform.position + Vector3.up * 100, Vector3.down);

		if (Physics.Raycast (ray, out hit, Mathf.Infinity, mask)) {
			if (hit.collider != null) {
				transform.position = new Vector3 (transform.position.x, hit.point.y + 20f, transform.position.z);
			}
		}

        time -= Time.deltaTime;
        //print(time);

        transform.position = new Vector3 (transform.position.x + moveSpeed, transform.position.y, transform.position.z);

        if (time <= 0)
        {
            time += toAdd;
            moveSpeed = -moveSpeed;
            Vector3 flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }

        if (gameObject == null) {
			return;
		}

		if (transform.position.y < -10f) {
			spawnStuff = false;
			Destroy (this.gameObject);
		}
		else
			spawnStuff = true;
		
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
