using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Icon_Control : MonoBehaviour
{
    public string ClickDirect = "ClickRight";
    Coroutine usingCor;

    void Start()
    {
        
    }

    void Update()
    {
        if(ClickDirect == "ClickUp")
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            ClickDirect = "";
            usingCor = StartCoroutine(ClickUpAnim());
        }
        else if (ClickDirect == "ClickDown")
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            ClickDirect = "";
            usingCor = StartCoroutine(ClickDownAnim());
        }
        else if (ClickDirect == "ClickRight")
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            ClickDirect = "";
            usingCor = StartCoroutine(ClickRightAnim());
        }
        else if (ClickDirect == "ClickLeft")
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            ClickDirect = "";
            usingCor = StartCoroutine(ClickLeftAnim());
        }

    }

    IEnumerator ClickUpAnim()
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y + 40f, 0.25f).setEaseInOutSine().setLoopPingPong();
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator ClickDownAnim()
    {
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y - 40f, 0.25f).setEaseInOutSine().setLoopPingPong();
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator ClickRightAnim()
    {
        LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + 40f, 0.25f).setEaseInOutSine().setLoopPingPong();
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator ClickLeftAnim()
    {
        LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - 40f, 0.25f).setEaseInOutSine().setLoopPingPong();
        yield return new WaitForSeconds(0.1f);
    }
}
