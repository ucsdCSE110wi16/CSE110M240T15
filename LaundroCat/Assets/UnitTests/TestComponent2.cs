using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestComponent2 : MonoBehaviour {
    GameObject poop;
    bool pass = false;

    // Use this for initialization
    void Start() {
        poop = GameObject.FindWithTag("UnitTestPoop2");
    }

    // Update is called once per frame
    void Update() {
        enemyChase script = poop.GetComponent<enemyChase>();
        Assert.IsTrue(script.chasingPlayer);

        if (script.chasingPlayer && !pass) {
            print("Test #2 PASS: Poop2 is chasing player");
            pass = true;
        }
    }
}
