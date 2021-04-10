using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton_Hiding : MonoBehaviour
{
    public string[] triggerMapName;
    public string currentMapName = "";
    public GameObject infoButton;
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("VariableCarrier"))
        {
            currentMapName = GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName;
            foreach (string name in triggerMapName)
			{
                if (name == currentMapName)
				{
                    infoButton.SetActive(true);
				}
			}
        }
    }
}
