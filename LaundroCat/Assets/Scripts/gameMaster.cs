using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour {

    public int laundryPoints = 0;
	public Text laundryText;

    void Update()
    {
        laundryText.text = ("Laundry: " + laundryPoints.ToString());
    }
}
