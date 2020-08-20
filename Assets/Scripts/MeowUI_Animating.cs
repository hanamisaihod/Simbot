using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MeowUI_Animating : MonoBehaviour
{
    public GameObject body;
    public GameObject tail;
    public GameObject ear_L;
    public GameObject ear_R;
    public GameObject eyes;
    public float showDuration = 1f;
    private float waitUntil;
    public bool showRTrigger;
    public bool showLTrigger;
    public bool cancelTrigger;
    private bool idleTrigger;
    Coroutine usingCor;

    public Vector3 bodyPosition;
    public Vector3 tailPosition;
    public Vector3 earLPosition;
    public Vector3 earRPosition;
    public Vector3 eyesPosition;

    void Start()
    {
        bodyPosition = body.transform.localPosition;
        tailPosition = tail.transform.localPosition;
        earLPosition = ear_L.transform.localPosition;
        earRPosition = ear_R.transform.localPosition;
        eyesPosition = eyes.transform.localPosition;
    }

    void Update()
    {
        if(showRTrigger)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            waitUntil = showDuration + Time.time;
            usingCor = StartCoroutine(ShowFromR());
            showRTrigger = false;
            idleTrigger = true;
        }
        if (idleTrigger && Time.time > waitUntil)
        {
            StartCoroutine(Bink());
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            idleTrigger = false;
            usingCor = StartCoroutine(MeowIdle());
        }
        if(showLTrigger)
        {
            if(usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            waitUntil = showDuration + Time.time;
            usingCor = StartCoroutine(ShowFromL());
            showLTrigger = false;
            idleTrigger = true;
        }
        if(cancelTrigger && Time.time > waitUntil)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(Cancel());
            cancelTrigger = false;
        }
       
    }

    IEnumerator ShowFromR()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
        LeanTween.rotateAround(gameObject, Vector3.forward, 20, 0.5f).setEaseInOutBack().setLoopPingPong(1);   
    }

    IEnumerator ShowFromL()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.moveLocalX(gameObject, gameObject.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
        LeanTween.rotateAround(gameObject, Vector3.forward, -20, 0.5f).setEaseInOutBack().setLoopPingPong(1);
    }

    IEnumerator Bink()
    {
        var eyesRect = eyes.GetComponent<RectTransform>();
        while (true)
        {
            eyes.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(12f);
            eyes.GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            eyes.GetComponent<Image>().enabled = true;
        }
        
    }

    IEnumerator MeowIdle()
    {
        LeanTween.moveLocalY(body, body.transform.localPosition.y + 0.5f, 1f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(eyes, eyes.transform.localPosition.y + 0.5f, 1f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(ear_L, ear_L.transform.localPosition.y + 0.6f, 1f).setLoopPingPong().setEaseInOutSine().setDelay(0.17f);
        LeanTween.moveLocalY(ear_R, ear_R.transform.localPosition.y + 0.6f, 1f).setLoopPingPong().setEaseInOutSine().setDelay(0.17f);
        LeanTween.rotateZ(tail, -10 * Math.Sign(gameObject.transform.localScale.x), 1f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalX(tail, tail.transform.localPosition.x + 0.7f, 1f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalY(tail, tail.transform.localPosition.y - 1f, 1f).setLoopPingPong().setEaseInOutSine();
        
        yield return new WaitForSeconds(0f);
    }

    IEnumerator Cancel()
    {
        LeanTween.cancel(body);
        LeanTween.cancel(tail);
        LeanTween.cancel(ear_L);
        LeanTween.cancel(ear_R);
        LeanTween.cancel(eyes);
        LeanTween.moveLocal(body, bodyPosition, 0.5f);
        LeanTween.moveLocal(tail, tailPosition, 0.5f);
        LeanTween.moveLocal(ear_L, earLPosition, 0.5f);
        LeanTween.moveLocal(ear_R, earRPosition, 0.5f);
        LeanTween.moveLocal(eyes, eyesPosition, 0.5f);
        yield return new WaitForSeconds(0f);
    }
}
