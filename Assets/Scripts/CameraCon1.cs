using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon1 : MonoBehaviour
{
    public Vector2 Limit;
    public float MinZoom,MaxZoom;
    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        float GetScroll = Input.GetAxis("Mouse ScrollWheel");
        position.x += Input.GetAxis("Horizontal") * .3f;
        position.y += -GetScroll * 10f;
        position.z += Input.GetAxis("Vertical") * .3f;

        position.x = Mathf.Clamp(position.x,0,Limit.x);
        position.z = Mathf.Clamp(position.z,-5,Limit.y);
        transform.position = position;
    }
}
