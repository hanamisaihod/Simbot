using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMission : MonoBehaviour
{
    public GameObject mainStage;
    public GameObject creativeStage;
    
    public void TriggerOnSwapAtMainStage()
    {
        mainStage.SetActive(false);
        creativeStage.SetActive(true);
    }

    public void TriggerOnSwapAtCreativeStage()
    {
        mainStage.SetActive(true);
        creativeStage.SetActive(false);
    }
}
