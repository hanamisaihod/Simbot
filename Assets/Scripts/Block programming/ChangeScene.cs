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
		bool mapExisted = false; ;
        inputMap = txt_Input.text;
        foreach (string str in LoadScreen.allKeyword)
        {
            if (inputMap == str)
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
