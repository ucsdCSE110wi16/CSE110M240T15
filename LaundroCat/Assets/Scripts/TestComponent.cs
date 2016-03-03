using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestComponent : MonoBehaviour {
    GameObject poop;
    bool pass;

	// Use this for initialization
	void Start () {
        poop = GameObject.FindWithTag("UnitTestPoop1");
	}
	
	// Update is called once per frame
	void Update () {
        enemyChase script = poop.GetComponent<enemyChase>();
        Assert.IsTrue(!script.chasingPlayer);

        if (!script.chasingPlayer && !pass) {
            pass = true;
            print("Test #1 PASS: Poop1 is not chasing player");
        }
	}
}
