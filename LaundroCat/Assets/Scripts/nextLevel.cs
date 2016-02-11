using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

    Scene[] scenes = SceneManager.GetAllScenes();

    void OnTriggerEnter2D(Collider2D col)
    {
       if( col.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
