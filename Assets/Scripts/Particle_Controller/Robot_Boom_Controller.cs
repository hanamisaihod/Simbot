using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_Boom_Controller : MonoBehaviour
{
    public ParticleSystem smokeSmall;
    public ParticleSystem smokeMedium;
    public ParticleSystem smokeBig;
    public ParticleSystem fragment;
    public GameObject lightPoint;
    public bool level1Trigger;
    public bool level2Trigger;
    public bool boomTrigger;
    AudioSource Audio;
    public float volume = 0.3f;     // volume of sound effect

    private GameObject levelController;

	void Start()
    {
        smokeSmall.Stop();
        smokeMedium.Stop();
        smokeBig.Stop();
        lightPoint.GetComponent<Light>().enabled = false;
		levelController = GameObject.FindGameObjectWithTag("LevelController");
        Audio = gameObject.GetComponent<AudioSource>();
    }

    
    //void Update()
    //{
    //    if (level1Trigger)
    //    {
    //        smokeSmall.Play();
    //        level1Trigger = false;
    //    }
    //    else if (level2Trigger)
    //    {
    //        smokeSmall.Play();
    //        smokeMedium.Play();
    //        level2Trigger = false;
    //    }
    //    else if (boomTrigger)
    //    {
    //        smokeSmall.Play();
    //        smokeMedium.Play();
    //        smokeBig.Play();
    //        fragment.Play();
    //        lightPoint.GetComponent<Light>().enabled = true;
    //        boomTrigger = false;
    //    }
    //}

	public void UpdateBoomEffect(float currentHealth, float maxHealth)
	{
        Audio.volume = volume;      // volume of sound effect
        Debug.Log("Health" + currentHealth + "/" + maxHealth);
		if (currentHealth <= maxHealth * 66.0f / 100.0f && currentHealth >= maxHealth * 33.0f / 100.0f)
		{
            if (!smokeSmall.isPlaying)
            {
                smokeSmall.Play();
            }
		}
		else if (currentHealth <= maxHealth * 33.0f / 100.0f && currentHealth > 0)
        {
            if (!smokeSmall.isPlaying)
            {
                smokeSmall.Play();
            }
            if (!smokeMedium.isPlaying)
            {
                smokeMedium.Play();
            }
		}
		else if (currentHealth <= 0)
        {
            if (!smokeSmall.isPlaying)
            {
                smokeSmall.Play();
            }
            if (!smokeMedium.isPlaying)
            {
                smokeMedium.Play();
            }
            if (!smokeBig.isPlaying)
            {
                smokeBig.Play();
            }
            if (!fragment.isPlaying)
            {
                fragment.Play();
            }
			lightPoint.GetComponent<Light>().enabled = true;
            Audio.Play();
            levelController.GetComponent<LevelController>().FailMission();
		}
	}
}
