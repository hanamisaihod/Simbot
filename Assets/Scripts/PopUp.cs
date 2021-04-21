using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public GameObject invis;
    void Start()
    {
        ChangeScene.subMode = "CreateNew";
        transform.localScale = new Vector3(0,0,0);
    }
    public void popOut()
    {
        LeanTween.scale(gameObject,new Vector3(1,1,1),0.5f);
        invis.SetActive(true);
    }

    public void close()
    {
        LeanTween.scale(gameObject,new Vector3(0,0,0),0.5f);
        invis.SetActive(false);
    }
}
