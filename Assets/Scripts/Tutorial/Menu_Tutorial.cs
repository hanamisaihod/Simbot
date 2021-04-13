using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * For menu tutorial (and any other tutorial)
 * Save name is {ScriptName}
 * Immediately delete this object when playerprefs returns true
 */

public class Menu_Tutorial : MonoBehaviour
{
    public static bool MenuTutorialTrigger = false;
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
    public GameObject buttonPlay;
    public GameObject buttonCreate;
    public GameObject buttonSetting;
    public GameObject buttonExit;
    public GameObject buttonInfo;
    public GameObject Boy;
    public Menu_Controller menuController;

    private Text textField1;
    private Text textField2;

    private bool textDone;
    private int partDone = 0;
    public static bool startText;

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
    private int textSum = 0;
    private int textLength = 0;


    void Start()
    {
        if (PlayerPrefs.HasKey("Menu_Tutorial"))
		{
            if (PlayerPrefs.GetInt("Menu_Tutorial") == 1)
            {
                MenuTutorialTrigger = false;
            }
		}
        playAudio = GetComponent<AudioSource>();
        ClickIcon1.GetComponent<Image>().enabled = false;
        ClickIcon2.GetComponent<Image>().enabled = false;
        ClickIcon3.GetComponent<Image>().enabled = false;
        ClickIcon4.GetComponent<Image>().enabled = false;
        ClickIcon5.GetComponent<Image>().enabled = false;
        StartCoroutine(ShowButton(buttonPlay));
        StartCoroutine(ShowButton(buttonCreate));
        StartCoroutine(ShowButton(buttonSetting));
        StartCoroutine(ShowButton(buttonExit));
        StartCoroutine(ShowButton(buttonInfo));
        fullText = new string[50];
        fullText[0] = "??? : Johny! Johny!!";
        fullText[1] = "Johny : Eh? What is this? Did dad create this?";
        fullText[2] = "??? : Hey Johny! Listen to me!";
        fullText[3] = "??? : Your Dad ... w. . was sucked into the game!!";
        fullText[4] = "Johny : Ehhhhhhhhhhhhhhhh!!!";
        fullText[5] = "??? : Please help me Johny, I tried to help your father but I alone could not do much.";
        fullText[6] = "Johny : B...But, who are you? WHAT are you?";
        fullText[7] = "Neko : I'm Neko, An AI that created by your father to guide players through this game.";
        fullText[8] = "Neko : But there was an anomaly during the development.\nIt seems that a virus has invaded the system.\nThen your father ....";
        fullText[9] = "Neko : I could not do anything...";
        fullText[10] = "Johny : A. .And what can I do!?";
        fullText[11] = "Neko : You have to play this game and take me to the deepest level.\nIf I were there, I can get rid of the virus there!";
        fullText[12] = "Johny : Then what are we waiting for? Let's GO!!!";
        fullText[13] = "Neko : In this game, you can control robots by creating block-based programs to complete missions and you also can create your mission.";
        fullText[14] = "Neko : When you want to play.\nJust press here.";
        fullText[15] = "";
        fullText[16] = "";
        fullText[17] = "Neko : If you want to create or edit your mission, press here.";
        fullText[18] = "Neko : You can press here when you want to adjust the sound volume or camera controls.";
        fullText[19] = "Neko : If you want to take some rest, press here.\nI hope to see you again.\nWe have a job to do";
        fullText[20] = "Neko : If you want to listen me again, press a button that looks like this";
        fullText[21] = "";
        fullText[22] = "";
        fullText[23] = "Neko : Even this game still hasn't been done, we hope you would have fun";
        fullText[24] = "Neko : Now let start the game";

        //Get text children
        foreach (Transform child in textBox1.transform)
		{
            if (child.name == "Text")
			{
                textField1 = child.gameObject.GetComponent<Text>();
			}
		}
        foreach (Transform child in textBox2.transform)
        {
            if (child.name == "Text")
            {
                textField2 = child.gameObject.GetComponent<Text>();
            }
        }
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
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
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
        textSum = textField1.text.Length + textField2.text.Length;
        if (stage > 0)
		{
            if (fullText[stage-1] != null)
			{
                textLength = fullText[stage - 1].Length;
            }
		}
        if (startText)
        {
            if ((Input.GetMouseButtonDown(0) && textSum >= textLength) || stage == 0 || stage == 17 || stage == 23)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                if (fullText[stage] != null) // Check if fulltext has any text to show
                {
                    if (partDone == 0 || partDone == 2)
                    {
                        usingCor = StartCoroutine(ShowText(textZone1, fullText[stage]));
                    }
                    else if (partDone == 1)
                    {
                        usingCor = StartCoroutine(ShowText(textZone2, fullText[stage]));
                    }
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
                    if (partDone == 0 || partDone == 2) textZone1.GetComponent<Text>().text = fullText[stage];
                    else if (partDone == 1) textZone2.GetComponent<Text>().text = fullText[stage];
                }
                stage++;
            }
        }

