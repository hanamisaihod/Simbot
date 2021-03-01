using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BuildingHandler : MonoBehaviour
{
	public float totalHeight;
	private GameObject[] startConnectorBlock;
	private GameObject[] doConnectorBlocks;
	private GameObject[] ifConnectorBlocks;
	public bool isBeingHeld = false;
	public GameObject[] conBlocks;
	public GameObject bestTarget;
	public int layerLevel;
	public int tempLayer;
	public float timer = 1;
	public GameObject startConnector, ifConnector, doConnector;
	public int currentRepeatTimes = 0;
    public GameObject speedDrop, torqueDrop, delayDrop, ifDrop, degreeDrop, compareDegreeDrop, distanceDrop, compareLeftDrop, compareRightDrop, colorLeftDrop, colorRightDrop, repeatDrop, timesDrop;
	public GameObject canvas;
    public Vector3 pos;

    //Variables to be saved
    public int blockNum;
    public int ifChoice, compareDegreeChoice, compareLeftChoice, compareRightChoice, colorLeftChoice, colorRightChoice, repeatChoice, timesChoice;
    public float speedChoice, torqueChoice, delayChoice, degreeChoice, distanceChoice;
    public int[] dropActives;

	void Awake()
	{
		layerLevel = 0;
		foreach(Transform child in transform)
		{
			if (child.GetComponent<MouseDrag>())
			{
				child.GetComponent<MouseDrag>().blockNum = blockNum;
			}
		}
		UpdateHeight();
        dropActives = new int[13];

	}

	void Update()
	{
		if (transform.parent != null)
		{
			if (transform.parent.GetComponent<BuildingHandler>())
			{
				if (transform.parent.GetComponent<BuildingHandler>().isBeingHeld)
				{
					isBeingHeld = true;
				}
				else
				{
					isBeingHeld = false;
				}
			}
		}
	}

	public float UpdateHeight()
	{
		totalHeight = 0f;
		foreach (Transform child in transform)
		{
			if (child.tag == "DoConnector" || child.tag == "IfConnector")
			{
				totalHeight += 0.92f;
			}
			if (child.tag == "BodyPart")
			{
				totalHeight += 0.59f;
			}
			if (child.tag == "IfBlock" || child.tag == "DoBlock" || child.tag == "RepeatBlock")
			{
				totalHeight += child.GetComponent<BuildingHandler>().UpdateHeight();
			}
		}
		return totalHeight;
	}

	public void ExtendMid(float newBlocksHeights, bool firstIf, GameObject attached, GameObject current)
	{
		if (attached.transform.parent.tag == "IfBlock" || attached.transform.parent.tag == "RepeatBlock")
		{
			if (!((current.tag == "IfConnector" && attached.tag == "DoConnector"
				 && (attached.transform.parent.tag == "IfBlock" || attached.transform.parent.tag == "RepeatBlock"))
				 || (current.tag == "DoConnector" && attached.tag == "DoConnector")))
			{
				foreach (Transform child in transform)
				{
					if (child.tag == "DoConnector" || child.tag == "BottomPart")
					{
						child.transform.localPosition += new Vector3(0f, -newBlocksHeights, 0f);
					}
					if (child.tag == "DoConnector")
					{
						if (child.GetComponent<MouseDrag>().attachedBy != null)
						{
							child.GetComponent<MouseDrag>().attachedBy.transform.parent.transform.localPosition += new Vector3(0f, -newBlocksHeights, 0f);
						}
					}
					if (child.tag == "BodyPart")
					{
						child.transform.localPosition += new Vector3(0f, -newBlocksHeights / 2f, 0f);
						child.transform.localScale += new Vector3(0f, 0.174f * newBlocksHeights, 0f);
					}
				}
			}
		}
		if (attached.transform.parent.tag == "IfBlock" || attached.transform.parent.tag == "RepeatBlock")
		{
			current = attached.GetComponent<MouseDrag>().ifConnectorChild;
			if (attached.GetComponent<MouseDrag>().ifConnectorChild.GetComponent<MouseDrag>().attachedTo)
			{
				attached = attached.GetComponent<MouseDrag>().ifConnectorChild.GetComponent<MouseDrag>().attachedTo;
			}
		}
		else
		{
			current = attached;
			attached = attached.GetComponent<MouseDrag>().attachedTo;
		}
		if (transform.parent)
		{
			if (transform.parent.GetComponent<BuildingHandler>())
			{
				gameObject.transform.parent.GetComponent<BuildingHandler>().ExtendMid(newBlocksHeights, firstIf, attached, current);
			}
		}
	}

	public void ExtendTop(int extendMode, GameObject center, GameObject tail)
	{
		switch (extendMode)
		{
			case 0:
				center.transform.localPosition = new Vector3(2.094f, 0.922f, 0);
				center.transform.localScale = new Vector3(0.1f, 0.1f, 1);
				tail.transform.localPosition = new Vector3(3.662f, 0.9219997f, 0);
				break;
			case 1:
				center.transform.localPosition = new Vector3(2.771f, 0.922f, 0);
				center.transform.localScale = new Vector3(0.1527485f, 0.1f, 1);
				tail.transform.localPosition = new Vector3(5.014f, 0.9219997f, 0);
				break;
			case 2:
				center.transform.localPosition = new Vector3(3.93f, 0.922f, 0);
				center.transform.localScale = new Vector3(0.2431269f, 0.1f, 1);
				tail.transform.localPosition = new Vector3(7.33f, 0.9219997f, 0);
				break;
		}
	}

	public GameObject[] FindAvailableBlocks()
	{
		startConnectorBlock = GameObject.FindGameObjectsWithTag("StartConnector");
		doConnectorBlocks = GameObject.FindGameObjectsWithTag("DoConnector");
		ifConnectorBlocks = GameObject.FindGameObjectsWithTag("IfConnector");
		conBlocks = startConnectorBlock.Concat(doConnectorBlocks).Concat(ifConnectorBlocks).ToArray();
		return conBlocks;
	}

	public void FindClosestBlock(GameObject connector)
	{
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = connector.transform.position;
		bestTarget = null;
		foreach (GameObject block in conBlocks)
		{
			if (block.transform.parent.gameObject != transform.gameObject)
			{
				Vector3 directionToTarget = block.transform.position - currentPosition;
				float dSqrToTarget = directionToTarget.sqrMagnitude;
				if (dSqrToTarget < closestDistanceSqr)
				{
					closestDistanceSqr = dSqrToTarget;
					if (!block.GetComponent<MouseDrag>().isLock && !block.transform.parent.GetComponent<BuildingHandler>().isBeingHeld)
					{
						if (closestDistanceSqr < 3f)
						{
							bestTarget = block;
						}
					}
				}
			}
		}
		DeHighlightCloseBlock();
		if (bestTarget)
		{
			HighlightCloseBlock(bestTarget);
		}
	}

	public void HighlightCloseBlock(GameObject sprite)
	{
		sprite.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .25f);
	}

	public void DeHighlightCloseBlock()
	{
		if (conBlocks.Length > 0)
		{
			foreach (GameObject block in conBlocks)
			{
				block.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	public void ChangeChildrenLayer(int level, bool sendToParent, bool toChildren, int bodyInt)
	{
		if (!sendToParent)
		{
			if (toChildren)
			{
				bodyInt++;
				tempLayer = layerLevel;
				layerLevel = level;
				foreach (Transform child in transform)
				{
					if (child.GetComponent<SpriteRenderer>())
					{
						if (child.tag == "BodyPart")
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = -layerLevel+40000+bodyInt;
						}
						else
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = layerLevel;
						}
					}
					else if (child.GetComponent<Canvas>())
					{
						child.GetComponent<Canvas>().sortingOrder = layerLevel + 1;
					}
					else if (child.GetComponent<BuildingHandler>())
					{
						child.GetComponent<BuildingHandler>().ChangeChildrenLayer(level-1, false, true, bodyInt);
					}
				}
			}
			else
			{
				layerLevel = tempLayer;
				foreach (Transform child in transform)
				{
					if (child.GetComponent<SpriteRenderer>())
					{
						if (child.tag == "BodyPart")
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = -layerLevel;
						}
						else
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = layerLevel;
						}
					}
					else if (child.GetComponent<Canvas>())
					{
						child.GetComponent<Canvas>().sortingOrder = layerLevel + 1;
					}
					else if (child.GetComponent<BuildingHandler>())
					{
						child.GetComponent<BuildingHandler>().ChangeChildrenLayer(level, false, false, 0);
					}
				}
			}
		}
		else
		{
			if (level > layerLevel - 1)
			{
				layerLevel = level + 1;
				foreach (Transform child in transform)
				{
					if (child.GetComponent<SpriteRenderer>())
					{
						if (child.tag == "BodyPart")
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = -layerLevel;
						}
						else
						{
							child.GetComponent<SpriteRenderer>().sortingOrder = layerLevel;
						}
					}
					else if (child.GetComponent<Canvas>())
					{
						child.GetComponent<Canvas>().sortingOrder = layerLevel + 1;
					}
				}
				if (transform.parent)
				{
					if (transform.parent.GetComponent<BuildingHandler>())
					{
						transform.parent.GetComponent<BuildingHandler>().ChangeChildrenLayer(layerLevel, true, false, 0);
					}
				}
			}
		}
		
	}

}
