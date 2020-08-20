using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNewInterpreter : MonoBehaviour
{
	public GameObject myInterpreter;
	public GameObject tempInterpreter;
	public void newInterpreter()
	{
		if (!tempInterpreter)
		{
			tempInterpreter = Instantiate(myInterpreter);
		}
		else
		{
			Destroy(tempInterpreter);
			tempInterpreter = Instantiate(myInterpreter);
		}
		tempInterpreter.GetComponent<BlockInterpreter>().StartReading();
	}
}
