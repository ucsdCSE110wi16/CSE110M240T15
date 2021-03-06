﻿using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

    
    public Texture2D texture1;
    public Texture2D texture2;
    public Texture2D texture3;
    public Texture2D texture4;

    public Sprite bg1;
    public Sprite bg2;
    public Sprite bg3;
    public Sprite bg4;

    private static int ABOVE = 0x01;
    private static int BELOW = 0x02;
    private static int LEFT = 0x04;
    private static int RIGHT = 0x08;

    bool done = false;
    int rando;


    // Use this for initialization
    void Start() {
        Random.seed = (int)System.DateTime.Now.Ticks;
        rando = 
            Random.Range(0, 4);
    }

    void doTheSprites(GameObject[] obj) {
        Texture2D texture = null;
        GameObject bg = GameObject.FindGameObjectWithTag("Background");
        SpriteRenderer bgRender = null;
        bgRender = bg.GetComponent<SpriteRenderer>();

        switch (rando) {
            case 0:
                texture = texture1;
                bgRender.sprite = bg1;
                break;
            case 1:
                texture = texture2;
                bgRender.sprite = bg2;
                break;
            case 2:
                texture = texture3;
                bgRender.sprite = bg3;
                break;
            case 3:
                texture = texture4;
                bgRender.sprite = bg4;
                break;
        }

        foreach (GameObject g in obj) {
            Rect textureRect = new Rect(0f,0f,16f,16f);
            float originX = textureRect.x;
            float originY = textureRect.y;
            float newX = originX;
            float newY = originY;

            SpriteRenderer spriteR = (SpriteRenderer)g.GetComponent<Renderer>();
            Transform trans = (Transform)g.GetComponent<Transform>();
            Vector3 pos = trans.position;

            int blockinfo = 0;
            Collider2D[] surrounding = Physics2D.OverlapAreaAll(new Vector2(pos.x-1, pos.y-1), new Vector2(pos.x+1,pos.y+1));
            foreach (Collider2D col in surrounding) {
                if (col.tag == "Ground") {
                    //block directly above
                    if (col.transform.position.x == pos.x && col.transform.position.y > pos.y) {
                        blockinfo |= ABOVE;
                        //Debug.Log("Block above");
                    }
                    //block directly to right
                    if (col.transform.position.x > pos.x && col.transform.position.y == pos.y) {
                        blockinfo |= RIGHT;
                        //Debug.Log("Block to right");
                    }
                    //block directly to left
                    if (col.transform.position.x < pos.x && col.transform.position.y == pos.y) {
                        blockinfo |= LEFT;
                        //Debug.Log("Block to left");
                    }
                    //block directly below
                    if (col.transform.position.x == pos.x && col.transform.position.y < pos.y) {
                        blockinfo |= BELOW;
                        //Debug.Log("Block below");
                    }
                }
            }

            if(((blockinfo & LEFT) != 0) && ((blockinfo & ABOVE) != 0) && ((blockinfo & RIGHT) != 0)) {
                //set the rectangle's x coordinate to the sprite for bottom blocks
                newX = originX + 16f + 1;
                if((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if(((blockinfo & LEFT) != 0) && ((blockinfo & ABOVE) == 0) && ((blockinfo & RIGHT) == 0)) {
                //set x coordinate to sprite for block with nothing above or to the right (a block that is top right)
                newX = originX + 32f + 2;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if(((blockinfo & LEFT) == 0) && ((blockinfo & ABOVE) == 0) && ((blockinfo & RIGHT) != 0)) {
                //set x coordinate to sprite for block that is top left
                newX = originX + 48f + 3;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if(((blockinfo & LEFT) == 0) && ((blockinfo & ABOVE) != 0) && ((blockinfo & RIGHT) != 0)) {
                //set x coordinate to sprite for block that is mid/bottom left
                newX = originX + 64f + 4;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if(((blockinfo & LEFT) != 0) && ((blockinfo & ABOVE) != 0) && ((blockinfo & RIGHT) == 0)) {
                //set x coordinate to sprite for block that is mid/bottom right
                newX = originX + 80f + 5;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if(((blockinfo & LEFT) == 0) && ((blockinfo & ABOVE) == 0) && ((blockinfo & RIGHT) == 0)) {
                //set x coordinate to sprite for block that is alone
                newX = originX + 96f + 6;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }
            if (((blockinfo & LEFT) == 0) && ((blockinfo & ABOVE) != 0) && ((blockinfo & RIGHT) == 0))
            {
                //set x coordinate to sprite for block that is only covered on top
                newX = originX + 112 + 7;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }

            if(((blockinfo & LEFT) != 0) && ((blockinfo & ABOVE) == 0) && ((blockinfo & RIGHT) != 0)) {
                //set x coordinate to default sprite
                newX = originX;
                if ((blockinfo & BELOW) != 0)
                {
                    newY = originY + 16f + 1;
                }
            }

            
            textureRect.Set(newX, newY, textureRect.width, textureRect.height);
            spriteR.sprite = Sprite.Create(texture, textureRect, new Vector2(0.5f, 0.5f), 15);
            spriteR.color = Color.white;
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
