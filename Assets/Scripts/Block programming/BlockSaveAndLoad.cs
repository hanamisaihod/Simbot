using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BlockSaveAndLoad : MonoBehaviour
{

    private string saveName;
    private int currentObj = 0;
    public GameObject startPrefab, doPrefab, ifPrefab, repeatPrefab;
    private List<GameObject> tempBlocks = new List<GameObject>();
    private GameObject[] conBlocks, startBlock, doBlocks, ifBlocks, repeatBlocks;
    public GameObject mainCamera;
    public static string mainStageKey;

    private void Start()
    {
        name = mainStageKey;
        Debug.Log(mainStageKey);
        Debug.Log(name);
        startBlock = GameObject.FindGameObjectsWithTag("StartBlock");
        doBlocks = GameObject.FindGameObjectsWithTag("DoBlock");
        ifBlocks = GameObject.FindGameObjectsWithTag("IfBlock");
        repeatBlocks = GameObject.FindGameObjectsWithTag("RepeatBlock");
        conBlocks = startBlock.Concat(doBlocks).Concat(ifBlocks).Concat(repeatBlocks).ToArray();
    }

    public void SaveBlockProgram(string name)
    {
        //currentObj = 0;
        //PlayerPrefs.SetInt(name + "count", conBlocks.Length);
        //int slot = GenerateSlot(name);
        //PlayerPrefs.SetString(slot.ToString() + "name", name);
        //PlayerPrefs.SetString(name + "slot", slot.ToString());
        //foreach (GameObject block in conBlocks)
        //{
        //    SaveBlock(block, name);
        //}
    }
}
