using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void ApplyGravity()
    {
        Rigidbody rb; 
        GameObject Robot= GameObject.Find("Robot");
        //rb = Robot.AddComponent<Rigidbody>();
        //rb.mass = 1500;
        //Robot.AddComponent<BoxCollider>();
        rb = Robot.GetComponent<Rigidbody>();
        rb.isKinematic = false;    
    }
}
