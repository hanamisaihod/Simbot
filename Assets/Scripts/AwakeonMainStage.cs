using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeonMainStage : MonoBehaviour
{
    public GameObject mainStageLoadObj;
    public DisableOnMainStage trigger;
    // Start is called before the first frame update
    void Start()
    {
        if(LoadMainStage.mainStageKey != null)
        {
            trigger.GetComponent<DisableOnMainStage>();
            trigger.disableOnMain();
            Debug.Log("Key: " + LoadMainStage.mainStageKey);
            mainStageLoadObj = Instantiate(Resources.Load("Map/" + LoadMainStage.mainStageKey, typeof(GameObject))) as GameObject;
            mainStageLoadObj.tag = "StageObjects";
            int childCount = mainStageLoadObj.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject childMain = mainStageLoadObj.transform.GetChild(i).gameObject;
                DetectEnvironment.keepPosition.Add(childMain);
            }
        }
    }
}
