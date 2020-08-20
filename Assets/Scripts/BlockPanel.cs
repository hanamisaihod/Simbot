using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPanel : MonoBehaviour
{

	public Image imageCom;
	public Color tempColor;
	public float delay;
	void Start()
	{
		imageCom = GetComponent<Image>();
	}
	public void OpenBlockPanel()
	{
		delay = 0.25f;
		LeanTween.alpha(imageCom.rectTransform, 0.6f, delay).setFrom(0f);
	}
	public void CloseBlockPanel()
	{
		LeanTween.alpha(imageCom.rectTransform, 0f, delay).setFrom(0.6f);
	}

}
