﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map1_Tutorial : MonoBehaviour
{
    public bool TutorialTrigger = true;
    private bool trigger2;
    public GameObject Meow1;
    public GameObject textBox1;
    public GameObject textZone1;
    public GameObject Boy;
    public GameObject back;

    private bool startText;
    private bool textDone;
    private int partDone = 0;
    public AudioClip soundLetter;
    public float volume = 0.5f;
    AudioSource playAudio;
    public float letterDelay = 0.06f;
    private string[] fullText;
    public Sprite[] boyEmo;
    private string curreentLetter = "";
    private int stage = 0;
    public int maxStage = 21;
    Coroutine usingCor;
    Coroutine delayCor;
    private int textSum = 0;
    private int textLength = 0;
    private Text textField1;

    public GameObject joys;
    public GameObject startPoint;
    public GameObject goal;
    public GameObject code_button;
    public GameObject palette_button;

    void Start()
    {
        if (PlayerPrefs.HasKey("Map1_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Map1_Tutorial") == 1)
            {
                TutorialTrigger = false;
            }
        }
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();
        playAudio = GetComponent<AudioSource>();
        fullText = new string[50];
        fullText[0] = "Johny : Wow!! Amazing!!";
        fullText[1] = "Johny : But, how do I play?";
        fullText[2] = "Neko : In this scene, you will be able to see all the obstacles in the mission before you start programming.";
        fullText[3] = "Neko : You can change the camera position by using the joystick on the left,\n" +
            "and you can change the camera angle using the joystick on the right.";
        fullText[4] = "Neko : The starting point is right here.";
        fullText[5] = "Neko : The destination is here.";
        fullText[6] = "Neko : In this mission, you just need to write a program for the robot to walk to the destination.";
        fullText[7] = "Johny : Doesn't seem too hard.";
        fullText[8] = "Neko : That’s the spirit! Alright, if you want to start writing the program,\n" +
            "you can just press the code button right there.";
        fullText[9] = "";
        fullText[10] = "";
        fullText[11] = "Neko : Let’s start writing a program.";
        fullText[12] = "Neko : The start block is the starting point of the program.\n" +
            "The Robot will begin reading the program from this block.";
        fullText[13] = "Neko : You can choose other types of blocks from the box on the right-hand side.";
        fullText[14] = "Neko : The first block we're going to use is the Move block. Hold and drag to start block.";
        fullText[15] = "Neko : In the Move block, the first box is for the forward movement speed of the robot, in block per second.\n" +
            "Of course, you can also enter a negative value to move backward.";
        fullText[16] = "Neko : The second one is the rotational speed of the robot, in degree per second.";
        fullText[17] = "Neko : The last box is the number of seconds the robot follows the instruction for.\n" +
            "If you want the robot to follow the instructions until there’s a new Move command, you can set the delay to 9999.";
        fullText[18] = "Neko : From the mission we’ve seen, let's set move speed to 1, rotate speed to 0, and delay to 3.";
        fullText[19] = "Neko : If you want to go back to see the mission again, press the map button.";
        fullText[20] = "Neko : If you want to test the program, you can press simulate button.";


        stage = 0;
        foreach (Transform child in textBox1.transform)
        {
            if (child.name == "Text")
            {
                textField1 = child.gameObject.GetComponent<Text>();
            }
        }
    }

    void Update()
    {
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();

        if(TutorialTrigger)
        {
            back.SetActive(true);
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
            code_button.SetActive(true);
            StartCoroutine(ShowButton(code_button));
            startText = true;
            TutorialTrigger = false;
        }
        if(trigger2)
        {
            palette_button.SetActive(true);
            StartCoroutine(ShowButton(palette_button));
            back.SetActive(true);
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
            trigger2 = false;
            startText = true;
        }

        if(startText)
        {
            textSum = textField1.text.Length;
            if (stage > 0)
            {
                if (fullText[stage - 1] != null)
                {
                    textLength = fullText[stage - 1].Length;
                }
            }
            if ((Input.GetMouseButtonDown(0) && textSum >= textLength) || stage == 0)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                if (fullText[stage] != null) // Check if fulltext has any text to show
                {
                    usingCor = StartCoroutine(ShowText(textZone1, fullText[stage]));
                }
                stage++;
            }
            else if (Input.GetMouseButtonDown(0) && textSum < textLength)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                stage--;
                if (fullText[stage] != null)
                {
                    textZone1.GetComponent<Text>().text = fullText[stage];
                }
                stage++;
            }

            // Boy Emotion
            if (stage == 1)
                Boy.GetComponent<Image>().sprite = boyEmo[2];

            // Object Control
            if (stage == 4)
                joys.SetActive(true);
            else
                joys.SetActive(false);
            if (stage == 5)
                startPoint.SetActive(true);
            else
                startPoint.SetActive(false);
            if (stage == 6)
                goal.SetActive(true);
            else
                goal.SetActive(false);
            if(stage == 9)
                code_button.GetComponent<Image>().enabled = true;
            else
                code_button.GetComponent<Image>().enabled = false;
            if (stage == 10)
            {
                textDone = true;
                partDone = 1;
            }
            if(stage == 14)
                palette_button.GetComponent<Image>().enabled = true;
            else
                palette_button.GetComponent<Image>().enabled = false;


            if (textDone)
            {
                if(partDone == 1)
                {
                    Meow1Script.showLTrigger = true;
                    textBox1Script.boxDownTrigger = true;
                    LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
                    back.SetActive(false);
                    Meow1Script.cancelTrigger = true;
                    startText = false;
                }
                textDone = false;
                stage++;
                /*Meow1Script.showLTrigger = true;
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
                PlayerPrefs.SetInt("Map1_Tutorial", 1); // remember that this dialogue already happened
                startText = false;*/
            }
        }
        
    }

    public void startPart2()
    {
        if (partDone == 1)
            trigger2 = true;
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