using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 5;
    private static int NUM_OF_CHUNKS = 8;
    private static int NUM_OF_CHUNK_TYPES= 5;

    public Transform prefab;
    public Transform prefab2;
    public Vector3 startPos;
    private Vector3 nextPos;

    void Start()
    {
        buildInitialTerrain(startPos);
        nextPos.x = startPos.x;
        Random.seed = (int)System.DateTime.Now.Ticks;

        for (int i = 0; i < NUM_OF_CHUNKS; i++) {
            int gap = Random.Range(0, 3);
            int chunk = Random.Range(0, NUM_OF_CHUNK_TYPES);

            nextPos.x = nextPos.x + CHUNK_SIZE + gap;

            // add more chunks
            switch (chunk)
            {
                case 0:
                    buildTerrain1(nextPos);
                    break;
                case 1:
                    buildInitialTerrain(nextPos);
                    break;
                case 2:
                    buildTerrain2(nextPos);
                    break;
                case 3:
                    buildTerrain3(nextPos);
                    break;
                case 4:
                    buildTerrain4(nextPos);
                    break;
            }
        }
    }
    

    // build initial chunk
    void buildInitialTerrain(Vector3 start)
    {
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(prefab);

            o.localPosition = next;
            next.x++;
        }
    }


    // build chunk 1
    void buildTerrain1(Vector3 start) {
        start.y++;
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++) {
            Transform o = (Transform)Instantiate(prefab);

            o.localPosition = next;
            next.x++;
        }
    }

    //builc chunk 2 = tunnel thing
    void buildTerrain2(Vector3 start) {
        Vector3 next = start;
        Vector3 next2 = start;
        next2.y += 4;
        for (int i = 0; i < CHUNK_SIZE; i++) {
            Instantiate(prefab);
            Transform o = (Transform)Instantiate(prefab);
            Transform o2 = (Transform)Instantiate(prefab);

            o.localPosition = next;
            o2.localPosition = next2;
            
            next.x++;
            next2.x++;
        }
    }

    // build chunk 3 - trampoline
    void buildTerrain3(Vector3 start)
    {
        start.y -= 3; 
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(prefab2);

            o.localPosition = next;
            next.x++;
        }
    }

    //build chunk 4 - bump
    void buildTerrain4(Vector3 start)
    {
        Vector3 next = start;
        Vector3 next2;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(prefab);

            if(i == CHUNK_SIZE/2)
            {
                next2 = next;
                next2.y++;
                Transform o2 = (Transform)Instantiate(prefab);
                o2.localPosition = next2;
            }
            o.localPosition = next;
            next.x++;
        }
    }
}
