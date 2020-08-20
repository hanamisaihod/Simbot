using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST2 : MonoBehaviour
{
    private Vector3 offset;
    private float ZCoord;
    private Transform closestPos;
    //private Transform parentClosestPos;
    private Vector3 CurrentPos;
    private float distance;
    private bool dragging = false;
    private int record;
    private Vector3 DisPosition;
    private Rigidbody rb;
    public int currentLayer;
    public int tagLayer;
    private GameObject child;
    public int checkConnect;
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

    void OnMouseUp()
    {
        closestPos = CheckCloseDistance();
        Debug.Log("Distance ="+ distance);
        if(distance < 50)
        {
            Vector3 PaDiff = gameObject.transform.position - DisPosition;
            gameObject.transform.position = closestPos.position + new Vector3(0.0f,0.0f,0.0f) + PaDiff;
            gameObject.transform.SetParent(closestPos.transform);
            Physics.IgnoreLayerCollision(10,11,true); 
            rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            checkConnect = 1;
        }
        if (distance > 50 && checkConnect == 1)
        {
            Debug.Log("Check");
            if(gameObject.name == "Lego1")
            {
                Debug.Log("child.gameObject.tag ="+ child.gameObject.tag);
                //child.gameObject.tag = "Beam";
            }
            if(gameObject.name == "Connector")
            {
                Debug.Log("child.gameObject.tag ="+ child.gameObject.tag);
                //child.gameObject.tag = "Connector";
            }
            gameObject.transform.parent = null;
        }
    }
    public Transform CheckCloseDistance()
    {
        Transform trans = null;
        GameObject[] taggedObjects = null;
        currentLayer = gameObject.layer;
        
        if(currentLayer == 11)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("Beam");
        }
        if(currentLayer == 10)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("Connector");
        }
        
        distance = Mathf.Infinity;
        foreach (Transform item in gameObject.transform)
        {
            DisPosition = item.gameObject.transform.position;
            
            foreach (GameObject go in taggedObjects)
            {

                Vector3 diff = go.transform.position - DisPosition;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < 50)
                {
                    trans = go.transform;
                    distance = curDistance;
                    item.gameObject.transform.tag = go.gameObject.transform.tag;
                    child = item.gameObject;
                    return trans;
                }
                if (curDistance < distance)
                {
                    trans = go.transform;
                    distance = curDistance;
                }
            }
    }
        return trans;
    }
}
