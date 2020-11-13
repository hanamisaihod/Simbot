using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFX_Controller : MonoBehaviour
{
    public bool goalTrigger;
    public bool clearTrigger;
    private bool showing;
    public ParticleSystem PS_C1;
    public ParticleSystem PS_C2;
    public ParticleSystem PS_C3;
    public ParticleSystem PS_C4;
    public ParticleSystem PS_Center;
    public GameObject lightPoint;

    void Start()
    {
        lightPoint.GetComponent<Light>().enabled = false;
    }

    
    void Update()
    {
        if (goalTrigger && !showing)
        {
            PS_C1.Play();
            PS_C2.Play();
            PS_C3.Play();
            PS_C4.Play();
            PS_Center.Play();
            lightPoint.GetComponent<Light>().enabled = true;
            showing = true;
        }
        else
            goalTrigger = false;
        if (showing && clearTrigger)
        {
            PS_C1.Stop();
            PS_C2.Stop();
            PS_C3.Stop();
            PS_C4.Stop();
            PS_Center.Stop();
            lightPoint.GetComponent<Light>().enabled = false;
            showing = false;
        }
        else
            clearTrigger = false;
    }
}
