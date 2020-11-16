using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System;


public class NewDrag : MonoBehaviour
{
    public static Transform ObjToMove;
    public static GameObject ObjToPlace;
    public static GameObject spawnObject;

    void OnMouseDrag()
    {
        ObjToMove = gameObject.transform;
        ObjToPlace = gameObject;
        //Debug.Log("CHECKKKKKKKKKKK");
        //Debug.Log("RaycastBuilder.blockPos.x" + RaycastBuilder.blockPos.x);
        //Debug.Log("RaycastBuilder.blockPos.y" + RaycastBuilder.blockPos.y);
        //Debug.Log("RaycastBuilder.blockPos.z" + RaycastBuilder.blockPos.z);
        if(RaycastBuilder.blockPos.x != RaycastBuilder.LastPosX || RaycastBuilder.blockPos.y != RaycastBuilder.LastPosY || RaycastBuilder.blockPos.z != RaycastBuilder.LastPosZ )
        {
            //Debug.Log("CHECKKKKKKKKKKK");
            
            RaycastBuilder.LastPosX = RaycastBuilder.blockPos.x;
            RaycastBuilder.LastPosY = RaycastBuilder.blockPos.y;
            RaycastBuilder.LastPosZ = RaycastBuilder.blockPos.z;
            ObjToMove.position = new Vector3(RaycastBuilder.blockPos.x, 0 ,RaycastBuilder.blockPos.z);      
        }

    }

    public static void TriggerOnConfirmation()
    {
        if(ObjToPlace != null && ConfirmPlace.confirmPlace == true)
            {
                if(IsLegalPosition())
                {
                    //Debug.Log("LastPosX: " + LastPosX + " & LastPosZ: " + LastPosZ);
                    spawnObject = Instantiate(ObjToPlace,ObjToMove.position,ObjToMove.transform.rotation) as GameObject;
                    spawnObject.tag = "Untagged";
                    for (int i = 0; i < spawnObject.transform.childCount; i++)
                    {
                        GameObject child = spawnObject.transform.GetChild(i).gameObject;
                        child.layer = 21;
                    }
                    string PlaceName = spawnObject.name.Replace("(Clone)", "");
                    //Debug.Log("Helloppppppppppppp" + PlaceName);
                    if(PlaceName == "Box-Cube-Floor")
                    {
                        Debug.Log("Helloppppppppppppp");
                        Transform getCube = spawnObject.transform.Find("Cube_Evo");
                        getCube.GetComponent<Rigidbody>().isKinematic = false;
                    }
                    spawnObject.layer = 21;
                    spawnObject.GetComponent<BoxCollider>().enabled = false;
                    spawnObject.GetComponent<MeshRenderer>().enabled = false;
                    //ObjToMove = null;
                    //ObjToPlace = null;
                    //Destroy(gameObject);
                    DetectEnvironment.StoreSpawnPosition();
                    ConfirmPlace.confirmPlace = false;
                    ObjToMove.transform.position = new Vector3(ObjToMove.transform.position.x, ObjToMove.transform.position.y, ObjToMove.transform.position.z);
                }
            }
    }

    public static bool IsLegalPosition()
    {
        if(ComparePosition.SearchForPosition(ObjToMove))
        {
            Debug.Log("CompareValue: " + ComparePosition.SearchForPosition(ObjToMove));
            return false;
        }
        return true;
    }
}
