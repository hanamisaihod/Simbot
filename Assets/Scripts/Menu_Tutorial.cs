﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu_Tutorial : MonoBehaviour
{
    public static bool MenuTutorialTrigger;
    private bool MenuTutorialTrigger2;
    private bool MenuTutorialTrigger3;
    public GameObject Meow1;
    public GameObject textBox1;
    public GameObject Meow2;
    public GameObject textBox2;
    public GameObject ClickIcon1;
    public GameObject ClickIcon2;
    public GameObject ClickIcon3;
    public GameObject ClickIcon4;
    public GameObject ClickIcon5;
    public GameObject textZone1;
    public GameObject textZone2;
    public GameObject buttonSim;
    public GameObject buttonRobot;
    public GameObject buttonMission;
    public GameObject buttonSetting;
    public GameObject buttonInfo;

    private bool textDone;
    private int partDone = 0;
    public static bool startText;

    public AudioClip soundLetter;
    public float volume = 0.5f;
    AudioSource playAudio;
    public float letterDelay = 0.01f;
    public string[] fullText;
    private string curreentLetter = "";
    private int stage = 0;
    private float waitUntil;
    Coroutine usingCor;
    Coroutine delayCor;


    void Start()
    {
        playAudio = GetComponent<AudioSource>();
        ClickIcon1.GetComponent<Image>().enabled = false;
        ClickIcon2.GetComponent<Image>().enabled = false;
        ClickIcon3.GetComponent<Image>().enabled = false;
        ClickIcon4.GetComponent<Image>().enabled = false;
        ClickIcon5.GetComponent<Image>().enabled = false;
        StartCoroutine(ShowButton(buttonSim));
        StartCoroutine(ShowButton(buttonRobot));
        StartCoroutine(ShowButton(buttonMission));
        StartCoroutine(ShowButton(buttonSetting));
        StartCoroutine(ShowButton(buttonInfo));
        fullText[0] = "Welcome to SIMBOT !!!";
        fullText[1] = "My name is Nyanko.\nI gonna guide you to play this game.";
        fullText[2] = "This game, you can create robots from various robot parts, program it to do the mission, and also can create your own map.";
        fullText[3] = "When you want simulate your robot to do mission in AR mode.\nYou can click here.";
        fullText[4] = "";
        fullText[5] = "";
        fullText[6] = "If you want to edit your robot or code, you would click here.";
        fullText[7] = "If you want to create or edit your mission, you would click here.";
        fullText[8] = "You can click here whenever you want to change sound volume or camera controlling.";
        fullText[9] = "If you want to listen me again, you would click sign like this.";
        fullText[10] = "";
        fullText[11] = "";
        fullText[12] = "Even this game still hasn't been done, we hope you would have fun";
        fullText[13] = "Please give us your feedback\nThank you so much";
    }

    void Update()
    {
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();
        var Meow2Script = Meow2.GetComponent<MeowUI_Animating>();
        var textBox2Script = textBox2.GetComponent<Textbox>();

        if (MenuTutorialTrigger)
        {
            stage = 0;
            partDone = 0;
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            MenuTutorialTrigger = false;
            startText = true;
        }

        if (MenuTutorialTrigger2)
        {
            textBox2Script.boxDownTrigger = true;
            Meow2Script.showLTrigger = true;
            MenuTutorialTrigger2 = false;
        }

        if (MenuTutorialTrigger3)
        {
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            MenuTutorialTrigger3 = false;
        }

        if (startText)
        {
            if ((Input.GetMouseButtonDown(0) && Time.time > waitUntil) || stage == 0 || stage == 6 || stage == 12)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                if (partDone == 0 || partDone == 2) usingCor = StartCoroutine(ShowText(textZone1,fullText[stage]));
                else if (partDone == 1) usingCor = StartCoroutine(ShowText(textZone2,fullText[stage]));
                waitUntil = fullText.Length * letterDelay * 2.5f + Time.time;
                stage++;
            }
            else if (Input.GetMouseButtonDown(0) && Time.time < waitUntil)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                stage--;
                if (partDone == 0 || partDone == 2) textZone1.GetComponent<Text>().text = fullText[stage];
                else if (partDone == 1) textZone2.GetComponent<Text>().text = fullText[stage];
                stage++;
            }
        }

        if (stage == 4)
        {
            ClickIcon1.GetComponent<Image>().enabled = true;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonSim.GetComponent<Image>().enabled = true;
            buttonRobot.GetComponent<Image>().enabled = false;
            buttonMission.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;

            buttonSim.transform.SetAsLastSibling();
            buttonRobot.transform.SetAsFirstSibling();
            buttonMission.transform.SetAsFirstSibling();
            buttonSetting.transform.SetAsFirstSibling();
            buttonInfo.transform.SetAsFirstSibling();
        }
        else if (stage == 7)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = true;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonSim.GetComponent<Image>().enabled = false;
            buttonRobot.GetComponent<Image>().enabled = true;
            buttonMission.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;

            buttonSim.transform.SetAsFirstSibling();
            buttonRobot.transform.SetAsLastSibling();
            buttonMission.transform.SetAsFirstSibling();
            buttonSetting.transform.SetAsFirstSibling();
            buttonInfo.transform.SetAsFirstSibling();
        }
        else if (stage == 8)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = true;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonSim.GetComponent<Image>().enabled = false;
            buttonRobot.GetComponent<Image>().enabled = false;
            buttonMission.GetComponent<Image>().enabled = true;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;

            buttonSim.transform.SetAsFirstSibling();
            buttonRobot.transform.SetAsFirstSibling();
            buttonMission.transform.SetAsLastSibling();
            buttonSetting.transform.SetAsFirstSibling();
            buttonInfo.transform.SetAsFirstSibling();
        }
        else if (stage == 9)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = true;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonSim.GetComponent<Image>().enabled = false;
            buttonRobot.GetComponent<Image>().enabled = false;
            buttonMission.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = true;
            buttonInfo.GetComponent<Image>().enabled = false;

            buttonSim.transform.SetAsFirstSibling();
            buttonRobot.transform.SetAsFirstSibling();
            buttonMission.transform.SetAsFirstSibling();
            buttonSetting.transform.SetAsLastSibling();
            buttonInfo.transform.SetAsFirstSibling();
        }
        else if (stage == 10)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = true;

            buttonSim.GetComponent<Image>().enabled = false;
            buttonRobot.GetComponent<Image>().enabled = false;
            buttonMission.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = true;

            buttonSim.transform.SetAsFirstSibling();
            buttonRobot.transform.SetAsFirstSibling();
            buttonMission.transform.SetAsFirstSibling();
            buttonSetting.transform.SetAsFirstSibling();
            buttonInfo.transform.SetAsLastSibling();
        }
        else
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonSim.GetComponent<Image>().enabled = false;
            buttonRobot.GetComponent<Image>().enabled = false;
            buttonMission.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = true;

            buttonSim.transform.SetAsFirstSibling();
            buttonRobot.transform.SetAsFirstSibling();
            buttonMission.transform.SetAsFirstSibling();
            buttonSetting.transform.SetAsFirstSibling();
            buttonInfo.transform.SetAsFirstSibling();
        }

        if (stage == 5)
        {
            textDone = true;
            partDone++;
            stage++;
        }
        else if (stage == 11)
        {
            textDone = true;
            partDone++;
            stage++;
        }
        else if (stage == 15)
        {
            textDone = true;
            partDone++;
            stage++;
        }

        if (textDone)
        {
            if (partDone == 1)
            {
                Meow1Script.showLTrigger = true;
                textBox1Script.boxDownTrigger = true;
                MenuTutorialTrigger2 = true;
                Meow1Script.cancelTrigger = true;
            }
            else if (partDone == 2)
            {
                Meow2Script.showRTrigger = true;
                textBox2Script.boxUpTrigger = true;
                MenuTutorialTrigger3 = true;
                Meow2Script.cancelTrigger = true;
            }
            else if (partDone == 3)
            {
                Meow1Script.showLTrigger = true;
                textBox1Script.boxDownTrigger = true;
                if (delayCor != null)
                {
                    StopCoroutine(delayCor);
                }
                delayCor = StartCoroutine(DelayCall());
                Meow1Script.cancelTrigger = true;
                Meow2Script.cancelTrigger = true;
            }
            textDone = false;
        }
    }

    IEnumerator ShowText(GameObject textZone,string fulltext)
    {
        for (int i = 0; i <= fulltext.Length; i++)
        {
            curreentLetter = fulltext.Substring(0, i);
            if(i%3 == 0) playAudio.PlayOneShot(soundLetter, volume);
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