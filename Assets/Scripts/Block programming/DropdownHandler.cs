using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownHandler : MonoBehaviour
{
    public int prevIf, prevRepeat ,prevTimes;
    public float prevDistance;
    public GameObject speedDrop, torqueDrop, delayDrop, ifDrop, degreeDrop, compareDegreeDrop, distanceDrop, compareLeftDrop, compareRightDrop, colorLeftDrop, colorRightDrop, repeatDrop, timesDrop, andDrop;
    private BuildingHandler parentHandlerScript;
	public GameObject centerExtendPart, tailExtendPart;
	public List<GameObject> tempList;
    void Awake()
	{
		parentHandlerScript = transform.parent.GetComponent<BuildingHandler>();
		tempList = new List<GameObject>();
		tempList.Add(speedDrop); tempList.Add(torqueDrop); tempList.Add(delayDrop); tempList.Add(ifDrop);
		tempList.Add(degreeDrop); tempList.Add(compareDegreeDrop); tempList.Add(distanceDrop); tempList.Add(compareLeftDrop);
        tempList.Add(compareRightDrop); tempList.Add(colorLeftDrop); tempList.Add(colorRightDrop); tempList.Add(repeatDrop);
        tempList.Add(timesDrop); tempList.Add(andDrop);

        UpdateActiveOptions();
	}
    private void Start()
    {
        if (GameObject.Find("ModeSwitcher"))
        {
            foreach (GameObject obj in GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects)
            {
                if (obj.tag == "SubCamera")
                {
                    this.GetComponent<Canvas>().worldCamera = obj.GetComponent<Camera>();
                }
            }
        }
    }
    /*public void HandleActionDropdown(int val)
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
			UpdateActiveOptions();
		}
	}
	public void HandleRotationDropdown(int val)
	{
		if (rotationDrop != null)
		{
			parentHandlerScript.rotationChoice = val;
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
	}*/
    public void HandleSpeedField(string val)
    {
        if (speedDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.speedChoice = 0;
            }
            else if (val == "-")
            {
                parentHandlerScript.speedChoice = 0;
            }
            else
            {
                if (float.Parse(val) > 2)
                {
                    speedDrop.GetComponent<TMP_InputField>().text = "2";
                    parentHandlerScript.speedChoice = 2;
                }
                else if (float.Parse(val) < -2)
                {
                    speedDrop.GetComponent<TMP_InputField>().text = "-2";
                    parentHandlerScript.speedChoice = -2;
                }
                else
                {
                    parentHandlerScript.speedChoice = float.Parse(val);
                }
            }
            UpdateActiveOptions();
            
        }
    }
    public void HandleTorqueField(string val)
    {
        if (torqueDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.torqueChoice = 0;
            }
            else if (val == "-")
            {
                parentHandlerScript.torqueChoice = 0;
            }
            else
            {
                if (float.Parse(val) > 90)
                {
                    torqueDrop.GetComponent<TMP_InputField>().text = "90";
                    parentHandlerScript.torqueChoice = 90;
                }
                else if (float.Parse(val) < -90)
                {
                    torqueDrop.GetComponent<TMP_InputField>().text = "-90";
                    parentHandlerScript.torqueChoice = -90;
                }
                else
                {
                    parentHandlerScript.torqueChoice = float.Parse(val);
                }
            }
            UpdateActiveOptions();
        }
    }
    public void HandleDelayField(string val)
    {
        if (delayDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.delayChoice = 0;
            }
            else
            {
                //if (float.Parse(val) == 9999)
                //{
                //    parentHandlerScript.delayChoice = Mathf.Infinity;
                //}
                //else
                //{
                //    parentHandlerScript.delayChoice = float.Parse(val);
                //}
                parentHandlerScript.delayChoice = float.Parse(val);
            }
            UpdateActiveOptions();
        }
    }
    public void HandleIfDropdown(int val)
    {
        parentHandlerScript.ifChoice = val;
        if (prevIf != 0 && val == 0)
        {
            //parentHandlerScript.ExtendTop(0, centerExtendPart, tailExtendPart);
            degreeDrop.SetActive(true); compareDegreeDrop.SetActive(true); distanceDrop.SetActive(true);

            compareLeftDrop.SetActive(false); compareRightDrop.SetActive(false); colorLeftDrop.SetActive(false); 
            colorRightDrop.SetActive(false); andDrop.SetActive(false);
        }
        else if (prevIf != 1 && val == 1)
        {
            //parentHandlerScript.ExtendTop(1, centerExtendPart, tailExtendPart);
            degreeDrop.SetActive(false); compareDegreeDrop.SetActive(false); distanceDrop.SetActive(false);

            compareLeftDrop.SetActive(true); compareRightDrop.SetActive(true); colorLeftDrop.SetActive(true);
            colorRightDrop.SetActive(true); andDrop.SetActive(true);
        }
        prevIf = val;
        //DisplayBlockOptions();
        UpdateActiveOptions();
    }
    public void HandleDegreeField(string val)
    {
        if (degreeDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.degreeChoice = 0;
            }
            else
            {
                parentHandlerScript.degreeChoice = float.Parse(val) % 360;
            }
            UpdateActiveOptions();
        }
    }
    public void HandleCompareDegreeDropdown(int val)
    {
        parentHandlerScript.compareDegreeChoice = val;
        UpdateActiveOptions();
    }

    public void HandleDistanceField(string val)
    {
        if (distanceDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.distanceChoice = 0;
            }
            else
            {
                prevDistance = parentHandlerScript.distanceChoice;
                if (float.Parse(val) >= 0)
                {
                    parentHandlerScript.distanceChoice = float.Parse(val);
                }
                else
                {
                    distanceDrop.GetComponent<TMP_InputField>().text = prevDistance.ToString();
                    Debug.LogError("Distance cannot be negative!");
                }
            }
            UpdateActiveOptions();
        }
    }
    public void HandleCompareLeftDropDown(int val)
    {
        parentHandlerScript.compareLeftChoice = val;
        UpdateActiveOptions();
    }
    public void HandleCompareRightDropDown(int val)
    {
        parentHandlerScript.compareRightChoice = val;
        UpdateActiveOptions();
    }
    public void HandleColorLeftDropDown(int val)
    {
        parentHandlerScript.colorLeftChoice = val;
        UpdateActiveOptions();
    }
    public void HandleColorRightDropDown(int val)
    {
        parentHandlerScript.colorRightChoice = val;
        UpdateActiveOptions();
    }
    public void HandleRepeatDropDown(int val)
    {
        parentHandlerScript.repeatChoice = val;
        if (prevRepeat != 0 && val == 0) // forever
        {
            //parentHandlerScript.ExtendTop(0, centerExtendPart, tailExtendPart);
            timesDrop.SetActive(false);
            degreeDrop.SetActive(false); compareDegreeDrop.SetActive(false); distanceDrop.SetActive(false);
            compareLeftDrop.SetActive(false); compareRightDrop.SetActive(false); colorLeftDrop.SetActive(false); colorRightDrop.SetActive(false); andDrop.SetActive(false);
        }
        else if (prevRepeat != 1 && val == 1) // x times
        {
            //parentHandlerScript.ExtendTop(1, centerExtendPart, tailExtendPart);
            timesDrop.SetActive(true);
            degreeDrop.SetActive(false); compareDegreeDrop.SetActive(false); distanceDrop.SetActive(false);
            compareLeftDrop.SetActive(false); compareRightDrop.SetActive(false); colorLeftDrop.SetActive(false); colorRightDrop.SetActive(false); andDrop.SetActive(false);
        }
        else if (prevRepeat != 2 && val == 2) //distance
        {
            //parentHandlerScript.ExtendTop(1, centerExtendPart, tailExtendPart);
            timesDrop.SetActive(false);
            degreeDrop.SetActive(true); compareDegreeDrop.SetActive(true); distanceDrop.SetActive(true);
            compareLeftDrop.SetActive(false); compareRightDrop.SetActive(false); colorLeftDrop.SetActive(false); colorRightDrop.SetActive(false); andDrop.SetActive(false);
        }
        else if (prevRepeat != 3 && val == 3) // color
        {
            //parentHandlerScript.ExtendTop(1, centerExtendPart, tailExtendPart);
            timesDrop.SetActive(false);
            degreeDrop.SetActive(false); compareDegreeDrop.SetActive(false); distanceDrop.SetActive(false);
            compareLeftDrop.SetActive(true); compareRightDrop.SetActive(true); colorLeftDrop.SetActive(true); colorRightDrop.SetActive(true); andDrop.SetActive(true);
        }
        prevRepeat = val;
        UpdateActiveOptions();
    }
    public void HandleTimesField(string val)
    {
        if (timesDrop != null)
        {
            if (val.Length == 0)
            {
                parentHandlerScript.timesChoice = 0;
            }
            else
            {
                prevTimes = parentHandlerScript.timesChoice;
                if (int.Parse(val) > 0)
                {
                    parentHandlerScript.timesChoice = int.Parse(val);
                }
                else
                {
                    timesDrop.GetComponent<TMP_InputField>().text = prevTimes.ToString();
                    Debug.LogError("Number of times cannot be less than 1!");
                }
            }
            UpdateActiveOptions();
        }
    }

    public void HandleAndDropdown(int val)
    {
        parentHandlerScript.andChoice = val;
        UpdateActiveOptions();
    }

    public void DisplayBlockOptions()
    {
        Debug.Log("Speed:" + parentHandlerScript.speedChoice + "\n" +
				"Torque:" + parentHandlerScript.torqueChoice + "\n" +
				"Delay:" + parentHandlerScript.delayChoice + "\n" +
				"If:" + parentHandlerScript.ifChoice + "\n" +
				"Degree:" + parentHandlerScript.degreeChoice + "\n" +
				"CompareDegree:" + parentHandlerScript.compareDegreeChoice + "\n" +
				"Distance:" + parentHandlerScript.distanceChoice + "\n" +
				"Left:" + parentHandlerScript.compareLeftChoice + "\n" +
				"Right:" + parentHandlerScript.compareRightChoice + "\n" +
				"ColorLeft:" + parentHandlerScript.colorLeftChoice + "\n" +
				"ColorRigt:" + parentHandlerScript.colorRightChoice + "\n" +
                "And:" + parentHandlerScript.andChoice + "\n" +
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