using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{

    public void OnClickToSave()
    {
        GameEvent.OnSaveInitiated();
        //SceneManager.LoadScene("MapBuilding");
    }
}
