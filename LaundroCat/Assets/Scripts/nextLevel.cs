using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
       if( col.name == "player_turtle")
        {
            SceneManager.LoadScene(0);
        }
    }
}
