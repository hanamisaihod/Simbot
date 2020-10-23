using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARSpawnBlockTest : MonoBehaviour
{

	public GameObject cubePrefab;

	private GameObject tempObject;
	private ARRaycastManager arRaycastManager;
	private Vector2 touchPosition;

	static List<ARRaycastHit> hits = new List<ARRaycastHit>();

	private void Awake()
    {
		arRaycastManager = GetComponent<ARRaycastManager>();
    }

	bool TryGetTouchPosition(out Vector2 touchPosition)
	{
		if (Input.touchCount > 0)
		{
			touchPosition = Input.GetTouch(0).position;
			return true;
		}

		touchPosition = default;
		return false;
	}

	void Update()
	{
		if(!TryGetTouchPosition(out Vector2 touchPosition))
		{
			return;
		}
		if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
		{
			Pose hitPose = hits[0].pose;

			if (!tempObject)
			{
				tempObject = Instantiate(cubePrefab, hitPose.position + new Vector3(0, 
					(cubePrefab.GetComponent<MeshFilter>().mesh.bounds.size.y * cubePrefab.transform.localScale.y)/2, 0), hitPose.rotation);
			}
			else
			{
				tempObject.transform.position = hitPose.position + new Vector3(0,
					(cubePrefab.GetComponent<MeshFilter>().mesh.bounds.size.y * cubePrefab.transform.localScale.y) / 2, 0);
			}
		}
	}
}
