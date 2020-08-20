using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateXYAxis : MonoBehaviour
{
    //public Quaternion rotation;
    public void RotateXOnClick()
    {
        if(SelectObject.SelectValid != null && SelectObject.SelectValid.gameObject.tag == "Dragable")
        {
            Vector3 rotation = SelectObject.SelectValid.transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x+90,rotation.y,rotation.z);
            SelectObject.SelectValid.transform.rotation = Quaternion.Euler(rotation);
            //rotation *= Quaternion.Euler(90, 0, 0);
            //SpawnComponent.model.transform.rotation = Quaternion.Slerp(SpawnComponent.model.transform.rotation, rotation, Time.deltaTime * damping);
        }
        else
        {
            Debug.Log("Please Select Object first");
        }
        
    }

    public void RotateYOnClick()
    {
        if(SelectObject.SelectValid != null && SelectObject.SelectValid.gameObject.tag == "Dragable")
        {
            Vector3 rotation = SelectObject.SelectValid.transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x,rotation.y+90,rotation.z);
            SelectObject.SelectValid.transform.rotation = Quaternion.Euler(rotation);
        }
        else
        {
            Debug.Log("Please Select Object first");
        }
        
    }
}
