﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Tutorial : MonoBehaviour
{
    public bool TutorialTrigger = true;
    public GameObject Meow1;
    public GameObject textBox1;
    public GameObject textZone1;
    public GameObject Boy;
    public GameObject back;

    private bool startText;
    private bool textDone;
    public AudioClip soundLetter;
    public float volume = 0.5f;
    AudioSource playAudio;
    public float letterDelay = 0.06f;
    private string[] fullText;
    public Sprite[] boyEmo;
    private string curreentLetter = "";
    private int stage = 0;
    private float waitUntil;
    Coroutine usingCor;
    Coroutine delayCor;

    void Start()
    {
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();

        back.SetActive(true);
        playAudio = GetComponent<AudioSource>();
        textBox1Script.boxUpTrigger = true;
        Meow1Script.showRTrigger = true;
        LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
        fullText = new string[50];
        fullText[0] = "Neko : You can select the mission that you want to play in this menu.";
        fullText[1] = "Neko : If you fail to complete a mission, you won’t be able to play the next one.";
        fullText[2] = "Johny : What!? I won’t make any progress if I fail!?";
        fullText[3] = "Neko : Don’t worry. You only need to fulfill at least one requirement of each mission to complete it.";
        fullText[4] = "Neko : Once you selected a mission, the game will show you the requirements.\nThen, you can press confirm.";

        stage = 0;

    }

    void Update()
    {
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();

        if ((Input.GetMouseButtonDown(0) && Time.time > waitUntil) || stage == 0)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(ShowText(textZone1, fullText[stage]));
            waitUntil = 35 * letterDelay * 1f + Time.time;
            stage++;
        }
        else if (Input.GetMouseButtonDown(0) && Time.time < waitUntil)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            stage--;
            textZone1.GetComponent<Text>().text = fullText[stage];
            stage++;
        }

        if(stage == 1)
            Boy.GetComponent<Image>().sprite = boyEmo[8];
        else if (stage == 2)
            Boy.GetComponent<Image>().sprite = boyEmo[0];
        else if(stage == 3)
            Boy.GetComponent<Image>().sprite = boyEmo[1];
        else if(stage == 4)
            Boy.GetComponent<Image>().sprite = boyEmo[8];
        else if(stage == 5)
            Boy.GetComponent<Image>().sprite = boyEmo[4];
        else if(stage == 6)
            textDone = true;

        if (textDone)
        {
            Meow1Script.showLTrigger = true;
            textBox1Script.boxDownTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
            if (delayCor != null)
            {
                StopCoroutine(delayCor);
            }
            delayCor = StartCoroutine(DelayCall());
            Meow1Script.cancelTrigger = true;
            back.SetActive(false);
            textDone = false;
            stage++;
        }
    }

    IEnumerator ShowText(GameObject textZone, string fulltext)
    {
        for (int i = 0; i <= fulltext.Length; i++)
        {
            curreentLetter = fulltext.Substring(0, i);
            if (i % 3 == 0) playAudio.PlayOneShot(soundLetter, volume);
            textZone.GetComponent<Text>().text = curreentLetter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    IEnumerator ShowButton(GameObject button)
    {
        var Rect = button.GetComponent<RectTransform>();
        LeanTween.scale(Rect, Rect.localScale * 1.05f, 0.5f).setLoopPingPong().setEaseInOutSine();
        yield return new WaitForSeconds(0);
    }

    IEnumerator DelayCall()
    {
        yield return new WaitForSeconds(1);
        startText = false;
    }
}
