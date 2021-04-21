using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFX_Controller : MonoBehaviour
{
    
    public bool goalTrigger;
    public bool clearTrigger;
    public bool unlock;
    public bool lockGoal;
    private bool locker;
    private bool showing;
    public ParticleSystem PS_C1;
    public ParticleSystem PS_C2;
    public ParticleSystem PS_C3;
    public ParticleSystem PS_C4;
    public ParticleSystem PS_Center;
    public GameObject lightPoint;
    public GameObject redLightCube;
    public GameObject whiteLightCube;
    Coroutine usingCor;

    private Vector3 redInitial;
    private Vector3 whiteInitial;

    void Awake()
    {
        lightPoint.GetComponent<Light>().enabled = false;
        redLightCube.SetActive(true);
        whiteLightCube.SetActive(true);
        lightPoint.SetActive(true);
        redLightCube.GetComponent<MeshRenderer>().enabled = false;
        whiteLightCube.GetComponent<MeshRenderer>().enabled = false;
		redLightCube.GetComponent<BoxCollider>().enabled = false;
		redInitial = redLightCube.transform.localScale;
        whiteInitial = whiteLightCube.transform.localScale;
    }

    
    //void Update()
    //{
    //    if (lockGoal && !locker)
    //    {
    //        if (usingCor != null)
    //        {
    //            StopCoroutine(usingCor);
    //        }
    //        usingCor = StartCoroutine(Lock());
    //        lockGoal = false;
    //        locker = true;
    //    }
    //    else
    //        lockGoal = false;

    //    if (unlock && locker)
    //    {
    //        if (usingCor != null)
    //        {
    //            StopCoroutine(usingCor);
    //        }
    //        usingCor = StartCoroutine(Unlock());
    //        unlock = false;
    //        locker = false;
    //    }
    //    else
    //        unlock = false;

    //    if (goalTrigger && !showing)
    //    {
    //        PS_C1.Play();
    //        PS_C2.Play();
    //        PS_C3.Play();
    //        PS_C4.Play();
    //        PS_Center.Play();
    //        lightPoint.GetComponent<Light>().enabled = true;
    //        showing = true;
    //    }
    //    else
    //        goalTrigger = false;

    //    if (showing && clearTrigger)
    //    {
    //        PS_C1.Stop();
    //        PS_C2.Stop();
    //        PS_C3.Stop();
    //        PS_C4.Stop();
    //        PS_Center.Stop();
    //        lightPoint.GetComponent<Light>().enabled = false;
    //        showing = false;
    //    }
    //    else
    //        clearTrigger = false;
    //}

    IEnumerator Lock()
    {
		Debug.Log("Locking");
        //redLightCube.GetComponent<MeshRenderer>().enabled = true;
        //whiteLightCube.GetComponent<MeshRenderer>().enabled = true;
		LeanTween.scale(redLightCube, new Vector3(redLightCube.transform.localScale.x * 10, redLightCube.transform.localScale.y * 10, redLightCube.transform.localScale.z), 0.7f).setEaseInOutBack();
        LeanTween.scale(whiteLightCube, new Vector3(whiteLightCube.transform.localScale.x * 9, whiteLightCube.transform.localScale.y * 9, whiteLightCube.transform.localScale.z), 0.7f).setEaseInOutBack().setDelay(0.1f);
        yield return new WaitForSeconds(0.9f);
    }
    IEnumerator Unlock()
    {
        LeanTween.scale(redLightCube,redInitial, 0.7f).setEaseInOutBack();
        LeanTween.scale(whiteLightCube,whiteInitial, 0.7f).setEaseInOutBack().setDelay(0.1f);
        yield return new WaitForSeconds(0.9f);
        redLightCube.GetComponent<MeshRenderer>().enabled = false;
		redLightCube.GetComponent<BoxCollider>().enabled = false;
        whiteLightCube.GetComponent<MeshRenderer>().enabled = false;
    }

	public void StartLockGoal()
	{
		redLightCube.GetComponent<MeshRenderer>().enabled = true;
		whiteLightCube.GetComponent<MeshRenderer>().enabled = true;
		redLightCube.GetComponent<BoxCollider>().enabled = true;
		StartCoroutine(Lock());
	}

	public void StartUnlockGoal()
	{
		StartCoroutine(Unlock());
	}

	public void StartTrigger()
	{
		PS_C1.Play();
		PS_C2.Play();
		PS_C3.Play();
		PS_C4.Play();
		PS_Center.Play();
		lightPoint.GetComponent<Light>().enabled = true;
	}

}
