using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Material floorMat_default;
    public Material floorMat_taken;

    public GameObject Base;
    private Renderer BaseRen;
    public GameObject Core;
    private Renderer CoreRen;
    public ParticleSystem AuraFX;
    public bool ObjectTaken = false;

    void Start()
    {
        Core.SetActive(true);
        AuraFX.Play();
    }

    
    void Update()
    {
        if (ObjectTaken)
        {
            AuraFX.Stop();
            Core.SetActive(false); // Hide the cube
            ObjectTaken = false;
        }
      
    }
}
