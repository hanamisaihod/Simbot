using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public static string inputMap;
	public static string inputBlock;
	public InputField txt_Input;
    public static string subMode;
    public Text txt;
    public void ClickChangeScene()
    {
		foreach (string str in LoadScreen.allKeyword)
		{
			Debug.Log("map name: " + str);
		}
		bool mapExisted = false;
        inputMap = txt_Input.text;
        string inputMapLower = inputMap.ToLower();
        foreach (string str in LoadScreen.allKeyword)
        {
            string strLower = str.ToLower();
            if (inputMapLower == strLower)
            {
                ChangeTextToRed();
                mapExisted = true;
			}
        }
        if (!mapExisted)
        {
            if (GameObject.FindGameObjectWithTag("VariableCarrier"))
            {
                GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName = inputMap;
                GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().newMap = true;
            }
            Debug.Log("Sim MODEEEEEEEEEEEEEEEEEEEEEEE = " + EnviSim.Mode);
            SceneManager.LoadScene("SelectRobot");
        }
    }
    public void ChangeTextToRed()
	{
        txt.color = Color.red;
	}
    public void ChangeTextToWhite()
    {
        txt.color = Color.white;
    }
}
