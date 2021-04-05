using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettingButton : MonoBehaviour
{
	public GameObject toggleLight;
	public GameObject otherControl;
	public bool isActive;
	public bool thisIsInverse;
	void Start()
	{
		if (PlayerPrefs.HasKey("controlInverse"))
		{
			if (PlayerPrefs.GetInt("controlInverse") == 1)
			{
				if (thisIsInverse)
				{
					toggleLight.SetActive(true);
				}
				else
				{
					toggleLight.SetActive(false);
				}
			}
		}
		isActive = toggleLight.activeInHierarchy;
	}

	public void Toggle()
	{
		if (!isActive)
		{
			ToggleOn();
		}
	}
    public void ToggleOn()
	{
		isActive = true;
		toggleLight.SetActive(isActive);
		otherControl.GetComponent<ControlSettingButton>().ToggleOff();
	}

	public void ToggleOff()
	{
		isActive = false;
		toggleLight.SetActive(isActive);
	}
}
