using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInterpreter : MonoBehaviour
{
	//public List<GameObject> repeats;
	//public GameObject player;
	//public int currentTimes = 0;

	
	//public void StartReading()
	//{
	//	currentTimes = 0;
	//	repeats = new List<GameObject>();
	//	GameObject startBlock = GameObject.FindGameObjectWithTag("StartBlock");
	//	if (!startBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy)
	//	{
	//		Debug.LogError("There is nothing connected to the start block!");
	//	}
	//	else
	//	{
	//		Debug.Log("Start interpreting . . .");
	//		StartCoroutine(WaitForRead(startBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject));
	//	}
	//}

	//public void ReadInBlock(GameObject block)
	//{
	//	block.LeanScale(new Vector3(block.transform.localScale.x + 0.2f, block.transform.localScale.y + 0.2f, block.transform.localScale.z + 0.2f), 0.2f);
	//	StartCoroutine(WaitAnimation(block));
	//	currentTimes = 0;
	//	if (block.tag == "DoBlock") //If it is an action block
	//	{
	//		int count = block.GetComponent<BuildingHandler>().timesChoice;
	//		if (count == 0)
	//		{
	//			count = 1;
	//		}
	//		StartCoroutine(DoCommand(count, 1.0f, block.GetComponent<BuildingHandler>().actionChoice, block));
	//	}
	//	else if (block.tag == "IfBlock") //If it is an if block
	//	{
	//		if (CheckCondition(block))
	//		{
	//			if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy)
	//			{
	//				StartCoroutine(WaitForRead(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject));
	//			}
	//		}
	//		else
	//		{
	//			if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy)
	//			{
	//				StartCoroutine(WaitForRead(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject));
	//			}
	//			else
	//			{
	//				if (repeats.Count > 0)
	//				{
	//					StartCoroutine(WaitForRead(repeats[repeats.Count - 1]));
	//				}
	//			}
	//		}
	//	}
	//	else if (block.tag == "RepeatBlock") //If it is a repeat block
	//	{
	//		if (CheckRepeat(block)) //If condition is true
	//		{
	//			if (!repeats.Contains(block)) //If not in the list
	//			{
	//				repeats.Add(block);
	//			}
	//			if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy)
	//			{
	//				StartCoroutine(WaitForRead(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject));
	//			}
	//			else
	//			{
	//				if (repeats.Count > 0)
	//				{
	//					StartCoroutine(WaitForRead(repeats[repeats.Count - 1]));
	//				}
	//			}
	//		}
	//		else
	//		{
	//			if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy)
	//			{
	//				StartCoroutine(WaitForRead(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject));
	//			}
	//			else
	//			{
	//				if (repeats.Count > 0)
	//				{
	//					StartCoroutine(WaitForRead(repeats[repeats.Count - 1]));
	//				}
	//			}
	//		}
	//	}
	//}

	//IEnumerator WaitAnimation(GameObject block)
	//{
	//	yield return new WaitForSeconds(0.2f);
	//	block.LeanScale(new Vector3(block.transform.localScale.x - 0.2f, block.transform.localScale.y - 0.2f, block.transform.localScale.z - 0.2f), 0.2f);
	//}

	//public bool CheckRepeat(GameObject block)
	//{
	//	if (block.GetComponent<BuildingHandler>().repeatChoice == 0) //Until finish
	//	{
	//		if (!block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy)
	//		{
	//			return false;
	//		}
	//		return true;
	//	}
	//	else if (block.GetComponent<BuildingHandler>().repeatChoice == 1) //For x times
	//	{
	//		if (block.GetComponent<BuildingHandler>().currentRepeatTimes < block.GetComponent<BuildingHandler>().timesChoice)
	//		{
	//			block.GetComponent<BuildingHandler>().currentRepeatTimes++;
	//			return true;
	//		}
	//		else
	//		{
	//			repeats.Remove(block);
	//			block.GetComponent<BuildingHandler>().currentRepeatTimes = 0;
	//			return false;
	//		}
	//	}
	//	else //While x is true
	//	{
	//		return (CheckCondition(block));
	//	}
	//}

	//public bool CheckCondition(GameObject block)
	//{
	//	if (block.GetComponent<BuildingHandler>().dropActives[2] == 1) //If check direction
	//	{
	//		if (block.GetComponent<BuildingHandler>().directionChoice == 4) //If check under(color)
	//		{
	//			switch (block.GetComponent<BuildingHandler>().colorChoice)
	//			{
	//				//Check color of ground
	//			}
	//		}
	//		else
	//		{
	//			//Check block at (directionChoice) if it is (isChoice)
	//		}
	//	}
	//	return true;
	//}

	//IEnumerator DoCommand(int count, float time, int command, GameObject doBlock)
	//{
	//	if (currentTimes < count)
	//	{
	//		yield return new WaitForSeconds(time);
	//		switch (command)
	//		{
	//			case 0: //Move
	//				Debug.Log("MoveForward");
	//				player.GetComponent<PlayerPosition>().callMoveForward();
	//				break;
	//			case 1: //Rotate
	//				switch (doBlock.GetComponent<BuildingHandler>().rotationChoice)
	//				{
	//					case 0: //Left
	//						Debug.Log("RotateLeft");
	//						player.GetComponent<PlayerPosition>().callRotateLeft();
	//						break;
	//					case 1: //Right
	//						Debug.Log("RotateRight");
	//						player.GetComponent<PlayerPosition>().callRotateRight();
	//						break;
	//					case 2: //Back
	//						break;
	//				}
	//				break;
	//			case 2: //Push
	//				break;
	//			case 3: //Pull
	//				break;
	//		}
	//		currentTimes++;
	//		StartCoroutine(DoCommand(count, time, command, doBlock));
	//	}
	//	else
	//	{
	//		yield return new WaitForSeconds(0);
	//		if (doBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy)
	//		{
	//			ReadInBlock(doBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.gameObject);
	//		}
	//		else
	//		{
	//			if (repeats.Count > 0)
	//			{
	//				ReadInBlock(repeats[repeats.Count - 1]);
	//			}
	//		}
	//	}
	//	//Switch case for command
	//}

	//IEnumerator WaitForRead(GameObject block)
	//{
	//	yield return new WaitForSeconds(1f);
	//	ReadInBlock(block);
	//}
}
