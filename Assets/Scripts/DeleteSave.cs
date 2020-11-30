using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSave : MonoBehaviour
{
    public static bool clickToDelete = false;
    public void confirmDeleteSlot()
    {
        if(LoadConfirm.waitForSelectSlot == true)
        {
            clickToDelete = true;
        }
        
    }
}
