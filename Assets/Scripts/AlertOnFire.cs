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
    public static Transform Up;
    public static Transform Down;
    public static Transform Left;
    public static Transform Right;
    public static Transform UpLeft;
    public static Transform UpRight;
    public static Transform DownLeft;
    public static Transform DownRight;
    // Update is called once per frame
    public void callOnFire()
    {
        if(startAlert == true)
        {
            Vector3 fireBox = gameObject.transform.position;
            fireBox.x = (float) Math.Round(fireBox.x,MidpointRounding.AwayFromZero);
            fireBox.y = (float) Math.Round(fireBox.y,MidpointRounding.AwayFromZero);
            fireBox.z = (float) Math.Round(fireBox.z,MidpointRounding.AwayFromZero);
            Vector3 forUp = new Vector3(fireBox.x,fireBox.y,fireBox.z + 1);
            Vector3 forDown = new Vector3(fireBox.x,fireBox.y,fireBox.z - 1);
            Vector3 forLeft = new Vector3(fireBox.x + 1,fireBox.y,fireBox.z);
            Vector3 forRight = new Vector3(fireBox.x - 1,fireBox.y,fireBox.z);
            Vector3 forUpLeft = new Vector3(fireBox.x + 1,fireBox.y,fireBox.z + 1);
            Vector3 forUpRight = new Vector3(fireBox.x - 1,fireBox.y,fireBox.z + 1);
            Vector3 forDownLeft = new Vector3(fireBox.x + 1,fireBox.y,fireBox.z - 1);
            Vector3 forDownRight = new Vector3(fireBox.x - 1,fireBox.y,fireBox.z - 1);
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
                        if(forUp == aroundPosition )
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            Up = item.transform.GetChild(0);
                            if(Up.gameObject.tag != "Wall")
                            {
                                ren = Up.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forDown == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            Down = item.transform.GetChild(0);
                            if(Down.gameObject.tag != "Wall")
                            {
                                ren = Down.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forLeft == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            Left = item.transform.GetChild(0);
                            if(Left.gameObject.tag != "Wall")
                            {
                                ren = Left.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forRight == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            Right = item.transform.GetChild(0);
                            if(Right.gameObject.tag != "Wall")
                            {
                                ren = Right.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forUpLeft == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            UpLeft = item.transform.GetChild(0);
                            if(UpLeft.gameObject.tag != "Wall")
                            {
                                ren = UpLeft.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forUpRight == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            UpRight = item.transform.GetChild(0);
                            if(UpRight.gameObject.tag != "Wall")
                            {
                                ren = UpRight.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forDownLeft == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            DownLeft = item.transform.GetChild(0);
                            if(DownLeft.gameObject.tag != "Wall")
                            {
                                ren = DownLeft.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                        if(forDownRight == aroundPosition)
                        {
                            Debug.Log("Found object name: " + item.name);
                            // floor
                            DownRight = item.transform.GetChild(0);
                            if(DownRight.gameObject.tag != "Wall")
                            {
                                ren = DownRight.GetComponent<Renderer>();
                                ren.sharedMaterial = floorMat_warning;
                                Debug.Log("not BUG");
                            }
                            
                        }
                    }
                    
                    
                }
            }
            
            //DetectEnvironment.keepPosition
        }
        else if(startAlert == false)
        {
            Debug.Log("BUGGGGGGGGGGGGGGGGGGGGGGGGGG");
            if(Up != null)
            {
                ren = Up.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(Down != null)
            {
                ren = Down.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(Left != null)
            {
                ren = Left.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(Right != null)
            {
                ren = Right.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(UpLeft != null)
            {
                ren = UpLeft.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(UpRight != null)
            {
                ren = UpRight.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(DownLeft != null)
            {
                ren = DownLeft.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }
            if(DownRight != null)
            {
                ren = DownRight.GetComponent<Renderer>();
                ren.sharedMaterial = floorMat_default; 
            }  
        }
    }
}
