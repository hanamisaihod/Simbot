﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX_Controller : MonoBehaviour
{
    public bool spawnTrigger;
    public bool clearBot; // Remove this
    public GameObject blueLightCube;
    public GameObject whiteLightCube;
    public GameObject lightPoint;
    public ParticleSystem StartFX;

    private Vector3 blueInitial;
    private Vector3 whiteInitial;
    private bool showing;
    private float waitUntil;
    Coroutine usingCor;

    public GameObject turtlePrefab;
	public GameObject stovePrefab;
    public GameObject spawnedRobot;
	void Start()
    {
        blueLightCube.SetActive(true);
        whiteLightCube.SetActive(true);
        lightPoint.SetActive(true);
        blueLightCube.GetComponent<MeshRenderer>().enabled = false;
        whiteLightCube.GetComponent<MeshRenderer>().enabled = false;
        lightPoint.GetComponent<Light>().enabled = false;

        blueInitial = blueLightCube.transform.localScale;
        whiteInitial = whiteLightCube.transform.localScale;
		//StartShowing();

    }

 //    void Update()
 //   {

	//	if (spawnTrigger && !showing)
	//	{
	//		if (usingCor != null)
	//		{
	//			StopCoroutine(usingCor);
	//		}
	//		usingCor = StartCoroutine(Show());
	//		spawnTrigger = false;
	//		showing = true;
	//		waitUntil = Time.time + 1f;
	//	}
	//	else
	//		spawnTrigger = false;

	//	if (showing && Time.time > waitUntil)
	//	{
	//		if (usingCor != null)
	//		{
	//			StopCoroutine(usingCor);
	//		}
	//		usingCor = StartCoroutine(Clear());
	//		showing = false;
	//	}
	//}

	public void StartShowing()
	{
		StartCoroutine(Show());
	}

    IEnumerator Show()
    {
        blueLightCube.GetComponent<MeshRenderer>().enabled = true;
        whiteLightCube.GetComponent<MeshRenderer>().enabled = true;
        lightPoint.GetComponent<Light>().enabled = true;
        StartFX.Play();
        LeanTween.scale(blueLightCube, new Vector3(blueLightCube.transform.localScale.x * 10, blueLightCube.transform.localScale.y * 10, blueLightCube.transform.localScale.z), 0.7f).setEaseInOutBack();
        LeanTween.scale(whiteLightCube, new Vector3(whiteLightCube.transform.localScale.x * 9, whiteLightCube.transform.localScale.y * 9, whiteLightCube.transform.localScale.z), 0.7f).setEaseInOutBack().setDelay(0.1f);
        yield return new WaitForSeconds(0.7f);

        yield return new WaitForSeconds(0.2f);

        blueLightCube.GetComponent<MeshRenderer>().enabled = false;
        whiteLightCube.GetComponent<MeshRenderer>().enabled = false;
        lightPoint.GetComponent<Light>().enabled = false;

        //bot.SetActive(true);    // Spawn robot here

        yield return new WaitForSeconds(0.1f);
		StartCoroutine(WaitBeforeClear(1.0f));
    }

    IEnumerator Clear()
    {
        LeanTween.scale(blueLightCube, blueInitial, 0.1f);
        LeanTween.scale(whiteLightCube, whiteInitial, 0.1f);
        yield return new WaitForSeconds(0.1f);
    }

	IEnumerator WaitBeforeClear(float delay)
	{
		yield return new WaitForSeconds(delay);
		StartCoroutine(Clear());
	}

    public void SpawnRobot(int botType, bool ar)
    {
        if (!ar)
        {
            if (botType == 0)
            {
                spawnedRobot = Instantiate(turtlePrefab);
                spawnedRobot.transform.position = transform.position + new Vector3(0, 0.3f, 0);
                spawnedRobot.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            else if (botType == 1) // check static variable for corresponding robot of current stage
            {
                spawnedRobot = Instantiate(stovePrefab);
                spawnedRobot.transform.position = transform.position + new Vector3(0, 0.17f, 0);
                spawnedRobot.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            else
            {

            }
        }
		else
		{
            Debug.Log("Spawning the robot in AR");
            if (botType == 0)
            {
                spawnedRobot = Instantiate(turtlePrefab, new Vector3(999,999,999), new Quaternion());
                spawnedRobot.transform.localScale *= 0.02f;
                spawnedRobot.transform.position = transform.position + new Vector3(0, 0.3f * 0.02f, 0);
                spawnedRobot.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            else if (botType == 1) // check static variable for corresponding robot of current stage
            {
                spawnedRobot = Instantiate(stovePrefab, new Vector3(999, 999, 999), new Quaternion());
                spawnedRobot.transform.localScale *= 0.02f;
                spawnedRobot.transform.position = transform.position + new Vector3(0, 0.17f * 0.02f, 0);
                spawnedRobot.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            else
            {

            }
        }
    }
}
