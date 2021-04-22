using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class TurtleBot_Movement_Animation : MonoBehaviour
{
    public GameObject wheel_left;
    public GameObject wheel_right;
    public GameObject head;
    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;
    AudioSource moveAudio;
    private float speed;
    private float torque;
    private float delay;
    private float headTargetDegree;
    private float headCurrentDegree;
    private bool moving;
    public float volume = 1f;     // volume of robot sound
    public GameObject levelController;
    void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        moveAudio = gameObject.GetComponent<AudioSource>();
    }
  
    void FixedUpdate()
    {
        moveAudio.volume = volume;  // volume of robot sound
        delay = gameObject.GetComponent<RobotMovementTest>().delay;
        speed = gameObject.GetComponent<RobotMovementTest>().speed;
        torque = gameObject.GetComponent<RobotMovementTest>().torque;
        headTargetDegree = gameObject.GetComponent<RobotMovementTest>().degree;
        if (levelController.GetComponent<LevelController>().canvasFX.GetComponent<CanvasFX_Controller>().won)
		{
            moveAudio.clip = null;
		}
        if (delay > 0.01999961)
        {
            wheel_left.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 300 * (speed + 0.00347f * torque), Space.Self);
            wheel_right.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 300 * (speed - 0.00347f * torque), Space.Self);
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        else
            moveAudio.Stop();

        if (headCurrentDegree < headTargetDegree - 5)
        {
            head.transform.Rotate(Vector3.forward * Time.fixedDeltaTime * 300, Space.Self);
            headCurrentDegree = headCurrentDegree + Time.fixedDeltaTime * 300;
        }
        else if (headCurrentDegree > headTargetDegree + 5)
        {
            head.transform.Rotate(Vector3.back * Time.fixedDeltaTime * 300, Space.Self);
            headCurrentDegree = headCurrentDegree - Time.fixedDeltaTime * 300;
        }
        else
            head.transform.Rotate(0,0,0, Space.Self);
    }
}
