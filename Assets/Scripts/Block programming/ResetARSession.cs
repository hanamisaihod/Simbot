using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ResetARSession : MonoBehaviour
{
	public ARSession tempSession;
	public GameObject plane;

	public void Reset()
	{
		tempSession.Reset();
	}

	public void TogglePlane()
	{
		plane.SetActive(!plane.activeSelf);
	}
}
