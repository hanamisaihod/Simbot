using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class StoveBot_OnMenu_Animation : MonoBehaviour
{
    private float speed;
    private float torque;
    private float delay;
    public GameObject wheel_left;
    public GameObject wheel_right;
    AudioSource moveAudio;
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
            if (!moveAudio.isPlaying && moveAudio.isActiveAndEnabled)
                moveAudio.Play();
        }
        else
        {
            rbd.velocity = new Vector3(0, 0, 0);
            rbd.angularVelocity = new Vector3(0, 0, 0);
            moveAudio.Stop();
        }

        if (!showing)
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
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 180;
        delay = 1;
        yield return new WaitForSeconds(1.04f);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 180;
        delay = 1;
        yield return new WaitForSeconds(1.04f);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = -180;
        delay = 1;
        yield return new WaitForSeconds(1.04f);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = -0.25f;
        torque = 0;
        delay = 1;
        yield return new WaitForSeconds(1);
        speed = 0.25f;
        torque = -180;
        delay = 1;
        yield return new WaitForSeconds(1.04f);
        showing = false;
    }
}
