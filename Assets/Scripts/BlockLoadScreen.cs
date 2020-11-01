using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BlockLoadScreen : MonoBehaviour
{
    public List<EnvironmentData> ItemEnv;
    public string loadSpawnObjectName;
    public static List<string> spawnFromLoadName = new List<string>();
    public static List<Vector3> spawnFromLoadVector3 = new List<Vector3>();
    public static List<Quaternion> spawnFromLoadQuaternion = new List<Quaternion>();
    public static int countMaxValue;
    public string saveKeyword;
    public string fileKeyword;
    public void Start()
    {
        ShowLoadScreen();
    }
    
    public string[] saveFiles;
    public void GetLoadSave()
    {
        if(!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
        Debug.Log(saveFiles);
    }

    public void ShowLoadScreen()
    {
        GetLoadSave();

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject buttonObject = Instantiate(Resources.Load("Prefabs/ButtonPrefab", typeof(GameObject))) as GameObject;
            buttonObject.GetComponentInChildren<Text>().text = saveFiles[i];
            string[] savePath = buttonObject.GetComponentInChildren<Text>().text.Split("/"[0]);
            for (int j = 0; j < savePath.Length; j++)
            {
                if(j == savePath.Length - 1)
                {
                    saveKeyword = savePath[j];
                }
            }
            saveKeyword = saveKeyword.Replace(".txt", "");
            buttonObject.GetComponentInChildren<Text>().text = saveKeyword;
            //buttonObject.GetComponentInChildren<Text>().text = buttonObject.GetComponentInChildren<Text>().text.Replace("C:/Users/asus/AppData/LocalLow/DefaultCompany/MyFirstGame/saves/", "");
            //buttonObject.GetComponentInChildren<Text>().text = buttonObject.GetComponentInChildren<Text>().text.Replace(".txt", "");
            GameObject Container = GameObject.Find("LocalArea");
            buttonObject.transform.SetParent(Container.transform,false);
            buttonObject.transform.localPosition = Vector3.zero;
            buttonObject.transform.localScale = Vector3.one;
            int buttonIndex = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    public void OnButtonClick(int index)
    {
        countMaxValue = 0;
        string file = saveFiles[index];
        string[] filePath = file.Split("/"[0]);
            for (int j = 0; j < filePath.Length; j++)
            {
                if(j == filePath.Length - 1)
                {
                    fileKeyword = filePath[j];
                }
            }
        fileKeyword = fileKeyword.Replace(".txt", "");
        file = fileKeyword;
        //file = file.Replace("C:/Users/asus/AppData/LocalLow/DefaultCompany/MyFirstGame/saves/", "");
        //file = file.Replace(".txt", "");
        ChangeScene.inputMap = file;
        Debug.Log(file);
        ItemEnv = SaveLoad.Load<List<EnvironmentData>>(file);
        Debug.Log(ItemEnv.Count);
        foreach (EnvironmentData item in ItemEnv)
        {
            foreach (string wordname in item.name)
            {
                loadSpawnObjectName = wordname;
                spawnFromLoadName.Add(loadSpawnObjectName);
                countMaxValue++;
            }
            foreach (string wordposition in item.position)
            {
                string vector3 = wordposition.Replace("(", "");
                vector3 = vector3.Replace(")", "");
                vector3 = vector3.Replace(",", "");
                vector3 = vector3.Replace(" ", "/"); 
                string[] envPos = vector3.Split("/"[0]);
                Vector3 SpawnPosition = new Vector3(float.Parse(envPos[0]),float.Parse(envPos[1]),float.Parse(envPos[2]));
                spawnFromLoadVector3.Add(SpawnPosition);
            }
            foreach (string wordrotation in item.rotation)
            {
                string rotation = wordrotation.Replace("(", "");
                rotation = rotation.Replace(")", "");
                rotation = rotation.Replace(",", "");
                rotation = rotation.Replace(" ", "/");
                //Debug.Log(rotation);
                string[] envRos = rotation.Split("/"[0]);
                Quaternion SpawnRotation = new Quaternion(float.Parse(envRos[0]),float.Parse(envRos[1]),float.Parse(envRos[2]),float.Parse(envRos[3]));
                spawnFromLoadQuaternion.Add(SpawnRotation);
            }
        }

        /*foreach (EnvironmentData item in ItemEnv)
        {
            string envName = item.name;
            //Debug.Log(item.name);
            //Debug.Log(item.position);
            //Debug.Log(item.rotation);
            Debug.Log("\n");
            loadSpawnObject = Resources.Load("Prefabs/"+ envName, typeof(GameObject)) as GameObject;
            
            string vector3 = item.position.Replace("(", "");
            vector3 = vector3.Replace(")", "");
            vector3 = vector3.Replace(",", "");
            vector3 = vector3.Replace(" ", "/"); 
            string[] envPos = vector3.Split("/"[0]);
            Vector3 SpawnPosition = new Vector3(float.Parse(envPos[0]),float.Parse(envPos[1]),float.Parse(envPos[2]));
            Debug.Log(SpawnPosition);

            string rotation = item.rotation.Replace("(", "");
            rotation = rotation.Replace(")", "");
            rotation = rotation.Replace(",", "");
            rotation = rotation.Replace(" ", "/");
            Debug.Log(rotation);
            string[] envRos = rotation.Split("/"[0]);
            Quaternion SpawnRotation = new Quaternion(float.Parse(envRos[0]),float.Parse(envRos[1]),float.Parse(envRos[2]),float.Parse(envRos[3]));
            Debug.Log(SpawnRotation);

            loadSpawnObject.name = envName;
            loadSpawnObject.transform.position = SpawnPosition;
            loadSpawnObject.transform.rotation = SpawnRotation;
            spawnFromLoad.Add(loadSpawnObject);

            Debug.Log("Finish");
            
        }*/
    //GetComponent<ResetScene>().sceneObjectReset();
    SceneManager.LoadScene("MapBuilding");
    }
}
