using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovementTest : MonoBehaviour
{
    public float speed;
    public float torque;
    public float delay;
    public float currentDelay;
    public bool isOnIce = false;
    private Rigidbody rbd;

    private void Start()
    {
        rbd = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {        
        if (delay > 0.01999961)
        {
            if (speed != 0)
            {
                if (isOnIce)
                {
                    rbd.AddForce(transform.forward * 2 * speed);
                }
                else
                {
                    rbd.velocity = transform.forward * speed / 0.99f;
                }
            }
            if (torque != 0)
            {
                if (isOnIce)
                {
                    rbd.AddTorque(transform.up * 2 * torque / 57.273f);
                }
                else
                {
                    rbd.angularVelocity = transform.up * torque/ 57.273f / 0.99f;
                }
            }
            delay -= Time.fixedDeltaTime;
        }
        else
        {
            if (!isOnIce)
            {
                rbd.velocity = new Vector3(0, 0, 0);
                rbd.angularVelocity = new Vector3(0, 0, 0);
            }
        }
    }
}