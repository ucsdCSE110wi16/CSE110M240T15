using UnityEngine;
using System.Collections;

public class enemyChase : MonoBehaviour {

    Transform target;
    public float speed = 1f;
    private Rigidbody2D r;

    // Use this for initialization
    void Start () {
        r = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        target = GameObject.FindWithTag("Player").transform;

        Vector3 forwardAxis = new Vector3(0, 0, -1);

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > 8) return;

        transform.LookAt(target.position, forwardAxis);
        Debug.DrawLine(transform.position, target.position);
        transform.eulerAngles = new Vector3(1, 0, 0);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), speed * 2 * Time.deltaTime);

        var relativePoint = transform.InverseTransformPoint(target.position);
        if (relativePoint.x < 0.0) {
            Vector3 flipped = transform.localScale;
            flipped.x *= -1;
            transform.localScale = flipped;
        }

    }

}