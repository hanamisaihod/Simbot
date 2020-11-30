using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeNewSceneSpawn : MonoBehaviour
{
    public GameObject LoadObject;
    public bool once = true;
    public string[] name;
    public Vector3[] vector3;
    public Quaternion[] rotation;
    public void Start()
    {
        name = new string[LoadScreen.countMaxValue];
        vector3 = new Vector3[LoadScreen.countMaxValue];
        rotation = new Quaternion[LoadScreen.countMaxValue];
        Debug.Log("LoadScreen.countMaxValue: "+LoadScreen.countMaxValue);
        Debug.Log("name.legth: "+name.Length);
        if(LoadScreen.spawnFromLoadName != null && LoadScreen.spawnFromLoadVector3 != null && LoadScreen.spawnFromLoadQuaternion != null && once == true)
        {
            int length = 0;
            int i = 0;
            foreach (string itemWord in LoadScreen.spawnFromLoadName)
            {
                Debug.Log("LoopCount: "+length);
                name[i] = itemWord;
                i++;
                
                length++;
            }
            int j = 0;
            foreach (Vector3 itemVector in LoadScreen.spawnFromLoadVector3)
            {
                vector3[j] = itemVector;
                j++;
            }
            int k = 0;
            foreach (Quaternion itemRotation in LoadScreen.spawnFromLoadQuaternion)
            {
                rotation[k] = itemRotation;
                k++;
            }

            for (int a = 0; a < length; a++)
            {
                LoadObject = Instantiate(Resources.Load(name[a], typeof(GameObject)),vector3[a],rotation[a]) as GameObject;
                LoadObject.tag = "Untagged";
                for (int z = 0; z < LoadObject.transform.childCount; z++)
                {
                    GameObject child = LoadObject.transform.GetChild(z).gameObject;
                    child.layer = 21;
                }
                LoadObject.layer = 21;
                LoadObject.GetComponent<BoxCollider>().enabled = false;
                LoadObject.GetComponent<MeshRenderer>().enabled = false;
                DetectEnvironment.keepPosition.Add(LoadObject);
            }
        }

    Debug.Log("Count all Object in Keep: "+DetectEnvironment.keepPosition.Count);   
    once = false;
    }
}
