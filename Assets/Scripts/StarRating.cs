using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class StarRating : MonoBehaviour
{
    public GameObject condition1;
    public GameObject condition2;
    public GameObject condition3;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject star1Appear;
    public GameObject star2Appear;
    public GameObject star3Appear;
    public string[] mapRating;
    public StarRatingData SRData;
    public List<StarRatingData> SREnv = new List<StarRatingData>();
    //public static List<StarRatingData> staticSREnv = new List<StarRatingData>();
    public void setCondition()
    {       
        //Debug.Log("EXISTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
        //mapRating = Directory.GetFiles(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating/" + LoadMainStage.savePrefabKeyword + ".txt");
        SREnv = SaveLoadSR.LoadSR<List<StarRatingData>>(LoadMainStage.currentKeyword);
        //staticSREnv = SREnv;
        foreach (StarRatingData item in SREnv)
        {
            foreach (string wordCon1 in item.mapCondition1)
            {
                //Debug.Log(wordCon1);
                condition1.GetComponent<Text>().text = wordCon1;
                if(item.mapStar1 == true)
                {
                    star1Appear.SetActive(true);
                    star1.SetActive(false);
                }
                if(item.mapStar1 == false)
                {
                    star1Appear.SetActive(false);
                    star1.SetActive(true);
                }
                
            }
            foreach (string wordCon2 in item.mapCondition2)
            {
                //Debug.Log(wordCon2);
                condition2.GetComponent<Text>().text = wordCon2;
                if(item.mapStar2 == true)
                {
                    star2Appear.SetActive(true);
                    star2.SetActive(false);
                }
                if(item.mapStar2 == false)
                {
                    star2Appear.SetActive(false);
                    star2.SetActive(true);
                }
            }
            foreach (string wordCon3 in item.mapCondition3)
            {
                //Debug.Log(wordCon3);
                condition3.GetComponent<Text>().text = wordCon3;
                if(item.mapStar3 == true)
                {
                    //Debug.Log("BUGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                    star3Appear.SetActive(true);
                    star3.SetActive(false);
                }
                if(item.mapStar3 == false)
                {
                    //Debug.Log("BUGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
                    star3Appear.SetActive(false);
                    star3.SetActive(true);
                }
            }
        }
    }

    public void setDirectory()
    {
        if(LoadMainStage.buttonArray != null)
        {
            foreach (GameObject item in LoadMainStage.buttonArray)
            {
                if(item != null)
                {
                    if(!Directory.Exists(Application.persistentDataPath + "/savesStage/" + item.GetComponentInChildren<Text>().text + "StarRating"))
                    {
                        Directory.CreateDirectory(Application.persistentDataPath + "/savesStage/" + item.GetComponentInChildren<Text>().text + "StarRating");
                        condition1.GetComponent<Text>().text = "Goal";
                        condition2.GetComponent<Text>().text = "Minimum line of code";
                        condition3.GetComponent<Text>().text = "Robot HP";

                        SRData.mapCondition1.Add(condition1.GetComponent<Text>().text);
                        SRData.mapCondition2.Add(condition2.GetComponent<Text>().text);
                        SRData.mapCondition3.Add(condition3.GetComponent<Text>().text);

                        
                        SREnv.Add(SRData);
                        foreach (StarRatingData SRitem in SREnv)
                        {
                            SRitem.mapStar1 = false;
                            SRitem.mapStar2 = false;
                            SRitem.mapStar3 = false;
                        }
                        Debug.Log(item.GetComponentInChildren<Text>().text);
                        SaveLoadSR.SaveSR<List<StarRatingData>>(SREnv,item.GetComponentInChildren<Text>().text);
                    }
                }
            }
        }
    }

    public void checkStar()
    {
        if(LoadMainStage.buttonArray != null)
        {
            int sizeOfList = LoadMainStage.buttonArray.Count;
            for (int i = 0; i < sizeOfList; i++)
            {
                Debug.Log("i = " + sizeOfList);
                if(LoadMainStage.buttonArray[i] != null)
                {
                    Debug.Log("Num = " + LoadMainStage.buttonArray[i].GetComponentInChildren<Text>().text);
                    SREnv = SaveLoadSR.LoadSR<List<StarRatingData>>(LoadMainStage.buttonArray[i].GetComponentInChildren<Text>().text);
                    foreach (StarRatingData item in SREnv)
                    {
                        if(item.mapStar1 != true && i != sizeOfList - 1)
                        {
                            LoadMainStage.buttonArray[i+1].GetComponent<UnityEngine.UI.Button>().enabled = false;
                        }
                    }
                }
            }
        }
    }
}
