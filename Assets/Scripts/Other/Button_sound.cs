using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_sound : MonoBehaviour
{
    public void OnClick()
    {
        GameObject.FindGameObjectWithTag("ButtonSound").GetComponent<Called_Button_sound>().CallSound();
    }

}
