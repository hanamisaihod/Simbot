using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviSim : MonoBehaviour
{
    public static string Mode;
    public GameObject CreativeObj;
    public GameObject MainObj;
    // Start is called before the first frame update
    void Start()
    {
        DetectEnvironment.keepPosition.Clear();
        if(Mode == "Creative")
        {
            Debug.Log("CREATIVEEEEEEEEEEEEEEEEEEEEE");
            for (int i = 0; i < AwakeNewSceneSpawn.MAX; i++)
            {
                CreativeObj = Instantiate(Resources.Load(AwakeNewSceneSpawn.name[i], typeof(GameObject)),AwakeNewSceneSpawn.vector3[i],AwakeNewSceneSpawn.rotation[i]) as GameObject;
                CreativeObj.tag = "Untagged";
                for (int z = 0; z < CreativeObj.transform.childCount; z++)
                {
                    GameObject child = CreativeObj.transform.GetChild(z).gameObject;
                    child.layer = 21;
                }
                CreativeObj.layer = 21;
                CreativeObj.GetComponent<BoxCollider>().enabled = false;
                CreativeObj.GetComponent<MeshRenderer>().enabled = false;
                DetectEnvironment.keepPosition.Add(CreativeObj);
            }
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
        }
        
    }

}
