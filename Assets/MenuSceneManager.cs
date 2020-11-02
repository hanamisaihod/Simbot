using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    public static bool isMainMission;
    public void GoToMissionSelect()
    {
        isMainMission = true;
        SceneManager.LoadScene("MapBuilding"); //Map building for main mission
    }

    public void GoToCreative()
    {
        isMainMission = false;
        SceneManager.LoadScene("SelectRobot"); //The scene SelectRobot doesn't exist yet.
    }
    public void OpenSettings()
    {

    }
}
