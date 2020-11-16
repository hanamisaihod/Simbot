﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
	private Plane dragPlane;
	private Vector3 offset;
	private Camera mainCamera;
	public bool isLock = false;
	private GameObject parentObject;
	private BuildingHandler parentHandlerScript;
	private GameObject closest;
	public float height;
	public Vector3 position;
	private int tempLayer = 0;
	private bool changed;
	public Vector2 mousePosition;
	private GameObject trashCan;
	private Vector3 trashCanWorldPosition;
	private Vector3 worldPosition;
	private float distanceToTrash;
	private float deleteDistance;
	public GameObject doConnectorChild = null;
	public GameObject ifConnectorChild = null;
	public string abName = null;
	public int abNum;
	public int blockNum = -1;

	//Variables to be saved
	public GameObject attachedTo;
	public GameObject attachedBy;

	public void Start()
	{
		changed = false;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
		mainCamera = Camera.main;
		parentObject = transform.parent.gameObject;
		/*foreach (Transform child in parentObject.transform)
		{
			if (child.tag == "DoConnector")
				doConnectorChild = child.gameObject;
			if (child.tag == "IfConnector")
				ifConnectorChild = child.gameObject;
		}*/
		trashCan = GameObject.FindGameObjectWithTag("TrashCan");
		if (trashCan)
		{
			trashCanWorldPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, trashCan.GetComponent<RectTransform>().transform.position);
		}
		deleteDistance = 400;
	}

	void Update()
	{
		position = transform.TransformPoint(Vector3.zero);
	}

	public void OnMouseDown()
	{
		parentHandlerScript = parentObject.GetComponent<BuildingHandler>();

		dragPlane = new Plane(mainCamera.transform.forward, transform.position);
		Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		float planeDist;
		dragPlane.Raycast(camRay, out planeDist);
		offset = parentObject.transform.position - camRay.GetPoint(planeDist);

		tempLayer = parentHandlerScript.layerLevel;
		//parentHandlerScript.totalHeight = 0f;
		parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();

		Camera.main.GetComponent<CameraDrag>().available = false;
	}

	public void OnMouseDrag()
	{
		parentHandlerScript.timer += Time.fixedDeltaTime;
		if (parentHandlerScript.timer >= 1)
		{
			if (!changed)
			{
				parentHandlerScript.ChangeChildrenLayer(30000, false, true, 0);
				changed = true;
			}

			Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			float planeDist;
			dragPlane.Raycast(camRay, out planeDist);
			parentObject.transform.position = Vector3.Lerp(parentObject.transform.position, camRay.GetPoint(planeDist) + offset, 0.07f); 

			if (gameObject.tag != "StartConnector")
			{
				parentHandlerScript.isBeingHeld = true;
				parentHandlerScript.FindAvailableBlocks();
				if (ifConnectorChild != null)
				{
					parentHandlerScript.FindClosestBlock(ifConnectorChild);
				}
				else
				{
					parentHandlerScript.FindClosestBlock(doConnectorChild);
				}
				if (doConnectorChild)
				{
					if (doConnectorChild.GetComponent<MouseDrag>().attachedTo)
					{
						doConnectorChild.GetComponent<MouseDrag>().attachedTo.transform.parent.GetComponent<BuildingHandler>()
							.ExtendMid(-parentHandlerScript.totalHeight, false, doConnectorChild.GetComponent<MouseDrag>().attachedTo, doConnectorChild);
						doConnectorChild.GetComponent<MouseDrag>().attachedTo.GetComponent<MouseDrag>().isLock = false;
						doConnectorChild.GetComponent<MouseDrag>().attachedTo.GetComponent<MouseDrag>().attachedBy = null;
						doConnectorChild.GetComponent<MouseDrag>().attachedTo = null;
						parentObject.transform.parent = null;
					}
				}
				if (ifConnectorChild)
				{
					if (ifConnectorChild.GetComponent<MouseDrag>().attachedTo)
					{
						ifConnectorChild.GetComponent<MouseDrag>().attachedTo.transform.parent.GetComponent<BuildingHandler>()
							.ExtendMid(-parentHandlerScript.totalHeight, false, ifConnectorChild.GetComponent<MouseDrag>().attachedTo, ifConnectorChild);
						ifConnectorChild.GetComponent<MouseDrag>().attachedTo.GetComponent<MouseDrag>().isLock = false;
						ifConnectorChild.GetComponent<MouseDrag>().attachedTo.GetComponent<MouseDrag>().attachedBy = null;
						ifConnectorChild.GetComponent<MouseDrag>().attachedTo = null;
						parentObject.transform.parent = null;
					}
				}
				worldPosition = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 10));
				distanceToTrash = Vector2.Distance(new Vector2(worldPosition.x, worldPosition.y), new Vector2(trashCanWorldPosition.x, trashCanWorldPosition.y));
				if (distanceToTrash <= deleteDistance)
				{
					trashCan.GetComponent<TrashCan>().ShowDeleteEffect();
				}
				else
				{
					if (trashCan.GetComponent<TrashCan>().deleteEffect)
					{
						trashCan.GetComponent<TrashCan>().StopDeleteEffect();
					}
				}
			}		
		}
	}

	private void OnMouseUp()
	{
		worldPosition = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 10));
		parentHandlerScript.timer = 0;
		Camera.main.GetComponent<CameraDrag>().available = true;
		if (parentHandlerScript.isBeingHeld)
		{
			changed = false;
			parentHandlerScript.ChangeChildrenLayer(tempLayer, false, false, 0);
			if (gameObject.tag != "StartConnector")
			{
				if (trashCan.GetComponent<TrashCan>().deleteEffect)
				{
					trashCan.GetComponent<TrashCan>().StopDeleteEffect();
					parentObject.LeanScale(new Vector3(0, 0, 0), 0.25f).setEaseInBack().setOnComplete(DestroyParent);
				}
				parentHandlerScript.isBeingHeld = false;
				closest = parentHandlerScript.bestTarget;
				AssignConnection(closest, parentObject);
				/*if (closest)
				{
					if (parentObject.tag == "DoBlock")
					{
						if (closest.gameObject.tag == "StartConnector")
						{
							parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(-0.414f, -1.076f, 0f)
										   + (parentObject.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
						}
						else if (closest.gameObject.tag == "DoConnector")
						{
							if (closest.transform.parent.tag == "IfBlock" || closest.transform.parent.tag == "RepeatBlock")
							{
								parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(-0.275f, -0.8276f, 0f)
										+ (parentObject.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
							}
							else if (closest.transform.parent.tag == "DoBlock")
							{
								parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.828f, 0f)
									+ (parentObject.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
							}
						}
						else if (closest.gameObject.tag == "IfConnector")
						{
							parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(0.29f, -0.828f, 0f)
									   + (parentObject.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
						}
						closest.GetComponent<MouseDrag>().attachedBy = gameObject;
						doConnectorChild.GetComponent<MouseDrag>().attachedTo = closest;
						closest.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, closest, doConnectorChild);
					}
					else if (parentObject.tag == "IfBlock" || parentObject.tag == "RepeatBlock")
					{
						if (closest.gameObject.tag == "StartConnector")
						{
							parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(-0.12f, -1.0761f, 0f)
										  + (parentObject.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
						}
						else if (closest.gameObject.tag == "DoConnector")
						{
							if (closest.transform.parent.tag == "IfBlock" || closest.transform.parent.tag == "RepeatBlock")
							{
								parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.8265f, 0f)
											 + (parentObject.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
							}
							else if (closest.transform.parent.tag == "DoBlock")
							{
								parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(0.2916f, -0.827f, 0f)
											 + (parentObject.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
							}
						}
						else if (closest.gameObject.tag == "IfConnector")
						{
							parentObject.transform.position = (closest.GetComponent<MouseDrag>().position) + new Vector3(0.58f, -0.827f, 0f)
											 + (parentObject.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
						}
						closest.GetComponent<MouseDrag>().attachedBy = gameObject;
						ifConnectorChild.GetComponent<MouseDrag>().attachedTo = closest;
						closest.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, closest, ifConnectorChild);
					}
					parentObject.transform.parent = closest.transform.parent;
					closest.GetComponent<MouseDrag>().isLock = true;
					parentHandlerScript.DeHighlightCloseBlock();
					parentHandlerScript.ChangeChildrenLayer(parentHandlerScript.layerLevel, true, false, 0);
				}*/
			}
		}
	}

	public void AssignConnection(GameObject connectTarget, GameObject objectToConnect)
	{
		if (connectTarget)
		{
			if (objectToConnect.tag == "DoBlock")
			{
				if (connectTarget.gameObject.tag == "StartConnector")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.414f, -1.076f, 0f)
								   + (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.gameObject.tag == "DoConnector")
				{
					if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
					{
						objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.275f, -0.8276f, 0f)
								+ (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
					}
					else if (connectTarget.transform.parent.tag == "DoBlock")
					{
						objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.828f, 0f)
							+ (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
					}
				}
				else if (connectTarget.gameObject.tag == "IfConnector")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.29f, -0.828f, 0f)
							   + (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
				}
				connectTarget.GetComponent<MouseDrag>().attachedBy = doConnectorChild;
				doConnectorChild.GetComponent<MouseDrag>().attachedTo = connectTarget;
				connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, doConnectorChild);
			}
			else if (objectToConnect.tag == "IfBlock" || objectToConnect.tag == "RepeatBlock")
			{
				if (connectTarget.gameObject.tag == "StartConnector")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.12f, -1.0761f, 0f)
								  + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.gameObject.tag == "DoConnector")
				{
					if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
					{
						objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.8265f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
					}
					else if (connectTarget.transform.parent.tag == "DoBlock")
					{
						objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.2916f, -0.827f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
					}
				}
				else if (connectTarget.gameObject.tag == "IfConnector")
				{
					objectToConnect.transform.position = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.58f, -0.827f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
				}
				connectTarget.GetComponent<MouseDrag>().attachedBy = ifConnectorChild;
				ifConnectorChild.GetComponent<MouseDrag>().attachedTo = connectTarget;
				connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, ifConnectorChild);
			}
			objectToConnect.transform.parent = connectTarget.transform.parent;
			connectTarget.GetComponent<MouseDrag>().isLock = true;
			parentHandlerScript.DeHighlightCloseBlock();
			parentHandlerScript.ChangeChildrenLayer(parentHandlerScript.layerLevel, true, false, 0);
			parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();
		}
	}

	void DestroyParent()
	{
		if (parentObject)
		{
			Destroy(parentObject);
		}
	}
}