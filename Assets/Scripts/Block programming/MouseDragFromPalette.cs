using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class MouseDragFromPalette : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Camera mainCamera;
	public GameObject connecter;
	private GameObject[] startConnectorBlock;
	private GameObject[] doConnectorBlocks;
	private GameObject[] ifConnectorBlocks;
	private GameObject[] conBlocks;
	public GameObject bestTarget;
	public GameObject spawnPrefab;
	private Vector3 initPosition;
	private int blockNum = 2;
	private GameObject varriableCarrier;

	public void Start()
    {
        if (GameObject.Find("ModeSwitcher"))
        {
            foreach (GameObject obj in GameObject.Find("ModeSwitcher").GetComponent<ModeSwitcher>().blockProgrammingObjects)
            {
                if (obj.tag == "SubCamera")
                {
                    mainCamera = obj.GetComponent<Camera>();
                }
            }
		}
		varriableCarrier = GameObject.FindGameObjectWithTag("VariableCarrier");
	}

    public GameObject[] FindBlock()
    {
        startConnectorBlock = GameObject.FindGameObjectsWithTag("StartConnector");
        doConnectorBlocks = GameObject.FindGameObjectsWithTag("DoConnector");
        ifConnectorBlocks = GameObject.FindGameObjectsWithTag("IfConnector");
        conBlocks = startConnectorBlock.Concat(doConnectorBlocks).Concat(ifConnectorBlocks).ToArray();
        return conBlocks;
    }

    public void OnBeginDrag(PointerEventData data)
	{
        //mainCamera.GetComponent<CameraDrag>().available = false;
        mainCamera.GetComponent<CameraHandler>().available = false;
        initPosition = transform.position;
		varriableCarrier.GetComponent<CarriedVariables>().mouseDragAvailable = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
        //mainCamera.GetComponent<CameraDrag>().available = false;
        mainCamera.GetComponent<CameraHandler>().available = false;
        transform.position = Input.mousePosition + new Vector3(0,0,10.0f);
		FindAvailableBlocks();
		FindClosestBlock(connecter);
	}
	public void OnEndDrag(PointerEventData eventData)
    {
        foreach (GameObject obj in FindBlock())
        {
            if (obj.transform.parent.GetComponent<BuildingHandler>().blockNum >= blockNum)
            {
                blockNum = obj.transform.parent.GetComponent<BuildingHandler>().blockNum + 1;
            }
        }
        //mainCamera.GetComponent<CameraDrag>().available = true;
        mainCamera.GetComponent<CameraHandler>().available = true;
        if (bestTarget != null)
		{
			GameObject tempPrefab;
			tempPrefab = Instantiate(spawnPrefab, new Vector3(0,0,9999), new Quaternion(0,0,0,0));
			tempPrefab.GetComponent<BuildingHandler>().blockNum = blockNum;
			tempPrefab.name = tempPrefab.name + blockNum.ToString();
			blockNum++;
			if (tempPrefab.tag == "DoBlock")
			{
				tempPrefab.GetComponent<BuildingHandler>().doConnector.GetComponent<MouseDrag>().AssignConnection(bestTarget, tempPrefab, true);
			}
			else if (tempPrefab.tag == "IfBlock" || tempPrefab.tag == "RepeatBlock")
			{
				tempPrefab.GetComponent<BuildingHandler>().ifConnector.GetComponent<MouseDrag>().AssignConnection(bestTarget, tempPrefab, true);
			}
			tempPrefab.transform.localPosition = new Vector3(tempPrefab.transform.localPosition.x, tempPrefab.transform.localPosition.y, 0);
		}
		varriableCarrier.GetComponent<CarriedVariables>().mouseDragAvailable = true;
		transform.position = initPosition;
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
		Vector3 currentPosition;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(connector.GetComponent<RectTransform>(), 
			new Vector2(connector.GetComponent<RectTransform>().transform.position.x, connector.GetComponent<RectTransform>().transform.position.y), mainCamera, out currentPosition);
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
	/*void DestroyParent()
	{
		if (parentObject)
		{
			Destroy(parentObject);
		}
	}*/
}
