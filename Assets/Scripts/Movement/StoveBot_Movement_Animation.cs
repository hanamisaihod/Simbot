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
    AudioSource moveAudio;
    public float volume = 1f;     // volume of robot sound

    void Start()
    {
        moveAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        delay = gameObject.GetComponent<RobotMovementTest>().delay;
        speed = gameObject.GetComponent<RobotMovementTest>().speed;
        torque = gameObject.GetComponent<RobotMovementTest>().torque;
    }

    void FixedUpdate()
    {
        moveAudio.volume = volume;  // volume of robot sound
        if (delay > 0.01999961)
        {
            wheel_left.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 600 * (speed + 0.00347f * torque), Space.Self);
            wheel_right.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 600 * (speed - 0.00347f * torque), Space.Self);
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        else
            moveAudio.Stop();
    }
}
