﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public static bool viewBlock;
    public void GoCode()
	{
		viewBlock = true;
		SceneManager.LoadScene("BlockProgramming");
	}
    public void GoMap()
    {
        Debug.Log("Go to map building scene . . .  ");
        SceneManager.LoadScene("MapBuilding");
    }
	public void GoMissionSelect()
	{
		viewBlock = false;
		SceneManager.LoadScene("LoadMap");
	}

	public void GoToMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
	public void GoToMenuFromBlock()
	{
		if (viewBlock)
		{
			SceneManager.LoadScene("MenuScene");
		}
		else
		{
			SceneManager.LoadScene("LoadMap");
		}
	}
    public void GoToSimulate() //Update this when AR is implemented
    {
        if (GameObject.Find("BlockSaveManager"))
        {
            GameObject.Find("BlockSaveManager").GetComponent<BlockSaveAndLoad>().SaveBlockProgram();
        }
        SceneManager.LoadScene("TestRobotMovementScene");
    }
}
