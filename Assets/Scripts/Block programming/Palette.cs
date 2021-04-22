using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
	// Start is called before the first frame update
	public float defTweenTime = 2f;
	public float tweenDistance = -100f;
	public float buttonTweenDistance = 36.1f;
	public bool isOpen = false;
	public bool moving = false;
	public bool isSetting = false;
	public GameObject moveBlock, ifBlock, whileBlock;
	[SerializeField] private Vector3 moveRect, ifRect, whileRect;

	private void Start()
	{
		moveRect = moveBlock.transform.localPosition;
		ifRect = ifBlock.transform.localPosition;
		whileRect = whileBlock.transform.localPosition;
	}

	public void OpenPalette()
	{
		DisableIfAndWhile();
		if (!moving)
		{
			moving = true;
			if (!isOpen)
			{
				moveBlock.transform.localPosition = moveRect;
				ifBlock.transform.localPosition = ifRect;
				whileBlock.transform.localPosition = whileRect;
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + tweenDistance, defTweenTime).setEaseOutCubic();
			}
			else
			{
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - tweenDistance, defTweenTime).setEaseOutCubic();
			}
			StartCoroutine(WaitOpenCoroutine(defTweenTime));
		}
	}

	IEnumerator WaitOpenCoroutine(float tweenTime)
	{
		yield return new WaitForSeconds(tweenTime);
		isOpen = !isOpen;
		moving = !moving;
	}

	IEnumerator WaitSettingCoroutine(float tweenTime)
	{
		yield return new WaitForSeconds(tweenTime);
		moving = !moving;
	}

	public void MoveForSetting(float tweenTime)
	{
		moving = true;
		if (!isSetting)
		{
			if (!isOpen)
			{
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + buttonTweenDistance, tweenTime).setEaseOutCubic();
				StartCoroutine(WaitSettingCoroutine(tweenTime));
			}
			else
			{
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + buttonTweenDistance - tweenDistance, tweenTime).setEaseOutCubic();
				StartCoroutine(WaitSettingCoroutine(tweenTime));
			}
			isSetting = true;
		}
		else
		{
			if (!isOpen)
			{
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - buttonTweenDistance, tweenTime).setEaseOutCubic();
				StartCoroutine(WaitSettingCoroutine(tweenTime));
			}
			else
			{
				LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - buttonTweenDistance + tweenDistance, tweenTime).setEaseOutCubic();
				StartCoroutine(WaitSettingCoroutine(tweenTime));
			}
			isSetting = false;
		}
	}
	private void DisableIfAndWhile() //Disable if and while block before encountering their tutorials
	{
		if (!PlayerPrefs.HasKey("Enable_IfWhile"))
		{
			ifBlock.SetActive(false);
			whileBlock.SetActive(false);
		}
	}
}
