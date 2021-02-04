using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class EndingStarRating : MonoBehaviour
{
    public GameObject endingCondition1;
    public GameObject endingCondition2;
    public GameObject endingCondition3;
    public GameObject endingStar1;
    public GameObject endingStar2;
    public GameObject endingStar3;
    public GameObject endingStar1Appear;
    public GameObject endingStar2Appear;
    public GameObject endingStar3Appear;
    // Start is called before the first frame update
    void Start()
    {
        if (StarRating.staticSREnv != null)
        {
            foreach (StarRatingData item in StarRating.staticSREnv)
            {
                foreach (string endingWord1 in item.mapCondition1)
                {
                    endingCondition1.GetComponent<Text>().text = endingWord1;
                }
                foreach (string endingWord2 in item.mapCondition2)
                {
                    endingCondition2.GetComponent<Text>().text = endingWord2;
                }
                foreach (string endingWord3 in item.mapCondition3)
                {
                    endingCondition3.GetComponent<Text>().text = endingWord3;
                }
            }
        }
    }
}