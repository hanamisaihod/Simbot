using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashCan : MonoBehaviour
{
	public bool deleteEffect = false;
	public GameObject effect;
	public float effectTime;

	public void ShowDeleteEffect()
	{
		if (!deleteEffect)
		{
			effect.LeanScale(new Vector3(0.9f, 0.9f, 0.9f), effectTime).setEaseOutBack();
			deleteEffect = true;
		}
	}

	public void StopDeleteEffect()
	{
		if (deleteEffect)
		{
			effect.LeanScale(new Vector3(0, 0, 0), effectTime).setEaseInBack();
			deleteEffect = false;
		}
	}

}
