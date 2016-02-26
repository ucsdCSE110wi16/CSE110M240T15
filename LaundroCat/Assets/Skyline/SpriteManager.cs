using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

    
    public Texture2D texture1;
    public Texture2D texture2;
    public Texture2D texture3;
    public Texture2D texture4;

    bool done = false;
    int rando;


    // Use this for initialization
    void Start() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        rando = Random.Range(0, 4);
    }

    void doTheSprites(GameObject[] obj) {
        
        Texture2D texture = null;
        switch (rando)
        {
            case 0:
                texture = texture1;
                break;
            case 1:
                texture = texture2;
                break;
            case 2:
                texture = texture3;
                break;
            case 3:
                texture = texture3;
                break;
        }
        foreach (GameObject g in obj)
        {
            Rect rect = new Rect(0f,0f,16f,16f);
            SpriteRenderer spriteR = (SpriteRenderer)g.GetComponent<Renderer>();
            //rect.Set(rect.x + 16f, rect.y, rect.width, rect.height);
            spriteR.sprite = Sprite.Create(texture, rect, new Vector2(0, 0));
        }
        done = true;
	}

    void Update()
    {
        if (!done) {
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Ground");
            doTheSprites(obj);
        }
        

    }
}
