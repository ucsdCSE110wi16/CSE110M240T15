using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 8;
    private static int NUM_OF_CHUNKS = 6;
    private static int NUM_OF_CHUNK_TYPES= 8;

    public Transform prefab;
    public Transform prefab2;
    public Transform prefab3;
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
           // /*
            switch (chunk)
              {
                case 0:
                    buildTerrainLine(nextPos);
                    break;
                case 1:
                    buildInitialTerrain(nextPos);
                    break;
                case 2:
                    buildTerrainTunnel(nextPos);
                    break;
                case 3:
                    buildTerrain3(nextPos);
                    break;
                case 4:
                    buildTerrain4(nextPos);
                    break;
                case 5:
                    buildTerrainHill(nextPos);
                    break;
                case 6:
                    buildTerrainSlope(nextPos);
                    break;
                case 7:
                    buildTerrainHOLYFUCK(nextPos);
                    break;
              }
            //*/
        }

        nextPos.x = nextPos.x + CHUNK_SIZE;
        buildEnd(nextPos);
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


    // build chunk 1 = straight line
    void buildTerrainLine(Vector3 start) {
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++) {
            Transform o = (Transform)Instantiate(prefab);

            o.localPosition = next;
            next.x++;
        }
    }

    //builc chunk 2 = tunnel thing
    void buildTerrainTunnel(Vector3 start) {
        buildTerrainLine(start);
        start.y += 4;
        buildTerrainLine(start);
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

    // build terrain hill
    void buildTerrainHill(Vector3 start)
    {
        Vector3 temp = start;
        Vector3 next;

        for (int i = 0; i < 3; i++) {
            next = temp;
            for (int j = 0; j < CHUNK_SIZE - (i*2); j++) { 
                Transform o = (Transform)Instantiate(prefab);

                o.localPosition = next;
                next.x++;
            }
            temp.x++;
            temp.y++;
        }
    
    }

    // build terrain slope
    void buildTerrainSlope(Vector3 start) {
        Vector3 temp = start;
        temp.y -= 2;
        buildTerrainLine(temp);
        temp.y++;

        Vector3 next;
        for (int i = 0; i < 3; i++) {
            next = temp;

            for (int j = 0; j < (CHUNK_SIZE/2)-i; j++) {
               Transform o = (Transform)Instantiate(prefab);

               o.localPosition = next;
                next.x++;
            }
            temp.x = start.x;
            temp.y++;
        }
    }

    // build terrain holy fuck
    void buildTerrainHOLYFUCK(Vector3 start) {
        Vector3 next = start;
        next.y++;
        next.x += (CHUNK_SIZE / 4);

        Transform o = (Transform)Instantiate(prefab);
        Transform o2 = (Transform)Instantiate(prefab);
        o.localPosition = next;
        next.x += (CHUNK_SIZE / 2);
        o2.localPosition = next;
    }

    void buildEnd(Vector3 start)
    {
        Transform o = (Transform)Instantiate(prefab3);
        o.localPosition = start;
    }
}
