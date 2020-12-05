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

    void Start()
    {
        smokeSmall.Stop();
        smokeMedium.Stop();
        smokeBig.Stop();
        lightPoint.GetComponent<Light>().enabled = false;
    }

    
    void Update()
    {
        if (level1Trigger)
        {
            smokeSmall.Play();
            level1Trigger = false;
        }
        else if (level2Trigger)
        {
            smokeSmall.Play();
            smokeMedium.Play();
            level2Trigger = false;
        }
        else if (boomTrigger)
        {
            smokeSmall.Play();
            smokeMedium.Play();
            smokeBig.Play();
            fragment.Play();
            lightPoint.GetComponent<Light>().enabled = true;
            boomTrigger = false;
        }
  
    }
}
