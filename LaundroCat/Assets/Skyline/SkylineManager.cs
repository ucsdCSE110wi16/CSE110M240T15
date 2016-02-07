using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 8;
    private static int NUM_OF_CHUNKS = 5;

    public Transform prefab;
    public Vector3 startPos;
    private Vector3 nextPos;

    void Start()
    {
        buildInitialTerrain(startPos);
        nextPos.x = startPos.x;
        Random.seed = (int)System.DateTime.Now.Ticks;

        for (int i = 0; i < NUM_OF_CHUNKS; i++) {
            int gap = Random.Range(0, 3);
            int chunk = Random.Range(1, 3);

            nextPos.x = nextPos.x + CHUNK_SIZE + gap;

            // add more chunks
            switch (chunk)
            {
                case 1:
                    buildTerrain1(nextPos);
                    break;
                case 2:
                    buildInitialTerrain(nextPos);
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
}
