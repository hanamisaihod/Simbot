using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public void RotateOnClick()
    {
        if (DetectEnvironment.attachModel != null)
        {
            Vector3 rotation = DetectEnvironment.attachModel.transform.rotation.eulerAngles;
            rotation = new Vector3(rotation.x,rotation.y+90,rotation.z);
            DetectEnvironment.attachModel.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
