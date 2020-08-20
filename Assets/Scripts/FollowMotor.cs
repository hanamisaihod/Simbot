using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] taggedObject = GameObject.FindGameObjectsWithTag("Motor");
        transform.GetComponent<Rigidbody>().angularVelocity = taggedObject[0].GetComponent<Rigidbody>().angularVelocity;
    }
}
