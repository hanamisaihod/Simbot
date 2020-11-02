using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
	public int cameraDragSpeed = 40;
	public bool available = true;
	private Camera mainCamera;
	public GameObject startBlock;
	public GameObject findButton;
	public bool dragging = false;
	public float findStartDistanceShow;
	void Start()
	{
		mainCamera = Camera.main;
	}
	private void Update()
	{
		if (Input.touchCount > 0)
		{
			if (available)
			{
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				float speed = cameraDragSpeed * Time.deltaTime;
				mainCamera.transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);

				/*transform.Translate(-touchDeltaPosition.x * speed,
							-touchDeltaPosition.y * speed, 0);*/
			}
			/*	if (available)
			{
				mainCamera.transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
			}*/
		}
		if (Input.GetMouseButton(0))
		{
			float speed = cameraDragSpeed * Time.deltaTime;
			if (available)
			{
				dragging = true;
				mainCamera.transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (dragging)
			{
				dragging = false;
				CheckDistanceStart();
			}
		}
	}

	public void GoToStart()
	{
		LeanTween.move(gameObject, new Vector3(
			startBlock.transform.position.x, startBlock.transform.position.y, transform.position.z), 1f).setEaseInOutCubic().setOnComplete(SetFindingFinish);
		findButton.GetComponent<FindButton>().StopAnimation();
		findButton.GetComponent<FindButton>().isFinding = true;
	}

	public void CheckDistanceStart()
	{
		if (startBlock != null)
		{
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y),
				new Vector2(startBlock.transform.position.x, startBlock.transform.position.y)) > findStartDistanceShow)
			{
				if (!(findButton.GetComponent<FindButton>().animate))
				{
					findButton.GetComponent<FindButton>().StartAnimation();
				}
			}
			else
			{
				if (findButton.GetComponent<FindButton>().animate)
				{
					findButton.GetComponent<FindButton>().StopAnimation();
				}
			}
		}
	}

	public void SetFindingFinish()
	{
		findButton.GetComponent<FindButton>().isFinding = false;
	}
}