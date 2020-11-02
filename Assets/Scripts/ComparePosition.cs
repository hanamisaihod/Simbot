using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ComparePosition : MonoBehaviour
{
    
    public static bool SearchForPosition(Transform ObjToMove)
    {
        bool unavailable = false;
        //Debug.Log("@Position: " + ObjToMove.position);

        foreach (GameObject item in DetectEnvironment.keepPosition)
        {
            if(ObjToMove.position == item.transform.position)
            {
                //Debug.Log("@Position: " + ObjToMove.position + "Same with Position: " + item.transform.position);
                //Debug.Log("TRUEEEEEEEEEEEEEE");
                unavailable = true;
            }
        }
        //Debug.Log("unavailable: "+ unavailable);

        return unavailable;
    }
}
