using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public static Transform SelectValid;
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out hit))
            {
                Transform Selection = hit.transform;
                if(Selection.gameObject.tag == "Dragable" || Selection.gameObject.tag == "Undrag")
                {
                    SelectValid = Selection;
                    Debug.Log("SelectValid = "+ SelectValid.name);
                }
            }
        }
    }
}
