using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	public GameObject groundChecker1;
	public GameObject groundChecker2;
	public GameObject groundChecker3;
	public GameObject groundChecker4;

	private GameObject levelController;
	// Update is called once per frame
	private void Start()
	{
		levelController = GameObject.FindGameObjectWithTag("LevelController");
	}
	void Update()
    {
		if (!groundChecker4) // if Turtle bot
		{
			if (!Physics.Raycast(groundChecker1.transform.position, -Vector3.up)
				&& !Physics.Raycast(groundChecker2.transform.position, -Vector3.up)
				&& !Physics.Raycast(groundChecker3.transform.position, -Vector3.up))
			{
				levelController.GetComponent<LevelController>().stopRobot = true;
				if (!transform.parent.GetComponent<RobotMovementTest>().falling)
				{
					transform.parent.GetComponent<RobotMovementTest>().FallAnimation();
					transform.parent.GetComponent<RobotMovementTest>().falling = true;
				}
			}
		}
		else
		{
			if (!Physics.Raycast(groundChecker1.transform.position, -Vector3.up)
				&& !Physics.Raycast(groundChecker2.transform.position, -Vector3.up)
				&& !Physics.Raycast(groundChecker3.transform.position, -Vector3.up)
				&& !Physics.Raycast(groundChecker4.transform.position, -Vector3.up))
			{
				levelController.GetComponent<LevelController>().stopRobot = true;
				if (!transform.parent.GetComponent<RobotMovementTest>().falling)
				{
					transform.parent.GetComponent<RobotMovementTest>().FallAnimation();
					transform.parent.GetComponent<RobotMovementTest>().falling = true;
				}
			}
		}
	}
}
