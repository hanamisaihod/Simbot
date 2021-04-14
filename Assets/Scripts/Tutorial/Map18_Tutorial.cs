using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map18_Tutorial : MonoBehaviour
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
    public int maxStage = 5;
    Coroutine usingCor;
    Coroutine delayCor;
    private int textSum = 0;
    private int textLength = 0;
    private Text textField1;
    public Button infoButton;

    public GameObject lava;

    void Start()
    {
        if (PlayerPrefs.HasKey("Map18_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Map18_Tutorial") == 1)
            {
                TutorialTrigger = false;
            }
        }
        infoButton.GetComponent<Button>().onClick.AddListener(ActivateTutorial);
        playAudio = GetComponent<AudioSource>();
        fullText = new string[50];
        fullText[0] = "Neko : We are close to the core…";
        fullText[1] = "Neko : What is that?...";
        fullText[2] = "Johny : The ground looks like it’s ready to explode at any time.";
        fullText[3] = "Neko : Originally, this game did not have such obstacles that could harm the players this badly…";
        fullText[4] = "Neko : It must be because of the virus. It's good that the color sensor can detect the red warning zone before each eruption.";
        fullText[5] = "Neko : Be careful.";
        stage = 0;
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
            stage = 0;
            back.SetActive(true);
            textBox1Script.boxUpTrigger = true;
            Meow1Script.showRTrigger = true;
            LeanTween.moveLocalX(Boy, Boy.transform.localPosition.x + 380f, 0.5f).setEaseInOutBack();
            startText = true;
            TutorialTrigger = false;
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

        if (stage == 1)
            Boy.GetComponent<Image>().sprite = boyEmo[4];
        else if (stage == 2)
        {
            Boy.GetComponent<Image>().sprite = boyEmo[10];
            lava.SetActive(true);
        }
        else if (stage == 3)
            Boy.GetComponent<Image>().sprite = boyEmo[1];
        else if (stage == 4)
            Boy.GetComponent<Image>().sprite = boyEmo[0];
        else if (stage == 6)
            textDone = true;

        if (textDone)
        {
            lava.SetActive(false);
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
            PlayerPrefs.SetInt("Map18_Tutorial", 1); // remember that this dialogue already happened
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
