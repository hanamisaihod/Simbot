using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
	private Plane dragPlane;
	private Vector3 offset;
	private Camera mainCamera;
	public bool isLock = false;
	private GameObject parentObject;
	public BuildingHandler parentHandlerScript;
	private GameObject closest;
	public float height;
	public Vector3 position;
	private int tempLayer = 0;
	private bool changed;
	public Vector2 mousePosition;
	public GameObject trashCan;
	public Vector3 trashCanWorldPosition;
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

	public void Awake()
	{
		changed = false;
		height = GetComponent<SpriteRenderer>().bounds.size.y;
        if (GameObject.Find("ModeSwitcher"))
        {
            foreach (GameObject obj in GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects)
            {
                if (obj.tag == "SubCamera")
                {
                    Debug.Log("FoundSubCamera");
                    mainCamera = obj.GetComponent<Camera>();
                }
            }
        }
        //mainCamera = GameObject.FindGameObjectWithTag("SubCamera").GetComponent<Camera>();
        parentObject = transform.parent.gameObject;
		/*foreach (Transform child in parentObject.transform)
		{
			if (child.tag == "DoConnector")
				doConnectorChild = child.gameObject;
			if (child.tag == "IfConnector")
				ifConnectorChild = child.gameObject;
		}*/
		deleteDistance = 400;
		parentHandlerScript = parentObject.GetComponent<BuildingHandler>();
		parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();
	}

    private void Start()
    {
        if (GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockTrashCan)
        {
            Debug.Log("FoundTrashCan");
            trashCan = GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockTrashCan;
        }
        if (trashCan)
        {
            //trashCanWorldPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, trashCan.GetComponent<RectTransform>().transform.position);
            //trashCanWorldPosition = new Vector2(1998f, 918f);
            //trashCanWorldPosition = trashCan.GetComponent<TrashCan>().screenPos;
            //Debug.Log("Trash screen position" + trashCanWorldPosition);
        }
    }

    void Update()
    {
        position = transform.TransformPoint(Vector3.zero);
	}

	public void OnMouseDown()
	{
		dragPlane = new Plane(mainCamera.transform.forward, transform.position);
		Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		float planeDist;
		dragPlane.Raycast(camRay, out planeDist);
		offset = parentObject.transform.position - camRay.GetPoint(planeDist);

		tempLayer = parentHandlerScript.layerLevel;
		//parentHandlerScript.totalHeight = 0f;
		parentHandlerScript.totalHeight = parentHandlerScript.UpdateHeight();

		//mainCamera.GetComponent<CameraDrag>().available = false;
        mainCamera.GetComponent<CameraHandler>().available = false;
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
                //distanceToTrash = Vector2.Distance(new Vector2(trashCanWorldPosition.x, trashCanWorldPosition.y), new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                distanceToTrash = Vector2.Distance(new Vector2(Screen.width - 75f*(Screen.width/2160), Screen.height - 59f*(Screen.height / 1080)), new Vector2(Input.mousePosition.x, Input.mousePosition.y));
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
        parentHandlerScript.timer = 0;
        //mainCamera.GetComponent<CameraDrag>().available = true;
        mainCamera.GetComponent<CameraHandler>().available = true;
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
				AssignConnection(closest, parentObject, false);
			}
		}
	}

	public void AssignConnection(GameObject connectTarget, GameObject objectToConnect, bool fromPalette)
	{
		if (connectTarget)
		{
			if (objectToConnect.tag == "DoBlock")
			{
				if (connectTarget.gameObject.tag == "StartConnector")
				{
					objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.414f, -1.076f, 0f)
								   + (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.gameObject.tag == "DoConnector")
				{
					if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
					{
						objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.275f, -0.8276f, 0f)
								+ (objectToConnect.transform.position - doConnectorChild.GetComponent<MouseDrag>().position);
					}
					else if (connectTarget.transform.parent.tag == "DoBlock")
					{
						objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.828f, 0f)
							+ (objectToConnect.transform.localPosition - doConnectorChild.GetComponent<MouseDrag>().position);
					}
				}
				else if (connectTarget.gameObject.tag == "IfConnector")
				{
					objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.29f, -0.828f, 0f)
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
					objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(-0.12f, -1.0761f, 0f)
								  + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
				}
				else if (connectTarget.gameObject.tag == "DoConnector")
				{
					if (connectTarget.transform.parent.tag == "IfBlock" || connectTarget.transform.parent.tag == "RepeatBlock")
					{
						objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0f, -0.8265f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
					}
					else if (connectTarget.transform.parent.tag == "DoBlock")
					{
						objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.2916f, -0.827f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
					}
				}
				else if (connectTarget.gameObject.tag == "IfConnector")
				{
					objectToConnect.transform.localPosition = (connectTarget.GetComponent<MouseDrag>().position) + new Vector3(0.58f, -0.827f, 0f)
									 + (objectToConnect.transform.position - ifConnectorChild.GetComponent<MouseDrag>().position);
				}
				if (fromPalette)
				{
					objectToConnect.transform.localPosition = objectToConnect.transform.localPosition + new Vector3(0, -0.833f, 0);
				}
				connectTarget.GetComponent<MouseDrag>().attachedBy = ifConnectorChild;
				ifConnectorChild.GetComponent<MouseDrag>().attachedTo = connectTarget;
				connectTarget.transform.parent.GetComponent<BuildingHandler>().ExtendMid(parentHandlerScript.totalHeight, false, connectTarget, ifConnectorChild);
			}
			objectToConnect.transform.parent = connectTarget.transform.parent;
			connectTarget.GetComponent<MouseDrag>().isLock = true;
			parentHandlerScript.FindAvailableBlocks();
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