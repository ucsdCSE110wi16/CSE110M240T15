using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 8;
    private static int NUM_OF_CHUNKS = 6;
    private static int NUM_OF_CHUNK_TYPES= 13;

    public Transform skyline;
    public Transform bounce;
    public Transform levelTrigger;
    public Transform platform;
    public Transform ramp;
    public Transform poop;
    
    public Vector3 startPos;
    private Vector3 nextPos;

    void Start()
    {
        buildInitialTerrain(startPos);
        nextPos.x = startPos.x + CHUNK_SIZE;
        nextPos.y = startPos.y; 
        Random.seed = (int)System.DateTime.Now.Ticks;

        for (int i = 0; i < NUM_OF_CHUNKS; i++) {
            int gap = Random.Range(0, 3);
            int chunk = 12;
                Random.Range(0, NUM_OF_CHUNK_TYPES);

            nextPos.x += gap;
            // add more chunks
            
            switch (chunk)
              {
                case 0:
                    nextPos = buildLine(nextPos, CHUNK_SIZE, skyline);
                    Debug.Log("Terrain: Line" + nextPos);
                    break;
                case 1:
                    nextPos = buildTerrainPlatform(nextPos);
                    Debug.Log("Terrain: Platform" + nextPos);
                    break;
                case 2:
                    nextPos = buildTerrainRunway(nextPos);
                    Debug.Log("Terrain: Runway" + nextPos);
                    break;
                case 3:
                    nextPos = buildTerrainBounce(nextPos);
                    Debug.Log("Terrain: Bounce" + nextPos);
                    break;
                case 4:
                    nextPos = buildTerrainLeapOfFaith(nextPos);
                    Debug.Log("Terrain: LeapOfFaith" + nextPos);
                    break;
                case 5:
                    nextPos = buildTerrainHill(nextPos);
                    Debug.Log("Terrain: Hill" + nextPos);
                    break;
                case 6:
                    nextPos = buildTerrainSlope(nextPos);
                    Debug.Log("Terrain: Slope" + nextPos);
                    break;
                case 7:
                    nextPos = buildTerrainHOLYFUCK(nextPos);
                    Debug.Log("Terrain: HOLYFUCK" + nextPos);
                    break;
                case 8:
                    nextPos = buildTerrainWalljump(nextPos);
                    Debug.Log("Terrain: WallJump" + nextPos);
                    break;
                case 9:
                    nextPos = buildTerrainJump(nextPos);
                    Debug.Log("Terrain: Jump");
                    break;
                case 10:
                    nextPos = buildTerrainEnemy(nextPos);
                    Debug.Log("Terrain: Enemy");
                    break;
                case 11:
                    nextPos = buildTerrainBattlefield(nextPos);
                    Debug.Log("Terrain: Battlefield");
                    break;
                case 12:
                    nextPos = buildTerrainSteps(nextPos);
                    Debug.Log("Terrain: Steps");
                    break;
                default:
                    nextPos = buildLine(nextPos, CHUNK_SIZE, skyline);
                    Debug.Log("Terrain: Line" + nextPos);
                    break;
              }
            
        }
        nextPos.x++;
        buildEnd(nextPos);
    }
    

    // build initial chunk
    void buildInitialTerrain(Vector3 start)
    {
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(skyline);
            o.localPosition = next;
            next.x++;
        }
    }

    /*******  BUILD BASIC STUFF (BLOCKS/LINES/ETC) *******/

    // instantiates a unit of whatever prefab passed in
    void buildBlock(Vector3 start, Transform t) {
        Transform o = (Transform)Instantiate(t);
        o.localPosition = start;
    }

    //call this method if you want to break the level
    void buildEnemy(Vector3 start) {
        buildBlock(start, poop);
    }

    Vector3 buildPlatform(Vector3 start, int length) {
        Vector3 next = start;
        for (int i = 0; i < length; i++) {
            Transform o = (Transform)Instantiate(platform);

            o.localPosition = next;
            next.x++;
        }
        return next;
    }

    Vector3 buildRamp(Vector3 start, int length) {
        for (int i = 0; i < length; i++) {
            buildBlock(start, ramp);
            start.x++;
            start.y++;
        }

        return start;
    }

    // build a wall of size height going from bottom to top
    // @return: returns the vector at the top of the wall
    Vector3 buildWall(Vector3 start, int height) {
        Vector3 next = start;
        for (int i = 0; i < height; i++) {
            Transform o = (Transform)Instantiate(skyline);

            o.localPosition = next;
            next.y++;
        }
        next.y--;
        return next;
    }


    // build a line out of any prefab t of size length going left to right
    // chunk default
    // @ return: returns the vector at the end of the line
    Vector3 buildLine(Vector3 start, int length, Transform t) {

        if (length > 0)
            for (int i = 0; i < length; i++) {
                buildBlock(start, t);
                start.x++;
            }
        if (length < 0) {
            length *= -1;
            for (int i = 0; i < length; i++) {
                buildBlock(start, t);
                start.x--;
            }
        }

        return start;
    }



    /********** BUILD TERRAIN ********/


    // build chunk 1 
    Vector3 buildTerrainPlatform(Vector3 start) {
        Vector3 end = start;
        end.x += CHUNK_SIZE;
        buildLine(start, CHUNK_SIZE, skyline);
        start.y += 2;
        buildPlatform(start, CHUNK_SIZE);

        return end;
    }


    //build chunk 2 = runway
    Vector3 buildTerrainRunway(Vector3 start) {
        Vector3 end = start;
        end.x += CHUNK_SIZE;
        buildLine(start, CHUNK_SIZE, skyline);
        start.x += 6;
        start.y++;
        buildRamp(start, 2);
        start.x++;
        buildBlock(start, skyline);

        return end;
    }

    // build chunk 3 - trampoline
    Vector3 buildTerrainBounce(Vector3 start)
    {
        Vector3 end = start;

        start.y -= 2;
        end = buildLine(start, CHUNK_SIZE, bounce);
        return end;
    }

    //build chunk 4
    Vector3 buildTerrainLeapOfFaith(Vector3 start)
    {
        Vector3 end = start;
        end.x += CHUNK_SIZE + 3;
        start.x += 3;
        start.y++;
        buildRamp(start, 2);
        start.y--;
        start.x-=3;
       
        start = buildLine(start, CHUNK_SIZE + 2, skyline);
        start = buildWall(start, 5);

        start = buildLine(start, -3, skyline);

        return end;
    }

    // build cuhnk 5: terrain hill
    Vector3 buildTerrainHill(Vector3 start)
    {
        Vector3 temp = start;
        Vector3 next;

        for (int i = 0; i < 3; i++) {
            next = temp;
            for (int j = 0; j < CHUNK_SIZE - (i*2); j++) { 
                Transform o = (Transform)Instantiate(skyline);

                o.localPosition = next;
                next.x++;
            }
            temp.x++;
            temp.y++;
        }
        start.x += CHUNK_SIZE;
        return start;
    }

    //chunk 6: Builds a ramp to a bounce platform
    Vector3 buildTerrainJump(Vector3 start) {
        Vector3 temp = buildTerrainRunway(start);
        temp.x += CHUNK_SIZE;
        temp = buildLine(temp, CHUNK_SIZE, bounce);
        temp.x += CHUNK_SIZE/2;
        return temp;
    }

    // chunk 7: build terrain slope
    Vector3 buildTerrainSlope(Vector3 start) {
        Vector3 temp = start;
        temp.y -= 2;
        buildLine(temp, CHUNK_SIZE, skyline);
        temp.y++;

        Vector3 next;
        for (int i = 0; i < 2; i++) {
            next = temp;

            for (int j = 0; j < (CHUNK_SIZE/2)-i; j++) {
               Transform o = (Transform)Instantiate(skyline);

               o.localPosition = next;
                next.x++;
            }
            temp.x = start.x;
            temp.y++;
        }

        start.x += CHUNK_SIZE;
        return start;
    }

    // chunk 8: build terrain holy fuck
    Vector3 buildTerrainHOLYFUCK(Vector3 start) {
        Vector3 next = start;
        next.y++;
        next.x += (CHUNK_SIZE / 4);

        Transform o = (Transform)Instantiate(skyline);
        Transform o2 = (Transform)Instantiate(skyline);
        o.localPosition = next;
        next.x += (CHUNK_SIZE / 2);
        o2.localPosition = next;

        start.x += CHUNK_SIZE;
        return start;
    }

    // chunk 9: build terrain walljump
    Vector3 buildTerrainWalljump(Vector3 start) {
        Vector3 next = start;
        buildLine(next, CHUNK_SIZE, skyline);
        next.x += 4;

        buildWall(next, 7);

        start.x += CHUNK_SIZE;
        return start;
    }

    // chunk 10: build terrain enemy
    Vector3 buildTerrainEnemy(Vector3 start)
    {
        Vector3 end = buildLine(start, CHUNK_SIZE, skyline);
        Vector3 temp = end;
        temp.y++;
        temp.x--;
        buildBlock(temp, skyline);
        temp.x -= CHUNK_SIZE / 2;
        
        buildEnemy(temp);
        start.y++;
        buildBlock(start, skyline);
        return end;
    }

    //chunk 11: builds a base with three soft platforms
    Vector3 buildTerrainBattlefield(Vector3 start)
    {
        int baseSize = CHUNK_SIZE * 2;
        int platSize = baseSize / 3;

        Vector3 end = buildLine(start, baseSize, skyline);
        start.y += 2;
        Vector3 pos = buildLine(start, platSize, platform);
        pos.y += 2;
        pos = buildLine(pos, platSize, platform);
        pos.y -= 2;
        buildLine(pos, platSize, platform);

        return end;
    }

    //Chunk 12: builds a series of steps up with soft platforms
    Vector3 buildTerrainSteps(Vector3 start)
    {
        int baseSize = CHUNK_SIZE / 2;
        int platSize = baseSize;

        Vector3 end = buildLine(start, baseSize, skyline);
        start.y += 2;
        start.x = (end.x + start.x) / 2;
        end = buildLine(start, platSize, platform);

        start.y += 2;
        start.x = (end.x + start.x) / 2;
        end = buildLine(start, platSize, platform);

        end = buildLine(end, baseSize, skyline);

        return end;
    }

    void buildEnd(Vector3 start)
    {
        Transform o = (Transform)Instantiate(levelTrigger);
        o.localPosition = start;
    }
}
