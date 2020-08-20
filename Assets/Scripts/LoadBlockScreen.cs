using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadBlockScreen : MonoBehaviour
{
	public GameObject buttonPrefab;
	public static bool waitForSelectSlotBlock = false;
	public void Start()
    {
        ShowLoadScreen();
    }

    public void ShowLoadScreen()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("totalBlock"); i++)
        {
            GameObject buttonObject = Instantiate(buttonPrefab) as GameObject;
            buttonObject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString((i+1).ToString() + "name");
			GameObject Container = GameObject.Find("LocalArea");
            buttonObject.transform.SetParent(Container.transform,false);
            buttonObject.transform.localPosition = Vector3.zero;
            buttonObject.transform.localScale = Vector3.one;
            int buttonIndex = i+1;
            buttonObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OnButtonClick(buttonIndex)));
        }
    }

    public IEnumerator OnButtonClick(int index)
	{
		waitForSelectSlotBlock = true;
		yield return new WaitUntil(() => LoadConfirm.clickToLoad == true || DeleteSave.clickToDelete == true || ChangeToSimulate.simulate == true);
		if (LoadConfirm.clickToLoad == true)
		{
			ChangeScene.inputBlock = PlayerPrefs.GetString(index.ToString() + "name");
			LoadConfirm.clickToLoad = false;
			waitForSelectSlotBlock = false;
			if (SceneChanger.viewBlock)
			{
				SceneManager.LoadScene("BlockProgramming");
			}
			else
			{
				SceneManager.LoadScene("Simulate");
			}
		}
	}

	public void DeleteAllKey()
	{
		PlayerPrefs.DeleteAll();
	}
}
