using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robotcheck : MonoBehaviour
{
    public static int robotTypeNum;
    public GameObject turtle;
    public GameObject line;
    public GameObject turtleButton1;
    //public GameObject turtleButton2;
    public GameObject lineButton1;
    //public GameObject lineButton2;
    // Start is called before the first frame update
    void Start()
    {
        if(EnviSim.Mode == "Main")
        {
            if(LoadMainStage.mainStageKey == "Map1" || LoadMainStage.mainStageKey == "Map2"|| LoadMainStage.mainStageKey == "Map3"
            || LoadMainStage.mainStageKey == "Map4"|| LoadMainStage.mainStageKey == "Map5"|| LoadMainStage.mainStageKey == "Map6"
            || LoadMainStage.mainStageKey == "Map7"|| LoadMainStage.mainStageKey == "Map8"|| LoadMainStage.mainStageKey == "Map9"
            || LoadMainStage.mainStageKey == "Map10"|| LoadMainStage.mainStageKey == "Map11"|| LoadMainStage.mainStageKey == "Map12")
            {
                turtle.SetActive(true);
                line.SetActive(false);
                turtleButton1.SetActive(true);
                lineButton1.SetActive(false);
                robotTypeNum = 0;
            }
            else
            {
                turtle.SetActive(false);
                line.SetActive(true);
                turtleButton1.SetActive(false);
                lineButton1.SetActive(true);
                lineButton1.transform.position = turtleButton1.transform.position;
                robotTypeNum = 1;
            }
        }
    }

}
