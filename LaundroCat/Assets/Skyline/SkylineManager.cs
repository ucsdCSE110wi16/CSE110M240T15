﻿using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {
    private static int CHUNK_SIZE = 8;
    private static int NUM_OF_CHUNKS = 6;
    private static int NUM_OF_CHUNK_TYPES= 9;

    public Transform skyline;
    public Transform bounce;
    public Transform levelTrigger;
    public Transform platform;
    public Transform ramp;
    
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
            int chunk = 2;
                //Random.Range(0, NUM_OF_CHUNK_TYPES);

            nextPos.x += gap;

            // add more chunks
            
            switch (chunk)
              {
                case 0:
                    nextPos = buildLine(nextPos, CHUNK_SIZE);
                    Debug.Log("TerrainLine" + nextPos);
                    break;
                case 1:
                    nextPos = buildTerrainPlatform(nextPos);
                    Debug.Log("HOLY FUCK" + nextPos);
                    break;
                case 2:
                    nextPos = buildTerrainRunway(nextPos);
                    Debug.Log("Tunnel" + nextPos);
                    break;
                case 3:
                    nextPos = buildTerrainBounce(nextPos);
                    Debug.Log("TerrainBounce" + nextPos);
                    break;
                case 4:
                    nextPos = buildTerrainBump(nextPos);
                    Debug.Log("TerrainBump" + nextPos);
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
                    nextPos = buildTerrainCWall(nextPos);
                    Debug.Log("Terrain: WallJump" + nextPos);
                    break;
                default:
                    nextPos = buildLine(nextPos, CHUNK_SIZE);
                    Debug.Log("Terrain: Line" + nextPos);
                    break;
              }
            
        }
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


    // build a line of size length going left to right
    // @ return: returns the vector at the end of the line
    Vector3 buildLine(Vector3 start, int length) {
        Vector3 next = start;
        for (int i = 0; i < length; i++) {
            Transform o = (Transform)Instantiate(skyline);

            o.localPosition = next;
            next.x++;
        }
        return next;
    }

    // build chunk 1 
    Vector3 buildTerrainPlatform(Vector3 start) {
        Vector3 end = start;
        end.x += CHUNK_SIZE;
        buildLine(start, CHUNK_SIZE);
        start.y += 2;
        buildPlatform(start, CHUNK_SIZE);

        return end;
    }


    //builc chunk 2 = runway
    Vector3 buildTerrainRunway(Vector3 start) {
        Vector3 end = start;
        end.x += CHUNK_SIZE;
        buildLine(start, CHUNK_SIZE);
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
        end.x += CHUNK_SIZE;

        start.y -= 2; 
        Vector3 next = start;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(bounce);

            o.localPosition = next;
            next.x++;
        }

        return end;
    }

    //build chunk 4 - bump
    Vector3 buildTerrainBump(Vector3 start)
    {
        Vector3 next = start;
        Vector3 next2;
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            Transform o = (Transform)Instantiate(skyline);

            if(i == CHUNK_SIZE/2)
            {
                next2 = next;
                next2.y++;
                Transform o2 = (Transform)Instantiate(skyline);
                o2.localPosition = next2;
            }
            o.localPosition = next;
            next.x++;
        }

        start.x += CHUNK_SIZE;
        return start;
    }

    // build terrain hill
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

    // build terrain slope
    Vector3 buildTerrainSlope(Vector3 start) {
        Vector3 temp = start;
        temp.y -= 2;
        buildLine(temp, CHUNK_SIZE);
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

    // build terrain holy fuck
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

    // build terrain walljump
    Vector3 buildTerrainCWall(Vector3 start) {
        Vector3 next = start;
        buildLine(next, CHUNK_SIZE);
        next.y+=3;
        next = buildWall(next, 4);
        next.x++;
        buildLine(next, 1);
        next.y -= 3;
        buildLine(next, 1);

        next.x = start.x;
        next.y -= 3;
        next.x += 4;

        buildWall(next, 7);

        start.x += CHUNK_SIZE;
        return start;
    }

    void buildEnd(Vector3 start)
    {
        Transform o = (Transform)Instantiate(levelTrigger);
        o.localPosition = start;
    }
}
