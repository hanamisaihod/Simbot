using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    
    void Update()
    {
		if (Input.GetKeyDown("space"))
		{
			LeanTween.rotateAround(gameObject, Vector3.up, 180.0f, 2.0f);
		}
    }
}
