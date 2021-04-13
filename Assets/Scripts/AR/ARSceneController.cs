using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ARSceneController : MonoBehaviour
{
	private ARPlacementIndicator placementIndicator;
	public ARModeSwitcher modeSwitcher;
	public GameObject robotFromLevelController;
	public EnviSim enviSim;
	public GameObject map;
	[SerializeField] Vector3 centroid;
	private void Start()
	{
		placementIndicator = FindObjectOfType<ARPlacementIndicator>();
	}

	private void Update()
	{
		//if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
		//{
		//	GameObject obj = Instantiate(objectToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
		//}
		if (placementIndicator.placementVisual.activeInHierarchy)
		{
			if (!map.activeInHierarchy)
			{
				map.SetActive(true);
				modeSwitcher.applyButton.gameObject.SetActive(true);
				modeSwitcher.lookAroundText.SetActive(false);
			}
			//map.transform.position = placementIndicator.transform.position;
			map.transform.position = new Vector3(placementIndicator.transform.position.x
				, placementIndicator.transform.position.y + 0.5f * 0.015f
				, placementIndicator.transform.position.z);
			map.transform.rotation = placementIndicator.transform.rotation;
		}
	}

	public void AssignMap(bool mainModeBool)
	{
		if (mainModeBool == true)
		{
			map = enviSim.MainObj;
		}
		else
		{
			map = enviSim.creativeMap;
		}
		modeSwitcher.AssignBossesAsChildren(map);
		map.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		centroid = CalculateMapCentroid();
		MoveMapToCentroid();
		AdjustParticleSize();
		AdjustLightRange();
		map.SetActive(false);
	}

	private bool finishedRotating = true;
	public void RotateMap()
	{
		map.transform.Rotate(map.transform.rotation.x, map.transform.rotation.y + 45, map.transform.rotation.z);
	}

	private Vector3 CalculateMapCentroid()
	{
		Vector3 tempCentroid = Vector3.zero;
		if (map.transform.childCount > 0)
		{
			foreach (Transform child in map.transform)
			{
				tempCentroid += child.transform.localPosition;
			}
			tempCentroid /= map.transform.childCount;
		}
		Debug.Log("centroid position: " + tempCentroid.ToString("F8"));
		return tempCentroid;
	}

	private void MoveMapToCentroid()
	{
		foreach (Transform child in map.transform)
		{
			child.localPosition = new Vector3(child.localPosition.x - centroid.x
				, 0.5f
				, child.localPosition.z - centroid.z);
		}
	}

	public void AdjustParticleSize()
	{
		ParticleSystem[] particleObjs = Resources.FindObjectsOfTypeAll(typeof(ParticleSystem)) as ParticleSystem[];
		foreach (ParticleSystem obj in particleObjs)
		{
			obj.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		}
	}
	public void AdjustLightRange()
	{
		Light[] lightObjs = Resources.FindObjectsOfTypeAll(typeof(Light)) as Light[];
		foreach (Light obj in lightObjs)
		{
			obj.range = 0.02f;
		}
	}
}
