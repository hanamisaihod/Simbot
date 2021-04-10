using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_sound : MonoBehaviour
{
    AudioSource Audio;
    public AudioClip sound;

    void Start()
    {
        Audio = gameObject.GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        Audio.Play();
    }

}
