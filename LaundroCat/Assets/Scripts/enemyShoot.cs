using UnityEngine;
using System.Collections;

public class enemyShoot : MonoBehaviour {

    Transform target;
    private Rigidbody2D r;
	private gameMaster gm;

    private int bulletSpeed = 10;
	private float time = 0f;
	private float toAdd = 2f;

	public bool spawnStuff = true;

    public Transform bullet;

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
		if (GameObject.FindGameObjectWithTag("GameMaster") != null)
			gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
    }
	
	// Update is called once per frame
	void Update () {

        target = GameObject.FindWithTag("Player").transform;

        Vector3 forwardAxis = new Vector3(0, 0, -1);

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > 5) return;

        transform.LookAt(target.position, forwardAxis);
        Debug.DrawLine(transform.position, target.position);
        transform.eulerAngles = new Vector3(1, 0, 0);

        var relativePoint = transform.InverseTransformPoint(target.position);
        if (relativePoint.x < 0.0)
        {
            Vector3 flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }

        time -= Time.deltaTime;

        if (time <= 0)
        {
            time += toAdd;
            GameObject obj = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());
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

		gm.enemyDeathSound();
	}
}
