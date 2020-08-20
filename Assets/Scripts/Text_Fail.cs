using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Fail : MonoBehaviour
{
    public GameObject dark;
    public GameObject try_button;
    public GameObject next_button;
    public GameObject BackFX;
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
        var darkRect = dark.GetComponent<RectTransform>();
        var tryRect = try_button.GetComponent<RectTransform>();
        var nextRect = next_button.GetComponent<RectTransform>();


        dark.GetComponent<Image>().enabled = false;
        LeanTween.alpha(darkRect, 0f, 1f);
        try_button.GetComponent<Image>().enabled = false;
        next_button.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<Image>().enabled = false;
        BackFX.SetActive(false);

        yield return new WaitForSeconds(4);

        gameObject.GetComponent<Image>().enabled = true;
        dark.GetComponent<Image>().enabled = true;
        BackFX.SetActive(true);

        yield return new WaitForSeconds(0.75f);

        LeanTween.alpha(darkRect, 0.9f, 0.1f);
        LeanTween.moveY(Rect, 0f, 0.75f).setEaseOutElastic();
        LeanTween.moveY(Rect, 10f, 1f).setDelay(0.75f).setLoopPingPong();


        yield return new WaitForSeconds(2f);

        try_button.GetComponent<Image>().enabled = true;
        LeanTween.scale(tryRect, tryRect.localScale * 8f, 0.75f).setEaseInOutBack();
        next_button.GetComponent<Image>().enabled = true;
        LeanTween.scale(nextRect, nextRect.localScale * 8f, 0.75f).setDelay(0.2f).setEaseInOutBack();

    }
}
