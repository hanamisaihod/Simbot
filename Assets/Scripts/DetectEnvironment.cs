using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetectEnvironment : MonoBehaviour
{
    public static List<GameObject> keepPosition = new List<GameObject>();
    public static Vector3 playerPos;
    public static GameObject attachModel;
    public static void StoreSpawnPosition()
    {
        int count = 1;
        if(NewDrag.spawnObject != null)
        {
            Debug.Log("NewDrag.spawnObject.gameObject: "+ NewDrag.spawnObject.gameObject.name);
            //Debug.Log("Spawn Position: " + RaycastBuilder.spawnObject.transform.position);
            keepPosition.Add(NewDrag.spawnObject.gameObject);
            foreach (GameObject item in keepPosition)
            {
                Debug.Log("Spawn No." + count + " Spawn Position: " + item.transform.position);
                //Debug.Log("KeepCount: " + keepPosition.Count);
                if(count  == keepPosition.Count)
                {
                    break;
                }
                count++;
            }
        }
    }
}
