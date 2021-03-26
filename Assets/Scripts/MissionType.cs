using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionType : MonoBehaviour
{
    public static bool viewCreative = false;
    public GameObject viewCreativeStage;
    public GameObject viewMainStage;
    // Start is called before the first frame update
    void Start()
    {
        if(viewCreative == true)
        {
            viewMainStage.SetActive(false);
            viewCreativeStage.SetActive(true);
        }
        else
        {
            viewMainStage.SetActive(true);
            viewCreativeStage.SetActive(false);
        }
    }
}
