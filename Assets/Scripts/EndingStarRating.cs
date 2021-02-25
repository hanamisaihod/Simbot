﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EndingStarRating : MonoBehaviour
{
    public GameObject endingCondition1;
    public GameObject endingCondition2;
    public GameObject endingCondition3;
    public GameObject emptyStar1;
    public GameObject emptyStar2;
    public GameObject emptyStar3;
    public GameObject endingStar1Appear;
    public GameObject endingStar2Appear;
    public GameObject endingStar3Appear;
    public static int lineOfCode;
    public static float robotHP;
    //public StarRatingData endingSRData;
    public List<StarRatingData> SREnvAtEnding = new List<StarRatingData>();
    // Start is called before the first frame update
    void Start()
    {
        //emptyStar1.SetActive(false);
        //emptyStar2.SetActive(false);
        //emptyStar3.SetActive(false);
        //endingStar1Appear.SetActive(false);
        //endingStar2Appear.SetActive(false);
        //endingStar3Appear.SetActive(false);

    }
    public void starRatingShow()
    {
        SREnvAtEnding = SaveLoadSR.LoadSR<List<StarRatingData>>(LoadMainStage.currentKeyword);
        if (SREnvAtEnding != null)
        {
            foreach (StarRatingData item in SREnvAtEnding)
            {
                //GOAL
                foreach (string endingWord1 in item.mapCondition1)
                {
                    endingCondition1.GetComponent<Text>().text = endingWord1;
                    //Debug.Log(endingCondition1.GetComponent<Text>().text);
                    item.mapStar1 = true;
                    endingStar1Appear.SetActive(true);

                }
                //LINE
                foreach (string endingWord2 in item.mapCondition2)
                {
                    endingCondition2.GetComponent<Text>().text = endingWord2;
                    //Debug.Log(endingCondition2.GetComponent<Text>().text);
                    int blockCode = GameObject.Find("BlockSaveManager").GetComponent<BlockSaveAndLoad>().conBlocks.Length - 1;
                    if (LoadMainStage.mainStageKey == "Map1")
                    {
                        if(blockCode <= 2)
                        {
                            //true
                            Debug.Log("True");
                            item.mapStar2 = true;
                            endingStar2Appear.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("False");
                            item.mapStar2 = false;
                            endingStar2Appear.SetActive(false);
                            emptyStar2.SetActive(true);
                        }
                    }
                    if (LoadMainStage.mainStageKey == "Map2")
                    {
                        if(blockCode <= 2)
                        {
                            //true
                            Debug.Log("True");
                            item.mapStar2 = true;
                            endingStar2Appear.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("False");
                            item.mapStar2 = false;
                            endingStar2Appear.SetActive(false);
                            emptyStar2.SetActive(true);
                        }
                    }
                }
                //HP
                foreach (string endingWord3 in item.mapCondition3)
                {
                    endingCondition3.GetComponent<Text>().text = endingWord3;
                    //Debug.Log(endingCondition3.GetComponent<Text>().text);
                    if(robotHP == 100)
                    {
                        item.mapStar3 = true;
                        endingStar3Appear.SetActive(true);
                    }
                    else
                    {
                        item.mapStar3 = false;
                        emptyStar3.SetActive(true);
                        endingStar3Appear.SetActive(false);
                    }

                }
            }
        }


    //SREnvAtEnding.Add(endingSRData);
    SaveLoadSR.SaveSR<List<StarRatingData>>(SREnvAtEnding,LoadMainStage.currentKeyword);
    }
}