using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Activator : MonoBehaviour
{
    public string triggerMapName = "";
    public string currentMapName = "";
    public bool creativeTutorial = false;
    [SerializeField] private GameObject blockSaveManager;

    void Start()
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
            }
        }
    }
}
