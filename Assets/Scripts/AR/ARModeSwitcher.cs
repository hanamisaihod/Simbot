using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARModeSwitcher : MonoBehaviour
{
    public ARPlacementIndicator placementIndicator;
	public ARSceneController sceneController;
	public GameObject levelController;
	public GameObject simulateButton;
    public GameObject hpSet;
    public GameObject retryButton;
	public GameObject confirmButton;
	public GameObject applyButton;
	public GameObject rotateMap;
	// There will be three modes in AR
	//1. Map placement 2. Map confirmation and adjustment 3. simulate

	private void Start()
	{
		hpSet.SetActive(false);
		retryButton.SetActive(false);
		simulateButton.SetActive(false);
		confirmButton.SetActive(false);
		rotateMap.SetActive(false);
		applyButton.SetActive(false);
	}

	public void MapPlacementToConfirmation() //When the player press apply button
	{
		placementIndicator.placed = true;
		placementIndicator.placementVisual.gameObject.SetActive(false);
		applyButton.SetActive(false);
		rotateMap.SetActive(true);
		confirmButton.SetActive(true);
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
		retryButton.SetActive(true);
		simulateButton.SetActive(true);
	}
}
