using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Controller : MonoBehaviour
{
    public GameObject back;
    public GameObject backTop;
    public GameObject S1;
    public GameObject S1_5;
    public GameObject S2;
    public GameObject S3;
    public GameObject S4;
    public GameObject S5;
    public GameObject S6;

    AudioSource Audio;
    public float volume = 0.5f;
    public AudioClip typeKeyboard;
    public AudioClip openDoor;
    public AudioClip walkSound;

    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
        StartCoroutine(showCutScene());
    }

    IEnumerator showCutScene()
    {
        var S1_Rect = S1.GetComponent<RectTransform>();
        var S1_5_Rect = S1_5.GetComponent<RectTransform>();
        var S2_Rect = S2.GetComponent<RectTransform>();
        var S3_Rect = S3.GetComponent<RectTransform>();
        var S4_Rect = S4.GetComponent<RectTransform>();
        var S5_Rect = S5.GetComponent<RectTransform>();
        var S6_Rect = S6.GetComponent<RectTransform>();
        var back_Rect = back.GetComponent<RectTransform>();
        var backTop_Rect = backTop.GetComponent<RectTransform>();

        LeanTween.alpha(S1_5_Rect, 0f, 0.5f);
        LeanTween.alpha(S2_Rect, 0f, 0.5f);
        LeanTween.alpha(S3_Rect, 0f, 0.5f);
        LeanTween.alpha(S4_Rect, 0f, 0.5f);
        LeanTween.alpha(S5_Rect, 0f, 0.5f);
        LeanTween.alpha(S6_Rect, 0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        LeanTween.alpha(backTop_Rect, 0f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        LeanTween.moveLocalY(S1,155,10f);
        Audio.PlayOneShot(typeKeyboard, volume);
        yield return new WaitForSeconds(11f);
        LeanTween.alpha(S1_5_Rect, 1f, 1f);
        yield return new WaitForSeconds(2f);
        LeanTween.alpha(S1_Rect, 0f, 1f);
        LeanTween.alpha(S1_5_Rect, 0f, 1f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.alpha(S2_Rect, 1f, 1f);
        yield return new WaitForSeconds(2f);
        LeanTween.alpha(S3_Rect, 1f, 1f);
        Audio.PlayOneShot(openDoor, volume);
        yield return new WaitForSeconds(3f);
        LeanTween.alpha(S4_Rect, 1f, 1f);
        yield return new WaitForSeconds(3f);
        LeanTween.alpha(S5_Rect, 1f, 1f);
        yield return new WaitForSeconds(2f);
        Audio.PlayOneShot(walkSound, volume);
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(S6_Rect, 1f, 1f);
        yield return new WaitForSeconds(3f);
        LeanTween.alpha(S2_Rect, 0f, 0.5f);
        LeanTween.alpha(S3_Rect, 0f, 0.5f);
        LeanTween.alpha(S4_Rect, 0f, 0.5f);
        LeanTween.alpha(S5_Rect, 0f, 0.5f);
        LeanTween.alpha(S6_Rect, 0f, 0.5f);
        LeanTween.alpha(back_Rect, 0f, 0.5f);
        yield return new WaitForSeconds(1f);
        Menu_Tutorial.MenuTutorialTrigger = true;
        gameObject.SetActive(false);
    }
}
