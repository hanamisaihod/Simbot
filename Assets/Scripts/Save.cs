﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public List<EnvironmentData> creativeEnv;
    public string spwObj;
    public static int Max;
    public static bool allowSpawn = true;
    //public ModeSwitcher callMode;

    public void OnClickToSave()
    {
        LoadScreen.spawnFromLoadName = new List<string>();
        LoadScreen.spawnFromLoadVector3 = new List<Vector3>();
        LoadScreen.spawnFromLoadQuaternion = new List<Quaternion>();
        GameObject.Find("Save").GetComponent<EnvInventory>().Save();
        //creativeEnv.Clear();        
        creativeEnv = SaveLoad.Load<List<EnvironmentData>>(ChangeScene.inputMap);
        foreach (EnvironmentData item in creativeEnv)
        {
            Max = 0;
            Debug.Log("Name count: " + item.name.Count);
            foreach (string wordname in item.name)
            {
                spwObj = wordname;
                LoadScreen.spawnFromLoadName.Add(spwObj);
                Max++;
            }
            foreach (string wordposition in item.position)
            {
                string vector3 = wordposition.Replace("(", "");
                vector3 = vector3.Replace(")", "");
                vector3 = vector3.Replace(",", "");
                vector3 = vector3.Replace(" ", "/"); 
                string[] envPos = vector3.Split("/"[0]);
                Vector3 SpawnPosition = new Vector3(float.Parse(envPos[0]),float.Parse(envPos[1]),float.Parse(envPos[2]));
                LoadScreen.spawnFromLoadVector3.Add(SpawnPosition);
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
                LoadScreen.spawnFromLoadQuaternion.Add(SpawnRotation);
            }
        }
        EnviSim.Mode = "Creative";
        ChangeScene.subMode = "CreatedScene";
        allowSpawn = false;
        GameObject.Find("Scene").GetComponent<AwakeNewSceneSpawn>().AwakeCreative();
        Debug.Log("Envi Count = " + DetectEnvironment.keepPosition.Count);
        //SceneManager.LoadScene("MapBuilding");
    }
}
