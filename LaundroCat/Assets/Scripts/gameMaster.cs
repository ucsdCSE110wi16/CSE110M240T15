using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int laundryPoints = 0;
    public Text laundryText;

    void update()
    {
        laundryText.text = ("Laundry: " + laundryPoints);

    }
}
