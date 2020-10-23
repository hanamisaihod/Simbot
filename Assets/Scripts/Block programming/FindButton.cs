using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindButton : MonoBehaviour
{

	public bool isFinding = false;
	public bool scaling = true;
	public bool animate = false;

	public void StartAnimation()
	{
		if (!isFinding)
		{
			scaling = true;
			animate = true;
			StartCoroutine(WaitAnimation(0.25f));
		}
	}

	public void StopAnimation()
	{
		animate = false;
		StartCoroutine(WaitForAnimationStop(0.5f));
	}
	
	IEnumerator WaitAnimation(float animTime)
	{
		yield return new WaitForSeconds(animTime);
		if (animate)
		{
			ScalingAnimation();
		}
	}

	IEnumerator WaitForAnimationStop(float animTime)
	{
		yield return new WaitForSeconds(animTime);
		LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.25f);
	}

	public void ScalingAnimation()
	{
		if (scaling)
		{
			LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
		}
		else
		{
			LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), 0.5f);
		}
		StartCoroutine(WaitAnimation(0.5f));
		scaling = !scaling;
	}
}
