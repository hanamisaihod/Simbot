using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Activator : MonoBehaviour
{
    public string triggerMapName = "";
    public string currentMapName = "";
    public bool creativeTutorial = false; // if this is creative tutorial
    public bool thisIsActive = false;
    [SerializeField] private GameObject blockSaveManager;

    void Start()
    {
        DeactivateObject();
    }

    public void DeactivateObject()
	{
        if (!creativeTutorial)
        {
            if (GameObject.FindGameObjectWithTag("VariableCarrier"))
            {
                currentMapName = GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName;
                if (currentMapName != triggerMapName)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    thisIsActive = true;
                }
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("VariableCarrier"))
            {
                if (!GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().newMap)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    thisIsActive = true;
                }
            }
        }
    }
}
