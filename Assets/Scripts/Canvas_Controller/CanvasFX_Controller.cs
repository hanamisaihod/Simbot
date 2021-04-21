using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasFX_Controller : MonoBehaviour
{
    public bool clearTrigger;
    public bool failTrigger;
    private bool startShow;                  // Please check this before trigger both clearTrigger and failTrigger
    public bool clearSceneTrigger;
    private bool FXshowing;
    public bool pass1;
    public bool pass2;
    public bool pass3;
    public GameObject textClear;
    public GameObject textFail;
    public GameObject whiteFlash;
    public GameObject darkTheme;
    public GameObject retryButton;
    public GameObject doneButton;

    public GameObject clearBackFX;
    public GameObject clearLeftFX;
    public GameObject clearRightFX;
    public GameObject failBackFX;
    public GameObject starTable;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject emptyStar1;
    public GameObject emptyStar2;
    public GameObject emptyStar3;
    public GameObject textCon1;
    public GameObject textCon2;
    public GameObject textCon3;

    private float clearDuration = 5.1f;
    private float failDuration = 3.6f;
    private float waitUntil;
    private Vector3 clearInitial;
    private Vector3 failInitial;
    private Vector3 retryInitial;
    private Vector3 doneInitial;
    Coroutine usingCor;
    Coroutine loopCor1;
    Coroutine loopCor2;
    Coroutine tableCor;
    AudioSource audioClearBack;
    AudioSource audioFailBack;
    AudioSource audioLeft;
    AudioSource audioRight;
    public AudioClip soundWin1;
    public AudioClip soundWin2;
    public AudioClip soundLose;
    public float volume = 0.5f;     // volume of sound effect
    public EndingStarRating callStarRating;
    public GameObject ratingBox;


    void Start()
    {
        audioClearBack = clearBackFX.GetComponent<AudioSource>();
        audioFailBack = failBackFX.GetComponent<AudioSource>();
        audioLeft = clearLeftFX.GetComponent<AudioSource>();
        audioRight = clearRightFX.GetComponent<AudioSource>();
        whiteFlash.SetActive(true);
        darkTheme.SetActive(true);
        textClear.GetComponent<Image>().enabled = false;
        textFail.GetComponent<Image>().enabled = false;
        whiteFlash.GetComponent<Image>().enabled = false;
        darkTheme.GetComponent<Image>().enabled = false;
        retryButton.GetComponent<Image>().enabled = false;
        doneButton.GetComponent<Image>().enabled = false;
        starTable.GetComponent<Image>().enabled = false;
        star1.GetComponent<Image>().enabled = false;
        star2.GetComponent<Image>().enabled = false;
        star3.GetComponent<Image>().enabled = false;
        emptyStar1.GetComponent<Image>().enabled = false;
        emptyStar2.GetComponent<Image>().enabled = false;
        emptyStar3.GetComponent<Image>().enabled = false;
        textCon1.GetComponent<Text>().enabled = false;
        textCon2.GetComponent<Text>().enabled = false;
        textCon3.GetComponent<Text>().enabled = false;

        clearBackFX.SetActive(false);
        clearLeftFX.SetActive(false);
        clearRightFX.SetActive(false);
        failBackFX.SetActive(false);
        clearInitial = textClear.transform.localScale;
        failInitial = textFail.transform.localScale;
        retryInitial = retryButton.transform.localScale;
        doneInitial = doneButton.transform.localScale;

        //retryButton.GetComponent<Button>().onClick.AddListener(ClearScene); // Please come back to edit this
    }

    void Update()
    {
        audioLeft.volume = volume;      // volume of sound effect
        audioRight.volume = volume;     // volume of sound effect
        //Debug.Log("Cleartype = " + clearTrigger);
        if (clearTrigger && !startShow && !FXshowing)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(ClearShow());
            if(tableCor != null)
            {
                StopCoroutine(tableCor);
            }
            //EndingStarRating.clear = true;
            //Debug.Log("clear = " + EndingStarRating.clear);
            tableCor = StartCoroutine(tableShow());
            startShow = true;
            FXshowing = true;
        }
        else
        {
            clearTrigger = false;        
        }
            

        if (failTrigger && !startShow && !FXshowing)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(FailShow());
            if (tableCor != null)
            {
                StopCoroutine(tableCor);
            }
            tableCor = StartCoroutine(tableShow());
            //EndingStarRating.fail = true;
            //Debug.Log("fail = " + EndingStarRating.fail);
            startShow = true;
            FXshowing = true;
        }
        else
            failTrigger = false;

        if (clearSceneTrigger && !startShow && FXshowing)
        {
            textClear.GetComponent<Image>().enabled = false;
            textFail.GetComponent<Image>().enabled = false;
            whiteFlash.GetComponent<Image>().enabled = false;
            darkTheme.GetComponent<Image>().enabled = false;
            retryButton.GetComponent<Image>().enabled = false;
            doneButton.GetComponent<Image>().enabled = false;
            starTable.GetComponent<Image>().enabled = false;
            star1.GetComponent<Image>().enabled = false;
            star2.GetComponent<Image>().enabled = false;
            star3.GetComponent<Image>().enabled = false;
            emptyStar1.GetComponent<Image>().enabled = false;
            emptyStar2.GetComponent<Image>().enabled = false;
            emptyStar3.GetComponent<Image>().enabled = false;
            textCon1.GetComponent<Text>().enabled = false;
            textCon2.GetComponent<Text>().enabled = false;
            textCon3.GetComponent<Text>().enabled = false;
            StopCoroutine(loopCor1);
            StopCoroutine(loopCor2);
            clearBackFX.SetActive(false);
            clearLeftFX.SetActive(false);
            clearRightFX.SetActive(false);
            failBackFX.SetActive(false);
            clearSceneTrigger = false;
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            if (tableCor != null)
            {
                StopCoroutine(tableCor);
            }
            usingCor = StartCoroutine(Clear());
            startShow = true;
        }
        else
            clearSceneTrigger = false;

    }

    // Function trigger when click try again button    Please change this
    //void ClearScene()
    //{
    //    if (Time.time > waitUntil) clearSceneTrigger = true;
	//	SceneManager.LoadScene("Simulate");
    //}

    IEnumerator ClearShow()
    {
        EndingStarRating.clear = true;
        var Rect = textClear.GetComponent<RectTransform>();
        var whiteRect = whiteFlash.GetComponent<RectTransform>();
        
        clearBackFX.SetActive(false);
        clearLeftFX.SetActive(false);
        clearRightFX.SetActive(false);

        LeanTween.scale(Rect, Rect.localScale / 10.5f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        textClear.GetComponent<Image>().enabled = true;
        //darkTheme.GetComponent<Image>().enabled = true;
        LeanTween.scale(Rect, Rect.localScale * 6f, 1f).setDelay(0.1f).setEaseInOutBack();
        LeanTween.rotateZ(textClear, 3600, 1f);

        yield return new WaitForSeconds(1.1f);

        clearBackFX.SetActive(true);
        whiteFlash.GetComponent<Image>().enabled = true;
        audioClearBack.PlayOneShot(soundWin1, volume);
        LeanTween.alpha(whiteRect, 0f, 1f).setDelay(0.1f);
        LeanTween.scale(Rect, clearInitial, 0.25f).setEaseInOutBack();

        yield return new WaitForSeconds(0.25f);

        clearLeftFX.SetActive(true);
        loopCor1 = StartCoroutine(LoopSound(audioLeft));
        LeanTween.moveLocalY(textClear, textClear.transform.localPosition.y + 8f, 0.75f).setLoopPingPong();

        yield return new WaitForSeconds(1f);

        whiteFlash.GetComponent<Image>().enabled = false;
        LeanTween.alpha(whiteRect, 1f, 0.1f);
        clearRightFX.SetActive(true);
        loopCor2 = StartCoroutine(LoopSound(audioRight));

        yield return new WaitForSeconds(1.7f);

        startShow = false;
    }

    IEnumerator FailShow()
    {
        EndingStarRating.fail = true;
        var Rect = textFail.GetComponent<RectTransform>();
        var darkRect = darkTheme.GetComponent<RectTransform>();

        LeanTween.alpha(darkRect, 0f, 0.1f);
        LeanTween.scale(Rect, Rect.localScale / 10.5f, 0.1f);
        failBackFX.SetActive(false);
        
        yield return new WaitForSeconds(0.1f);

        textFail.GetComponent<Image>().enabled = true;
        darkTheme.GetComponent<Image>().enabled = true;
        failBackFX.SetActive(true);
        LeanTween.scale(Rect, failInitial, 0.75f).setEaseInOutBack();

        yield return new WaitForSeconds(0.75f);

        audioFailBack.PlayOneShot(soundLose, volume);
        LeanTween.alpha(darkRect, 0.9f, 0.1f);
        
        LeanTween.moveLocalY(textFail, textFail.transform.localPosition.y + 8f, 0.75f).setDelay(0.75f).setLoopPingPong();

        yield return new WaitForSeconds(3.3f);

        startShow = false;
    }

    IEnumerator tableShow()
    {
        
        var tableRect = starTable.GetComponent<RectTransform>();
        var retryRect = retryButton.GetComponent<RectTransform>();
        var doneRect = doneButton.GetComponent<RectTransform>();
        var starRect1 = star1.GetComponent<RectTransform>();
        var starRect2 = star2.GetComponent<RectTransform>();
        var starRect3 = star3.GetComponent<RectTransform>();

        LeanTween.scale(tableRect, tableRect.localScale / 10.5f, 0.1f);
        LeanTween.scale(retryRect, retryRect.localScale / 8f, 0.1f);
        LeanTween.scale(doneRect, doneRect.localScale / 8f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        if(EnviSim.Mode == "Main")
        {
            callStarRating.starRatingShow();
            starTable.GetComponent<Image>().enabled = true;
            emptyStar1.GetComponent<Image>().enabled = true;
            emptyStar2.GetComponent<Image>().enabled = true;
            emptyStar3.GetComponent<Image>().enabled = true;
            textCon1.GetComponent<Text>().enabled = true;
            textCon2.GetComponent<Text>().enabled = true;
            textCon3.GetComponent<Text>().enabled = true;
        }
        else
        {
            ratingBox.GetComponent<Image>().enabled = false;
            emptyStar1.SetActive(false);
            emptyStar2.SetActive(false);
            emptyStar3.SetActive(false);
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            textCon1.SetActive(false);
            textCon2.SetActive(false);
            textCon3.SetActive(false);
            
        }
        
        LeanTween.scale(tableRect, tableRect.localScale * 10.5f, 0.75f).setEaseInOutBack();

        yield return new WaitForSeconds(0.75f);

        if(pass1)
        {
            LeanTween.scale(starRect1, starRect1.localScale / 10.5f, 0.1f);
            yield return new WaitForSeconds(0.1f);
            star1.GetComponent<Image>().enabled = true;
            LeanTween.scale(starRect1, starRect1.localScale * 10.5f, 0.75f).setEaseInOutBack();
            yield return new WaitForSeconds(0.75f);
        }
        if (pass2)
        {
            LeanTween.scale(starRect2, starRect2.localScale / 10.5f, 0.1f);
            yield return new WaitForSeconds(0.1f);
            star2.GetComponent<Image>().enabled = true;
            LeanTween.scale(starRect2, starRect2.localScale * 10.5f, 0.75f).setEaseInOutBack();
            yield return new WaitForSeconds(0.75f);
        }
        if (pass3)
        {
            LeanTween.scale(starRect3, starRect3.localScale / 10.5f, 0.1f);
            yield return new WaitForSeconds(0.1f);
            star3.GetComponent<Image>().enabled = true;
            LeanTween.scale(starRect3, starRect3.localScale * 10.5f, 0.75f).setEaseInOutBack();
            yield return new WaitForSeconds(0.75f);
        }

        retryButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(retryRect, retryRect.localScale * 8f, 0.75f).setDelay(0.1f).setEaseInOutBack();
        doneButton.GetComponent<Image>().enabled = true;
        LeanTween.scale(doneRect, doneRect.localScale * 8f, 0.75f).setDelay(0.1f).setEaseInOutBack();

        yield return new WaitForSeconds(0.75f);
    }

    IEnumerator Clear()
    {
        LeanTween.cancel(textClear);
        LeanTween.cancel(textFail);
        LeanTween.cancel(retryButton);
        LeanTween.cancel(doneButton);
        LeanTween.scale(textClear, clearInitial, 0.1f);
        LeanTween.scale(textFail, failInitial, 0.1f);
        LeanTween.scale(retryButton, retryInitial, 0.1f);
        LeanTween.scale(doneButton, doneInitial, 0.1f);
        yield return new WaitForSeconds(0.1f);
        startShow = false;
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
