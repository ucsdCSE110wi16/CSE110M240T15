using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class TestGetLaundry : MonoBehaviour
{

    private bool ready;
    private int beforeCount;
    // Use this for initialization
    void Start()
    {
        ready = false;
        beforeCount = PlayerPrefs.GetInt("socks");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("laundry"))
        {
            ready = true;
        }

    }

    void Update()
    {
        if (ready)
        {
            if (PlayerPrefs.GetInt("socks") < 9999)
            {
                Assert.IsTrue(beforeCount + 1 == PlayerPrefs.GetInt("socks"));
                Debug.Log("Before: " + (beforeCount + 1) + " = After:" + PlayerPrefs.GetInt("socks"));
            }
            ready = false;
        }
    }
}
