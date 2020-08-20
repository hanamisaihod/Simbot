using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponent : MonoBehaviour
{
    public void RemoveOnClick()
    {
        if(SelectObject.SelectValid != null && SelectObject.SelectValid.gameObject.tag == "Undrag")
        {
            Destroy(SelectObject.SelectValid.gameObject);
            //rotation *= Quaternion.Euler(90, 0, 0);
            //SpawnComponent.model.transform.rotation = Quaternion.Slerp(SpawnComponent.model.transform.rotation, rotation, Time.deltaTime * damping);
        }
        else
        {
            Debug.Log("Please Select Object first");
        }
    }
}
