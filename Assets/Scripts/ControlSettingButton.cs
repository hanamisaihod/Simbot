using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSettingButton : MonoBehaviour
{
	public GameObject toggleLight;
	public GameObject otherControl;
	public bool isActive;
	void Start()
	{
		isActive = toggleLight.activeSelf;
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
