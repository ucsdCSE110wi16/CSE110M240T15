using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

    void OnTriggerStay(Collider col)
    {
        Debug.Log ("FUCK");
       /* if( col.name == "player_turtle")
        {
            SceneManager.LoadScene(0);
        } */
    }
}
