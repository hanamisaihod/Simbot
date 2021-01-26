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
    public void ClickChangeScene()
    {
        inputMap = txt_Input.text;
        if (GameObject.FindGameObjectWithTag("VariableCarrier"))
        {
            GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName = inputMap;
        }
        SceneManager.LoadScene("MapBuilding");
    }
}
