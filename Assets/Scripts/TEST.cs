using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TEST : MonoBehaviour
{
    private Vector3 offset;
    private float ZCoord;
    private List<Transform> closestPos;
    //private Transform parentClosestPos;
    private Vector3 CurrentPos;
    private float distance;
    private bool dragging = false;
    private int record;
    private Vector3 DisPosition;
    private Rigidbody rb;
    private Rigidbody rb2;
    public int currentLayer;
    public int tagLayer;
    private List<GameObject> child = new List<GameObject>();
    public int checkConnect;
    public int CountClosest = 0;
    public GameObject[] taggedObjects = null;

    void Update()
    {
        GameObject Center = GameObject.Find("Center");
        Vector3 CenterDiff = Center.transform.position - gameObject.transform.position;
        float curCenterDistance = CenterDiff.sqrMagnitude;
        if(curCenterDistance < 1400)
        {
            gameObject.transform.SetParent(Center.transform);
        }
        
    }
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
        Vector3 PaDiff;
        GameObject CurrentCondition;
        GameObject TaggedCondition;
        closestPos = CheckCloseDistance();
        Debug.Log("Distance ="+ distance);
        if(distance < 50)
        {
            //Vector3 PaDiff = gameObject.transform.position - DisPosition;
            for (int i = 0; i < CountClosest; i++)
            {
                PaDiff = new Vector3();
                Debug.Log("gameObject.transform.position ="+gameObject.name);
                Debug.Log("child[i].transform.position ="+child[i].name);
                Debug.Log("Before PaDiff ="+PaDiff);
                PaDiff = gameObject.transform.position - child[i].transform.position;
                Debug.Log("After PaDiff ="+PaDiff);
                CurrentCondition = child[i].transform.GetChild(0).gameObject;
                TaggedCondition = closestPos[i].transform.GetChild(0).gameObject;
                if(CurrentCondition.gameObject.tag == "Available" && TaggedCondition.gameObject.tag == "Available")
                {
                    Debug.Log("CHECKKKKKKKKKKKKKKKKKKKKKK+ "+ closestPos[i].name);
                    gameObject.transform.position = closestPos[i].position + new Vector3(0.0f,0.0f,0.0f) + PaDiff;
                    CurrentCondition.gameObject.tag = "Unavailable";
                    TaggedCondition.gameObject.tag = "Unavailable";
                    if(currentLayer == 10)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        GameObject StoreparentComponent = parentComponent.transform.Find("LegoComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }
                    if(currentLayer == 11)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        Debug.Log("CHECKKKKKKKKKKKKKKKKKKKKKK +"+ parentComponent.name);
                        GameObject StoreparentComponent = parentComponent.transform.Find("ConnectorComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }
                    /*if(currentLayer == 12)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        GameObject StoreparentComponent = parentComponent.transform.Find("LegoComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }
                    if(currentLayer == 13)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        GameObject StoreparentComponent = parentComponent.transform.Find("LegoComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }
                    if(currentLayer == 14)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        GameObject StoreparentComponent = parentComponent.transform.Find("LegoComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }*/
                    if(currentLayer == 15)
                    {
                        GameObject parentComponent = closestPos[i].transform.parent.gameObject;
                        GameObject StoreparentComponent = parentComponent.transform.Find("WheelComponent").gameObject;
                        gameObject.transform.SetParent(StoreparentComponent.transform);
                    }
                    Debug.Log("Error");
                }
            }
            //GameObject CurrentCondition = child.transform.GetChild(0).gameObject;
            //GameObject TaggedCondition = closestPos.transform.GetChild(0).gameObject;
            
            //gameObject.transform.SetParent(closestPos.transform);
            //Physics.IgnoreLayerCollision(10,11,true); 
            //rb = gameObject.GetComponent<Rigidbody>();
            //rb.isKinematic = true;
            //checkConnect = 1;
        }
        /*if (distance > 50 && checkConnect == 1)
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
        }*/
    }
    public List<Transform> CheckCloseDistance()
    {
        CountClosest = 0;
        List<Transform> trans = new List<Transform>();
        currentLayer = gameObject.layer;
        
        if(currentLayer == 11)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("ConnectorSlot");
        }
        else
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("AnythingToConnector");
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
                    //rb2.isKinematic = true;
                    Debug.Log("item.gameObject.name = " +item.gameObject.name);
                    child.Add(item.gameObject);
                    trans.Add(go.transform);
                    distance = curDistance;
                    CountClosest++;
                }
            }
        }
        return trans;
    }
    
    /*void FindChildClosest(GameObject item)
    {
        if(item.CompareTag("AnythingToConnector"))
        {
            return;
        }

        foreach (Transform item in gameObject.transfor)
        {
            
        }
    }*/
}
