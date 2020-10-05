﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public static bool viewBlock;
    public void GoCodeSelect()
	{
		viewBlock = true;
		SceneManager.LoadScene("LoadBlock");
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
}