using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasFX_Controller : MonoBehaviour
{
    public static bool clearTrigger;
    public bool failTrigger;
    public bool FXshowing;                  // Please check this before trigger both clearTrigger and failTrigger
    public bool clearSceneTrigger;
    public GameObject textClear;
    public GameObject textFail;
    public GameObject whiteFlash;
    public GameObject darkTheme;
    public GameObject tryButton;
    public GameObject nextButton;

    public GameObject clearBackFX;
    public GameObject clearLeftFX;
    public GameObject clearRightFX;
    public GameObject failBackFX;

    private float clearDuration = 5.1f;
    private float failDuration = 3.6f;
    private float waitUntil;
    private Vector3 clearInitial;
    private Vector3 failInitial;
    private Vector3 tryInitial;
    private Vector3 nextInitial;
    Coroutine usingCor;
    Coroutine loopCor1;
    Coroutine loopCor2;
    AudioSource audioClearBack;
    AudioSource audioFailBack;
    AudioSource audioLeft;
    AudioSource audioRight;
    public AudioClip soundWin1;
    public AudioClip soundWin2;
    public AudioClip soundLose;
    public float volume = 0.5f;


    void Start()
    {
        audioClearBack = clearBackFX.GetComponent<AudioSource>();
        audioFailBack = failBackFX.GetComponent<AudioSource>();
        audioLeft = clearLeftFX.GetComponent<AudioSource>();
        audioRight = clearRightFX.GetComponent<AudioSource>();
        audioLeft.volume = volume;
        audioRight.volume = volume;
        textClear.GetComponent<Image>().enabled = false;
        textFail.GetComponent<Image>().enabled = false;
        whiteFlash.GetComponent<Image>().enabled = false;
        darkTheme.GetComponent<Image>().enabled = false;
        tryButton.GetComponent<Image>().enabled = false;
        nextButton.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
        clearBackFX.SetActive(false);
        clearLeftFX.SetActive(false);
        clearRightFX.SetActive(false);
        failBackFX.SetActive(false);
        clearInitial = textClear.transform.localScale;
        failInitial = textFail.transform.localPosition;
        tryInitial = tryButton.transform.localScale;
        nextInitial = nextButton.transform.localScale;

        tryButton.GetComponent<Button>().onClick.AddListener(ClearScene); // Please come back to edit this
    }

    void Update()
    {
        if (clearTrigger && Time.time > waitUntil && !FXshowing)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            FXshowing = true;
            if(usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(ClearShow());
            clearTrigger = false;
            waitUntil = Time.time + clearDuration;
        }

        if (failTrigger && Time.time > waitUntil && !FXshowing)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
            FXshowing = true;
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(FailShow());
            failTrigger = false;
            waitUntil = Time.time + failDuration;
        }

        if(clearSceneTrigger && Time.time > waitUntil)
        {
            gameObject.GetComponent<Canvas>().enabled = false;
            textClear.GetComponent<Image>().enabled = false;
            textFail.GetComponent<Image>().enabled = false;
            whiteFlash.GetComponent<Image>().enabled = false;
            darkTheme.GetComponent<Image>().enabled = false;
            tryButton.GetComponent<Image>().enabled = false;
            nextButton.GetComponent<Image>().enabled = false;
            StopCoroutine(loopCor1);
            StopCoroutine(loopCor2);
            clearBackFX.SetActive(false);
            clearLeftFX.SetActive(false);
            clearRightFX.SetActive(false);
            failBackFX.SetActive(false);
            clearSceneTrigger = false;
            if(usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(Clear());
        }

        if(FXshowing)
        {
            clearTrigger = false;
            failTrigger = false;
        }

    }

    // Function trigger when click try again button    Please change this
    void ClearScene()
    {
        if (Time.time > waitUntil) clearSceneTrigger = true;
		SceneManager.LoadScene("Simulate");
    }

    IEnumerator ClearShow()
    {
        var Rect = textClear.GetComponent<RectTransform>();
        var whiteRect = whiteFlash.GetComponent<RectTransform>();
        var tryRect = tryButton.GetComponent<RectTransform>();
        var nextRect = nextButton.GetComponent<RectTransform>();

        clearBackFX.SetActive(false);
        clearLeftFX.SetActive(false);
        clearRightFX.SetActive(false);

        textClear.GetComponent<Image>().enabled = true;
        LeanTween.scale(Rect, Rect.localScale * 6f, 1f).setDelay(0.1f).setEaseInOutBack();
        LeanTween.rotateZ(textClear, 3600, 1f);

        yield return new WaitForSeconds(1.1f);

        clearBackFX.SetActive(true);
        whiteFlash.GetComponent<Image>().enabled = true;
        audioClearBack.PlayOneShot(soundWin1, volume);
        LeanTween.alpha(whiteRect, 0f, 1f).setDelay(0.1f);
        LeanTween.scale(Rect, Rect.localScale * 1.75f, 0.25f).setEaseInOutBack();

        yield return new WaitForSeconds(0.25f);

        clearLeftFX.SetActive(true);
        loopCor1 = StartCoroutine(LoopSound(audioLeft));
        LeanTween.moveY(Rect, 15f, 1f).setLoopPingPong();

        yield return new WaitForSeconds(1f);

        clearRightFX.SetActive(true);
        loopCor2 = StartCoroutine(LoopSound(audioRight));

        yield return new WaitForSeconds(2f);

        tryButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(tryRect, tryRect.localScale * 8f, 0.75f).setDelay(0.1f).setEaseInOutBack();

        nextButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(nextRect, nextRect.localScale * 8f, 0.75f).setDelay(0.1f).setEaseInOutBack();

        yield return new WaitForSeconds(0.75f);
    }

    IEnumerator FailShow()
    {
        var Rect = textFail.GetComponent<RectTransform>();
        var darkRect = darkTheme.GetComponent<RectTransform>();
        var tryRect = tryButton.GetComponent<RectTransform>();
        var nextRect = nextButton.GetComponent<RectTransform>();

        LeanTween.alpha(darkRect, 0f, 0.1f);
        failBackFX.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        textFail.GetComponent<Image>().enabled = true;
        darkTheme.GetComponent<Image>().enabled = true;
        failBackFX.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        audioFailBack.PlayOneShot(soundLose, volume);
        LeanTween.alpha(darkRect, 0.9f, 0.1f);
        LeanTween.moveY(Rect, 0f, 0.75f).setEaseOutElastic();
        LeanTween.moveY(Rect, 10f, 1f).setDelay(0.75f).setLoopPingPong();

        yield return new WaitForSeconds(2f);

        tryButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(tryRect, tryRect.localScale * 8f, 0.75f).setEaseInOutBack();
        nextButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(nextRect, nextRect.localScale * 8f, 0.75f).setDelay(0.2f).setEaseInOutBack();

        yield return new WaitForSeconds(0.75f);
    }

    IEnumerator Clear()
    {
        LeanTween.cancel(textClear);
        LeanTween.cancel(textFail);
        LeanTween.cancel(tryButton);
        LeanTween.cancel(nextButton);
        LeanTween.scale(textClear, clearInitial, 0.1f);
        LeanTween.moveLocal(textFail, failInitial, 0.1f);
        LeanTween.scale(tryButton, tryInitial, 0.1f);
        LeanTween.scale(nextButton, nextInitial, 0.1f);
        yield return new WaitForSeconds(0.1f);
        FXshowing = false;
    }

    IEnumerator LoopSound(AudioSource source)
    {
        while(true)
        {
            audioLeft.PlayOneShot(soundWin2, volume);
            yield return new WaitForSeconds(6.6f);
        }
    }
}
