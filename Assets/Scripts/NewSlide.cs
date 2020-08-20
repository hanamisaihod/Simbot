using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSlide : MonoBehaviour
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
                LeanTween.moveLocalY(gameObject,gameObject.transform.localPosition.y + 235,1).setEaseOutCubic();
            }
            else
            {
                LeanTween.moveLocalY(gameObject,gameObject.transform.localPosition.y - 235,1).setEaseOutCubic();
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
