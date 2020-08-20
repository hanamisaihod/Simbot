using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(DetectEnvironment.keepPosition != null)
        {
            foreach (GameObject item in DetectEnvironment.keepPosition)
            {
                Debug.Log("Name: " + item.name + "Position: " + item.transform.position);
            }
        }
        else
        {
            Debug.Log("BUGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
        }
        
    }
}
