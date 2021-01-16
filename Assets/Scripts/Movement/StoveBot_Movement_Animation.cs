using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class StoveBot_Movement_Animation : MonoBehaviour
{
    public GameObject wheel_left;
    public GameObject wheel_right;
    public bool wheelUpdate;
    public float wheel_L_speed;
    public float wheel_R_speed;
    public float delay;
    Coroutine wheelCor;

    void Start()
    {
        
    }

    /*
    void Update()
    {
        if (wheelUpdate)
        {
            if (wheelCor != null)
            {
                StopCoroutine(wheelCor);
            }
            wheelCor = StartCoroutine(WheelRotating(wheel_L_speed, wheel_R_speed, delay));
            wheelUpdate = false;
        }
    }*/

    // Index May be changed later
    IEnumerator WheelRotating(float L_speed, float R_speed, float rotatingDelay)
    {
        LeanTween.rotateAround(wheel_left, Vector3.right, 720 * L_speed * rotatingDelay, rotatingDelay);
        LeanTween.rotateAround(wheel_right, Vector3.right, 720 * R_speed * rotatingDelay, rotatingDelay);
        yield return new WaitForSeconds(rotatingDelay);
    }
}
