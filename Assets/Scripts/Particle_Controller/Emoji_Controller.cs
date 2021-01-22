using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Rompruk
public class Emoji_Controller : MonoBehaviour
{
    public ParticleSystem Ehh;
    public ParticleSystem Confuse1;
    public ParticleSystem Confuse2;
    public ParticleSystem Confuse3;
    public ParticleSystem Drop1;
    public ParticleSystem Drop2;
    public bool EhhTrigger;
    public bool ConfuseTrigger;
    public bool CryTrigger;

    void Start()
    {
        
    }

    void Update()
    {
        if (EhhTrigger && !Ehh.isPlaying)
        {
            Ehh.Play();
        }
        else
            EhhTrigger = false;
        if (ConfuseTrigger && !Confuse1.isPlaying)
        {
            Confuse1.Play();
            Confuse2.Play();
            Confuse3.Play();
        }
        else
            ConfuseTrigger = false;
        if(CryTrigger && !Drop1.isPlaying)
        {
            Drop1.Play();
            Drop2.Play();
        }
        else if(!CryTrigger)
        {
            Drop1.Stop();
            Drop2.Stop();
        }
        
    }
}
