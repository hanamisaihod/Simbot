using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementIndicator : MonoBehaviour
{
	private ARRaycastManager rayManager;
	public GameObject placementVisual;
	public bool placed = false;

	void Start()
	{
		rayManager = FindObjectOfType<ARRaycastManager>();
		placementVisual = transform.GetChild(0).gameObject;

		placementVisual.SetActive(false);
	}
	void Update()
	{
		// shoot a raycast from the center of the screen
		List<ARRaycastHit> hits = new List<ARRaycastHit>();
		rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

		// if hit ar plane, update pos and rot
		if (hits.Count > 0)
		{
			transform.position = hits[0].pose.position;
			transform.rotation = hits[0].pose.rotation;

			if (!placementVisual.activeInHierarchy && !placed)
			{
				placementVisual.SetActive(true);
			}
		}


	}
}
