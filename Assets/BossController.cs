﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float boss1Delay;
	public GameObject boss1;
    void Start()
	{
		//if (boss1)
		//{
		//	if (boss1.activeInHierarchy)
		//	{
		//		boss1.SetActive(false);
		//		StartCoroutine(delayedSpawn(boss1Delay, boss1));
		//	}
		//}
	}

	public void StartSpawning()
	{
		if (boss1 != null)
		{
			if (boss1.GetComponent<Tutorial_Activator>().thisIsActive)
			{
				boss1.SetActive(false);
				StartCoroutine(delayedSpawn(boss1Delay, boss1));
			}
		}
	}

	IEnumerator delayedSpawn(float delay, GameObject delayedSpawnObject)
	{
		yield return new WaitForSeconds(delay);
		delayedSpawnObject.SetActive(true);
	}
}
