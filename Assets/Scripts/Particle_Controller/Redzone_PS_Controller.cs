using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redzone_PS_Controller : MonoBehaviour
{
    public Material floorMat_default;
    public Material floorMat_warning;
    private Renderer ren;
    public GameObject lightPoint;
    public bool redZoneTrigger;
    
    private bool showing;
    public float delayWarning;
    public float delayB4Boom;
    public ParticleSystem smokeSmall;
    public ParticleSystem smokeBig;
    public ParticleSystem pre_lava;
    public ParticleSystem lava;
    Coroutine usingCor;

    void Start()
    {
        ren = GetComponent<Renderer>();
        ren.enabled = true;
        ren.sharedMaterial = floorMat_default;
        lightPoint.GetComponent<Light>().enabled = false;
        smokeSmall.Play();
    }

    void Update()
    {
        if(redZoneTrigger && !showing)
        {
            if (usingCor != null)
            {
                StopCoroutine(usingCor);
            }
            usingCor = StartCoroutine(Show());
            showing = true;
            redZoneTrigger = false;
        }
        else
            redZoneTrigger = false;
    }

    IEnumerator Show()
    {
        ren.sharedMaterial = floorMat_warning;
        yield return new WaitForSeconds(delayWarning);
        lightPoint.GetComponent<Light>().enabled = true;
        pre_lava.Play();
        yield return new WaitForSeconds(delayB4Boom);
        lava.Play();
        smokeBig.Play();
        yield return new WaitForSeconds(1f);
        lava.Stop();
        yield return new WaitForSeconds(0.3f);
        lightPoint.GetComponent<Light>().enabled = false;
        pre_lava.Stop();
        ren.sharedMaterial = floorMat_default;
        showing = false;

        //yield return new WaitForSeconds(1f); // For show
        //redZoneTrigger = true; // For show
    }
}
