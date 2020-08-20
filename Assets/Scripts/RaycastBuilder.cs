using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RaycastBuilder : MonoBehaviour
{
    public Transform ObjToMove;
    public GameObject ObjToPlace;
    public LayerMask mask;
    public static float LastPosX,LastPosY,LastPosZ;
    Vector3 mousePos;
    public static GameObject spawnObject;
    public DetectEnvironment call;
    public static  Vector3 blockPos;
    private float ZCoord;
    private Vector3 offset;
    private Transform BuildObject;
    public static Transform hitObject;
    //private CollisionStructure collisionStructure;
    //private int Level = 0;

    void Update()
    {
        //if(SelectPrefab.model != null)
        //{
            //ObjToMove = SelectPrefab.model.transform;
            //ObjToPlace = SelectPrefab.model;
        //}
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit,Mathf.Infinity,mask))
        {
            hitObject = hit.transform;
            blockPos = hit.point + hit.normal/2.0f;
            
            blockPos.x = (float) Math.Round(blockPos.x,MidpointRounding.AwayFromZero);
            blockPos.y = (float) Math.Round(blockPos.y,MidpointRounding.AwayFromZero);
            blockPos.z = (float) Math.Round(blockPos.z,MidpointRounding.AwayFromZero);

            /*if(blockPos.x != LastPosX || blockPos.y != LastPosY || blockPos.z != LastPosZ )
            {
                LastPosX = blockPos.x;
                LastPosY = blockPos.y;
                LastPosZ = blockPos.z;
                //Debug.Log("LastPosX: " + LastPosX + " & LastPosZ: " + LastPosZ);
                //collisionStructure = ObjToMove.GetComponent<CollisionStructure>();
                if(ObjToMove != null)
                {
                    ObjToMove.position = new Vector3(blockPos.x, 0 ,blockPos.z);
                    Debug.Log("ObjToMove: "+ ObjToMove.transform.position);
                }
            }
            
            if(Input.GetMouseButtonDown(0) && ObjToPlace != null)
            {
                if(IsLegalPosition())
                {
                    //Debug.Log("LastPosX: " + LastPosX + " & LastPosZ: " + LastPosZ);
                    spawnObject = Instantiate(ObjToPlace,ObjToMove.position,ObjToMove.transform.rotation) as GameObject;
                    call.StoreSpawnPosition();
                }
            }*/
        }
    }
    /*bool IsLegalPosition()
    {
        if(ComparePosition.SearchForPosition(ObjToMove))
        {
            return false;
        }
        if(collisionStructure.colliders.Count > 0)
        {
            return false;
        }
        return true;
    }*/
}
