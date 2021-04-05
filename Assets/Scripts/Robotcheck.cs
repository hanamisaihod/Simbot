using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robotcheck : MonoBehaviour
{
    public static int robotTypeNum;
    public GameObject turtle;
    public GameObject line;
    public GameObject turtleButton1;
    public GameObject turtleButtonOff;
    public GameObject turtleButtonOn;
    public GameObject lineButton1;
    public GameObject lineButtonOff;
    public GameObject lineButtonOn;
    // Start is called before the first frame update
    void Start()
    {
        turtle.SetActive(false);
        line.SetActive(false);
        turtleButtonOn.SetActive(false);
        turtleButtonOff.SetActive(true);
        lineButtonOff.SetActive(true);
        lineButtonOn.SetActive(false);
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
                turtleButtonOn.SetActive(true);
                turtleButtonOff.SetActive(false);
                robotTypeNum = 0;
            }
            else
            {
                turtle.SetActive(false);
                line.SetActive(true);
                turtleButton1.SetActive(false);
                lineButton1.SetActive(true);
                lineButtonOn.SetActive(true);
                lineButtonOff.SetActive(false);
                lineButton1.transform.position = turtleButton1.transform.position;
                robotTypeNum = 1;
            }
        }
        if(EnviSim.Mode == "Creative")
        {
            turtleButton1.AddComponent<Button>();
            lineButton1.AddComponent<Button>();
            turtleButton1.GetComponent<Button>().onClick.AddListener(creativeTurtleRobotNum);
            lineButton1.GetComponent<Button>().onClick.AddListener(creativeLineRobotNum);
        }
    }
    void creativeTurtleRobotNum()
    {
        robotTypeNum = 0;
        turtle.SetActive(true);
        line.SetActive(false);
        turtleButtonOn.SetActive(true);
        turtleButtonOff.SetActive(false);
        lineButtonOff.SetActive(true);
        lineButtonOn.SetActive(false);
    }
    void creativeLineRobotNum()
    {
        robotTypeNum = 1;
        turtle.SetActive(false);
        line.SetActive(true);
        lineButtonOn.SetActive(true);
        lineButtonOff.SetActive(false);
        turtleButtonOff.SetActive(true);
        turtleButtonOn.SetActive(false);
    }
}
