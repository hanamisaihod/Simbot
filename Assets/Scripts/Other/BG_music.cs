using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BG_music : MonoBehaviour
{
    AudioSource Audio;
    public float volume = 0.5f;     // volume of BG music
    private GameObject[] sameTag;
    private bool NotFirst = false;
    public AudioClip Music1;
    public AudioClip Music2;

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
        Scene scene = SceneManager.GetActiveScene();
        if (PlayerPrefs.HasKey("Menu_Tutorial"))
        {
            if (PlayerPrefs.GetInt("Menu_Tutorial") == 1)
            {
                if(!Audio.isPlaying)
                    Audio.Play();
            }
            if(scene.name == "TestRobotMovementScene" || scene.name == "ARTest")
            {
                Audio.clip = Music2;
                Audio.volume = 1;
            }
            else
            {
                Audio.clip = Music1;
                Audio.volume = volume;
            }
        }
    }

}
