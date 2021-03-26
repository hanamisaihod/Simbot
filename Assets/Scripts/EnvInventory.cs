using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvInventory : MonoBehaviour
{
    public List<EnvironmentData> ItemEnv = new List<EnvironmentData>();
    public EnvironmentData envData;
    public string PosInString;
    public string RosInString;
    private void Start()
    {
        Debug.Log("CHECK");
        GameEvent.SaveInitiated += Save;
    }

    public void Save()
    {
        envData.name.Clear();
        envData.position.Clear();
        envData.rotation.Clear();
        ItemEnv = new List<EnvironmentData>();
        foreach (GameObject item in DetectEnvironment.keepPosition)
        {
            item.name = item.name.Replace("(Clone)", "");
            envData.name.Add(item.name);

            Vector3 position = item.transform.position;
            PosInString = position.ToString();
            envData.position.Add(PosInString);

            Quaternion rotation = item.transform.rotation;
            RosInString = rotation.ToString();
            envData.rotation.Add(RosInString);
        }
        //Debug.Log("countloop" + countloop);
        ItemEnv.Add(envData);
        SaveLoad.Save<List<EnvironmentData>>(ItemEnv, ChangeScene.inputMap);
    }
}
