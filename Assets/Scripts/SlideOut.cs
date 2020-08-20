using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideOut : MonoBehaviour
{
    //public bool slideOutCheck = false;
    public bool isOpen = false;
	public bool moving = false;
    public void SlideOnClick()
    {
        
        Debug.Log("GameObject.name = " + gameObject.name);
        if (!moving)
        {
            moving = true;
            if(!isOpen)
            {
                LeanTween.moveLocalX(gameObject,gameObject.transform.localPosition.x - 352.0913f,1).setEaseOutCubic();
            }
            else
            {
                LeanTween.moveLocalX(gameObject,gameObject.transform.localPosition.x + 352.0913f,1).setEaseOutCubic();
            }
            StartCoroutine(WaitOpenCoroutine());
        }   
    }

    IEnumerator WaitOpenCoroutine()
	{
		yield return new WaitForSeconds(1);
		isOpen = !isOpen;
		moving = !moving;
	}
}
