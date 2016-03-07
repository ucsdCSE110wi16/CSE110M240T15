using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    public GameMenuScript gms;


	// Use this for initialization
	void Start () {
        gms = GameObject.Find("EventSystem").GetComponent<GameMenuScript>();

        gms.cat.active = false;
        gms.dino.active = false;
        gms.duck.active = false;
        gms.elephant.active = false;
        gms.sheep.active = false;
        gms.turtle.active = false;

        switch (PlayerPrefs.GetString("current")) {
            case "cat":
                gms.cat.SetActive(true);
                break;
            case "dino":
                gms.dino.SetActive(true);
                break;
            case "duck":
                gms.duck.SetActive(true);
                break;
            case "elephant":
                gms.elephant.SetActive(true);
                break;
            case "sheep":
                gms.sheep.SetActive(true);
                break;
            case "turtle":
                gms.turtle.SetActive(true);
                break;
        }
        player = GameObject.FindGameObjectWithTag("Player");
	}
	


	// Update is called once per frame
	void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x,
                                    ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y,
                                    ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                        Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                        Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    public void SetMinCamPosition()
    {
        minCameraPos = gameObject.transform.position;
    }

    public void SetMaxCamPosition()
    {
        maxCameraPos = gameObject.transform.position;
    }
}
