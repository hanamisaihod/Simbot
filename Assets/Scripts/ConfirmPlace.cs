using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPlace : MonoBehaviour
{
    public static bool confirmPlace = false;
    // Update is called once per frame
    public void OnClickConfirmPlace()
    {
        
        if(CurrentState.state == 1)
        {
            confirmPlace = true;
        }
    }
}
