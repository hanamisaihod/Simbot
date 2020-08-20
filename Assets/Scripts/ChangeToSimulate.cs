using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToSimulate : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject[] removeOnlyKeep;
    public static bool simulate = false;
    public void ClickChangeScene()
    {
        if(LoadScreen.waitForSelectSlot == true)
        {
            simulate = true;
        }
    }
}
