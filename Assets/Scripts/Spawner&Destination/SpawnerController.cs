using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Material BaseMat_default;
    public Material BaseMat_taken;

    public GameObject Base;
    private Renderer BaseRen;
    public GameObject Core;
    private Renderer CoreRen;
    public ParticleSystem AuraFX;
    public bool ObjectTaken = false;

    void Start()
    {
        BaseRen = Base.GetComponent<Renderer>();
        BaseRen.sharedMaterial = BaseMat_default;
        Core.SetActive(true);
        AuraFX.Play();
    }

    
    void Update()
    {
        if (ObjectTaken)
        {
            AuraFX.Stop();
            Core.SetActive(false); // Hide the cube
            BaseRen.sharedMaterial = BaseMat_taken;
            ObjectTaken = false;
        }
      
    }
}
