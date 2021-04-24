using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleBot_Tutorial : MonoBehaviour
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
    public float volume = 0.5f;     // volume of sound effect
    AudioSource playAudio;
    public float letterDelay = 0.06f;
    private string[] fullText;
    public Sprite[] boyEmo;
    private string curreentLetter = "";
    private int stage = 0;
    public int maxStage = 7;
    Coroutine usingCor;
    Coroutine delayCor;
    private int textSum = 0;
    private int textLength = 0;
    private Text textField1;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("TurtleBot_Tutorial"))
        {
            if (PlayerPrefs.GetInt("TurtleBot_Tutorial") == 1)
            {
                Destroy(gameObject);
            }
        }

        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();
        back.SetActive(true);
        playAudio = GetComponent<AudioSource>();
        textBox1Script.boxUpTrigger = true;
        Meow1Script.showRTrigger = true;
        LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
        fullText = new string[50];
        fullText[0] = "Neko: This is the robot that will help us clear the missions.";
        fullText[1] = "Neko: This one is the “TurtleBot”. It is inspired by transport robots.\n" +
            "It has a distance sensor on its head to detect things in 360 degrees.";
        fullText[2] = "Johny: Subarashiiiiiiiiiiii !!!!!!!!!!!!! (Amazing!!!)";
        fullText[3] = "Johny: So how good is the distance sensor?";
        fullText[4] = "Neko: It helps the robot to know the distance between itself and the target object. " +
            "It lets know when to stop the robot before it collides.";
        fullText[5] = "Johny: I see.";
        fullText[6] = "Neko: If you are ready, hit the next button, and let’s get started!";
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
        playAudio.volume = volume;  // volume of sound effect

        var Meow1Script = Meow1.GetComponent<MeowUI_Animating>();
        var textBox1Script = textBox1.GetComponent<Textbox>();

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

        if(stage == 1)
            Boy.GetComponent<Image>().sprite = boyEmo[4];
        else if (stage == 3)
            Boy.GetComponent<Image>().sprite = boyEmo[2];
        else if (stage == 4)
            Boy.GetComponent<Image>().sprite = boyEmo[1];
        else if (stage == 5)
            Boy.GetComponent<Image>().sprite = boyEmo[8];
        else if (stage == 6)
            Boy.GetComponent<Image>().sprite = boyEmo[3];
        else if (stage == 7)
            Boy.GetComponent<Image>().sprite = boyEmo[4];
        else if (stage == 8)
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
            PlayerPrefs.SetInt("TurtleBot_Tutorial", 1); // remember that this dialogue already happened
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
