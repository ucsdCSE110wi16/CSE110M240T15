using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestComponent : MonoBehaviour {
    GameObject go;
    float xPos;
	// Use this for initialization
	void Start () {
        go = GameObject.FindWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
        enemyChase script = go.GetComponent<enemyChase>();
        Assert.IsTrue(!script.chasingPlayer);
	}
}
