using UnityEngine;
using System.Collections;

public class enemyShoot : MonoBehaviour {

    Transform target;
    private Rigidbody2D r;
    private int bulletSpeed = 10;
    float time = 0f;
    float toAdd = 2f;

    public Transform bullet;

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
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

}
