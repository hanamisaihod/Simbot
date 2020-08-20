using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Clear : MonoBehaviour
{
    public GameObject white;
    public GameObject text;
    public GameObject BackFX;
    public GameObject LeftFX;
    public GameObject RightFX;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
 

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator ShowText()
    {
        var Rect = gameObject.GetComponent<RectTransform>();
        var whiteRect = white.GetComponent<RectTransform>();
        var textRect = text.GetComponent<RectTransform>();

        white.GetComponent<Image>().enabled = false;
        text.GetComponent<Image>().enabled = false;
        BackFX.SetActive(false);
        LeftFX.SetActive(false);
        RightFX.SetActive(false);
        gameObject.GetComponent<Image>().enabled = false;

        yield return new WaitForSeconds(4);

        gameObject.GetComponent<Image>().enabled = true;
        LeanTween.scale(Rect, Rect.localScale * 6f, 1f).setDelay(0.1f).setEaseInOutBack();
        LeanTween.rotateZ(gameObject, 3600, 1f);

        yield return new WaitForSeconds(1.1f);

        BackFX.SetActive(true);
        white.GetComponent<Image>().enabled = true;
        LeanTween.alpha(whiteRect, 0f, 1f).setDelay(0.1f);
        LeanTween.scale(Rect, Rect.localScale * 1.75f, 0.25f).setEaseInOutBack();

        yield return new WaitForSeconds(0.25f);

        LeftFX.SetActive(true);
        LeanTween.moveY(Rect, 15f, 1f).setLoopPingPong();

        yield return new WaitForSeconds(1f);

        RightFX.SetActive(true);

        yield return new WaitForSeconds(2f);

        text.GetComponent<Image>().enabled = true;
        LeanTween.scale(textRect, textRect.localScale * 8f, 0.75f).setDelay(0.1f).setEaseInOutBack();

    }
}
