using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    public List<EnvironmentData> ItemEnv;
    public string loadSpawnObjectName;
    public static List<string> spawnFromLoadName = new List<string>();
    public static List<Vector3> spawnFromLoadVector3 = new List<Vector3>();
    public static List<Quaternion> spawnFromLoadQuaternion = new List<Quaternion>();
    public static int countMaxValue;
    public string saveKeyword;
    public string fileKeyword;
    public static bool waitForSelectSlot = false;
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
            buttonObject.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(OnButtonClick(buttonIndex)));
        }
    }

    public IEnumerator OnButtonClick(int index)
	{
		waitForSelectSlot = true;
        yield return new WaitUntil(() => LoadConfirm.clickToLoad == true || DeleteSave.clickToDelete == true || ChangeToSimulate.simulate == true);
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
        Debug.Log(file);
        if(LoadConfirm.clickToLoad == true)
        {
            countMaxValue = 0;
            
            //file = file.Replace("C:/Users/asus/AppData/LocalLow/DefaultCompany/MyFirstGame/saves/", "");
            //file = file.Replace(".txt", "");
            ChangeScene.inputMap = file;
            
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

            LoadConfirm.clickToLoad = false;
            waitForSelectSlot = false;
            SceneManager.LoadScene("MapBuilding");
        }
        else if(DeleteSave.clickToDelete == true)
        {
            SaveLoad.DeleteSaveFiles(file);
            waitForSelectSlot = false;
            DeleteSave.clickToDelete = false;
            SceneManager.LoadScene("LoadMap");
        }
        else if (ChangeToSimulate.simulate == true)
        {
            countMaxValue = 0;
            
            //file = file.Replace("C:/Users/asus/AppData/LocalLow/DefaultCompany/MyFirstGame/saves/", "");
            //file = file.Replace(".txt", "");
            ChangeScene.inputMap = file;
            
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
            ChangeToSimulate.simulate = false;
            waitForSelectSlot = false;
            SceneManager.LoadScene("LoadBlock");
        }
    }
}
