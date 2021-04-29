using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits_Controller : MonoBehaviour
{
    public GameObject BG;
    public GameObject script;
    private Vector3 scriptPosition;

    public void StartCredits()
    {
        StartCoroutine(showCredits());
    }

    IEnumerator showCredits()
    {
        script.SetActive(true);
        scriptPosition = script.transform.localPosition;
        BG.SetActive(true);
        LeanTween.moveLocalY(script, script.transform.localPosition.y + 3600f, 32f);
        yield return new WaitForSeconds(32f);
        script.transform.localPosition = scriptPosition;
        script.SetActive(false);
        BG.SetActive(false);
    }

}