        // Boy Emotion
        if (stage == 2)
            Boy.GetComponent<Image>().sprite = boyEmo[1];
        else if (stage == 3)
            Boy.GetComponent<Image>().sprite = boyEmo[0];
        else if (stage == 5)
            Boy.GetComponent<Image>().sprite = boyEmo[2];
        else if (stage == 7)
            Boy.GetComponent<Image>().sprite = boyEmo[1];
        else if (stage == 8)
            Boy.GetComponent<Image>().sprite = boyEmo[8];
        else if (stage == 9)
            Boy.GetComponent<Image>().sprite = boyEmo[10];
        else if (stage == 11)
            Boy.GetComponent<Image>().sprite = boyEmo[11];
        else if (stage == 12)
            Boy.GetComponent<Image>().sprite = boyEmo[8];
        else if (stage == 13)
            Boy.GetComponent<Image>().sprite = boyEmo[7];
        else if (stage == 14)
            Boy.GetComponent<Image>().sprite = boyEmo[6];

        if (stage == 15)
        {
            ClickIcon1.GetComponent<Image>().enabled = true;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonPlay.GetComponent<Image>().enabled = true;
            buttonCreate.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonExit.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;
            
        }
        else if (stage == 18)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = true;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonPlay.GetComponent<Image>().enabled = false;
            buttonCreate.GetComponent<Image>().enabled = true;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonExit.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;
           
        }
        else if (stage == 19)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = true;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonPlay.GetComponent<Image>().enabled = false;
            buttonCreate.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = true;
            buttonExit.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = false;
            
        }
        else if (stage == 20)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = true;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonPlay.GetComponent<Image>().enabled = false;
            buttonCreate.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonExit.GetComponent<Image>().enabled = true;
            buttonInfo.GetComponent<Image>().enabled = false;
            
        }
        else if (stage == 21)
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = true;

            buttonPlay.GetComponent<Image>().enabled = false;
            buttonCreate.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonExit.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = true;
            
        }
        else
        {
            ClickIcon1.GetComponent<Image>().enabled = false;
            ClickIcon2.GetComponent<Image>().enabled = false;
            ClickIcon3.GetComponent<Image>().enabled = false;
            ClickIcon4.GetComponent<Image>().enabled = false;
            ClickIcon5.GetComponent<Image>().enabled = false;

            buttonPlay.GetComponent<Image>().enabled = false;
            buttonCreate.GetComponent<Image>().enabled = false;
            buttonSetting.GetComponent<Image>().enabled = false;
            buttonExit.GetComponent<Image>().enabled = false;
            buttonInfo.GetComponent<Image>().enabled = true;
            
        }

        if (stage == 16)
        {
            textDone = true;
            partDone++;
            stage++;
        }
        else if (stage == 22)
        {
            textDone = true;
            partDone++;
            stage++;
        }
        else if (stage == 26)
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
                LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
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
                PlayerPrefs.SetInt("Menu_Tutorial", 1); // remember that this dialogue already happened
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


//Backup code
//if (startText)
//{
//    if ((Input.GetMouseButtonDown(0) && Time.time > waitUntil) || stage == 0 || stage == 17 || stage == 23)
//    {
//        if (usingCor != null)
//        {
//            StopCoroutine(usingCor);
//        }
//        if (fullText[stage] != null) // Check if fulltext has any text to show
//        {
//            if (partDone == 0 || partDone == 2)
//            {
//                usingCor = StartCoroutine(ShowText(textZone1, fullText[stage]));
//            }
//            else if (partDone == 1)
//            {
//                usingCor = StartCoroutine(ShowText(textZone2, fullText[stage]));
//            }
//        }
//        waitUntil = 35 * letterDelay * 1f + Time.time;
//        stage++;
//    }
//    else if (Input.GetMouseButtonDown(0) && Time.time < waitUntil)
//    {
//        if (usingCor != null)
//        {
//            StopCoroutine(usingCor);
//        }
//        stage--;
//        if (partDone == 0 || partDone == 2) textZone1.GetComponent<Text>().text = fullText[stage];
//        else if (partDone == 1) textZone2.GetComponent<Text>().text = fullText[stage];
//        stage++;
//    }
//}