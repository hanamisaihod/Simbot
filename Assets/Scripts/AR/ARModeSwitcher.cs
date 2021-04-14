using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARModeSwitcher : MonoBehaviour
{
    public ARPlacementIndicator placementIndicator;
	public ARSceneController sceneController;
	public GameObject levelController;
	public GameObject lookAroundText;
	public GameObject simulateButton;
    public GameObject hpSet;
    public GameObject retryButton;
	public GameObject confirmButton;
	public GameObject applyButton;
	public GameObject rotateMap;
	public GameObject boss1, boss2;
	public GameObject map1tutorial;
	// There will be three modes in AR
	//1. Map placement 2. Map confirmation and adjustment 3. simulate

	private void Start()
	{
		hpSet.SetActive(false);
		simulateButton.SetActive(false);
		confirmButton.SetActive(false);
		rotateMap.SetActive(false);
		applyButton.SetActive(false);
		boss1.GetComponent<Tutorial_Activator>().DeactivateObject();
		boss2.GetComponent<Tutorial_Activator>().DeactivateObject();
		map1tutorial.GetComponent<Tutorial_Activator>().DeactivateObject();
		boss1.SetActive(false);
		boss2.SetActive(false);
		map1tutorial.SetActive(false);
		sceneController.AdjustParticleSize();
		sceneController.AdjustLightRange();
	}

	public void MapPlacementToConfirmation() //When the player press apply button
	{
		placementIndicator.placed = true;
		placementIndicator.placementVisual.gameObject.SetActive(false);
		applyButton.SetActive(false);
		rotateMap.SetActive(true);
		confirmButton.SetActive(true);
		if (map1tutorial.GetComponent<Tutorial_Activator>().thisIsActive)
		{
			map1tutorial.SetActive(true);
		}
		if (boss2.GetComponent<Tutorial_Activator>().thisIsActive)
		{
			boss2.SetActive(true);
		}
		sceneController.AdjustParticleSize();
		sceneController.AdjustLightRange();
	}

    public void ConfirmationToSimulate() //When the player press confirm button
	{
		if (!levelController.activeInHierarchy)
		{
			levelController.SetActive(true);
		}
		rotateMap.SetActive(false);
		confirmButton.SetActive(false);
		hpSet.SetActive(true);
		simulateButton.SetActive(true);
		if (boss1.GetComponent<Tutorial_Activator>().thisIsActive)
		{
			StartCoroutine(Boss1SpawnDelay(5));
		}
		sceneController.AdjustParticleSize();
		sceneController.AdjustLightRange();
	}

	public void AssignBossesAsChildren(GameObject map)
	{
		boss1.transform.parent = map.transform;
		boss2.transform.parent = map.transform;
	}

	IEnumerator Boss1SpawnDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		boss1.SetActive(true);
	}
}
