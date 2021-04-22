using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class RemoveObject : MonoBehaviour
{
    public GameObject highlight;

    public void removeMode()
    {
        Destroy(DetectEnvironment.attachModel);
        if (CurrentState.state != 2)
            CurrentState.state = 2;
        else
            CurrentState.state = 1;
        Debug.Log("Bug or Not");
        //RemoveCom();
    }

    void Update()
    {
        //Debug.Log("RaycastBuilder.hitObject.gameObject.name: "+ RaycastBuilder.hitObject.gameObject.name);
        if (RaycastBuilder.hitObject != null)
		{
            if (Input.GetMouseButtonDown(0) && RaycastBuilder.hitObject.gameObject.layer == 21 && CurrentState.state == 2)
            {
                
                foreach (GameObject item in DetectEnvironment.keepPosition)
                {

                    Debug.Log("item.transform.position: " + item.transform.position);
                    Vector3 hitObjectPos = RaycastBuilder.hitObject.position;
                    hitObjectPos.x = (float)Math.Round(hitObjectPos.x, MidpointRounding.AwayFromZero);
                    hitObjectPos.y = (float)Math.Round(hitObjectPos.y, MidpointRounding.AwayFromZero);
                    hitObjectPos.z = (float)Math.Round(hitObjectPos.z, MidpointRounding.AwayFromZero);
                    Debug.Log("hitObjectPos: " + hitObjectPos);
                    if (hitObjectPos == item.transform.position)
                    {
                        Debug.Log("Check");
                        DetectEnvironment.keepPosition.Remove(item);
                        break;
                    }
                }
                Transform hitParent = RaycastBuilder.hitObject.transform.parent;

                if (hitParent != null)
                {
                    foreach (Transform item in hitParent)
                    {
                        Destroy(item.gameObject);
                    }
                    Destroy(hitParent.gameObject);
                }
                Debug.Log("List count: " + DetectEnvironment.keepPosition.Count);
            }
            else
                highlight.SetActive(false);
        }
        if(CurrentState.state == 2)
            highlight.SetActive(true);
        else
            highlight.SetActive(false);

    }
}
