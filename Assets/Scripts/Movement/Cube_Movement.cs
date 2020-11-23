using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Movement : MonoBehaviour
{
    public GameObject cube;
    void Start()
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y + 0.2f, 1f).setLoopPingPong().setEaseInOutSine();
        LeanTween.rotateAroundLocal(cube, Vector3.forward, 360, 4f).setLoopClamp();
        LeanTween.rotateAround(gameObject, Vector3.left, 360, 3f).setLoopClamp();
    }

    
    void Update()
    {
        
    }
}
