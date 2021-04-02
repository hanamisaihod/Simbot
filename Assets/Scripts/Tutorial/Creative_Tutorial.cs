using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creative_Tutorial : MonoBehaviour
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
    public int maxStage = 1;
    Coroutine usingCor;
    Coroutine delayCor;
    private int textSum = 0;
    private int textLength = 0;
    private Text textField1;

    public GameObject bin;
    public GameObject rotate;
    public GameObject confirm;
    public GameObject clickIcon;
    public GameObject clickIcon2;
    public GameObject tool;

    void Start()
    {
        if (PlayerPrefs.HasKey("Creative_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Creative_Tutorial") == 1)
            {
                TutorialTrigger = false;
            }
        }
        playAudio = GetComponent<AudioSource>();
        fullText = new string[50];
        fullText[0] = "Neko : For creating missions, You can select a structure or obstacle from the toolbar underneath it.";
        fullText[1] = "Neko : The building outline will appear in the scene. The icons in the top right corner are used to delete, rotate, and confirm building locations respectively.";
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

        if (TutorialTrigger)
        {
            bin.SetActive(true);
            rotate.SetActive(true);
            confirm.SetActive(true);
            tool.SetActive(true);
            StartCoroutine(ShowButton(bin));
            StartCoroutine(ShowButton(rotate));
            StartCoroutine(ShowButton(confirm));
            StartCoroutine(ShowButton(tool));
            bin.GetComponent<Image>().enabled = false;
            rotate.GetComponent<Image>().enabled = false;
            confirm.GetComponent<Image>().enabled = false;
            tool.GetComponent<Image>().enabled = false;
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
        {
            Boy.GetComponent<Image>().sprite = boyEmo[4];
            clickIcon.SetActive(true);
            tool.GetComponent<Image>().enabled = true;
        }
        else if (stage == 2)
        {
            clickIcon.SetActive(false);
            clickIcon2.SetActive(true);
            tool.GetComponent<Image>().enabled = false;
            bin.GetComponent<Image>().enabled = true;
            rotate.GetComponent<Image>().enabled = true;
            confirm.GetComponent<Image>().enabled = true;
        }
        else if (stage == 3)
        {
            textDone = true;
            clickIcon2.SetActive(false);
            bin.GetComponent<Image>().enabled = false;
            rotate.GetComponent<Image>().enabled = false;
            confirm.GetComponent<Image>().enabled = false;
        }
           

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
            PlayerPrefs.SetInt("Creative_Tutorial", 1); // remember that this dialogue already happened
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
