using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfirmOnPlace
{
    public static void OnPlace()
    {
        if(ConfirmPlace.confirmPlace == true)
        {
            NewDrag.TriggerOnConfirmation();
        }
    }
}
