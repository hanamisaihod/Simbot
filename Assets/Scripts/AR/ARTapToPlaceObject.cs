using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
	private bool objectPlaced = false;
	public GameObject objectToPlace;
	public GameObject placementIndicator;
	private ARSession tempSession;
	public ARRaycastManager arRaycastManager;
	private bool placementPoseIsValid = false;
	private Pose placementPose;
	private GameObject tempObject;

    // Start is called before the first frame update
    void Start()
    {
		tempSession = FindObjectOfType<ARSession>();
    }

    // Update is called once per frame
    void Update()
    {
		UpdatePlacementPose();
		UpdatePlacementIndicator();

		if (placementPoseIsValid && Input.touchCount > 0
			&& Input.GetTouch(0).phase == TouchPhase.Began
			&& !objectPlaced)
		{
			PlaceObject();
			placementIndicator.SetActive(false);
			objectPlaced = true;
		}
    }

	private void PlaceObject()
	{
		//tempObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
		//tempObject.SetActive(true);
		objectToPlace.transform.position = placementPose.position;
		objectToPlace.transform.rotation = placementPose.rotation;
	}

	private void UpdatePlacementIndicator()
	{
		if (placementPoseIsValid && !objectPlaced)
		{
			placementIndicator.SetActive(true);
			placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		}
		else
		{
			placementIndicator.SetActive(false);
		}
	}

	private void UpdatePlacementPose()
	{

		Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		List<ARRaycastHit> hits = new List<ARRaycastHit>();
		arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

		placementPoseIsValid = hits.Count > 0;
		if (placementPoseIsValid)
		{
			placementPose = hits[0].pose;


		}
	}
	public void Reset()
	{
		tempSession.Reset();
		objectPlaced = false;
		placementIndicator.SetActive(true);
		Destroy(tempObject);
	}
}
