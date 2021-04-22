using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	private List<GameObject> productList;
    public List<GameObject> redzoneList;
	public int greenDestination, yellowDestination, redDestination, purpleDestination;
	public int greenProduct, yellowProduct, redProduct, purpleProduct;
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
		//      if (GameObject.Find("Green_Destination"))
		//{
		//	deliveryLeft++;
		//}
		//if (GameObject.Find("Purple_Destination"))
		//{
		//	deliveryLeft++;
		//}
		//if (GameObject.Find("Red_Destination"))
		//{
		//	deliveryLeft++;
		//}
		//if (GameObject.Find("Yellow_Destination"))
		//{
		//	deliveryLeft++;
		//}
		//if (deliveryLeft > 0)
		//{
		//	Debug.Log(deliveryLeft);
		//	LockGoal();
		//}
		if (GatherProductsAndDestination() > 0)
		{
			LockGoal();
		}
		redzoneList.AddRange(GameObject.FindGameObjectsWithTag("RedZone"));
        ActivateRedZone();
	}

	public int GatherProductsAndDestination()
	{
		int allDestinationAndProductCount = 0;
		foreach(DestinationController desc in FindObjectsOfType<DestinationController>())
		{
			if (desc.green)
			{
				greenDestination++;
				allDestinationAndProductCount++;
			}
			if (desc.yellow)
			{
				yellowDestination++;
				allDestinationAndProductCount++;
			}
			if (desc.red)
			{
				redDestination++;
				allDestinationAndProductCount++;
			}
			if (desc.purple)
			{
				purpleDestination++;
				allDestinationAndProductCount++;
			}
		}
		foreach (SpawnerController spwn in FindObjectsOfType<SpawnerController>())
		{
			if (spwn.green)
			{
				greenProduct++;
				allDestinationAndProductCount++;
			}
			if (spwn.yellow)
			{
				yellowProduct++;
				allDestinationAndProductCount++;
			}
			if (spwn.red)
			{
				redProduct++;
				allDestinationAndProductCount++;
			}
			if (spwn.purple)
			{
				purpleProduct++;
				allDestinationAndProductCount++;
			}
		}
		return allDestinationAndProductCount;
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

	public void UnlockGoal(bool green, bool yellow, bool red, bool purple)
	{
		//deliveryLeft--;
		//if (deliveryLeft == 0)
		//{
		//	Debug.Log("Unlock");
		//	//Unlock Goal
		//	goal.GetComponent<GoalFX_Controller>().StartUnlockGoal();
		//}
		if (CheckForUnlockGoalCondition(green, yellow, red, purple))
		{
			goal.GetComponent<GoalFX_Controller>().StartUnlockGoal();
		}
	}

	private bool CheckForUnlockGoalCondition(bool green, bool yellow, bool red, bool purple)
	{
		if (greenDestination + yellowDestination + redDestination + purpleDestination != 
			greenProduct + yellowProduct + redProduct + purpleProduct)
		{
			return false;
		}
		else if (greenDestination != greenProduct 
			|| yellowDestination != yellowProduct
			|| redDestination != redProduct
			|| purpleDestination != purpleProduct)
		{
			return false;
		}
		if (green)
		{
			greenDestination--;
			greenProduct--;
		}
		if (yellow)
		{
			yellowDestination--;
			yellowProduct--;
		}
		if (red)
		{
			redDestination--;
			redProduct--;
		}
		if (purple)
		{
			purpleDestination--;
			purpleProduct--;
		}
		if (greenDestination == 0 && greenProduct == 0 
			&& yellowDestination == 0 && yellowProduct == 0
			&& redDestination == 0 && redProduct == 0
			&& purpleDestination == 0 && purpleProduct == 0)
		{
			return true;
		}
		return false;
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
