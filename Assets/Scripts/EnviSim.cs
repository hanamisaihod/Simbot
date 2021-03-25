using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviSim : MonoBehaviour
{
    public static string Mode;
    public bool mainModeBool; // false is creative, true is main --- North's line
    public GameObject CreativeObj;
    public GameObject MainObj;
    public GameObject creativeMap;
    // Start is called before the first frame update
    void Start()
    {
        DetectEnvironment.keepPosition.Clear();
        if(Mode == "Creative")
        {
            creativeMap = new GameObject("creativeMap");
            Debug.Log("CREATIVEEEEEEEEEEEEEEEEEEEEE");
            for (int i = 0; i < AwakeNewSceneSpawn.MAX; i++)
            {
                CreativeObj = Instantiate(Resources.Load(AwakeNewSceneSpawn.name[i], typeof(GameObject)),AwakeNewSceneSpawn.vector3[i],AwakeNewSceneSpawn.rotation[i]) as GameObject;
                CreativeObj.tag = "StageObjects";
                for (int z = 0; z < CreativeObj.transform.childCount; z++)
                {
                    GameObject child = CreativeObj.transform.GetChild(z).gameObject;
                    child.layer = 21;
                }
                CreativeObj.layer = 21;
                CreativeObj.GetComponent<BoxCollider>().enabled = false;
                CreativeObj.GetComponent<MeshRenderer>().enabled = false;
                CreativeObj.transform.parent = creativeMap.transform;
                DetectEnvironment.keepPosition.Add(CreativeObj);
            }
            //North's line
            if (GameObject.Find("ARSceneController"))
                GameObject.Find("ARSceneController").GetComponent<ARSceneController>().AssignMap(false);
        }
        if (Mode == "Main")
        {
            Debug.Log("MAINNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
            MainObj = Instantiate(Resources.Load("Map/" + LoadMainStage.mainStageKey, typeof(GameObject))) as GameObject;
            MainObj.tag = "StageObjects";
            int childCount = MainObj.transform.childCount;
            for (int a = 0; a < childCount; a++)
            {
                GameObject childMain = MainObj.transform.GetChild(a).gameObject;
                DetectEnvironment.keepPosition.Add(childMain);
            }
            //North's line
            if (GameObject.Find("ARSceneController"))
              GameObject.Find("ARSceneController").GetComponent<ARSceneController>().AssignMap(true);
        }
        
    }

}
