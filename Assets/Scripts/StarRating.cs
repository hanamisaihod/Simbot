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
    public string[] mapRating;
    public StarRatingData SRData;
    public List<StarRatingData> SREnv = new List<StarRatingData>();
    public static List<StarRatingData> staticSREnv = new List<StarRatingData>();
    public void setCondition()
    {
        if(!Directory.Exists(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating");
            condition1.GetComponent<Text>().text = "Condition 1";
            condition2.GetComponent<Text>().text = "Condition 2";
            condition3.GetComponent<Text>().text = "Condition 3";

            SRData.mapCondition1.Add(condition1.GetComponent<Text>().text);
            SRData.mapCondition2.Add(condition2.GetComponent<Text>().text);
            SRData.mapCondition3.Add(condition3.GetComponent<Text>().text);

            SREnv.Add(SRData);
            SaveLoadSR.SaveSR<List<StarRatingData>>(SREnv,LoadMainStage.savePrefabKeyword);
            
        }
        else
        {
            Debug.Log("BUG");
            //mapRating = Directory.GetFiles(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating/" + LoadMainStage.savePrefabKeyword + ".txt");
            SREnv = SaveLoadSR.LoadSR<List<StarRatingData>>(LoadMainStage.savePrefabKeyword);
            staticSREnv = SREnv;
            foreach (StarRatingData item in staticSREnv)
            {
                foreach (string wordCon1 in item.mapCondition1)
                {
                    condition1.GetComponent<Text>().text = wordCon1;
                    Debug.Log(wordCon1);
                }
                foreach (string wordCon2 in item.mapCondition2)
                {
                    condition2.GetComponent<Text>().text = wordCon2;
                    Debug.Log(wordCon2);
                }
                foreach (string wordCon3 in item.mapCondition3)
                {
                    condition3.GetComponent<Text>().text = wordCon3;
                    Debug.Log(wordCon3);
                }
            }


        }
    }
}
