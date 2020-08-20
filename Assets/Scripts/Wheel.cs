using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public WheelCollider WC;
    public float torque = 200;
    public GameObject tag;

    void start()
    {

    }
    void Go(float accel)
    {
        
        accel = Mathf.Clamp(accel,-1,1);
        float thrustTorque = accel * torque * 1000;
        WC = gameObject.GetComponent<WheelCollider>();
        WC.motorTorque = thrustTorque;
        //Debug.Log(" WC.motorTorque =" +  WC.motorTorque);
        
        //Quaternion quat;
        //Vector3 position;
        //WC.GetWorldPose(out position,out quat);
        tag.transform.position = transform.position;
        tag.transform.rotation = transform.rotation;
    }
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        //Debug.Log("float a ="+ a);
        Go(a);
        
    }
}
