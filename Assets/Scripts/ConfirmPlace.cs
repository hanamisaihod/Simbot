using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System;

public class ConfirmPlace : MonoBehaviour
{
    public static bool confirmPlace = false; 

    public void OnClickConfirmPlace()
    {
        if(CurrentState.state == 1)
        {
            confirmPlace = true;
            ConfirmOnPlace.OnPlace();
        }   
    }
}
