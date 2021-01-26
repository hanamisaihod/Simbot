using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject_Movement : MonoBehaviour
{
    public GameObject cube;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public GameObject cube9;
    public GameObject cube10;
    public GameObject cube11;
    public GameObject cube12;
    
    void Start()
    {
        LeanTween.moveLocalY(cube10, 3.2f, 3f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube4, 6.1f, 4f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube2, 0.28f, 5f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube3, 0.94f, 5f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube1, 0.9f, 10f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube, 2.24f, 5f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube5, 2.47f, 3f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube6, 2.32f, 4f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube7, -1.31f, 3f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalZ(cube11, 1.6f, 2.5f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(cube8, 5.9f, 6f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalZ(cube12, 1.3f, 5f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalZ(cube9, 3.45f, 6f).setLoopPingPong().setEaseInOutSine();
    }

}
