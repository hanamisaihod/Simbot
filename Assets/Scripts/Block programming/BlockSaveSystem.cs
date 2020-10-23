using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BlockSaveSystem : MonoBehaviour
{
	private int saveSlot;
	private string saveName;
	private int currentObj = 0;
	public GameObject startPrefab, doPrefab, ifPrefab, repeatPrefab;
	private List<GameObject> tempBlocks = new List<GameObject>();
	private GameObject[] conBlocks, startBlock, doBlocks, ifBlocks, repeatBlocks;
	private List<GameObject> strayBlocks;
	public GameObject mainCamera;
	public static bool newBlockProgram = false;

	private void Start()
	{
		CheckStrayBlock(0);
	}

	public void CheckStrayBlock(int command) //Will delete stray block when save or simulate
	{
		saveName = ChangeScene.inputBlock;
		strayBlocks = new List<GameObject>();
		startBlock = GameObject.FindGameObjectsWithTag("StartBlock");
		doBlocks = GameObject.FindGameObjectsWithTag("DoBlock");
		ifBlocks = GameObject.FindGameObjectsWithTag("IfBlock");
		repeatBlocks = GameObject.FindGameObjectsWithTag("RepeatBlock");
		conBlocks = startBlock.Concat(doBlocks).Concat(ifBlocks).Concat(repeatBlocks).ToArray();
		foreach (GameObject block in conBlocks)
		{
			GatherStrayBlocks(block);
		}
		if (strayBlocks.Count > 0)
		{
			//Ask confirmation
			if (ConfirmDelete())
			{
				DeleteStrayBlocks();
				//Delete all strayblocks then save/simulate
			}
		}
		if (command == 0) //If want to simulate
		{
			LoadSlot(saveName);
		}
		else if (command == 1) //If want to save
		{
			SaveBlockProgram(saveName);
		}
	}

	public void GatherStrayBlocks(GameObject block)
	{
		if (block.tag != "StartBlock")
		{
			if (!block.transform.parent)
			{
				strayBlocks.Add(block);
			}
		}
	}

	public bool ConfirmDelete()
	{
		return true;
	}

	public void DeleteStrayBlocks()
	{
		foreach (GameObject block in strayBlocks)
		{
			Destroy(block);
		}
	}

    public void SaveBlockProgram(string name)
	{
		currentObj = 0;
		PlayerPrefs.SetInt(name + "count", conBlocks.Length);
		int slot = GenerateSlot(name);
		PlayerPrefs.SetString(slot.ToString() + "name", name);
		PlayerPrefs.SetString(name + "slot", slot.ToString());
		foreach (GameObject block in conBlocks)
		{
			SaveBlock(block, name);
		}
	}

	public int GenerateSlot(string name)
	{
		int count = PlayerPrefs.GetInt("totalBlock");
		for (int i = 0; i < count; i++)
		{
			if (PlayerPrefs.GetString((i+1).ToString() + "name") == name)
			{
				return i + 1;
			}
		}
		PlayerPrefs.SetInt("totalBlock", count + 1);
		return count + 1;
	}

	public void SaveBlock(GameObject block, string name)
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
				PlayerPrefs.SetInt(name + currentObj.ToString() + "sc" + "ab", 
					block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.GetComponent<MouseDrag>().blockNum);
			}
		}
		else
		{
			PlayerPrefs.SetInt(name + currentObj.ToString() + "ach", block.GetComponent<BuildingHandler>().actionChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "rch", block.GetComponent<BuildingHandler>().rotationChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "dch", block.GetComponent<BuildingHandler>().directionChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "ich", block.GetComponent<BuildingHandler>().isChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "cch", block.GetComponent<BuildingHandler>().colorChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "rech", block.GetComponent<BuildingHandler>().repeatChoice);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "tch", block.GetComponent<BuildingHandler>().timesChoice);

			//PlayerPrefs.SetInt(name + currentObj.ToString() + "pa", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevAction);
			//PlayerPrefs.SetInt(name + currentObj.ToString() + "pd", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevDirection);
			//PlayerPrefs.SetInt(name + currentObj.ToString() + "pt", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevTimes);
			//PlayerPrefs.SetInt(name + currentObj.ToString() + "pr", block.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevRepeat);

			PlayerPrefs.SetInt(name + currentObj.ToString() + "da0", block.GetComponent<BuildingHandler>().dropActives[0]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da1", block.GetComponent<BuildingHandler>().dropActives[1]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da2", block.GetComponent<BuildingHandler>().dropActives[2]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da3", block.GetComponent<BuildingHandler>().dropActives[3]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da4", block.GetComponent<BuildingHandler>().dropActives[4]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da5", block.GetComponent<BuildingHandler>().dropActives[5]);
			PlayerPrefs.SetInt(name + currentObj.ToString() + "da6", block.GetComponent<BuildingHandler>().dropActives[6]);

			if (block.tag == "DoBlock")
			{
				PlayerPrefs.SetInt(name + currentObj.ToString() + "type", 1);

				if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy != null)
				{
					//PlayerPrefs.SetString(name + currentObj.ToString() + "dc" + "ab", block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.name);
					PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
						block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.GetComponent<MouseDrag>().blockNum);
				}
			}
			else
			{
				if (block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy != null)
				{
					//PlayerPrefs.SetString(name + currentObj.ToString() + "dc" + "ab", block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.name);
					PlayerPrefs.SetInt(name + currentObj.ToString() + "dc" + "ab",
						block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().attachedBy.GetComponent<MouseDrag>().blockNum);
				}
				if (block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy != null)
				{
					//PlayerPrefs.SetString(name + currentObj.ToString() + "ic" + "ab", block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.name);
					PlayerPrefs.SetInt(name + currentObj.ToString() + "ic" + "ab",
						block.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedBy.GetComponent<MouseDrag>().blockNum);
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

	//FIX
	public void LoadSlot (string name)
	{
		if (newBlockProgram)
		{
			GameObject tempBlock;
			tempBlock = Instantiate(startPrefab);
		}
		else
		{
			DeleteExist();
			currentObj = 0;
			tempBlocks.Clear();
			int count = PlayerPrefs.GetInt(name + "count");
			for (int i = 0; i < count; i++)
			{
				LoadInitialBlock(name, i);
			}
			foreach (GameObject block in tempBlocks)
			{
				AssignAllBlocks(block);
			}
			foreach (GameObject block in tempBlocks)
			{
				AssignPosition(block);
			}
			foreach (GameObject block in tempBlocks)
			{
				AssignDropdown(block);
			}
		}
		newBlockProgram = false;
	}

	public void AssignAllBlocks(GameObject block)
	{
		if (block.tag == "StartBlock")
		{
			if //(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum != -1)
				(FindBlockWithNum(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum))
			{
				AssignConnection(block.GetComponent<BuildingHandler>().startConnector,
					FindBlockWithNum(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abNum));
				//AssignConnection(block.GetComponent<BuildingHandler>().startConnector, GameObject.Find(block.GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
			}
		}
		else if (block.tag == "DoBlock")
		{
			if //(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum != -1)
				(FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum))
			{
				AssignConnection(block.GetComponent<BuildingHandler>().doConnector,
					FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum));
				//AssignConnection(block.GetComponent<BuildingHandler>().doConnector, GameObject.Find(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
			}
		}
		else if (block.tag == "IfBlock" || block.tag == "RepeatBlock")
		{
			if //(!(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName == null
				//|| block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName == ""))
				(FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum))
			{
				AssignConnection(block.GetComponent<BuildingHandler>().doConnector,
					FindBlockWithNum(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abNum));
				//AssignConnection(block.GetComponent<BuildingHandler>().doConnector, GameObject.Find(block.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().abName).transform.parent.gameObject);
			}
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

	public void LoadInitialBlock(string name, int current)
	{
		GameObject tempBlock;
		if (PlayerPrefs.GetInt(name + current.ToString() + "type") == 0) //If this block is StartBlock
		{
			tempBlock = Instantiate(startPrefab);
			tempBlock.name = PlayerPrefs.GetString(name + current.ToString() + "name");
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
			tempBlock.name = PlayerPrefs.GetString(name + current.ToString() + "name");
			tempBlock.GetComponent<BuildingHandler>().pos = new Vector3(PlayerPrefs.GetFloat(name + current.ToString() + "lx")
				, PlayerPrefs.GetFloat(name + current.ToString() + "ly")
				, PlayerPrefs.GetFloat(name + current.ToString() + "lz"));

			tempBlock.GetComponent<BuildingHandler>().blockNum = PlayerPrefs.GetInt(name + current.ToString() + "bn");
			tempBlock.GetComponent<BuildingHandler>().actionChoice = PlayerPrefs.GetInt(name + current.ToString() + "ach");
			tempBlock.GetComponent<BuildingHandler>().rotationChoice = PlayerPrefs.GetInt(name + current.ToString() + "rch");
			tempBlock.GetComponent<BuildingHandler>().directionChoice = PlayerPrefs.GetInt(name + current.ToString() + "dch");
			tempBlock.GetComponent<BuildingHandler>().isChoice = PlayerPrefs.GetInt(name + current.ToString() + "ich");
			tempBlock.GetComponent<BuildingHandler>().colorChoice = PlayerPrefs.GetInt(name + current.ToString() + "cch");
			tempBlock.GetComponent<BuildingHandler>().repeatChoice = PlayerPrefs.GetInt(name + current.ToString() + "rech");
			tempBlock.GetComponent<BuildingHandler>().timesChoice = PlayerPrefs.GetInt(name + current.ToString() + "tch");

			tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevAction = PlayerPrefs.GetInt(name + current.ToString() + "pa");
			tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevDirection = PlayerPrefs.GetInt(name + current.ToString() + "pd");
			tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevTimes = PlayerPrefs.GetInt(name + current.ToString() + "pt");
			tempBlock.GetComponent<BuildingHandler>().canvas.GetComponent<DropdownHandler>().prevRepeat = PlayerPrefs.GetInt(name + current.ToString() + "pr");

			tempBlock.GetComponent<BuildingHandler>().dropActives[0] = PlayerPrefs.GetInt(name + current.ToString() + "da0");
			tempBlock.GetComponent<BuildingHandler>().dropActives[1] = PlayerPrefs.GetInt(name + current.ToString() + "da1");
			tempBlock.GetComponent<BuildingHandler>().dropActives[2] = PlayerPrefs.GetInt(name + current.ToString() + "da2");
			tempBlock.GetComponent<BuildingHandler>().dropActives[3] = PlayerPrefs.GetInt(name + current.ToString() + "da3");
			tempBlock.GetComponent<BuildingHandler>().dropActives[4] = PlayerPrefs.GetInt(name + current.ToString() + "da4");
			tempBlock.GetComponent<BuildingHandler>().dropActives[5] = PlayerPrefs.GetInt(name + current.ToString() + "da5");
			tempBlock.GetComponent<BuildingHandler>().dropActives[6] = PlayerPrefs.GetInt(name + current.ToString() + "da6");
		}
		foreach (Transform child in tempBlock.transform)
		{
			child.name = child.name + tempBlock.GetComponent<BuildingHandler>().blockNum;
			if (child.GetComponent<SpriteRenderer>() && SceneChanger.viewBlock == false)
			{
				child.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		tempBlocks.Add(tempBlock);

	}
	//FIX
	public void AssignConnection(GameObject connectTarget, GameObject objectToConnect)
	{
		BuildingHandler parentHandlerScript = objectToConnect.GetComponent<BuildingHandler>();
		objectToConnect.transform.parent = connectTarget.transform.parent;
		connectTarget.transform.parent.GetComponent<BuildingHandler>().totalHeight = parentHandlerScript.UpdateHeight();
		if (objectToConnect.tag == "DoBlock")
		{
			GameObject doConnector = objectToConnect.GetComponent<BuildingHandler>().doConnector;
			/*if (connectTarget.gameObject.tag == "StartConnector")
			{
				objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.414f, -1.076f, 0f)
								+ (objectToConnect.transform.position - doConnector.GetComponent<MouseDrag>().position);
			}
			else if (connectTarget.gameObject.tag == "DoConnector")
			{
				if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.275f, -0.8276f, 0f)
							+ (objectToConnect.transform.position - doConnector.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.transform.parent.tag == "DoBlock")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.828f, 0f)
						+ (objectToConnect.transform.position - doConnector.GetComponent<MouseDrag>().position);
				}
			}
			else if (connectTarget.gameObject.tag == "IfConnector")
			{
				objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.29f, -0.828f, 0f)
							+ (objectToConnect.transform.position - doConnector.GetComponent<MouseDrag>().position);
			}*/
			connectTarget.GetComponent<MouseDrag>().attachedBy = doConnector;
			connectTarget.GetComponent<MouseDrag>().isLock = true;
			doConnector.GetComponent<MouseDrag>().attachedTo = connectTarget;
			connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, doConnector);
		}
		else if (objectToConnect.tag == "IfBlock" || objectToConnect.tag == "RepeatBlock")
		{
			GameObject doConnector = objectToConnect.GetComponent<BuildingHandler>().doConnector;
			GameObject ifConnector = objectToConnect.GetComponent<BuildingHandler>().ifConnector;
			/*if (connectTarget.gameObject.tag == "StartConnector")
			{
				objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.12f, -1.0761f, 0f)
								+ (objectToConnect.transform.position - ifConnector.GetComponent<MouseDrag>().position);
			}
			else if (connectTarget.gameObject.tag == "DoConnector")
			{
				if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.8265f, 0f)
									+ (objectToConnect.transform.position - ifConnector.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.transform.parent.tag == "DoBlock")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.2916f, -0.827f, 0f)
									+ (objectToConnect.transform.position - ifConnector.GetComponent<MouseDrag>().position);
				}
			}
			else if (connectTarget.gameObject.tag == "IfConnector")
			{
				objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.58f, -0.827f, 0f)
									+ (objectToConnect.transform.position - ifConnector.GetComponent<MouseDrag>().position);
			}*/
			connectTarget.GetComponent<MouseDrag>().attachedBy = ifConnector;
			connectTarget.GetComponent<MouseDrag>().isLock = true;
			objectToConnect.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().attachedTo = connectTarget;
			connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, ifConnector);
		}
		//objectToConnect.transform.parent = connectTarget.transform.parent;
		//connectTarget.transform.parent.GetComponent<BuildingHandler>().totalHeight = parentHandlerScript.UpdateHeight();
		parentHandlerScript.ChangeChildrenLayer(parentHandlerScript.layerLevel, true, false, 0);
	}

	public void AssignPosition(GameObject objectToAssign)
	{
		Vector3 pos = objectToAssign.GetComponent<BuildingHandler>().pos;
		objectToAssign.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
		if (objectToAssign.tag != "StartBlock")
		{
			if (tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy)
			{
				if (objectToAssign.GetComponent<BuildingHandler>().doConnector.name
					== tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.name) //TODO fix
				{
					if (objectToAssign.tag == "DoBlock")
					{
						objectToAssign.transform.localPosition = new Vector3(-0.414f, -1.076f, 0f);
					}
					/*if (objectToAssign.tag == "IfBlock")
					{
						objectToAssign.transform.localPosition = new Vector3(-0.12f, -1.911f, 0f);
					}*/
				}
				if (objectToAssign.GetComponent<BuildingHandler>().ifConnector)
				{
					if (objectToAssign.GetComponent<BuildingHandler>().ifConnector.name
					   == tempBlocks[0].GetComponent<BuildingHandler>().startConnector.GetComponent<MouseDrag>().attachedBy.name) //TODO fix
					{
						/*if (objectToAssign.tag == "DoBlock")
						{
							objectToAssign.transform.localPosition = new Vector3(-0.414f, -1.076f, 0f);
						}*/
						if (objectToAssign.tag == "IfBlock" || objectToAssign.tag == "RepeatBlock")
						{
							objectToAssign.transform.localPosition = new Vector3(-0.12f, -1.911f, 0f);
						}
					}
				}
			}
		}
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
			if (block.GetComponent<BuildingHandler>().dropActives[0] == 1)
			{
				block.GetComponent<BuildingHandler>().actionDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().actionChoice;
				block.GetComponent<BuildingHandler>().timesDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().timesChoice.ToString();
				if (block.GetComponent<BuildingHandler>().dropActives[1] == 1)
				{
					block.GetComponent<BuildingHandler>().rotationDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().rotationChoice;
				}
			}
		}
		else if (block.tag == "IfBlock")
		{
			if (block.GetComponent<BuildingHandler>().dropActives[2] == 1)
			{
				block.GetComponent<BuildingHandler>().directionDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().directionChoice;
				if (block.GetComponent<BuildingHandler>().dropActives[3] == 1)
				{
					block.GetComponent<BuildingHandler>().isDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().isChoice;
				}
				if (block.GetComponent<BuildingHandler>().dropActives[4] == 1)
				{
					block.GetComponent<BuildingHandler>().colorDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorChoice;
				}
			}
		}
		else if (block.tag == "RepeatBlock")
		{
			if (block.GetComponent<BuildingHandler>().dropActives[5] == 1)
			{
				block.GetComponent<BuildingHandler>().repeatDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().repeatChoice;
				block.GetComponent<BuildingHandler>().timesDrop.GetComponent<TMP_InputField>().text = block.GetComponent<BuildingHandler>().timesChoice.ToString();
				if (block.GetComponent<BuildingHandler>().dropActives[2] == 1)
				{
					block.GetComponent<BuildingHandler>().directionDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().directionChoice; block.GetComponent<BuildingHandler>().directionDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().directionChoice;
					if (block.GetComponent<BuildingHandler>().dropActives[3] == 1)
					{
						block.GetComponent<BuildingHandler>().isDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().isChoice;
					}
					if (block.GetComponent<BuildingHandler>().dropActives[4] == 1)
					{
						block.GetComponent<BuildingHandler>().colorDrop.GetComponent<TMP_Dropdown>().value = block.GetComponent<BuildingHandler>().colorChoice;
					}
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

}
