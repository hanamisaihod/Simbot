using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    private Vector3 MousePos;
    private Vector3 position;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            MousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            position = MousePos - Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Camera.main.transform.position = new Vector3(0,10,0);

            Camera.main.transform.Rotate(new Vector3(1,0,0),position.y * 180);
            Camera.main.transform.Rotate(new Vector3(0,1,0),-position.x * 180,Space.World);

            Camera.main.transform.Translate(new Vector3(0,0,-10));
            MousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
