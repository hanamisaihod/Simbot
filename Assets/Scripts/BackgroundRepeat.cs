using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
	private float width, height;
	private Vector2 startPos;
	private Vector2 camPosNow;
	public GameObject cam;
    void Start()
    {
		camPosNow = cam.transform.position;
		width = GetComponent<SpriteRenderer>().bounds.size.x;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        if (cam.transform.position.x > camPosNow.x + width)
		{
			transform.position = transform.position + new Vector3(width, 0, 0);
			camPosNow.x += width;
		}
		else if (cam.transform.position.x < camPosNow.x - width)
		{
			transform.position = transform.position - new Vector3(width, 0, 0);
			camPosNow.x -= width;
		}
		if (cam.transform.position.y > camPosNow.y + height)
		{
			transform.position = transform.position + new Vector3(0, height, 0);
			camPosNow.y += height;
		}
		else if (cam.transform.position.y < camPosNow.y - height)
		{
			transform.position = transform.position - new Vector3(0, height, 0);
			camPosNow.y -= height;
		}
	}
}
