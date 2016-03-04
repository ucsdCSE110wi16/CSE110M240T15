using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int speed = 3;
    private Rigidbody2D r;

    Transform target;

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        r.velocity = (target.position - transform.position).normalized * speed;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        DestroyObject(gameObject);
    }
}
