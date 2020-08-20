using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSensor : MonoBehaviour
{

	public bool isTouching = false;

	void OnCollisionEnter(Collision other)
	{
		isTouching = true;
	}

	void OnCollisionExit(Collision other)
	{
		isTouching = false;
	}
}
