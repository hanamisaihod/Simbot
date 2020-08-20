using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGear : MonoBehaviour
{
    void FixedUpdate()
    {
        GameObject[] taggedObject = GameObject.FindGameObjectsWithTag("Gear");
        transform.GetComponent<Rigidbody>().angularVelocity = taggedObject[0].GetComponent<Rigidbody>().angularVelocity;
    }
}
