using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private List<GameObject> productList;
	private int deliveryLeft;
	private GameObject goal;
	public GameObject canvasFX;
    // Start is called before the first frame update
    void Start()
    {
		goal = GameObject.FindGameObjectWithTag("Goal");
        if (GameObject.Find("Green_Destination"))
		{
			deliveryLeft++;
		}
		if (GameObject.Find("Purple_Destination"))
		{
			deliveryLeft++;
		}
		if (GameObject.Find("Red_Destination"))
		{
			deliveryLeft++;
		}
		if (GameObject.Find("Yellow_Destination"))
		{
			deliveryLeft++;
		}
		if (deliveryLeft > 0)
		{
			Debug.Log(deliveryLeft);
			LockGoal();
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void FindRobotAndStartReading()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<RobotMovementTest>().StartReading();
	}

	public void LockGoal()
	{
		//Lock
		goal.GetComponent<GoalFX_Controller>().StartLockGoal();
	}

	public void UnlockGoal()
	{
		deliveryLeft--;
		if (deliveryLeft == 0)
		{
			Debug.Log("Unlock");
			//Unlock Goal
			goal.GetComponent<GoalFX_Controller>().StartUnlockGoal();
		}
	}

	public void FinishMission()
	{
		goal.GetComponent<GoalFX_Controller>().StartTrigger();
		CheckStar();
		SaveMissionProgress();
		canvasFX.GetComponent<CanvasFX_Controller>().clearTrigger = true;
		//Star calculation
		//show complete ui
		//save progress
	}

	public void FailMission()
	{
		canvasFX.GetComponent<CanvasFX_Controller>().failTrigger = true;
	}

	public void CheckStar()
	{

	}

	public void SaveMissionProgress()
	{

	}
}
