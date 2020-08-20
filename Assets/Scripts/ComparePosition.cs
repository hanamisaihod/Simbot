using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparePosition : MonoBehaviour
{
    public static bool SearchForPosition(Transform ObjToMove)
    {
        bool unavailable = false;
        foreach (GameObject item in DetectEnvironment.keepPosition)
        {
            if(ObjToMove.position == item.transform.position)
            {
                unavailable = true;
            }
        }
        Debug.Log("unavailable: "+ unavailable);
        return unavailable;
    }
}
