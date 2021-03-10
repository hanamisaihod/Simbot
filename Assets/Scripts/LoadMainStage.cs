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
    public GameObject invisibleFrame;
    public static string currentKeyword; 
    public string[] savePrefab;
    public object[] presavePrefab;
    public static List<GameObject> buttonArray = new List<GameObject>();
    public void Start()
    {
        enableFrame.transform.localScale = new Vector3(0,0,0);
        Debug.Log("Start");
        ShowLoadMainStage();
    }
   

    public void GetSavePrefab()
    {
        presavePrefab = Resources.LoadAll("Map");
        savePrefab = new string[presavePrefab.Length];
        for (int a = 0; a < presavePrefab.Length; a++)
        {
            string storeWord = presavePrefab[a].ToString().Replace(@" (UnityEngine.GameObject)","");
            //Debug.Log(storeWord);
            savePrefab[a] = storeWord;
        }
        Array.Sort(savePrefab, new AlphanumComparatorFast());
        
    }

    public void ShowLoadMainStage()
    {
        GetSavePrefab();
        EnviSim.Mode = "Main";
        for (int i = 0; i < savePrefab.Length; i++)
        {
            GameObject buttonPrefabs = Instantiate(Resources.Load("ButtonPrefab", typeof(GameObject))) as GameObject;
            //Debug.Log(savePrefab[i]);
            buttonPrefabs.GetComponentInChildren<Text>().text = savePrefab[i].ToString();
            buttonPrefabs.GetComponentInChildren<Text>().text = buttonPrefabs.GetComponentInChildren<Text>().text.Replace(@"\","/");
            buttonPrefabs.GetComponentInChildren<Text>().text = buttonPrefabs.GetComponentInChildren<Text>().text.Replace(@" (UnityEngine.GameObject)","");
            string[] savePrefabPath = buttonPrefabs.GetComponentInChildren<Text>().text.Split("/"[0]);
            for (int j = 0; j < savePrefabPath.Length; j++)
            {
                //Debug.Log(savePrefabPath[j] + "ORDERRRRRRRRRRR");
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
            buttonArray.Add(buttonPrefabs);
            int index = i;
            
            buttonPrefabs.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(mainStageClick(index)));
        }
        foreach (GameObject item in buttonArray)
        {
            //Debug.Log("Name: " + item.GetComponentInChildren<Text>().text); 
            starRating.GetComponent<StarRating>().setDirectory();
            //starRating.GetComponent<StarRating>().checkStar();
        }
    }

    public IEnumerator mainStageClick(int mainIndex)
    {
        //Debug.Log("CLICKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK" + mainIndex);
        LeanTween.scale(enableFrame,new Vector3(1,1,1),0.5f);
        invisibleFrame.SetActive(true);        
        string currentPreWord = savePrefab[mainIndex].ToString();
        currentPreWord = currentPreWord.Replace(@"\","/");
        string[] currentWordSpit = currentPreWord.Split("/"[0]);
        for (int i = 0; i < currentWordSpit.Length; i++)
        {
            if(i == currentWordSpit.Length - 1)
                {
                    currentKeyword = currentWordSpit[i];
                }
        }
        currentKeyword = currentKeyword.Replace(".prefab","");
        Debug.Log(currentKeyword);
        starRating.GetComponent<StarRating>().setCondition();
        

        LoadConfirm.waitForSelectSlot = true;
        

        yield return new WaitUntil(() => LoadConfirm.clickToLoad == true || DeleteSave.clickToDelete == true || ChangeToSimulate.simulate == true);

        string mainWord = savePrefab[mainIndex].ToString();
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
        if (GameObject.FindGameObjectWithTag("VariableCarrier"))
        {
            GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName = mainStageKey;
        }
        LoadConfirm.clickToLoad = false;
        LoadConfirm.waitForSelectSlot = false;
        LeanTween.scale(enableFrame,new Vector3(0,0,0),0.5f);
        //Debug.Log("GO GO GO GO GO");
        SceneManager.LoadScene("SelectRobot");
    }
}
