using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToOtherScene : MonoBehaviour
{
    public GameObject[] removeItem;
    public void ChangeSceneReset()
    {
        if(DetectEnvironment.keepPosition != null)
        {
            int MaxList = 0;
            int listNum = 0;
            MaxList = DetectEnvironment.keepPosition.Count;
            removeItem = new GameObject[MaxList];
            Debug.Log(MaxList);
            foreach (GameObject item in DetectEnvironment.keepPosition)
            {
                removeItem[listNum] = item;
                listNum++;
            }
            for (int i = 0; i < listNum; i++)
            {
                DetectEnvironment.keepPosition.Remove(removeItem[i]); 
                Destroy(removeItem[i]);
            }
        }
        LoadScreen.spawnFromLoadName.Clear();
        LoadScreen.spawnFromLoadVector3.Clear();
        LoadScreen.spawnFromLoadQuaternion.Clear();
        SceneManager.LoadScene("MenuScene");
    }
}
