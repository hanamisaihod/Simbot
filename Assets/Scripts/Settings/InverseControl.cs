using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InverseControl : MonoBehaviour
{
    public RectTransform moveJoyStick;
    public RectTransform panJoyStick;
    public Vector3 fixedLeftPosition;
    public Vector3 fixedRightPosition;

    void Start()
    {
        ApplyInverseControl();
    }

    public void ApplyInverseControl()
	{
        if (PlayerPrefs.HasKey("controlInverse"))
		{
            if (PlayerPrefs.GetInt("controlInverse") == 1)
			{
                panJoyStick.anchorMin = new Vector2 (0, 0);
                panJoyStick.anchorMax = new Vector2(0, 0);
                panJoyStick.anchoredPosition = fixedLeftPosition;
                moveJoyStick.anchorMin = new Vector2(1, 0);
                moveJoyStick.anchorMax = new Vector2(1, 0);
                moveJoyStick.anchoredPosition = fixedRightPosition;
            }
			else
            {
                panJoyStick.anchorMin = new Vector2(1, 0);
                panJoyStick.anchorMax = new Vector2(1, 0);
                panJoyStick.anchoredPosition = fixedRightPosition;
                moveJoyStick.anchorMin = new Vector2(0, 0);
                moveJoyStick.anchorMax = new Vector2(0, 0);
                moveJoyStick.anchoredPosition = fixedLeftPosition;
            }
		}
	}
}
