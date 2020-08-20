using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateParts : MonoBehaviour
{
    public float rotateSen = 100f;
    public float pitch;
    public float yaw;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //MousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
