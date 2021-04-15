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
    public static List<GameObject> Up = new List<GameObject>();
    public static List<GameObject> Down = new List<GameObject>();
    public static List<GameObject> Left = new List<GameObject>();
    public static List<GameObject> Right = new List<GameObject>();
    public static List<GameObject> UpLeft = new List<GameObject>();
    public static List<GameObject> UpRight = new List<GameObject>();
    public static List<GameObject> DownLeft = new List<GameObject>();
    public static List<GameObject> DownRight = new List<GameObject>();
    public GameObject[] allFireBox;
    // Update is called once per frame
    public void callOnFire()
    {
        allFireBox = GameObject.FindGameObjectsWithTag("RedZoneFloor");
        Debug.Log("Lenght = " + allFireBox.Length);
        if(startAlert == true)
        {
            for (int i = 0; i < allFireBox.Length; i++)
            {
                //Debug.Log("Name: " + gameObject.name);
                Vector3 fireBox = allFireBox[i].transform.position;
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
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                Up.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                           if(forDown == aroundPosition )
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                if(item == null)
                                {
                                    Debug.Log("WTF");
                                }
                                Down.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forLeft == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                Left.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forRight == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                Right.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forUpLeft == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                UpLeft.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forUpRight == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                UpRight.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forDownLeft == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                DownLeft.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                            if(forDownRight == aroundPosition)
                            {
                                //Debug.Log("Found object name: " + item.name);
                                // floor
                                DownRight.Add(item.transform.GetChild(0).gameObject);
                                if(item.transform.GetChild(0).gameObject.tag != "Wall")
                                {
                                    ren = item.transform.GetChild(0).gameObject.GetComponent<Renderer>();
                                    ren.sharedMaterial = floorMat_warning;
                                    //Debug.Log("not BUG");
                                }
                                
                            }
                        }
                        
                        
                    }
                }
            }
        }
        else if(startAlert == false)
        {
            foreach (GameObject itemUp in Up)
            {
                if(itemUp != null)
                {
                    ren = itemUp.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemDown in Down)
            {
                if(itemDown != null)
                {
                    ren = itemDown.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemLeft in Left)
            {
                if(itemLeft != null)
                {
                    ren = itemLeft.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemRight in Right)
            {
                if(itemRight != null)
                {
                    ren = itemRight.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemUpLeft in UpLeft)
            {
                if(itemUpLeft != null)
                {
                    ren = itemUpLeft.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemUpRight in UpRight)
            {
                if(itemUpRight != null)
                {
                    ren = itemUpRight.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemDownLeft in DownLeft)
            {
                if(itemDownLeft != null)
                {
                    ren = itemDownLeft.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
            foreach (GameObject itemDownRight in DownRight)
            {
                if(itemDownRight != null)
                {
                    ren = itemDownRight.GetComponent<Renderer>();
                    ren.sharedMaterial = floorMat_default; 
                }
            }
        } 
    }
}
