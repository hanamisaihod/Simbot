using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentState : MonoBehaviour
{
    public static int state;

    void updateState()
    {
        //Beginning state
        state = 1;// build
        if(state == 1)
        {
            Debug.Log("Build Mode");
        }
        else if(state == 2)
        {
            Debug.Log("Remove Mode");
        }
    }
}
