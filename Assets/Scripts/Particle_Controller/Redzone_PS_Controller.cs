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
	public GameObject red1; // Sprite for checking
	public GameObject lavaCollider; // Collider for damage calculation

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
		red1.SetActive(false);
		lavaCollider.SetActive(false);
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
		red1.SetActive(true);
        yield return new WaitForSeconds(delayWarning);
        lightPoint.GetComponent<Light>().enabled = true;
        pre_lava.Play();
        yield return new WaitForSeconds(delayB4Boom);
        lava.Play();
		lavaCollider.SetActive(true);
		smokeBig.Play();
        yield return new WaitForSeconds(1f);
        lava.Stop();
		lavaCollider.SetActive(false);
		yield return new WaitForSeconds(0.3f);
        lightPoint.GetComponent<Light>().enabled = false;
        pre_lava.Stop();
        ren.sharedMaterial = floorMat_default;
        showing = false;
		red1.SetActive(false);
        //yield return new WaitForSeconds(1f); // For show
        //redZoneTrigger = true; // For show
    }
}
