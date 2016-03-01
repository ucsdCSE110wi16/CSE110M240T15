using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 8;
    private static int NUM_OF_CHUNK_TYPES= 18;

    public Transform skyline;
    public Transform bounce;
    public Transform levelTrigger;
    public Transform platform;
    public Transform ramp;
    public Transform rampRev;
    public Transform poop;
    public Transform laundry;
    
    public Vector3 startPos;
    private Vector3 nextPos;

    public int levelSize;

    void Start()
    {
        buildInitialTerrain(startPos);
        nextPos.x = startPos.x + CHUNK_SIZE;
        nextPos.y = startPos.y; 
        Random.seed = (int)System.DateTime.Now.Ticks;

        for (int i = 0; i < levelSize; i++) {
            int gap = Random.Range(0, 3);
            int chunk =
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
                    nextPos = buildTerrainRainingPoop(nextPos);
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
                    Debug.Log("Terrain: Jump" + nextPos);
                    break;
                case 10:
                    nextPos = buildTerrainArena(nextPos);
                    Debug.Log("Terrain: Enemy" + nextPos);
                    break;
                case 11:
                    nextPos = buildTerrainBattlefield(nextPos);
                    Debug.Log("Terrain: Battlefield" + nextPos);
                    break;
                case 12:
                    nextPos = buildTerrainSteps(nextPos);
                    Debug.Log("Terrain: Steps" + nextPos);
                    break;
                case 13:
                    nextPos = buildTerrainTower(nextPos);
                    Debug.Log("Terrain: Tower" + nextPos);
                    break;
                case 14:
                    nextPos = buildTerrainCrevice(nextPos);
                    Debug.Log("Terrain: Crevice" + nextPos);
                    break;
                case 15:
                    nextPos = buildTerrainDropzone(nextPos);
                    Debug.Log("Terrain: Dropzone" + nextPos);
                    break;
                case 16:
                    nextPos = buildTerrainMountain(nextPos);
                    Debug.Log("Terrain: Mountain" + nextPos);
                    break;
                case 17:
                    nextPos = buildTerrainVolcano(nextPos);
                    Debug.Log("Terrain: Volcano" + nextPos);
                    break;
                // in case we mess up heres a default case
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

    /*******  BUILD BASIC UNITS (BLOCKS/LINES/ETC) *******/

    // instantiates a unit of whatever prefab passed in
    void buildBlock(Vector3 start, Transform t) {
        Transform o = (Transform)Instantiate(t);
        o.localPosition = start;
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
        if (length > 0)
            for (int i = 0; i < length; i++) {
                buildBlock(start, ramp);
                start.x++;
                start.y++;
            }
        
        if (length < 0)
            for (int i = 0; i > length; i--) {
                buildBlock(start, rampRev);
                start.x++;
                start.y--;
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

        start.y--;
        buildBlock(start, poop);
        start.x += 2;
        buildBlock(start, laundry);
        start.x += 2;
        buildBlock(start, laundry);

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

        Vector3 laundrySpawn = end;
        laundrySpawn.x += (float)6.8;
        laundrySpawn.y += 4;
        buildBlock(laundrySpawn, laundry);

        return end;
    }

    // build chunk 3 - raining poop
    Vector3 buildTerrainRainingPoop(Vector3 start)
    {
        Vector3 end = buildLine(start, CHUNK_SIZE, bounce);

        start.y += (float)4.5;
        start.x++;

        for (int i = 0; i < 2; i++) {
            start = buildPlatform(start, 2);
            Vector3 tmp = start;
            tmp.x--;
            tmp.y++;
            buildBlock(tmp, poop);
            start.x += 2;
        }

        return end;
    }

    //build chunk 4: leap of faith
    Vector3 buildTerrainLeapOfFaith(Vector3 start)
    {
        Vector3 end = start;
        end.x += CHUNK_SIZE + 3;
        start.x += 3;
        start.y++;
        Vector3 blockLoc = buildRamp(start, 2);
        blockLoc.y -= 2;
        blockLoc.x--;
        buildBlock(blockLoc, skyline);
        start.y--;
        start.x-=3;
       
        start = buildLine(start, CHUNK_SIZE + 2, skyline);
        start = buildWall(start, 5);
        Vector3 enemySpawn = start;
        enemySpawn.y++;
        enemySpawn.x--;
        buildBlock(enemySpawn, poop);
        enemySpawn.x--;
        buildBlock(enemySpawn, laundry);

        start = buildLine(start, -3, skyline);

        return end;
    }

    // build chunk 5: terrain hill
    Vector3 buildTerrainHill(Vector3 start)
    {
        Vector3 temp = start;
        Vector3 next;

        for (int i = 0; i < 3; i++) {
            next = temp;
            for (int j = 0; j < CHUNK_SIZE - (i*2); j++) {
                buildBlock(next, skyline);
                next.x++;
            }
            temp.x++;
            temp.y++;
        }
        Vector3 enemySpawn = start;
        enemySpawn.y+=3;
        enemySpawn.x += 3;
        buildBlock(enemySpawn, poop);
        enemySpawn.x++;
        buildBlock(enemySpawn, poop);
        start.x += CHUNK_SIZE;
        return start;
    }


    // chunk 6: build terrain slope
    Vector3 buildTerrainSlope(Vector3 start) {
        Vector3 temp = start;
        temp.y+= (float)1.5 ;
        buildBlock(temp, laundry);
        temp.y -= (float)3.5;
        buildLine(temp, CHUNK_SIZE, skyline);
        temp.y++;

        Vector3 next;
        for (int i = 0; i < 2; i++) {
            next = temp;

            for (int j = 0; j < (CHUNK_SIZE/2)-i; j++) {
                buildBlock(next, skyline);
                next.x++;
            }
            temp.x = start.x;
            temp.y++;
        }

        Vector3 enemySpawn = start;
        enemySpawn.x += 4;
        enemySpawn.y--;
        buildBlock(enemySpawn, poop);

        start.x += CHUNK_SIZE;
        return start;
    }

    // chunk 7: build terrain holy fuck
    Vector3 buildTerrainHOLYFUCK(Vector3 start) {
        Vector3 next = start;
        Vector3 laundrySpawn = start;
        laundrySpawn.x += CHUNK_SIZE / 2;
        laundrySpawn.y--;
        buildBlock(laundrySpawn, laundry);

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

    // chunk 8: build terrain walljump
    Vector3 buildTerrainWalljump(Vector3 start) {
        Vector3 next = start;
        buildLine(next, CHUNK_SIZE + 1, skyline);
        next.x += 4;

        Vector3 laundrySpawn = buildWall(next, 7);
        laundrySpawn.x--;
        laundrySpawn.y-= (float)0.5;
        buildBlock(laundrySpawn, laundry);
        laundrySpawn.y -= (float)1.8;
        buildBlock(laundrySpawn, laundry);
        laundrySpawn.y -= (float)1.8;
        buildBlock(laundrySpawn, laundry);
        next.x++;
        next.y++;
        buildBlock(next, poop);

        start.x += CHUNK_SIZE + 1;
        return start;
    }

    //chunk 9: Builds a ramp to a bounce platform
    Vector3 buildTerrainJump(Vector3 start) {
        Vector3 temp = buildTerrainRunway(start);
        temp.x += CHUNK_SIZE;
        temp = buildLine(temp, CHUNK_SIZE, bounce);
        temp.x += CHUNK_SIZE / 2;
        return temp;
    }

    // chunk 10: build terrain arena
    Vector3 buildTerrainArena(Vector3 start)
    {
        Vector3 end = buildLine(start, CHUNK_SIZE, skyline);
        Vector3 temp = end;
        temp.y++;
        temp.x--;
        buildBlock(temp, skyline);
        temp.x -= CHUNK_SIZE / 2;
        
        buildBlock(temp, poop);
        start.y++;
        buildBlock(start, skyline);
        return end;
    }

    //chunk 11: builds a base with three soft platforms
    Vector3 buildTerrainBattlefield(Vector3 start)
    {
        int baseSize = CHUNK_SIZE * 2;
        int platSize = baseSize / 3;

        Vector3 enemySpawn = start;
        enemySpawn.y += (float)3.5;
        enemySpawn.x += (float)2.5;
        buildBlock(enemySpawn, laundry);
        enemySpawn.x += (float)4.5;
        enemySpawn.y += (float)3.5;
        buildBlock(enemySpawn, poop);
        enemySpawn.x += (float)5.5;
        enemySpawn.y -= (float)3.5;
        buildBlock(enemySpawn, laundry);


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
        Vector3 laundrySpawn = end;
        laundrySpawn.x--;
        laundrySpawn.y++;
        buildBlock(laundrySpawn, laundry);
        start.y += 2;
        start.x = (end.x + start.x) / 2;
        end = buildLine(start, platSize, platform);
        Vector3 enemySpawn = end;
        enemySpawn.y ++;
        enemySpawn.x --;
        buildBlock(enemySpawn, poop);

        start.y += 2;
        start.x = (end.x + start.x) / 2;
        end = buildLine(start, platSize, platform);

        end = buildLine(end, baseSize, skyline);
        end.y -= 4;
        return end;
    }

    // chunk 13: build tower
    Vector3 buildTerrainTower(Vector3 start) {
        Vector3 end = start;
        end.x += 9;

        start = buildLine(start, 4, skyline);
        buildBlock(start, bounce);
        start.x++;
        buildLine(start, 4, skyline);
        start.x -= 2;

        start.y += 3;
        start = buildWall(start, 4);
        start.x++;
        start = buildLine(start, 1, platform);
        start.y -= 3;
        buildWall(start, 4);
        start.x--;
        start.y += (float)0.5;
        buildLine(start, 1, platform);
        start.y += (float)1.5;
        Vector3 lauundrySpawn = start;
        lauundrySpawn.x += (float)0.2;
        buildBlock(lauundrySpawn, laundry);
        start.y += 2;
        buildBlock(start, poop);


        return end;
    }

    // chunk 14: build terrain crevice
    Vector3 buildTerrainCrevice(Vector3 start) {
        Vector3 end = start;
        Vector3 enemySpawn = start;

        end.x += CHUNK_SIZE;
        buildLine(start, 6, skyline);
        start.y--;
        start = buildLine(start, 4, skyline);
        start.y--;
        start = buildLine(start, 4, platform);
        start.y += (float)1.5;
        start.x -= (float)1.5;
        buildBlock(start, laundry);

        enemySpawn.x += 4;
        enemySpawn.y++;
        buildBlock(enemySpawn, poop);
        enemySpawn.x += 2;
        enemySpawn.y-=2;
        buildBlock(enemySpawn, poop);

        return end;
    }

    // build chunk 15: build terrain dropzone
    Vector3 buildTerrainDropzone(Vector3 start) {
        Vector3 end = start;
        end.x += CHUNK_SIZE;

        start = buildLine(start, CHUNK_SIZE, skyline);
        start.y++;
        start.x--;
        buildLine(start, -(CHUNK_SIZE / 2), skyline);
        start.y++;
        start = buildLine(start, -(CHUNK_SIZE / 2), skyline);
        start.x-=2;
        start = buildLine(start, -2, platform);
        start.y++;
        start.x += (float)1.5;
        buildBlock(start, poop);
        start.y -= 2;
        buildBlock(start, laundry);

        start.x += 2;
        buildBlock(start, laundry);
        start.x += 2;
        start.y += 2;
        buildBlock(start, poop);


        return end;
    }

    // build chunk 16: mountain
    Vector3 buildTerrainMountain(Vector3 start) {
        Vector3 end = start;
        end.x += 9;

        buildLine(start, 4, skyline);
        start.y--;
        start = buildLine(start, 4, skyline);
        Vector3 bounceLoc = start;

        start.x--;
        start.y += 2;
        buildLine(start, -3, skyline);
        start.y++;
        start = buildLine(start, -2, skyline);
        buildLine(start, -1, platform);
        start.x++;
        start.y++;
        buildBlock(start, skyline);
        start.x++;
        buildBlock(start, poop);
        start.x++;
        start.y += (float)0.5;
        buildBlock(start, platform);
        start.y -= (float)0.5;
        start.x++;
        start=buildLine(start, 2, poop);
        start.y--;
        buildRamp(start, -2);

        start = bounceLoc;
        buildBlock(start, bounce);
        start.x++;
        buildLine(start, 4, skyline);
        start.y++;
        buildLine(start, 4, skyline);
        start.y++;
        buildLine(start, 3, skyline);
        start.y++;
        buildLine(start, 2, skyline);

        return end;
    }

    //build chunk 17: volcano
    Vector3 buildTerrainVolcano(Vector3 start) {
        Vector3 end = start;
        end.x += 8;

        Vector3 next = start;

        next= buildLine(next, 1, bounce);
        next= buildLine(next, 6, skyline);
        next = buildLine(next, 1, bounce);
        next = start;
        next.y += 3;
        next.x++;
        buildLine(next, 6, skyline);
        next.y += 2;
        next.x++;
        buildLine(next, 4, platform);
        next.x++;
        next.y++;
        next = buildLine(next, 2, laundry);
        buildBlock(next, poop);
        next = start;
        next.y++;
        next.x += 5;
        buildBlock(next, poop);

        return end;
    }


    /********** BUILD END *********/

    void buildEnd(Vector3 start)
    {
        Transform o = (Transform)Instantiate(levelTrigger);
        o.localPosition = start;
    }
}
