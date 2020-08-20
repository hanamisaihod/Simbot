using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tend : MonoBehaviour
{
    // Start is called before the first frame update
    void ResetTensor()
    {
        GetComponent<Rigidbody>().inertiaTensorRotation = Quaternion.identity;
    }
}
