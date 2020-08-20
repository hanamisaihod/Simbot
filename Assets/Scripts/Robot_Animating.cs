using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Animating : MonoBehaviour
{
    public Animator anim;
    public GameObject robotParent;
    
    void Start()
    {

    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            //anim.SetTrigger("Rotate");
            StartCoroutine(RotatingRight());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //anim.SetTrigger("Rotate");
            StartCoroutine(RotatingRightLeft());
        }
        
    }

    IEnumerator RotatingRight()
    {
        //Debug.Log("robotParent: "+ robotParent.name);
        LeanTween.rotateAround(robotParent, Vector3.up, 90, 0.67f).setEaseInOutBack();
        yield return new WaitForSeconds(0.67f);
    }

        IEnumerator RotatingRightLeft()
    {
        //Debug.Log("robotParent: "+ robotParent.name);
        LeanTween.rotateAround(robotParent, Vector3.up, -90, 0.67f).setEaseInOutBack();
        yield return new WaitForSeconds(0.67f);
    }
}
