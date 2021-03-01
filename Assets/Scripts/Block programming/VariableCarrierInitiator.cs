using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableCarrierInitiator : MonoBehaviour
{
    public GameObject variableCarrier;
    private void Awake()
    {
        if (!GameObject.FindGameObjectWithTag("VariableCarrier"))
        {
            GameObject tempCarrier = Instantiate(variableCarrier);
            DontDestroyOnLoad(tempCarrier);
        }
    }
}
