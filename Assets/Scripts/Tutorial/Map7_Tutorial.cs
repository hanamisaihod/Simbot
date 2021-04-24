using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map7_Tutorial : MonoBehaviour
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
    public float volume = 0.5f;     // volume of sound effect
    AudioSource playAudio;
    public float letterDelay = 0.06f;
    private string[] fullText;
    public Sprite[] boyEmo;
    private string curreentLetter = "";
    private int stage = 0;
    public int maxStage = 8;
    Coroutine usingCor;
    Coroutine delayCor;
    private int textSum = 0;
    private int textLength = 0;
    private Text textField1;
    public Button infoButton;

    public GameObject tutorial_if;
    public GameObject tutorial_while;
    public GameObject code_tutorial;

    void Start()
    {
        if (PlayerPrefs.HasKey("Map7_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Map7_Tutorial") == 1)
            {
                TutorialTrigger = false;
            }
        }
        infoButton.GetComponent<Button>().onClick.AddListener(ActivateTutorial);
        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();
        playAudio = GetComponent<AudioSource>();
        fullText = new string[50];
        fullText[0] = "Johny: Whew! How did I make it through all that!";
        fullText[1] = "Neko: Ha! You really are his son!";
        fullText[2] = "";
        fullText[3] = "";
        fullText[4] = "Neko: Now, I will introduce you to new types of blocks. Under the Move block is the If block. It will execute the code inside only if the condition is true.";
        fullText[5] = "Neko: Conditions will be different depending on the robot used for the mission. For this robot, it will detect the distance in the direction we set.";
        fullText[6] = "Neko: For example, the front is 0 degrees and the right is 90 degrees.";
        fullText[7] = "Neko: The last one is the While block that repeats the code inside until the condition is false.";
        fullText[8] = "Neko: In this mission, try writing a program like this.";
        foreach (Transform child in textBox1.transform)
        {
            if (child.name == "Text")
            {
                textField1 = child.gameObject.GetComponent<Text>();
            }
        }
    }
    void ActivateTutorial()
    {
        if (!startText) TutorialTrigger = true;
    }

    void Update()
    {
        playAudio.volume = volume;  // volume of sound effect

        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();

        if (TutorialTrigger)
        {
            PlayerPrefs.SetInt("Enable_IfWhile", 1);
            stage = 0;
            back.SetActive(true);
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
            startText = true;
            TutorialTrigger = false;
        }

        if (trigger2)
        {
            back.SetActive(true);
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
            trigger2 = false;
            startText = true;
        }

        if (startText)
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
        }

        if(stage == 1)
            Boy.GetComponent<Image>().sprite = boyEmo[11];
        else if (stage == 2)
            Boy.GetComponent<Image>().sprite = boyEmo[10];
        else if (stage == 3)
        {
            textDone = true;
            partDone = 1;
        }
        else if (stage == 5)
        {
            Boy.GetComponent<Image>().sprite = boyEmo[4];
            tutorial_if.SetActive(true);
        }
        else if (stage == 8)
        {
            tutorial_if.SetActive(false);
            tutorial_while.SetActive(true);
        }
        else if (stage == 9)
        {
            code_tutorial.SetActive(true);
            tutorial_while.SetActive(false);
        }
        else if (stage == 10)
            code_tutorial.SetActive(false);
        else if (stage == 11)
        {
            textDone = true;
            partDone = 2;
        }

        if (textDone)
        {
            if (partDone == 1)
            {
                Meow1Script.showLTrigger = true;
                textBox1Script.boxDownTrigger = true;
                LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
                back.SetActive(false);
                Meow1Script.cancelTrigger = true;
                startText = false;
            }
            else if (partDone == 2)
            {
                Meow1Script.showLTrigger = true;
                textBox1Script.boxDownTrigger = true;
                LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x - 380f, 0.5f).setEaseInOutBack();
                back.SetActive(false);
                if (delayCor != null)
                {
                    StopCoroutine(delayCor);
                }
                delayCor = StartCoroutine(DelayCall());
                Meow1Script.cancelTrigger = true;
                startText = false;
                PlayerPrefs.SetInt("Map7_Tutorial", 1);
            }
            textDone = false;
            stage++;
        }
    }

    public void startPart2()
    {
        if (partDone == 1)
        {
            trigger2 = true;
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
