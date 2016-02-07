using UnityEngine;
using System.Collections;

public class SkylineManager : MonoBehaviour {

    public Transform prefab;
    public int numberOfObjects;
    public Vector3 startPos;
    private Vector3 nextPos;

    void Start() { 
        nextPos = startPos;

        for (int i = 0; i < numberOfObjects; i++) {
            Transform o = (Transform)Instantiate(prefab);
          //  GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
          //  cube.AddComponent<Rigidbody>();
          //  cube.transform.position = nextPos;

            o.localPosition = nextPos;
            nextPos.x ++;
            Debug.Log("nextPos = " + nextPos.x + ", localPos = " + o.localPosition.x);
        }
    }
}
