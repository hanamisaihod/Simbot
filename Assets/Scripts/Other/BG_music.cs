using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_music : MonoBehaviour
{
    AudioSource Audio;
    public float volume = 0.5f;     // volume of BG music
    private GameObject[] sameTag;
    private bool NotFirst = false;

    void Start()
    {
        sameTag = GameObject.FindGameObjectsWithTag("BG_Music");

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

    private void Update()
    {
        Audio.volume = volume;      // volume of BG music
        if (PlayerPrefs.HasKey("Menu_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Menu_Tutorial") == 1)
            {
                if(!Audio.isPlaying)
                    Audio.Play();
            }
        }
    }

}
