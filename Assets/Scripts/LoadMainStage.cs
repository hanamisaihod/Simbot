using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class LoadMainStage : MonoBehaviour
{
    public static string savePrefabKeyword;
    public static string mainStageKey;
    public StarRating starRating;
    public GameObject enableFrame;
    public void Start()
    {
        enableFrame.transform.localScale = new Vector3(0,0,0);
        Debug.Log("Start");
        ShowLoadMainStage();
    }
    public string[] savePrefab;

    public void GetSavePrefab()
    {
        if(!Directory.Exists(Application.dataPath+"/Prefabs/Map"))
        {
            Debug.Log("Error");
        }
        savePrefab = Directory.GetFiles(Application.dataPath + "/Resources/Map","*.prefab");
        Array.Sort(savePrefab, new AlphanumComparatorFast());
        Debug.Log(savePrefab);
    }

    public void ShowLoadMainStage()
    {
        GetSavePrefab();
        for (int i = 0; i < savePrefab.Length; i++)
        {
            GameObject buttonPrefabs = Instantiate(Resources.Load("ButtonPrefab", typeof(GameObject))) as GameObject;
            Debug.Log(savePrefab[i]);
            buttonPrefabs.GetComponentInChildren<Text>().text = savePrefab[i];
            buttonPrefabs.GetComponentInChildren<Text>().text = buttonPrefabs.GetComponentInChildren<Text>().text.Replace(@"\","/");
            string[] savePrefabPath = buttonPrefabs.GetComponentInChildren<Text>().text.Split("/"[0]);
            for (int j = 0; j < savePrefabPath.Length; j++)
            {
                Debug.Log(savePrefabPath[j] + "ORDERRRRRRRRRRR");
                if(j == savePrefabPath.Length - 1)
                {
                    savePrefabKeyword = savePrefabPath[j];
                }
            }
            savePrefabKeyword = savePrefabKeyword.Replace(".prefab", "");
            buttonPrefabs.GetComponentInChildren<Text>().text = savePrefabKeyword;
            //buttonObject.GetComponentInChildren<Text>().text = buttonObject.GetComponentInChildren<Text>().text.Replace("C:/Users/asus/AppData/LocalLow/DefaultCompany/MyFirstGame/saves/", "");
            //buttonObject.GetComponentInChildren<Text>().text = buttonObject.GetComponentInChildren<Text>().text.Replace(".txt", "");
            GameObject Container = GameObject.Find("LocalArea");
            buttonPrefabs.transform.SetParent(Container.transform,false);
            buttonPrefabs.transform.localPosition = Vector3.zero;
            buttonPrefabs.transform.localScale = Vector3.one;
            int index = i;
            
            buttonPrefabs.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(mainStageClick(index)));
        }
    }

    public IEnumerator mainStageClick(int mainIndex)
    {
        LeanTween.scale(enableFrame,new Vector3(1,1,1),0.5f);
        starRating.GetComponent<StarRating>().setCondition();
        LoadConfirm.waitForSelectSlot = true;
        yield return new WaitUntil(() => LoadConfirm.clickToLoad == true || DeleteSave.clickToDelete == true || ChangeToSimulate.simulate == true);

        string mainWord = savePrefab[mainIndex];
        mainWord = mainWord.Replace(@"\","/");
        string[] mainWordSpit = mainWord.Split("/"[0]);
        for (int i = 0; i < mainWordSpit.Length; i++)
        {
            if(i == mainWordSpit.Length - 1)
                {
                    mainStageKey = mainWordSpit[i];
                }
        }
        mainStageKey = mainStageKey.Replace(".prefab",""); // mainStageKey is the confirmed main stage name
        LoadConfirm.clickToLoad = false;
        LoadConfirm.waitForSelectSlot = false;
        LeanTween.scale(enableFrame,new Vector3(0,0,0),0.5f);
        SceneManager.LoadScene("MapBuilding");
    }
}
