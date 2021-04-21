using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private List<GameObject> productList;
    public List<GameObject> redzoneList;
	public ARModeSwitcher arModeSwitcher;
	private int deliveryLeft;
    public GameObject start;
    private GameObject goal;
	public GameObject canvasFX;
    public int robotType;
    public bool stopRobot = false;

    public bool completionStar = false;
    public bool robotHealthStar = false;
    public bool codeAmountStar = false;
    // Start is called before the first frame update
    private void Awake()
    {
        start = GameObject.FindGameObjectWithTag("Start");
        goal = GameObject.FindGameObjectWithTag("Goal");
    }
    void Start()
    {
        start = GameObject.FindGameObjectWithTag("Start");
        goal = GameObject.FindGameObjectWithTag("Goal");
		if (start)
		{
			if (!arModeSwitcher)
			{
				start.GetComponent<SpawnFX_Controller>().StartShowing();
				start.GetComponent<SpawnFX_Controller>().SpawnRobot(Robotcheck.robotTypeNum, false);
			}
			else
			{
				start.GetComponent<SpawnFX_Controller>().StartShowing();
				start.GetComponent<SpawnFX_Controller>().SpawnRobot(Robotcheck.robotTypeNum, true);
			}
		}
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
        redzoneList.AddRange(GameObject.FindGameObjectsWithTag("RedZone"));
        ActivateRedZone();
	}

    public void ActivateRedZone()
    {
        StartCoroutine(WaitRedZoneTime());
    }

    IEnumerator WaitRedZoneTime()
    {
        yield return new WaitForSeconds(6.0f);
        foreach (GameObject redzone in redzoneList)
        {
            redzone.GetComponent<Redzone_PS_Controller>().redZoneTrigger = true;
        }
        ActivateRedZone();
    }

	public void FindRobotAndStartReading()
	{
		Redzone_PS_Controller.startEruption = true;
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<RobotMovementTest>().StartReading();
		}
	}

	public void LockGoal()
	{
		//Lock
		if (goal != null)
		{
			goal.GetComponent<GoalFX_Controller>().StartLockGoal();
		}
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
		canvasFX.GetComponent<CanvasFX_Controller>().clearTrigger = true;
        stopRobot = true;
	}

	public void FailMission()
	{
		canvasFX.GetComponent<CanvasFX_Controller>().failTrigger = true;
	}
}
