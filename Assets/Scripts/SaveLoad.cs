using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    //public GameObject loadButtonPrefab;
    public string[] saveFiles;

    public static void Save<T>(T objectToSave ,string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);
        BinaryFormatter formatter = new BinaryFormatter();
        //Debug.Log(path + key + ".txt");
        using (FileStream fileStream = new FileStream(path + key + ".txt",FileMode.Create))
        {
            formatter.Serialize(fileStream, objectToSave);
            //Debug.Log("Save");
        }
    }

    public static T Load<T>(string key)
    {
        //Debug.Log("Key" + key);
        string path = Application.persistentDataPath + "/saves/";
        BinaryFormatter formatter = new BinaryFormatter();
        T returnValue = default(T);
        using (FileStream fileStream = new FileStream(path + key + ".txt",FileMode.Open))
        {
            returnValue = (T)formatter.Deserialize(fileStream);
        }
        //Debug.Log("Load");
        return returnValue;
    }

    public static bool SaveExist(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key + ".txt";
        return File.Exists(path);
    }

    public static void DeleteSaveFiles(string key)
    {
        string path = Application.persistentDataPath + "/saves/" + key + ".txt";
        Debug.Log(path);
        File.Delete(path);
        //Directory.CreateDirectory(path);
    }
}
