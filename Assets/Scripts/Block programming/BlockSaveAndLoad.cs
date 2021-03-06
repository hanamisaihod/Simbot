﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BlockSaveAndLoad : MonoBehaviour
{
    public string saveName;
    private int currentObj = 0;
    public GameObject startPrefab, doPrefab, ifPrefab, repeatPrefab;
    private List<GameObject> tempBlocks = new List<GameObject>();
    public GameObject[] conBlocks, startBlock, doBlocks, ifBlocks, repeatBlocks;
    public GameObject mainCamera;
    public bool newBlockProgram = false;
    public int debugButton = 0;
    public bool allowManualAssigning = false;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("VariableCarrier"))
        {
            saveName = GameObject.FindGameObjectWithTag("VariableCarrier").GetComponent<CarriedVariables>().currentMapName;
        }
        allowManualAssigning = false;
        LoadSave();
        currentObj = 0;
        DebugButton();
        GatherAllBlocks();
        if (startBlock[0])
        {
            startBlock[0].SetActive(false);
            if (GameObject.Find("ModeSwitcher"))
            {
                GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects.Add(startBlock[0]);
            }
        }
        ClearStrayBlocks();
    }

    public void DeleteAllKeysOfSave(string savename)
	{
        PlayerPrefs.DeleteKey(savename + "count");
        PlayerPrefs.DeleteKey(savename);
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "name");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "bn");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "lx");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "ly");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "lz");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "type");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "sc" + "ab");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "dc" + "ab");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "ic" + "ab");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "ich");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "cpdch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "cplch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "cprch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "clch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "crch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "rch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "tich");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "sch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "tch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "dch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "ach");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "dgch");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "dich");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "pi");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "pr");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "pt");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "pd");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da0");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da1");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da2");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da3");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da4");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da5");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da6");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da7");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da8");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da9");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da10");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da11");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da12");
        PlayerPrefs.DeleteKey(savename + currentObj.ToString() + "da13");
    }

    public void ClearStrayBlocks()
	{
        foreach (GameObject block in conBlocks)
        {
            if (!block.CompareTag("StartBlock"))
			{
                if (block.transform.parent == null)
				{
                    Destroy(block);
				}
			}
        }
    }

    public void GatherAllBlocks()
    {
        GameObject startBlockTemp = null;
        if (GameObject.Find("ModeSwitcher"))
        {
            foreach (GameObject obj in GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects)
            {
                if (obj.tag == "StartBlock")
				{
                    startBlockTemp = obj;
                    if (startBlockTemp.activeInHierarchy == false)
					{
                        startBlockTemp.SetActive(true);
					}
                }
            }
        }
        startBlock = GameObject.FindGameObjectsWithTag("StartBlock");
        doBlocks = GameObject.FindGameObjectsWithTag("DoBlock");
        ifBlocks = GameObject.FindGameObjectsWithTag("IfBlock");
        repeatBlocks = GameObject.FindGameObjectsWithTag("RepeatBlock");
        conBlocks = startBlock.Concat(doBlocks).Concat(ifBlocks).Concat(repeatBlocks).ToArray();
        if (startBlockTemp != null)
        {
            if (GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().inBuildingMode == true)
            {
                startBlockTemp.SetActive(false);
            }
        }
    }

    public void SaveBlockProgram()
    {
        for (int i = 0; i < conBlocks.Length; i++)
        {
            conBlocks[i] = null;
        }
        GatherAllBlocks();
        currentObj = 0;
        for (int i = 0; i < PlayerPrefs.GetInt(saveName + "count"); i++)
        {
            //DeleteAllKeysOfSave(saveName);
            currentObj++;
        }
        currentObj = 0;
        PlayerPrefs.SetInt(saveName + "count", conBlocks.Length); //Save block program size
        PlayerPrefs.SetInt(saveName, 1); //Save name (if the name exist, this int key exist and will be 1)
        foreach (GameObject block in conBlocks)
        {
            SaveBlock(block, saveName);
        }
    }


    private void SaveBlock(GameObject block, string name)
    {
        PlayerPrefs.SetString(name + currentObj.ToString() + "name", block.name);
        PlayerPrefs.SetInt(name + currentObj.ToString() + "bn", block.GetComponent<BuildingHandler>().blockNum);
        PlayerPrefs.SetFloat(name + currentObj.ToString() + "lx", block.transform.localPosition.x);
        PlayerPrefs.SetFloat(name + currentObj.ToString() + "ly", block.transform.localPosition.y);
        PlayerPrefs.SetFloat(name + currentObj.ToString() + "lz", block.transform.localPosition.z);
        if (block.tag == "StartBlock")
        {
            PlayerPrefs.SetInt(name + currentObj.ToString() + "type", 0);
            if (block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy != null)
            {
                //PlayerPrefs.SetString(name + currentObj.ToString() + "sc" + "ab", block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.name);
                //PlayerPrefs.SetInt(name + currentObj.ToString() + "sc" + "ab",
                //    block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.GetComponent<BuildingHandler>().blockNum);
                PlayerPrefs.SetInt(name + currentObj.ToString() + "sc" + "ab",
                    block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum);
            }
			else
			{
                PlayerPrefs.SetInt(name + currentObj.ToString() + "sc" + "ab", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt(name + currentObj.ToString() + "ich", block.GetComponent<BuildingHandler>().ifChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "cpdch", block.GetComponent<BuildingHandler>().compareDegreeChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "cplch", block.GetComponent<BuildingHandler>().compareLeftChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "cprch", block.GetComponent<BuildingHandler>().compareRightChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "clch", block.GetComponent<BuildingHandler>().colorLeftChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "crch", block.GetComponent<BuildingHandler>().colorRightChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "rch", block.GetComponent<BuildingHandler>().repeatChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "tich", block.GetComponent<BuildingHandler>().timesChoice);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "sch", block.GetComponent<BuildingHandler>().speedChoice);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "tch", block.GetComponent<BuildingHandler>().torqueChoice);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "dch", block.GetComponent<BuildingHandler>().delayChoice);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "dgch", block.GetComponent<BuildingHandler>().degreeChoice);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "dich", block.GetComponent<BuildingHandler>().distanceChoice);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "ach", block.GetComponent<BuildingHandler>().andChoice);

            PlayerPrefs.SetInt(name + currentObj.ToString() + "pi", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevIf);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "pr", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevRepeat);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "pt", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevTimes);
            PlayerPrefs.SetFloat(name + currentObj.ToString() + "pd", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevDistance);

            PlayerPrefs.SetInt(name + currentObj.ToString() + "da0", block.GetComponent<BuildingHandler>().dropActives[0]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da1", block.GetComponent<BuildingHandler>().dropActives[1]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da2", block.GetComponent<BuildingHandler>().dropActives[2]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da3", block.GetComponent<BuildingHandler>().dropActives[3]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da4", block.GetComponent<BuildingHandler>().dropActives[4]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da5", block.GetComponent<BuildingHandler>().dropActives[5]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da6", block.GetComponent<BuildingHandler>().dropActives[6]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da7", block.GetComponent<BuildingHandler>().dropActives[7]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da8", block.GetComponent<BuildingHandler>().dropActives[8]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da9", block.GetComponent<BuildingHandler>().dropActives[9]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da10", block.GetComponent<BuildingHandler>().dropActives[10]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da11", block.GetComponent<BuildingHandler>().dropActives[11]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da12", block.GetComponent<BuildingHandler>().dropActives[12]);
            PlayerPrefs.SetInt(name + currentObj.ToString() + "da13", block.GetComponent<BuildingHandler>().dropActives[13]);

            if (block.CompareTag("DoBlock"))
            {
                PlayerPrefs.SetInt(name + currentObj.ToString() + "type", 1);

                if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy != null)
                {
                    //PlayerPrefs.SetString(name + currentObj.ToString() + "dc" + "ab", block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.name);
                    //PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
                    //    block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.GetComponent<BuildingHandler>().blockNum);
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
                            block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum);
                }
                else
                {
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab", 0);
                }
            }
            else
            {
                if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy != null)
                {
                    //PlayerPrefs.SetString(name + currentObj.ToString() + "dc" + "ab", block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.name);
                    //PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
                    //    block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.GetComponent<BuildingHandler>().blockNum);
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
                            block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum);
                }
                else
                {
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab", 0);
                }
                if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy != null)
                {
                    //PlayerPrefs.SetString(name + currentObj.ToString() + "ic" + "ab", block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.name);
                    //PlayerPrefs.SetInt(name + currentObj.ToString() + "ic" + "ab",
                    //    block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.transform.parent.GetComponent<BuildingHandler>().blockNum);
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "ic" + "ab",
                            block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum);
                }
                else
                {
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "ic" + "ab", 0);
                }
                if (block.tag == "IfBlock")
                {
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "type", 2);
                }
                else if (block.tag == "RepeatBlock")
                {
                    PlayerPrefs.SetInt(name + currentObj.ToString() + "type", 3);
                }
            }
        }
        currentObj++;
    }

    public void LoadSave()
    {
        //if (newBlockProgram)
        //{
        //    GameObject tempBlock;
        //    tempBlock = Instantiate(startPrefab);
        //    Debug.Log("Hey, a start block has been instantiated, bitch!");
        //}
        //else
        //{
        //    DeleteExist();
        //    currentObj = 0;
        //    tempBlocks.Clear();
        //    int count = PlayerPrefs.GetInt(saveName + "count");
        //    for (int i = 0; i < count; i++)
        //    {
        //        LoadInitialBlock(saveName, i);
        //    }
        //    foreach (GameObject block in tempBlocks)
        //    {
        //        AssignAllBlocks(block);
        //    }
        //    foreach (GameObject block in tempBlocks)
        //    {
        //        AssignPosition(block);
        //    }
        //    foreach (GameObject block in tempBlocks)
        //    {
        //        AssignDropdown(block);
        //    }
        //}
        //newBlockProgram = false;
        if (PlayerPrefs.GetInt(saveName) != 1)
        {
            GameObject tempBlock;
            tempBlock = Instantiate(startPrefab);
        }
        else
        {
            DeleteExist();
            currentObj = 0;
            tempBlocks.Clear();
			int count = PlayerPrefs.GetInt(saveName + "count");
			for (int i = 0; i < count*4; i++)
			{
				DebugButton();
			}

			//         int count = PlayerPrefs.GetInt(saveName + "count");
			//         for (int i = 0; i < count; i++)
			//         {
			//             LoadInitialBlock(saveName, i);
			//         }
			//foreach (GameObject block in tempBlocks)
			//{
			//	AssignAllBlocks(block);
			//         }
			//foreach (GameObject block in tempBlocks)
			//         {
			//             AssignPosition(block);
			//             AssignPosition(block);
			//}
			//foreach (GameObject block in tempBlocks)
			//         {
			//             AssignDropdown(block);
			//         }
		}
    }

    public void LoadInitialBlock(string name, int current)
    {
        GameObject tempBlock;
        if (PlayerPrefs.GetInt(name + current.ToString() + "type") == 0) //If this block is StartBlock
        {
            tempBlock = Instantiate(startPrefab);
            //Comment this because initial poition doesn't really matter (for now)
            tempBlock.GetComponent<BuildingHandler>().pos = new Vector3(PlayerPrefs.GetFloat(name + current.ToString() + "lx")
                , PlayerPrefs.GetFloat(name + current.ToString() + "ly")
                , PlayerPrefs.GetFloat(name + current.ToString() + "lz"));
            tempBlock.GetComponent<BuildingHandler>().blockNum = PlayerPrefs.GetInt(name + current.ToString() + "bn");
            if (PlayerPrefs.HasKey(name + current.ToString() + "sc" + "ab"))
            {
                //tempBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abName = 
                //	PlayerPrefs.GetString(name + current.ToString() + "sc" + "ab");
                tempBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum =
                    PlayerPrefs.GetInt(name + current.ToString() + "sc" + "ab");
            }
            if (mainCamera.GetComponent<CameraDrag>())
            {
                mainCamera.GetComponent<CameraDrag>().startBlock = tempBlock;
            }
        }
        else //If not StartBlock
        {
            if (PlayerPrefs.GetInt(name + current.ToString() + "type") == 1) //If this is DoBlock
            {
                tempBlock = Instantiate(doPrefab);
                if (PlayerPrefs.HasKey(name + current.ToString() + "dc" + "ab")) //If do connector is attached
                {
                    //tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName = 
                    //	PlayerPrefs.GetString(savename + current.ToString() + "dc" + "ab");
                    tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum =
                        PlayerPrefs.GetInt(name + current.ToString() + "dc" + "ab");
                }
            }
            else //If this is if or repeat block
            {
                if (PlayerPrefs.GetInt(name + current.ToString() + "type") == 2)
                {
                    tempBlock = Instantiate(ifPrefab);
                }
                else //(PlayerPrefs.GetInt(savename + current.ToString() + "type") == 3)
                {
                    tempBlock = Instantiate(repeatPrefab);
                }
                if (PlayerPrefs.HasKey(name + current.ToString() + "dc" + "ab")) //If do connector is attached
                {
                    //tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName =
                    //	PlayerPrefs.GetString(savename + current.ToString() + "dc" + "ab");
                    tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum =
                        PlayerPrefs.GetInt(name + current.ToString() + "dc" + "ab");
                }
                if (PlayerPrefs.HasKey(name + current.ToString() + "ic" + "ab")) //If if connector is attached
                {
                    //tempBlock.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abName =
                    //	PlayerPrefs.GetString(savename + current.ToString() + "ic" + "ab");
                    tempBlock.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum =
                        PlayerPrefs.GetInt(name + current.ToString() + "ic" + "ab");
                }
            }
            tempBlock.GetComponent<BuildingHandler>().pos = new Vector3(PlayerPrefs.GetFloat(name + current.ToString() + "lx")
                , PlayerPrefs.GetFloat(name + current.ToString() + "ly")
                , PlayerPrefs.GetFloat(name + current.ToString() + "lz"));


            tempBlock.GetComponent<BuildingHandler>().blockNum = PlayerPrefs.GetInt(name + current.ToString() + "bn");
            tempBlock.GetComponent<BuildingHandler>().ifChoice = PlayerPrefs.GetInt(name + current.ToString() + "ich");
            tempBlock.GetComponent<BuildingHandler>().compareDegreeChoice = PlayerPrefs.GetInt(name + current.ToString() + "cpdch");
            tempBlock.GetComponent<BuildingHandler>().compareLeftChoice = PlayerPrefs.GetInt(name + current.ToString() + "cplch");
            tempBlock.GetComponent<BuildingHandler>().compareRightChoice = PlayerPrefs.GetInt(name + current.ToString() + "cprch");
            tempBlock.GetComponent<BuildingHandler>().colorLeftChoice = PlayerPrefs.GetInt(name + current.ToString() + "clch");
            tempBlock.GetComponent<BuildingHandler>().colorRightChoice = PlayerPrefs.GetInt(name + current.ToString() + "crch");
            tempBlock.GetComponent<BuildingHandler>().repeatChoice = PlayerPrefs.GetInt(name + current.ToString() + "rch");
            tempBlock.GetComponent<BuildingHandler>().timesChoice = PlayerPrefs.GetInt(name + current.ToString() + "tich");
            tempBlock.GetComponent<BuildingHandler>().speedChoice = PlayerPrefs.GetFloat(name + current.ToString() + "sch");
            tempBlock.GetComponent<BuildingHandler>().torqueChoice = PlayerPrefs.GetFloat(name + current.ToString() + "tch");
            tempBlock.GetComponent<BuildingHandler>().delayChoice = PlayerPrefs.GetFloat(name + current.ToString() + "dch");
            tempBlock.GetComponent<BuildingHandler>().degreeChoice = PlayerPrefs.GetFloat(name + current.ToString() + "dgch");
            tempBlock.GetComponent<BuildingHandler>().distanceChoice = PlayerPrefs.GetFloat(name + current.ToString() + "dich");
            tempBlock.GetComponent<BuildingHandler>().andChoice = PlayerPrefs.GetInt(name + current.ToString() + "ach");

            //tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevIf = PlayerPrefs.GetInt(name + current.ToString() + "pi");
            //tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevRepeat = PlayerPrefs.GetInt(name + current.ToString() + "pr");
            //tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevTimes = PlayerPrefs.GetInt(name + current.ToString() + "pt");
            //tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevDistance = PlayerPrefs.GetInt(name + current.ToString() + "pd");

            tempBlock.GetComponent<BuildingHandler>().dropActives[0] = PlayerPrefs.GetInt(name + current.ToString() + "da0");
            tempBlock.GetComponent<BuildingHandler>().dropActives[1] = PlayerPrefs.GetInt(name + current.ToString() + "da1");
            tempBlock.GetComponent<BuildingHandler>().dropActives[2] = PlayerPrefs.GetInt(name + current.ToString() + "da2");
            tempBlock.GetComponent<BuildingHandler>().dropActives[3] = PlayerPrefs.GetInt(name + current.ToString() + "da3");
            tempBlock.GetComponent<BuildingHandler>().dropActives[4] = PlayerPrefs.GetInt(name + current.ToString() + "da4");
            tempBlock.GetComponent<BuildingHandler>().dropActives[5] = PlayerPrefs.GetInt(name + current.ToString() + "da5");
            tempBlock.GetComponent<BuildingHandler>().dropActives[6] = PlayerPrefs.GetInt(name + current.ToString() + "da6");
            tempBlock.GetComponent<BuildingHandler>().dropActives[7] = PlayerPrefs.GetInt(name + current.ToString() + "da7");
            tempBlock.GetComponent<BuildingHandler>().dropActives[8] = PlayerPrefs.GetInt(name + current.ToString() + "da8");
            tempBlock.GetComponent<BuildingHandler>().dropActives[9] = PlayerPrefs.GetInt(name + current.ToString() + "da9");
            tempBlock.GetComponent<BuildingHandler>().dropActives[10] = PlayerPrefs.GetInt(name + current.ToString() + "da10");
            tempBlock.GetComponent<BuildingHandler>().dropActives[11] = PlayerPrefs.GetInt(name + current.ToString() + "da11");
            tempBlock.GetComponent<BuildingHandler>().dropActives[12] = PlayerPrefs.GetInt(name + current.ToString() + "da12");
            tempBlock.GetComponent<BuildingHandler>().dropActives[13] = PlayerPrefs.GetInt(name + current.ToString() + "da13");
        }
        tempBlock.name = PlayerPrefs.GetString(name + current.ToString() + "name");
        //Don't know what this does exactly?
        /*foreach (Transform child in tempBlock.transform)
        {
            child.name = child.name + tempBlock.GetComponent<BuildingHandler>().blockNum;
            if (child.GetComponent<SpriteRenderer>() && SceneChanger.viewBlock == false)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
        }*/
        tempBlocks.Add(tempBlock);
		//if (tempBlock.tag == "StartBlock")
		//{
		//	Debug.Log("tempBlock name: " + tempBlock.name + "\n" +
		//		"sc abNum: " + tempBlock.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum);
		//}
		//else if (tempBlock.tag == "DoBlock")
		//{
		//	Debug.Log("tempBlock name: " + tempBlock.name + "\n" +
		//		"dc abNum: " + tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum);
		//}
		//else if (tempBlock.tag == "IfBlock" || tempBlock.tag == "RepeatBlock")
		//{
		//	Debug.Log("tempBlock name: " + tempBlock.name + "\n" +
		//		"dc abNum: " + tempBlock.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum + "\n" +
		//		"ic abNum: " + tempBlock.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum);
		//}
	}

    public void AssignAllBlocks(GameObject block)
    {
        if (block.CompareTag("StartBlock"))
        {
            if (block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum != 0)
            {
                if //(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum != -1)
                (FindBlockWithNum(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum))
                {
                    AssignConnection(block.GetComponent<BuildingHandler>().startConnector,
                        FindBlockWithNum(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum));
                    //AssignConnection(block.GetComponent<BuildingHandler>().startConnector, GameObject.Find(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
                }
            }
        }
        else if (block.CompareTag("DoBlock"))
        {
            if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum != 0)
            {
                if //(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum != -1)
                (FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum))
                {
                    AssignConnection(block.GetComponent<BuildingHandler>().doConnector,
                        FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum));
                    //AssignConnection(block.GetComponent<BuildingHandler>().doConnector, GameObject.Find(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
                }
            }
        }
        else if (block.CompareTag("IfBlock") || block.CompareTag("RepeatBlock"))
        {
            if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum != 0)
            {
                if //(!(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName == null
                   //|| block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName == ""))
                    (FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum))
                {
                    AssignConnection(block.GetComponent<BuildingHandler>().doConnector,
                        FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum));
                    //AssignConnection(block.GetComponent<BuildingHandler>().doConnector, GameObject.Find(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
                }
            }
            if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum != 0)
            {
                if //(!(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abName == null
                   //|| block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abName == ""))
                    (FindBlockWithNum(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum))
                {
                    AssignConnection(block.GetComponent<BuildingHandler>().ifConnector,
                        FindBlockWithNum(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abNum));
                    //AssignConnection(block.GetComponent<BuildingHandler>().ifConnector, GameObject.Find(block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
                }
            }
        }
    }

    public GameObject FindBlockWithNum(int numToFind)
    {
        foreach (GameObject block in tempBlocks)
        {
            if (block.GetComponent<BuildingHandler>().blockNum == numToFind)
            {
                return block;
            }
        }
        return null;
    }

    //FIX (connectTarget = me (connector), objectToConnect = attachedBy (block))
    //The issue now is that with this function activated, connector seems to be getting abNum out of thin air. . .
    //The culprit seems to be the parent assigning line. . .
    public void AssignConnection(GameObject connectTarget, GameObject objectToConnect)
    {
        BuildingHandler parentHandlerScript = objectToConnect.GetComponent<BuildingHandler>();
        objectToConnect.transform.parent = connectTarget.transform.parent;
        connectTarget.GetComponent<MouseDrag>().isLock = true;
		if (objectToConnect.CompareTag("DoBlock"))
		{
			GameObject doConnector = objectToConnect.GetComponent<BuildingHandler>().doConnector;
			connectTarget.GetComponent<MouseDrag>().attachedBy = doConnector;
			doConnector.GetComponent<MouseDrag>().attachedTo = connectTarget;
            parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();
            connectTarget.transform.parent.GetComponent<BuildingHandler>().totalHeight = parentHandlerScript.UpdateHeight();
            connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, doConnector);
		}
		else if (objectToConnect.CompareTag("IfBlock") || objectToConnect.CompareTag("RepeatBlock"))
		{
			GameObject ifConnector = objectToConnect.GetComponent<BuildingHandler>().ifConnector;
			connectTarget.GetComponent<MouseDrag>().attachedBy = ifConnector;
			ifConnector.GetComponent<MouseDrag>().attachedTo = connectTarget;
            parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();
            connectTarget.transform.parent.GetComponent<BuildingHandler>().totalHeight = parentHandlerScript.UpdateHeight();
            connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, ifConnector);
		}
        connectTarget.GetComponent<MouseDrag>().abNum = parentHandlerScript.blockNum;
        parentHandlerScript.ChangeChildrenLayer(parentHandlerScript.layerLevel, true, false, 0);
    }
    public void AssignPosition(GameObject objectToAssign)
    {
        Vector3 pos = objectToAssign.GetComponent<BuildingHandler>().pos;
        objectToAssign.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        //if (objectToAssign.tag != "StartBlock")
        //{
        //    if (tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy)
        //    {
        //        if (objectToAssign.GetComponent<BuildingHandler>().doConnector.name
        //            == tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.name) //TODO fix
        //        {
        //            if (objectToAssign.tag == "DoBlock")
        //            {
        //                objectToAssign.transform.localPosition = new Vector3(-0.414f, -1.076f, 0f);
        //            }
        //            /*if (objectToAssign.tag == "IfBlock")
    				//{
    				//	objectToAssign.transform.localPosition = new Vector3(-0.12f, -1.911f, 0f);
    				//}*/
        //        }
        //        if (objectToAssign.GetComponent<BuildingHandler>().ifConnector)
        //        {
        //            if (objectToAssign.GetComponent<BuildingHandler>().ifConnector.name
        //               == tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.name) //TODO fix
        //            {
        //                /*if (objectToAssign.tag == "DoBlock")
    				//	{
    				//		objectToAssign.transform.localPosition = new Vector3(-0.414f, -1.076f, 0f);
    				//	}*/
        //                if (objectToAssign.tag == "IfBlock" || objectToAssign.tag == "RepeatBlock")
        //                {
        //                    objectToAssign.transform.localPosition = new Vector3(-0.12f, -1.911f, 0f);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    public void AssignDropdown(GameObject block)
    {
        /*PlayerPrefs.SetInt(savename + currentObj.ToString() + "ach", block.GetComponent<BuildingHandler>().actionChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "rch", block.GetComponent<BuildingHandler>().rotationChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "dch", block.GetComponent<BuildingHandler>().directionChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "ich", block.GetComponent<BuildingHandler>().isChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "cch", block.GetComponent<BuildingHandler>().colorChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "rech", block.GetComponent<BuildingHandler>().repeatChoice);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "tch", block.GetComponent<BuildingHandler>().timesChoice);

    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da0", block.GetComponent<BuildingHandler>().dropActives[0]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da1", block.GetComponent<BuildingHandler>().dropActives[1]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da2", block.GetComponent<BuildingHandler>().dropActives[2]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da3", block.GetComponent<BuildingHandler>().dropActives[3]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da4", block.GetComponent<BuildingHandler>().dropActives[4]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da5", block.GetComponent<BuildingHandler>().dropActives[5]);
    	PlayerPrefs.SetInt(savename + currentObj.ToString() + "da6", block.GetComponent<BuildingHandler>().dropActives[6]);*/
        if (block.tag == "DoBlock")
        {
            block.GetComponent<BuildingHandler>().speedDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().speedChoice.ToString();
            block.GetComponent<BuildingHandler>().torqueDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().torqueChoice.ToString();
            block.GetComponent<BuildingHandler>().delayDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().delayChoice.ToString();
        }
        else if (block.tag == "IfBlock")
        {
            if (block.GetComponent<BuildingHandler>().dropActives[3] == 1) //ifDrop
            {
                block.GetComponent<BuildingHandler>().ifDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().ifChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[4] == 1) //degreeDrop
            {
                block.GetComponent<BuildingHandler>().degreeDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().degreeChoice.ToString();
            }
            if (block.GetComponent<BuildingHandler>().dropActives[5] == 1) //compareDegreeDrop
            {
                block.GetComponent<BuildingHandler>().compareDegreeDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareDegreeChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[6] == 1) //distanceDrop
            {
                block.GetComponent<BuildingHandler>().distanceDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().distanceChoice.ToString();
            }
            if (block.GetComponent<BuildingHandler>().dropActives[7] == 1) //compareLeftDrop
            {
                block.GetComponent<BuildingHandler>().compareLeftDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareLeftChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[8] == 1) //compareRightDrop
            {
                block.GetComponent<BuildingHandler>().compareRightDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareRightChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[9] == 1) //colorLeftDrop
            {
                block.GetComponent<BuildingHandler>().colorLeftDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorLeftChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[10] == 1) //colorRightDrop
            {
                block.GetComponent<BuildingHandler>().colorRightDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorRightChoice;
            }
            if (block.GetComponent<BuildingHandler>().dropActives[13] == 1) //andDrop
            {
                block.GetComponent<BuildingHandler>().andDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().andChoice;
            }
        }
        else if (block.tag == "RepeatBlock")
        {
            if (block.GetComponent<BuildingHandler>().dropActives[11] == 1) //repeatrop
            {
                block.GetComponent<BuildingHandler>().repeatDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().repeatChoice;
                if (block.GetComponent<BuildingHandler>().dropActives[12] == 1) //timesDrop
                {
                    block.GetComponent<BuildingHandler>().timesDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().timesChoice.ToString();
                }
                if (block.GetComponent<BuildingHandler>().dropActives[4] == 1) //degreeDrop
                {
                    block.GetComponent<BuildingHandler>().degreeDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().degreeChoice.ToString();
                }
                if (block.GetComponent<BuildingHandler>().dropActives[5] == 1) //compareDegreeDrop
                {
                    block.GetComponent<BuildingHandler>().compareDegreeDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareDegreeChoice;
                }
                if (block.GetComponent<BuildingHandler>().dropActives[6] == 1) //distanceDrop
                {
                    block.GetComponent<BuildingHandler>().distanceDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().distanceChoice.ToString();
                }
                if (block.GetComponent<BuildingHandler>().dropActives[7] == 1) //compareLeftDrop
                {
                    block.GetComponent<BuildingHandler>().compareLeftDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareLeftChoice;
                }
                if (block.GetComponent<BuildingHandler>().dropActives[8] == 1) //compareRightDrop
                {
                    block.GetComponent<BuildingHandler>().compareRightDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().compareRightChoice;
                }
                if (block.GetComponent<BuildingHandler>().dropActives[9] == 1) //colorLeftDrop
                {
                    block.GetComponent<BuildingHandler>().colorLeftDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorLeftChoice;
                }
                if (block.GetComponent<BuildingHandler>().dropActives[10] == 1) //colorRightDrop
                {
                    block.GetComponent<BuildingHandler>().colorRightDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorRightChoice;
                }
                if (block.GetComponent<BuildingHandler>().dropActives[13] == 1) //andDrop
                {
                    block.GetComponent<BuildingHandler>().andDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().andChoice;
                }
            }
        }
    }

    public void DeleteExist()
    {
        startBlock = GameObject.FindGameObjectsWithTag("StartBlock");
        doBlocks = GameObject.FindGameObjectsWithTag("DoBlock");
        ifBlocks = GameObject.FindGameObjectsWithTag("IfBlock");
        repeatBlocks = GameObject.FindGameObjectsWithTag("RepeatBlock");
        conBlocks = startBlock.Concat(doBlocks).Concat(ifBlocks).Concat(repeatBlocks).ToArray();
        foreach (GameObject block in conBlocks)
        {
            Destroy(block);
        }
    }

    public void DebugButton()
	{
        int count = 0;
        count = PlayerPrefs.GetInt(saveName + "count");
        if (debugButton < count)
        {
            LoadInitialBlock(saveName, debugButton);
            debugButton++;
        }
        else if (debugButton < (count * 2))
        {
            AssignAllBlocks(tempBlocks[debugButton - count]);
            debugButton++;
        }
        else if (debugButton < (count * 3))
        {
            AssignPosition(tempBlocks[debugButton - count * 2]);
            debugButton++;
        }
        else if (debugButton < (count * 4))
        {
            AssignDropdown(tempBlocks[debugButton - count * 3]);
            debugButton++;
        }
        else if (debugButton >= (count * 4))
        {
            allowManualAssigning = true;
        }
    }
}