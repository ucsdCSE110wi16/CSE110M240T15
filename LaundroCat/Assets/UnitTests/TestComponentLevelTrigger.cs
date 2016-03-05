using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestComponentLevelTrigger : MonoBehaviour {
    static int level = 1;
    static int numTerrain = 5;

    // Use this for initialization
    void Start() {
        bool b1 = SkylineManager.currLevel == level;
        bool b2 = SkylineManager.levelSize == numTerrain;

        Assert.IsTrue(b1);
        Assert.IsTrue(b2);

        if (b1) {
           // level++;
            print("Test #1 PASS: levels are incrementing properly.");
            print("\tExpected: " + level + ", Got: " + SkylineManager.currLevel);
            level++;
        }
        if (b2) {
            //numTerrain += 2;
            print("Test #2 PASS: num of terrain is incrementing properly.");
            print("\tExpected: " + numTerrain + ", Got: " + SkylineManager.levelSize);
            numTerrain += 2;
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
