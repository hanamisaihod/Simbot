using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSensor : MonoBehaviour
{
	public Color color;
	public Image image;

    void Update()
    {
		RaycastHit hit;
		Ray landingRay = new Ray(transform.position, transform.up);

		if (Physics.Raycast(landingRay, out hit, 10))
		{
			color = hit.collider.GetComponent<Renderer>().material.color;
			image.color = color;
		}
		Debug.DrawRay(transform.position, transform.up * 10, color);
    }
}
