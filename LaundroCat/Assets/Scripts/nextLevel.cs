using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
       if( col.tag == "Player")
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            for (int i = 0; i < objects.Length; i++) {
                Destroy(objects[i]);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
