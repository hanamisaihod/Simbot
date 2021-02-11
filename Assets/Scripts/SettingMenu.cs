using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingMenu : MonoBehaviour
{
	public bool showing = false;
	public GameObject blockPanel;
	public Color tempColor;
	public float showDelay = 0.35f;
	private GameObject[] blocks;
	public GameObject palette;
	public Slider bgmSlider;
	public Slider robotSlider;
	public Slider sfxSlider;
    public GameObject mainCamera;
    public GameObject mixer;
	public float[] initVolumes = {0,0,0};
    
    public void ShowSetting()
	{
		initVolumes[0] = bgmSlider.value;
		initVolumes[1] = robotSlider.value;
		initVolumes[2] = sfxSlider.value;
		if (palette)
		{
			if (!(palette.GetComponent<Palette>().moving))
            {
                if (mainCamera)
                {
                    mainCamera.GetComponent<CameraHandler>().available = false;
                }
                blocks = null;
				FindBlocks();
				HideBlocks();
				blockPanel.gameObject.SetActive(true);
				blockPanel.GetComponent<BlockPanel>().OpenBlockPanel();
				transform.localScale = new Vector3(0, 0, 0);
				gameObject.SetActive(true);
				palette.GetComponent<Palette>().MoveForSetting(showDelay);
				LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), showDelay)
					.setEaseOutBack();
				StartCoroutine(waitForOpen());
			}
		}
		else
        {
            if (mainCamera)
            {
                mainCamera.GetComponent<CameraHandler>().available = true;
            }
            blocks = null;
			FindBlocks();
			HideBlocks();
			blockPanel.gameObject.SetActive(true);
			blockPanel.GetComponent<BlockPanel>().OpenBlockPanel();
			transform.localScale = new Vector3(0, 0, 0);
			gameObject.SetActive(true);
			LeanTween.scale(gameObject, new Vector3(1f, 1f, 1f), showDelay)
				.setEaseOutBack();
			StartCoroutine(waitForOpen());
		}
	}

	public void CloseSetting(bool apply)
	{
		if (!apply)
		{
			bgmSlider.value = initVolumes[0];
			robotSlider.value = initVolumes[1];
			sfxSlider.value = initVolumes[2];
		}
        else
        {
            mixer.GetComponent<MixerController>().SaveAudioSetting();
        }
		if (showing)
		{
			if (palette)
			{
				palette.GetComponent<Palette>().MoveForSetting(showDelay);
			}
			LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), showDelay)
				.setEaseInBack();
			showing = false;
			StartCoroutine(waitForClose());
		}
	}

	public void setNotActive()
	{
		gameObject.SetActive(false);
		blockPanel.gameObject.SetActive(false);
		ShowBlocks();
	}

	IEnumerator waitForClose()
	{
		blockPanel.GetComponent<BlockPanel>().CloseBlockPanel();
		yield return new WaitForSeconds(showDelay);
		setNotActive();
	}
	IEnumerator waitForOpen()
	{
		yield return new WaitForSeconds(showDelay);
		showing = true;
	}

	public void FindBlocks()
	{
		GameObject[] startBlock = GameObject.FindGameObjectsWithTag("StartBlock");
		GameObject[] doBlock = GameObject.FindGameObjectsWithTag("DoBlock");
		GameObject[] ifBlocks = GameObject.FindGameObjectsWithTag("IfBlock");
		GameObject[] repBlocks = GameObject.FindGameObjectsWithTag("RepeatBlock");
		blocks = startBlock.Concat(doBlock).Concat(ifBlocks).Concat(repBlocks).ToArray();
	}

	public void HideBlocks()
	{
		foreach(GameObject block in blocks)
		{
			block.SetActive(false);
		}
	}

	public void ShowBlocks()
	{
		foreach (GameObject block in blocks)
		{
			block.SetActive(true);
		}
	}
}
