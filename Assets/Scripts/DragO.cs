using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragO : MonoBehaviour
{
    private Vector3 offset;
    private float ZCoord;
    private List<Transform> closestPos = new List<Transform>();
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
    public List<Transform> trans = new List<Transform>();
    public GameObject newParent;
    

    void OnMouseDown()
    {
        ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        offset = Camera.main.WorldToScreenPoint(gameObject.transform.position) - Input.mousePosition;
    }

    void OnMouseDrag()
    {
        if(gameObject.tag == "Dragable")
        {
            Vector3 position = new Vector3 (Input.mousePosition.x,Input.mousePosition.y,ZCoord);
            transform.position = Camera.main.ScreenToWorldPoint(position + new Vector3(offset.x,offset.y));
        }
                
    }

    void OnMouseUp()
    {
        Vector3 PaDiff;
        GameObject CurrentCondition;
        GameObject TaggedCondition;
        GameObject robot;
        GameObject SubtractParent;
        GameObject SubtractCurrentParent;
        float CurrentMag;
        float CloseMag;
        float DotProduct;
        float CosBet;
        double AngleBet;
        int IntAng;
        float x = 0;
        float y = 0;
        float z = 0;
        float Diffx = 0;
        float Diffy = 0;
        float Diffz = 0;
        int IntDiffx = 0;
        int IntDiffy = 0;
        int IntDiffz = 0;
        int ErrorInput = 0;

        closestPos = CheckCloseDistance();
        Debug.Log("ErrorInput = " + ErrorInput);
        //Debug.Log("Distance ="+ distance);
        if(distance < 0.3)
        {
            //Vector3 PaDiff = gameObject.transform.position - DisPosition;
            for (int i = 0; i < CountClosest; i++)
            {
                PaDiff = new Vector3();
                //Debug.Log("After PaDiff ="+PaDiff);
                CurrentCondition = child[i].transform.GetChild(0).gameObject;
                TaggedCondition = closestPos[i].transform.GetChild(0).gameObject;
                SubtractParent = closestPos[i].transform.parent.gameObject;
                SubtractCurrentParent = child[i].transform.parent.gameObject;
                closestPos[i].transform.parent = null;
                child[i].transform.parent = null;
                Debug.Log("gameObject.transform.localEulerAngles =" + gameObject.transform.localEulerAngles);
                Debug.Log("closestPos[i].localEulerAngles =" + closestPos[i].localEulerAngles);
                Debug.Log("CHILDDDDDDDDDDDDDDDDDDD.transform.localEulerAnglesBB =" + child[i].transform.localEulerAngles);
                //gameObject.transform.rotation = closestPos[i].rotation;
                if(currentLayer == 11)
                {
                    Diffx = gameObject.transform.localEulerAngles.x - closestPos[i].localEulerAngles.x;
                    Diffy = gameObject.transform.localEulerAngles.y - closestPos[i].localEulerAngles.y;
                    Diffz = gameObject.transform.localEulerAngles.z - closestPos[i].localEulerAngles.z;
                    IntDiffx = Mathf.RoundToInt((float)Diffx);
                    IntDiffy = Mathf.RoundToInt((float)Diffy);
                    IntDiffz = Mathf.RoundToInt((float)Diffz);
                    IntDiffx = Mathf.Abs(IntDiffx);
                    IntDiffy = Mathf.Abs(IntDiffy);
                    IntDiffz = Mathf.Abs(IntDiffz);
                    if(IntDiffx != 0)
                    {
                        if(IntDiffx != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorAAAAAAAAA11111111111111");
                        }
                    }
                    if(IntDiffy != 0)
                    {
                        if(IntDiffy != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorAAAAAAAA222222222222222");
                        }
                    }
                    if(IntDiffz != 0)
                    {
                        if(IntDiffz != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorAAAAAAAAAAA333333333333");
                        }
                    }
                    Debug.Log("Diffx = " + Diffx);
                    Debug.Log("Diffy = " + Diffy);
                    Debug.Log("Diffz = " + Diffz);
                    

                    /*CurrentMag = Mathf.Sqrt(gameObject.transform.localEulerAngles.sqrMagnitude);
                    CloseMag = Mathf.Sqrt(closestPos[i].localEulerAngles.sqrMagnitude);
                    DotProduct = Vector3.Dot(gameObject.transform.localEulerAngles,closestPos[i].localEulerAngles);
                    CosBet = DotProduct/(CurrentMag*CloseMag);
                    AngleBet = Mathf.Acos(CosBet) * 57.2957795;
                    IntAng = Mathf.RoundToInt((float)AngleBet);

                    if(DotProduct == 0 && (CurrentMag == 0 || CurrentMag == 180) && (CloseMag == 0 || CloseMag == 180))
                    {
                        IntAng = 0;
                        
                    }
                Debug.Log("IntAngAAAAAAA = "+ IntAng);
                Debug.Log("CurrentCondition.name = "+ CurrentCondition);
                Debug.Log("TaggedCondition.name = "+ TaggedCondition);*/
                }
                else
                {
                    Debug.Log("gameObject.transform.localEulerAnglesBB =" + gameObject.transform.localEulerAngles);
                    Debug.Log("closestPos[i].localEulerAnglesBB =" + closestPos[i].localEulerAngles);
                    Diffx = child[i].transform.localEulerAngles.x - closestPos[i].localEulerAngles.x;
                    Diffy = child[i].transform.localEulerAngles.y - closestPos[i].localEulerAngles.y;
                    Diffz = child[i].transform.localEulerAngles.z - closestPos[i].localEulerAngles.z;
                    IntDiffx = Mathf.RoundToInt((float)Diffx);
                    IntDiffy = Mathf.RoundToInt((float)Diffy);
                    IntDiffz = Mathf.RoundToInt((float)Diffz);
                    IntDiffx = Mathf.Abs(IntDiffx);
                    IntDiffy = Mathf.Abs(IntDiffy);
                    IntDiffz = Mathf.Abs(IntDiffz);

                    /*float ParentDiffx = gameObject.transform.localEulerAngles.x - closestPos[i].localEulerAngles.x;
                    float ParentDiffy = gameObject.transform.localEulerAngles.y - closestPos[i].localEulerAngles.y;
                    float ParentDiffz = gameObject.transform.localEulerAngles.z - closestPos[i].localEulerAngles.z;
                    int ParentIntDiffx = Mathf.RoundToInt((float)ParentDiffx);
                    int ParentIntDiffy = Mathf.RoundToInt((float)ParentDiffx);
                    int ParentIntDiffz = Mathf.RoundToInt((float)ParentDiffx);
                    ParentIntDiffx = Mathf.Abs(ParentIntDiffx);
                    ParentIntDiffy = Mathf.Abs(ParentIntDiffy);
                    ParentIntDiffz = Mathf.Abs(ParentIntDiffz);*/
                    if(IntDiffx != 0)
                    {
                        if(IntDiffx != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorBBBBBBBBBBBBBB11111111111111");
                        }
                    }
                    if(IntDiffy != 0)
                    {
                        if(IntDiffy != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorBBBBBBBBBBB222222222222222");
                        }
                    }
                    if(IntDiffz != 0)
                    {
                        if(IntDiffz != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorBBBBBBBBBBBBB333333333333");
                        }
                    }

                    /*if(ParentIntDiffx != 0)
                    {
                        if(ParentIntDiffx != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorCCCCCCCCCCCCCC11111111111111");
                        }
                    }
                    if(ParentIntDiffy != 0)
                    {
                        if(ParentIntDiffy != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorCCCCCCCCCCCCCCCCC222222222222222");
                        }
                    }
                    if(ParentIntDiffz != 0)
                    {
                        if(ParentIntDiffz != 180)
                        {
                            ErrorInput = 1;
                            Debug.Log("ErrorCCCCCCCCCCCCCCCCCCCCC333333333333");
                        }
                    }*/
                    /*Debug.Log("DiffxB = " + Diffx);
                    Debug.Log("DiffyB = " + Diffy);
                    Debug.Log("DiffzB = " + Diffz);
                    Debug.Log("ParentDiffx = " + ParentDiffx);
                    Debug.Log("ParentDiffy = " + ParentDiffy);
                    Debug.Log("ParentDiffz = " + ParentDiffz);*/
                    /*(SubtractChild = child[i].transform.parent.gameObject;
                    child[i].transform.parent = null;
                    CurrentMag = Mathf.Sqrt(child[i].transform.localEulerAngles.sqrMagnitude);
                    CloseMag = Mathf.Sqrt(closestPos[i].localEulerAngles.sqrMagnitude);
                    DotProduct = Vector3.Dot(child[i].transform.localEulerAngles,closestPos[i].localEulerAngles);
                    CosBet = DotProduct/(CurrentMag*CloseMag);
                    AngleBet = Mathf.Acos(CosBet) * 57.2957795;
                    IntAng = Mathf.RoundToInt((float)AngleBet);

                    if(DotProduct == 0 && (CurrentMag == 0 || CurrentMag == 180) && (CloseMag == 0 || CloseMag == 180))
                    {
                        IntAng = 0;
                    }
                    Debug.Log("IntAngBBBBBBBBBBBB = "+ IntAng);
                    Debug.Log("CurrentCondition.name = "+ CurrentCondition);
                    Debug.Log("TaggedCondition.name = "+ TaggedCondition);
                    child[i].transform.SetParent(SubtractChild.transform);*/
                }
                //Debug.Log("DotProduct = " + DotProduct);
                //Debug.Log("CurrentMag = "+ CurrentMag);
                //Debug.Log("CloseMag = "+ CloseMag);
                //Debug.Log("CosBet = "+ CosBet);
                closestPos[i].transform.SetParent(SubtractParent.transform);
                child[i].transform.SetParent(SubtractCurrentParent.transform);
                if(child[i].layer != 11 && closestPos[i].gameObject.layer != 11)
                {
                    ErrorInput = 1;
                    Debug.Log("ErrorCCCCCCCCCCCCCC11111111111111");
                }
                //Debug.Log("Current Layer = "+ currentLayer);
                //Vector3 CurrentQua = Vector3(Mathf.Abs(gameObject.transform.localEulerAngles.x),Mathf.Abs(gameObject.transform.localEulerAngles.y),Mathf.Abs(gameObject.transform.localEulerAngles.z));
                Debug.Log("CurrentCondition.gameObject.tag" + CurrentCondition.gameObject.tag);
                Debug.Log("CurrentCondition.gameObject.tag" + TaggedCondition.gameObject.tag);
                Debug.Log("ErrorInput"+ ErrorInput);
                if(CurrentCondition.gameObject.tag == "Available" && TaggedCondition.gameObject.tag == "Available" && ErrorInput == 0)
                {
                    
                    Debug.Log("Current Layer = "+ currentLayer);
                    //Debug.Log("CLOSESTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT + "+ closestPos[i].name);
                    //gameObject.transform.position = closestPos[i].position;
                    if(currentLayer == 10)
                    {
                        GameObject parentComponent1 = FindParent(closestPos[i].gameObject);
                        GameObject parentComponent2 = FindParent(child[i].gameObject);
                        //if(parentComponent1.name != "Robot" && parentComponent2.name != "Robot" && parentComponent1.name != parentComponent2.name)
                        //{
                        //    Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //}
                        //else if(parentComponent1.layer == 11)
                        //{
                        //    Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //    parentComponent1.transform.SetParent(parentComponent2.transform);
                        //}
                        //GameObject StoreparentComponent = parentComponent1.transform.Find("Component").gameObject;
                        parentComponent2.transform.SetParent(parentComponent1.transform);
                        gameObject.tag = "Undrag";
                    }
                    if(currentLayer == 11)
                    {
                        GameObject parentComponent1 = FindParent(closestPos[i].gameObject);
                        GameObject parentComponent2 = FindParent(child[i].gameObject);
                        //if(parentComponent1.name != "Robot" && parentComponent2.name != "Robot" && parentComponent1.name != parentComponent2.name)
                        //{
                        //    Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //}
                        //GameObject StoreparentComponent = parentComponent1.transform.Find("Component").gameObject;
                        parentComponent2.transform.SetParent(parentComponent1.transform);
                        gameObject.tag = "Undrag";
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
                    }*/
                    if(currentLayer == 14)
                    {
                        GameObject parentComponent1 = FindParent(closestPos[i].gameObject);
                        GameObject parentComponent2 = FindParent(child[i].gameObject);
                        //if(parentComponent1.layer == 15)
                        //{
                        //    if(parentComponent1.name != "Robot" && parentComponent2.name != "Robot" && parentComponent1.name != parentComponent2.name)
                        //    {
                        Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //        Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //    }
                        //    //GameObject StoreparentComponent = parentComponent2.transform.Find("Component").gameObject;
                        parentComponent2.transform.SetParent(parentComponent1.transform);
                        gameObject.tag = "Undrag";
                        //}
                        //else if(parentComponent1.layer == 11)
                        //{
                        //    Destroy(parentComponent1.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent1.GetComponent<BoxCollider>());
                        //    parentComponent1.transform.SetParent(parentComponent2.transform);
                        //}
                        //else
                        //{
                        //    Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent2.GetComponent<BoxCollider>());
                            //GameObject StoreparentComponent = parentComponent.transform.Find("Component").gameObject;
                        //    parentComponent2.transform.SetParent(parentComponent1.transform);
                        //}
                        
                    }
                    if(currentLayer == 15)
                    {
                        GameObject parentComponent1 = FindParent(closestPos[i].gameObject);
                        GameObject parentComponent2 = FindParent(child[i].gameObject);
                        //Debug.Log("PARENT1 = "+parentComponent1.name);
                        Debug.Log("PARENT2 = "+parentComponent2.name);                        
                        //if(parentComponent1.layer == 11)
                        //{
                        Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //Destroy(parentComponent2.GetComponent<BoxCollider>());
                        parentComponent2.transform.SetParent(parentComponent1.transform);
                        gameObject.tag = "Undrag";
                        //}

                        //else if(parentComponent1.name != "Robot" && parentComponent2.name != "Robot" && parentComponent1.name != parentComponent2.name)
                        //{
                        //    Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //    parentComponent2.transform.SetParent(parentComponent1.transform);
                        //}
                        //GameObject StoreparentComponent = parentComponent.transform.Find("Component").gameObject;
                    }
                    if(currentLayer == 17)
                    {
                        GameObject parentComponent1 = FindParent(closestPos[i].gameObject);
                        GameObject parentComponent2 = FindParent(child[i].gameObject);
                        //GameObject StoreparentComponent = parentComponent2.transform.Find("Component").gameObject;
                        //if(parentComponent1.layer == 17)
                        //{
                        //    if(parentComponent1.name != "Robot" && parentComponent2.name != "Robot")
                        //    {
                        //        robot = GameObject.Find("Robot");
                        //        parentComponent1.transform.SetParent(robot.transform);
                        //       parentComponent2.transform.SetParent(robot.transform);
                        //        Destroy(parentComponent1.GetComponent<Rigidbody>());
                        //        Destroy(parentComponent1.GetComponent<BoxCollider>());
                        //        Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //        Destroy(parentComponent2.GetComponent<BoxCollider>());
                        //    }
                        //    else if(parentComponent1.name != "Robot" && parentComponent2.name == "Robot")
                        //    {
                        //        robot = GameObject.Find("Robot");
                        //        parentComponent1.transform.SetParent(robot.transform);
                        //        Destroy(parentComponent1.GetComponent<Rigidbody>());
                        //        Destroy(parentComponent1.GetComponent<BoxCollider>());   
                        //    }
                        //    else if(parentComponent1.name == "Robot" && parentComponent2.name != "Robot")
                        //    {
                        //        robot = GameObject.Find("Robot");
                        //        parentComponent2.transform.SetParent(robot.transform);
                         //       Destroy(parentComponent2.GetComponent<Rigidbody>());
                        //        Destroy(parentComponent2.GetComponent<BoxCollider>());   
                         //   }
                        //}
                        //else if(parentComponent1.layer == 11)
                        //{
                        //    Destroy(parentComponent1.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent1.GetComponent<BoxCollider>());
                        //    parentComponent1.transform.SetParent(parentComponent2.transform);
                        //}
                        //else if(parentComponent1.layer != 17 && parentComponent1.name != "Robot" && parentComponent2.name != "Robot" && parentComponent1.name != parentComponent2.name)
                        //{
                        //    Destroy(parentComponent1.GetComponent<Rigidbody>());
                        //    Destroy(parentComponent1.GetComponent<BoxCollider>()); 
                            parentComponent2.transform.SetParent(parentComponent1.transform);
                            gameObject.tag = "Undrag";
                        //}
                    }
                    /*Debug.Log("CLOSESTTTTTROTATIONNNNNNNNNNN + "+ closestPos[i].localEulerAngles.y);
                    Debug.Log("transformROTATIONNNNNNNNNNN + "+ transform.localEulerAngles.y);
                    gameObject.transform.eulerAngles = new Vector3(Mathf.Abs(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x)
                    ,Mathf.Abs(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y),Mathf.Abs(UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z));
                    gameObject.transform.position = closestPos[i].position;/*
                    //float Diffx = UnityEditor.TransformUtils.GetInspectorRotation(closestPos[i].transform).x - UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
                    //Debug.Log("Diffx ="+Diffx);
                    /*Debug.Log("Current x ="+ UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x);
                    Debug.Log("Current y ="+ UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).y);
                    Debug.Log("Current z ="+ UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).z);
                    x = UnityEditor.TransformUtils.GetInspectorRotation(closestPos[i].transform).x;
                    y = UnityEditor.TransformUtils.GetInspectorRotation(closestPos[i].transform).y;
                    z = UnityEditor.TransformUtils.GetInspectorRotation(closestPos[i].transform).z;
                    Debug.Log("x ="+ x);
                    Debug.Log("y ="+ y);
                    Debug.Log("z ="+ z);
                    transform.eulerAngles = new Vector3(-x,-y,-z);
                    Debug.Log("tranform.eulerAngles x ="+ x );
                    Debug.Log("tranform.eulerAngles y ="+ y );
                    Debug.Log("tranform.eulerAngles z ="+ z );*/
                    /*float EulerDiffx = (-1)*(closestPos[i].localEulerAngles.x - transform.localEulerAngles.x);
                    float EulerDiffy = (-1)*(closestPos[i].localEulerAngles.y - transform.localEulerAngles.y);
                    float EulerDiffz = (-1)*(closestPos[i].localEulerAngles.z - transform.localEulerAngles.z);
                    Debug.Log("EulerDiffEulerDiffEulerDiffxxxxxxxxxxxxxxxx + "+ EulerDiffx);
                    Debug.Log("EulerDiffEulerDiffEulerDiffyyyyyyyyyyyyyy + "+ EulerDiffy);
                    Debug.Log("EulerDiffEulerDiffEulerDiffzzzzzzzzzzzzz + "+ EulerDiffz);
                    if(EulerDiffx >= 90 || EulerDiffx <= -90)
                    {
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEE="+ closestPos[i].name);
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEE="+ closestPos[i].rotation);
                        if(currentLayer != 15)
                        {
                            Debug.Log("FKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                            Debug.Log("transform.rotation.eulerAngles.y"+ transform.rotation.eulerAngles.y);
                            Debug.Log("transform.rotation.eulerAngles.y"+ transform.rotation.eulerAngles.z);
                            //gameObject.transform.localRotation  = closestPos[i].transform.localRotation;
                            gameObject.transform.rotation = Quaternion.Euler(EulerDiffx, 0, 0);
                        }    
                    }
                    if(EulerDiffy >= 90 || EulerDiffy <= -90)
                    {
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEy="+ closestPos[i].name);
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEy="+ closestPos[i].rotation);
                        if(currentLayer != 15)
                        {
                            Debug.Log("FKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                            //gameObject.transform.localRotation  = closestPos[i].transform.localRotation;
                            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,EulerDiffy, transform.rotation.eulerAngles.z);
                        }    
                    }
                    if(EulerDiffz >= 90 || EulerDiffz <= -90)
                    {
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEE="+ closestPos[i].name);
                        Debug.Log("CLOSEEEEEEEEEEENAMEEEEEEEEEEEEE="+ closestPos[i].rotation);
                        if(currentLayer != 15)
                        {
                            Debug.Log("FKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
                            //gameObject.transform.localRotation  = closestPos[i].transform.localRotation;
                            gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,EulerDiffz);
                        }    
                    }*/
                    //Debug.Log("closestPos[i].localEulerAngles.x" + closestPos[i].localEulerAngles.x);
                    //Debug.Log("closestPos[i].localEulerAngles.y" + closestPos[i].localEulerAngles.y);
                    //Debug.Log("closestPos[i].localEulerAngles.z" + closestPos[i].localEulerAngles.z);
                    //Debug.Log("transform.localEulerAngles.x" + transform.localEulerAngles.x);
                    //Debug.Log("transform.localEulerAngles.y" + transform.localEulerAngles.y);
                    //Debug.Log("transform.localEulerAngles.z" + transform.localEulerAngles.z);

                    //transform.localEulerAngles  = closestPos[i].localEulerAngles;
                    
                    //float PaDiffx = Mathf.Abs(gameObject.transform.position.x - child[i].transform.position.x);
                    //float PaDiffy = Mathf.Abs(gameObject.transform.position.y - child[i].transform.position.y);
                    //float PaDiffz = Mathf.Abs(gameObject.transform.position.z - child[i].transform.position.z);
                    gameObject.transform.position = closestPos[i].position;
                    PaDiff = gameObject.transform.position - child[i].transform.position;
                    //Debug.Log("PaDiffx ="+ PaDiffx);
                    //Debug.Log("PaDiffy ="+ PaDiffy);
                    //Debug.Log("PaDiffz ="+ PaDiffz);
                    gameObject.transform.position = gameObject.transform.position + PaDiff;
                    CurrentCondition.gameObject.tag = "Unavailable";
                    TaggedCondition.gameObject.tag = "Unavailable";
                    //Debug.Log("CurrentLayer ="+child[i].layer);
                    //Debug.Log("Error");
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
        trans.Clear();
        closestPos.Clear();
        child.Clear();
        CountClosest = 0;
        currentLayer = gameObject.layer;
        taggedObjects = GameObject.FindGameObjectsWithTag("ConnectorSlot");
        
        foreach (GameObject something in taggedObjects)
        {
            //Debug.Log("TAGGEDNAMEEEEEEEEEE: "+ something.name);
        }
        distance = Mathf.Infinity;
        trans = FindChildClosest(gameObject);
        /*foreach (Transform item in gameObject.transform)
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
        }*/
        return trans;
    }
    
    public List<Transform> FindChildClosest(GameObject item)
    {
        DisPosition = item.gameObject.transform.position;
        foreach (GameObject go in taggedObjects)
            {
                Vector3 diff = go.transform.position - DisPosition;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < 0.3 && item.CompareTag("ConnectorSlot") && item.layer != go.layer)
                {
                    Debug.Log("GOOOOOOOOINNNNNNNNNNNNNN+ "+ go.name);
                    Debug.Log("CountClosestCountClosestCountClosest =========" + CountClosest);
                    //rb2.isKinematic = true;
                    //go.GetComponent<Renderer>().material.color = new Color32(255,255,255,255);
                    child.Add(item.gameObject);
                    trans.Add(go.transform);
                    distance = curDistance;
                    CountClosest++;    
                }
            }
        for (int i = 0; i < item.transform.childCount; i++)
        {
            GameObject newChild = item.transform.GetChild(i).gameObject;
            FindChildClosest(newChild);
        }
    //Debug.Log("TRANS[0] + "+ trans[2].name);
    return trans;
    }

    public GameObject FindParent(GameObject item)
    {
        
        if(item.transform.parent == null)
        {
            return newParent;
        }

        if(item.transform.parent.name == "Component")
        {
            return item;
        }
        else
        {
            newParent = item.transform.parent.gameObject;
            FindParent(newParent);
        }
    //Debug.Log("TRANS[0] + "+ trans[2].name);
    return newParent;
    }
}
