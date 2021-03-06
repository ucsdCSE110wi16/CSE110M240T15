﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {
    public bool canProgress = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (canProgress)
        {
            if (col.tag == "Player") {
                GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
                for (int i = 0; i < objects.Length; i++) {
                    Destroy(objects[i]);
                }

                // if # of enemies = 0
                SkylineManager.levelSize += 2;
                SkylineManager.currLevel++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void Update() {
        GameObject[] enemeiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemeiesRemaining.Length == 0)
            canProgress = true;

        if (canProgress) {
            GameObject[] stopsigns = GameObject.FindGameObjectsWithTag("StopSign");
            foreach (GameObject sign in stopsigns) {
                Destroy(sign);
            }
        }
    }
}
