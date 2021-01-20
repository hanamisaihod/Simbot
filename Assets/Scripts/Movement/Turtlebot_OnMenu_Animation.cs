using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class Turtlebot_OnMenu_Animation : MonoBehaviour
{
    private float speed;
    private float torque;
    private float delay;
    private Animation anim;
    public GameObject wheel_left;
    public GameObject wheel_right;
    public GameObject head;
    public GameObject battery1;
    public GameObject battery2;
    public GameObject battery3;
    AudioSource moveAudio;
    private float headTargetDegree;
    private float headCurrentDegree;
    private bool moving;
    public AudioClip soundMove;
    private Rigidbody rbd;
    private GameObject levelController;
    private bool showing;
    Coroutine usingCor;

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        levelController = GameObject.FindGameObjectWithTag("LevelController");
        moveAudio = gameObject.GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }


    void FixedUpdate()
    {
        if (levelController.GetComponent<LevelController>().stopRobot)
        {
            delay = 0;
        }
        if (delay > 0.01999961)
        {
            if (speed != 0)
            {
                rbd.velocity = transform.forward * speed / 0.99f;
            }
            else
            {
                rbd.velocity = new Vector3(0, 0, 0);
            }
            if (torque != 0)
            {
                rbd.angularVelocity = transform.up * torque / 57.273f / 0.99f;
            }
            else
            {
                rbd.angularVelocity = new Vector3(0, 0, 0);
            }
            delay -= Time.fixedDeltaTime;
            wheel_left.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 300 * (speed + 0.00347f * torque), Space.Self);
            wheel_right.transform.Rotate(Vector3.down * Time.fixedDeltaTime * 300 * (speed - 0.00347f * torque), Space.Self);
            if (!moveAudio.isPlaying)
                moveAudio.Play();
        }
        else
        {
            rbd.velocity = new Vector3(0, 0, 0);
            rbd.angularVelocity = new Vector3(0, 0, 0);
            moveAudio.Stop();
        }

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
            head.transform.Rotate(0, 0, 0, Space.Self);

        if(!showing)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(OnMenuShow());
            showing = true;
        }
    }

    IEnumerator OnMenuShow()
    {
        Debug.Log("read");
        anim.Play("Turtle_OnMenu");
        Debug.Log("play");
        yield return new WaitForSeconds(5);
        showing = false;
    }
}
