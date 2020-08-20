    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{   
    float cameraSenH = 400f;
    float cameraSenV = 400f;
    float yaw = 0.0f;
    float pitch = 0.0f;
    bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        Screen.lockCursor = true;
        trigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Screen.lockCursor = false;
            trigger = false;
        }
        if (Input.GetKey(KeyCode.X) || trigger == true)
        {
            Screen.lockCursor = true;
            trigger = true;
            yaw += cameraSenH * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= cameraSenV * Input.GetAxis("Mouse Y") * Time.deltaTime;
            transform.eulerAngles = new Vector3(pitch,yaw,0.0f);

            transform.position += transform.forward * 10 * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * 10 * Input.GetAxis("Horizontal") * Time.deltaTime;

            if (Input.GetKey(KeyCode.E))
            {
                transform.position += transform.up * 5 * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= transform.up * 5 * Time.deltaTime;
            }
        }

    }
}
