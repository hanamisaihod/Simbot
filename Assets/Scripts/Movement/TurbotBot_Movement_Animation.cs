using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class TurbotBot_Movement_Animation : MonoBehaviour
{
    public GameObject wheel_left;
    public GameObject wheel_right;
    public GameObject head;
    public float wheel_L_speed;
    public float wheel_R_speed;
    public float delay;
    public bool headUpdate;
    public float headDegreeTarget;
    private float headCurrentDegree;
    Coroutine headCor;
    Coroutine wheelCor;

    void Start()
    {
        //LeanTween.rotateAround(head, Vector3.up, 720, 1f).setLoopClamp();     // Infinite Head rotating
        //LeanTween.rotateAround(wheel_left, Vector3.right, 360, 2f).setLoopClamp(); // For test
        //LeanTween.rotateAround(wheel_right, Vector3.right, 360, 2f).setLoopClamp(); // For test
    }

    void Update()
    {
        delay = RobotMovementTest.delayAnim;
        wheel_L_speed = RobotMovementTest.speedAnim;
        wheel_R_speed = RobotMovementTest.speedAnim;
    }
    void FixedUpdate()
    {
        if(delay > 0.01999961)
        {
            if (wheelCor != null)
            {
                StopCoroutine(wheelCor);
            }
            wheelCor = StartCoroutine(WheelRotating(wheel_L_speed, wheel_R_speed, delay));
        }
        if (headUpdate)
        {
            if (headCor != null)
            {
                StopCoroutine(headCor);
            }
            headCor = StartCoroutine(HeadRotating(headDegreeTarget));
            headUpdate = false;
        }
    }

    // Index May be changed later
    IEnumerator WheelRotating(float L_speed, float R_speed,float rotatingDelay)
    {
        LeanTween.rotateAround(wheel_left, Vector3.right, 360 * L_speed * rotatingDelay, rotatingDelay);
        LeanTween.rotateAround(wheel_right, Vector3.right, 360 * R_speed * rotatingDelay, rotatingDelay);
        yield return new WaitForSeconds(rotatingDelay);
    }

    // This too
    IEnumerator HeadRotating(float degreeTarget)
    {
        LeanTween.rotateAround(head, Vector3.up, degreeTarget - headCurrentDegree, 1f);
        headCurrentDegree = degreeTarget;
        yield return new WaitForSeconds(1);
    }
}
