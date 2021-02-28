using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFrame : MonoBehaviour
{
    public GameObject ratingFrame;
    public GameObject closeInvis;
    public void close()
    {
        LeanTween.scale(ratingFrame,new Vector3(0,0,0),0.5f);
        closeInvis.SetActive(false);
    }
}
