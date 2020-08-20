using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour
{
    public GameObject tutorialCanvas;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject logo;
    public static bool startTutorial;
    public Button infoButton;

    void Start()
    {
        StartCoroutine(SceneShow());
        infoButton.GetComponent<Button>().onClick.AddListener(TutorialTrigger);
        tutorialCanvas.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (startTutorial && Menu_Tutorial.startText == false)
        {
            startTutorial = false;
            tutorialCanvas.GetComponent<Canvas>().enabled = true;
            Menu_Tutorial.MenuTutorialTrigger = true;
        }
        if(Menu_Tutorial.startText)
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = false;
        }
    }

    void TutorialTrigger()
    {
        if(!Menu_Tutorial.startText) startTutorial = true;
    }

    IEnumerator SceneShow()
    {
        LeanTween.moveLocalX(cloud1, cloud1.transform.localPosition.x - 50, 4f).setLoopPingPong().setEaseInOutSine();
        LeanTween.moveLocalX(cloud2, cloud2.transform.localPosition.x + 50, 4f).setLoopPingPong().setEaseInOutSine().setDelay(1f);
        yield return new WaitForSeconds(0);
    }
}
