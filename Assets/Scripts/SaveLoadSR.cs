using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveLoadSR : MonoBehaviour
{
    public string[] saveFiles;

    public static void SaveSR<T>(T objectToSaveSR ,string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating/" + LoadMainStage.savePrefabKeyword + ".txt",FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSaveSR);
            Debug.Log("Save");
        }
    }

    public static T LoadSR<T>(string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(Application.dataPath + "/Resources/" + LoadMainStage.savePrefabKeyword + "StarRating/" + LoadMainStage.savePrefabKeyword + ".txt",FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        Debug.Log("Load");
        return returnValue;
    }
}
