using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public Material BaseMat_default;
    public Material BaseMat_active;
    public ParticleSystem AuraFX;
    public GameObject Base;
    private Renderer BaseRen; 
    public bool Received = false;

    void Start()
    {
        BaseRen = Base.GetComponent<Renderer>();
        BaseRen.sharedMaterial = BaseMat_default;
        AuraFX.Stop();
    }

    void Update()
    {
        if(Received)
        {
            AuraFX.Play();
            BaseRen.sharedMaterial = BaseMat_active;
            Received = false;
        }
    }
}
