using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Animating : MonoBehaviour
{
    public Animator anim;
    private int pull;
    private int push;
    //public static int forward;
    public GameObject catFace;
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelBL;
    public GameObject wheelBR;
    public Material faceDefault;
    public Material faceAh;

    void Start()
    {
        StartCoroutine(WheelRotating(wheelFL));
        StartCoroutine(WheelRotating(wheelFR));
        StartCoroutine(WheelRotating(wheelBL));
        StartCoroutine(WheelRotating(wheelBR));
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            anim.SetTrigger("Rotate");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            pull++;
            pull = pull % 2;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            push++;
            push = push % 2;
        }
        else if (PlayerPosition.isMovingForward == true)
        {
            anim.SetBool("Forward", true);
            catFace.GetComponent<MeshRenderer>().material = faceAh;
            //PlayerPosition.isMovingForward = false;
            //forward = forward % 2;
        }
        else if (pull > 0)
        {
            anim.SetBool("Pull", true);
            catFace.GetComponent<MeshRenderer>().material = faceAh;
        }
        else if (push > 0)
        {
            anim.SetBool("Push", true);
            catFace.GetComponent<MeshRenderer>().material = faceAh;
        }

        else if (PlayerPosition.isMovingForward == false)
        {
            anim.SetBool("Pull", false);
            anim.SetBool("Push", false);
            anim.SetBool("Forward", false);
            catFace.GetComponent<MeshRenderer>().material = faceDefault;
        }
    }
    IEnumerator WheelRotating(GameObject wheel)
    {
        LeanTween.rotateZ(wheel, wheel.transform.localRotation.z + 720, 0.5f).setLoopClamp();
        yield return new WaitForSeconds(0f);
    }
}
