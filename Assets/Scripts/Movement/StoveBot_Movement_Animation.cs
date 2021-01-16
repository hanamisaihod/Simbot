using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class StoveBot_Movement_Animation : MonoBehaviour
{
    public GameObject wheel_left;
    public GameObject wheel_right;
    private float speed;
    private float torque;
    private float delay;

    void Update()
    {
        delay = gameObject.GetComponent<RobotMovementTest>().delay;
        speed = gameObject.GetComponent<RobotMovementTest>().speed;
        torque = gameObject.GetComponent<RobotMovementTest>().torque;
    }

    void FixedUpdate()
    {
        if (delay > 0.01999961)
        {
            wheel_left.transform.Rotate(Vector3.down * Time.deltaTime * 600 * (speed + 0.00347f * torque), Space.Self);
            wheel_right.transform.Rotate(Vector3.down * Time.deltaTime * 600 * (speed - 0.00347f * torque), Space.Self);
        }
    }
}
