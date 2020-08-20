using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textbox : MonoBehaviour
{
    public bool boxUpTrigger;
    public bool boxDownTrigger;
    public GameObject miniTriangle;
    Coroutine usingCor;

    void Start()
    {
        StartCoroutine(miniTriangleAnim());
    }

    void Update()
    {
        if (boxUpTrigger)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(ShowUp());
            boxUpTrigger = false;
        }

        if (boxDownTrigger)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(ShowDown());
            boxDownTrigger = false;
        }

    }

    IEnumerator miniTriangleAnim()
    {
        LeanTween.moveLocalY(miniTriangle, miniTriangle.transform.localPosition.y - 0.2f, 0.3f).setEaseInOutSine().setLoopPingPong();
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator ShowUp()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y + 312f, 0.5f).setEaseOutBack();
    }

    IEnumerator ShowDown()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocalY(gameObject, gameObject.transform.localPosition.y - 312f, 0.5f).setEaseOutBack();
    }
}
