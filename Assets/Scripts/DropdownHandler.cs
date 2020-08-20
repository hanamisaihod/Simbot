using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
	public int prevAction, prevDirection, prevTimes, prevRepeat;
	public GameObject actionDrop, rotationDrop, directionDrop, isDrop, colorDrop, repeatDrop, numField;
	private BuildingHandler parentHandlerScript;
	public GameObject centerExtendPart, tailExtendPart;
	public List<GameObject> tempList;
	void Awake()
	{
		parentHandlerScript = transform.parent.GetComponent<BuildingHandler>();
		tempList = new List<GameObject>();
		tempList.Add(actionDrop); tempList.Add(rotationDrop); tempList.Add(directionDrop); tempList.Add(isDrop);
		tempList.Add(colorDrop); tempList.Add(repeatDrop); tempList.Add(numField);
		UpdateActiveOptions();
	}

    public void HandleActionDropdown(int val)
	{
		if (actionDrop != null)
		{
			parentHandlerScript.actionChoice = val;
			if (prevAction != 1 && val == 1)
			{
				numField.SetActive(false);
				rotationDrop.SetActive(true);
			}
			else if (prevAction == 1 && val != 1)
			{
				numField.SetActive(true);
				rotationDrop.SetActive(false);
			}
			prevAction = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleRotationDropdown(int val)
	{
		if (rotationDrop != null)
		{
			parentHandlerScript.rotationChoice = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleDirectionDropdown(int val)
	{
		if (directionDrop != null)
		{
			parentHandlerScript.directionChoice = val;
			if (prevDirection != 4 && val == 4)
			{
				isDrop.SetActive(false);
				colorDrop.SetActive(true);
				colorDrop.GetComponent<TMP_Dropdown>().value = parentHandlerScript.colorChoice;
			}
			else if (prevDirection == 4 && val != 4)
			{
				isDrop.SetActive(true);
				colorDrop.SetActive(false);
				isDrop.GetComponent<TMP_Dropdown>().value = parentHandlerScript.isChoice;
			}
			prevDirection = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleIsDropdown(int val)
	{
		if (isDrop != null)
		{
			parentHandlerScript.isChoice = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleColorDropdown(int val)
	{
		if (colorDrop != null)
		{
			parentHandlerScript.colorChoice = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleRepeatDropdown(int val)
	{
		if (repeatDrop != null)
		{
			parentHandlerScript.repeatChoice = val;
			if (prevRepeat != 0 && val == 0)
			{
				parentHandlerScript.ExtendTop(0, centerExtendPart, tailExtendPart);
				directionDrop.SetActive(false);
				numField.SetActive(false);
			}
			else if (prevRepeat != 1 && val == 1)
			{
				parentHandlerScript.ExtendTop(1, centerExtendPart, tailExtendPart);
				directionDrop.SetActive(false);
				numField.SetActive(true);
			}
			else if (prevRepeat != 2 && val == 2)
			{
				parentHandlerScript.ExtendTop(2, centerExtendPart, tailExtendPart);
				directionDrop.SetActive(true);
				numField.SetActive(false);
			}
			prevRepeat = val;
			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}
	public void HandleTimesField(string val)
	{
		if (numField != null)
		{
			prevTimes = parentHandlerScript.timesChoice;
			if (int.Parse(val) > 0)
			{
				parentHandlerScript.timesChoice = int.Parse(val);
			}
			else
			{
				numField.GetComponent<TMP_InputField>().text = prevTimes.ToString();
				Debug.LogError("Number of times cannot be less than 1!");
			}

			//DisplayBlockOptions();
			UpdateActiveOptions();
		}
	}

	public void DisplayBlockOptions()
	{
		Debug.Log("Action:" + parentHandlerScript.actionChoice + "\n" +
				"Rotation:" + parentHandlerScript.rotationChoice + "\n" +
				"Direction:" + parentHandlerScript.directionChoice + "\n" +
				"Is:" + parentHandlerScript.isChoice + "\n" +
				"Color:" + parentHandlerScript.colorChoice + "\n" +
				"Repeat:" + parentHandlerScript.repeatChoice + "\n" +
				"Times:" + parentHandlerScript.timesChoice + "\n"); 
	}

	public void UpdateActiveOptions()
	{
		for (int i = 0; i < parentHandlerScript.dropActives.Length; i++)
		{
			if (tempList[i])
			{
				if (tempList[i].activeInHierarchy)
				{
					parentHandlerScript.dropActives[i] = 1;
				}
				else
				{
					parentHandlerScript.dropActives[i] = 0;
				}
			}
		}
	}
}
