using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSway : MonoBehaviour
{
	public float rightCloudSwayTime;
	public float rightCloudSwayDistance;
	public float leftCloudSwayTime;
	public float leftCloudSwayDistance;
	void Start()
    {
		if (gameObject.name == "CloudRight")
		{
			RightCloudSway(true, true);
		}
		else if (gameObject.name == "CloudLeft")
		{
			LeftCloudSway(false, true);
		}
	}

	private void RightCloudSway(bool direction, bool first)
	{
		
		if (direction)
		{
			LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + rightCloudSwayDistance, rightCloudSwayTime)
				.setEaseInOutSine();
			StartCoroutine(WaitRightCloudSway(direction));
		}
		else
		{
			LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - rightCloudSwayDistance, rightCloudSwayTime)
				.setEaseInOutSine();
			StartCoroutine(WaitRightCloudSway(direction));
		}
		if (first)
		{
			rightCloudSwayDistance *= 2;
			rightCloudSwayTime *= 2;
		}
	}

	private void LeftCloudSway(bool direction, bool first)
	{
		
		if (direction)
		{
			LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + leftCloudSwayDistance, leftCloudSwayTime)
				.setEaseInOutSine();
			StartCoroutine(WaitLeftCloudSway(direction));
		}
		else
		{
			LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - leftCloudSwayDistance, leftCloudSwayTime)
				.setEaseInOutSine();
			StartCoroutine(WaitLeftCloudSway(direction));
		}
		if (first)
		{
			leftCloudSwayDistance *= 2;
			leftCloudSwayTime *= 2;
		}
	}

	IEnumerator WaitRightCloudSway(bool direction)
	{
		yield return new WaitForSeconds(rightCloudSwayTime);
		RightCloudSway(!direction, false);
	}
	IEnumerator WaitLeftCloudSway(bool direction)
	{
		yield return new WaitForSeconds(leftCloudSwayTime);
		LeftCloudSway(!direction, false);
	}
}
