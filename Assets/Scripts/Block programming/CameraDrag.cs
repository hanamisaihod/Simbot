using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
	public bool available = true;
    public float cameraDragSpeed = 1;
    private Camera mainCamera;
	public GameObject startBlock;
	public GameObject findButton;
	public bool dragging = false;
	public float findStartDistanceShow;
	void Start()
    {
        if (GameObject.Find("ModeSwitcher"))
        {
            foreach (GameObject obj in GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects)
            {
                if (obj.tag == "SubCamera")
                {
                    mainCamera = obj.GetComponent<Camera>();
                }
            }
        }
    }
    //private void FixedUpdate()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        if (available)
    //        {
    //            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
    //            //float speed = cameraDragSpeed * Time.fixedDeltaTime;
    //            mainCamera.transform.Translate(-touchDeltaPosition.x * cameraDragSpeed, -touchDeltaPosition.y * cameraDragSpeed, 0);

    //            /*transform.Translate(-touchDeltaPosition.x * speed,
    //    					-touchDeltaPosition.y * speed, 0);*/
    //        }
    //    }
    //    //if (Input.GetMouseButton(0))
    //    //{
    //    //    float speed = cameraDragSpeed * Time.fixedDeltaTime;
    //    //    if (available)
    //    //    {
    //    //        dragging = true;
    //    //        mainCamera.transform.position -= new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
    //    //    }
    //    //}
    //    //if (Input.GetMouseButtonUp(0))
    //    //{
    //    //    if (dragging)
    //    //    {
    //    //        dragging = false;
    //    //        //CheckDistanceStart();
    //    //    }
    //    //}
    //}
    
    public bool Rotate;
    protected Plane Plane;

    private void Update()
    {

        //Update Plane
        if (Input.touchCount >= 1)
            Plane.SetNormalAndPosition(transform.up, transform.position);

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                mainCamera.transform.Translate(Delta1, Space.World);
        }

        //Pinch
        if (Input.touchCount >= 2)
        {
            var pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            if (zoom == 0 || zoom > 10)
                return;

            //Move cam amount the mid ray
            mainCamera.transform.position = Vector3.LerpUnclamped(pos1, mainCamera.transform.position, 1 / zoom);

            if (Rotate && pos2b != pos2)
                mainCamera.transform.RotateAround(pos1, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
        }

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = mainCamera.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = mainCamera.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = mainCamera.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
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