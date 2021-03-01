using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlertOnFire : MonoBehaviour
{
    public static bool startAlert;
    public Material floorMat_default;
    public Material floorMat_warning;
    private Renderer ren;
    public static Transform zPlusOne;
    public static Transform zMinusOne;
    // Update is called once per frame
    public void callOnFire()
    {
        if(startAlert == true)
        {
            Vector3 fireBox = gameObject.transform.position;
            fireBox.x = (float) Math.Round(fireBox.x,MidpointRounding.AwayFromZero);
            fireBox.y = (float) Math.Round(fireBox.y,MidpointRounding.AwayFromZero);
            fireBox.z = (float) Math.Round(fireBox.z,MidpointRounding.AwayFromZero);
            Vector3 forFun = new Vector3(fireBox.x,fireBox.y,fireBox.z + 1);
            Vector3 forFunn = new Vector3(fireBox.x,fireBox.y,fireBox.z - 1);
            Debug.Log("Position: " + fireBox);
            if (DetectEnvironment.keepPosition != null)
            {
                foreach (GameObject item in DetectEnvironment.keepPosition)
                {
                    if(item != null)
                    {
                        Vector3 aroundPosition = item.transform.position;
                        aroundPosition.x = (float) Math.Round(aroundPosition.x,MidpointRounding.AwayFromZero);
                        aroundPosition.y = (float) Math.Round(aroundPosition.y,MidpointRounding.AwayFromZero);
                        aroundPosition.z = (float) Math.Round(aroundPosition.z,MidpointRounding.AwayFromZero);
                        if(forFun == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            zPlusOne = item.transform.GetChild(0);
                            ren = zPlusOne.GetComponent<Renderer>();
                            ren.sharedMaterial = floorMat_warning;
                            Debug.Log("not BUG");
                        }
                        if(forFunn == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            zMinusOne = item.transform.GetChild(0);
                            ren = zMinusOne.GetComponent<Renderer>();
                            ren.sharedMaterial = floorMat_warning;
                            Debug.Log("not BUG");
                        }
                    }
                    
                    
                }
            }
            
            //DetectEnvironment.keepPosition
        }
        else if(startAlert == false)
        {
            Debug.Log("BUGGGGGGGGGGGGGGGGGGGGGGGGGG");
            ren = zPlusOne.GetComponent<Renderer>();
            ren.sharedMaterial = floorMat_default; 
            ren = zMinusOne.GetComponent<Renderer>();
            ren.sharedMaterial = floorMat_default;  
        }
    }
}
