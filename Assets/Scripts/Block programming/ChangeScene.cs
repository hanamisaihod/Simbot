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
        SceneManager.LoadScene("LoadMap");
    }
	public void ClickChangeSceneBlock()
	{
		inputBlock = txt_Input.text;
		BlockSaveSystem.newBlockProgram = true;
		SceneManager.LoadScene("BlockProgramming");
	}
}
