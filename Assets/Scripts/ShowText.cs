using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public AudioClip soundLetter;
    public float volume;
    AudioSource playAudio;
    public float delay = 0.06f;
    public string fullText;
    public static string textShowTutorial = "";
    public static bool textDone;
    private string curreentLetter = "";
    private int stage = 0;
    private float waitUntil;
    Coroutine usingCor;

    void Start()
    {
       playAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(textShowTutorial == "Menu")
        {
            if ((Input.GetMouseButtonDown(0) && Time.time > waitUntil) || stage == 0)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                usingCor = StartCoroutine(Show());
                waitUntil = 2 + Time.time;
                stage++;
            }
            else if(Input.GetMouseButtonDown(0) && Time.time < waitUntil)
            {
                if (usingCor != null)
                {
                    StopCoroutine(usingCor);
                }
                this.GetComponent<Text>().text = fullText;
            }

            if (stage == 1) fullText = "Welcome to SIMBOT !!!";
            else if (stage == 2) fullText = "My name is Nyanko.\nI gonna guide you to play this game.";
            else if (stage == 3)
            {
                fullText = "This game is a robot simulator game that the you can create a robot from various robot parts, program it to do the mission, and also can create your own map.";
            }
            else if(stage == 5)
            {
                textDone = true;
                stage++;
            }
            else if (stage == 9)
            {
                textShowTutorial = "";
                fullText = "";
                textDone = true;
                stage++;
            }
            else
            {
                fullText = "Number " + stage;
            }
        }
        
    }

    IEnumerator Show()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            curreentLetter = fullText.Substring(0, i);
            this.GetComponent<Text>().text = curreentLetter;
            playAudio.PlayOneShot(soundLetter,volume);
            yield return new WaitForSeconds(delay);
        }
    }
}
