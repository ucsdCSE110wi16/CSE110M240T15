using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int speed = 3;
    private Rigidbody2D r;
	private Player player;

    Transform target;

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        r.velocity = (target.position - transform.position).normalized * speed;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
			if (player.invincible == true)
				return;
			player.Die();
			player.invincible = true;
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        DestroyObject(gameObject);
    }
}
