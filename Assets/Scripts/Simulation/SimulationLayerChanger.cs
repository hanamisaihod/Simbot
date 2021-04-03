using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationLayerChanger : MonoBehaviour
{
	[SerializeField]
	public List<GameObject> objects = new List<GameObject>();
	public void ChangeLayerToIgnoreRaycast()
	{
		foreach (GameObject obj in objects)
		{
			obj.layer = 2;
		}
	}

	public void FindAndChangeAllLayerToIgnoreRaycast()
	{
		SimulationLayerChanger[] slc = Object.FindObjectsOfType<SimulationLayerChanger>();
		foreach (SimulationLayerChanger obj in slc)
		{
			obj.ChangeLayerToIgnoreRaycast();
		}
	}
}
