using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementTest : MonoBehaviour
{
    public bool isOnIce = false;
    public bool setMove = false;
    public bool setTurn = false;
    public float speed;
    public float initialIceSpeed;
    public float iceCounter;
    public float iceSpeed;
    public float turnSpeed;
    public float iceTurnSpeed;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 forwardPosition = transform.position + transform.forward;
        Vector3 backwardPosition = transform.position - transform.forward;
        if (isOnIce)
        {
            initialIceSpeed = speed;
            if (iceCounter < 50)
            {
                iceCounter += 3 * Time.deltaTime;
            }
            if (iceSpeed == 0)
            {
                iceSpeed = initialIceSpeed;
            }
            iceSpeed += (iceCounter / 100) * speed * Time.deltaTime;
            setMove = true;
            setTurn = true;
            if (setMove)
            {
                if (speed > 0)
                    transform.position = Vector3.Lerp(transform.position, forwardPosition, Time.deltaTime * iceSpeed);
                else
                    transform.position = Vector3.Lerp(transform.position, backwardPosition, Time.deltaTime * -iceSpeed);
            }
            else
            {

            }
            if (setTurn)
            {
                transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
            }
            else
            {

            }
        }
        else
        {
            initialIceSpeed = 0;
            iceCounter = 0;
            if (setMove)
            {
                if (speed > 0)
                    transform.position = Vector3.Lerp(transform.position, forwardPosition, Time.deltaTime * speed);
                else
                    transform.position = Vector3.Lerp(transform.position, backwardPosition, Time.deltaTime * -speed);
            }
            if (setTurn)
            {
                transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
            }
        }
    }
}
