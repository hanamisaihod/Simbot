using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectToMap : MonoBehaviour
{
    // Update is called once per frame
    public void ChangeSelectToMapBuidling()
    {
        SceneManager.LoadScene("MapBuilding");
    }
    public void ChangeSelectToLoadMap()
    {
        SceneManager.LoadScene("LoadMap");
    }

}
