using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragObj : MonoBehaviour
{
    private Vector3 offset;
    private float ZCoord;
    private Transform closestPos;
    private Vector3 CurrentPos;
    private float distance;
    private bool dragging = false;
    
    void OnMouseDown()
    {
        ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = Camera.main.WorldToScreenPoint(gameObject.transform.position) - Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 position = new Vector3 (Input.mousePosition.x,Input.mousePosition.y,ZCoord);
        transform.position = Camera.main.ScreenToWorldPoint(position + new Vector3(offset.x,offset.y));
    }
}
