using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Called_Button_sound : MonoBehaviour
{
    AudioSource Audio;
    public float volume = 0.5f;     // volume of sound effect
    private GameObject[] sameTag;
    private bool NotFirst = false;

    void Start()
    {
        sameTag = GameObject.FindGameObjectsWithTag("ButtonSound");

        foreach (GameObject member in sameTag)
        {
            if (member.scene.buildIndex == -1)
                NotFirst = true;
        }
        if (NotFirst == true)
            Destroy(gameObject);
        DontDestroyOnLoad(transform.gameObject);
        Audio = gameObject.GetComponent<AudioSource>();
    }

    public void CallSound()
    {
        Audio.volume = volume;      // volume of sound effect
        Audio.Play();
    }
}
