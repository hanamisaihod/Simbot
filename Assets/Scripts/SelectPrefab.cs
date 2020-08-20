using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPrefab : MonoBehaviour
{
    [SerializeField] public GameObject modelPrefab;
    public GameObject model;
    public static Vector3 currentPrefabPos = new Vector3();

    public void OnClickSelect()
    {
        CurrentState.state = 1;
        if(DetectEnvironment.attachModel != null)
        {
            currentPrefabPos = DetectEnvironment.attachModel.transform.position;
            Destroy(DetectEnvironment.attachModel);
            model = Instantiate(modelPrefab, currentPrefabPos ,modelPrefab.transform.rotation);
        }
        else if(DetectEnvironment.attachModel == null)
        {
            Vector3 atMidPos = RaycastBuilder.hitObject.position;
            atMidPos = new Vector3(atMidPos.x,0,atMidPos.z);
            model = Instantiate(modelPrefab, atMidPos ,modelPrefab.transform.rotation);
        }
        
        DetectEnvironment.attachModel = model;  
    }

    
}
