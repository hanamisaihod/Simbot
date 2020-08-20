using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSpawnBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public GameObject block;
	private GameObject newBlock;
	static int blockNum = 1;
	public void OnPointerDown(PointerEventData eventData)
	{
		Camera.main.GetComponent<CameraDrag>().available = false;
		transform.parent.LeanScale(new Vector3(0.85f, 0.85f, 0.85f), 0.1f);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		transform.parent.LeanScale(new Vector3(0.75f, 0.75f, 0.75f), 0.1f);
		newBlock = Instantiate(block, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2 + 100, Screen.height / 2, 10)), transform.rotation);
		newBlock.GetComponent<BuildingHandler>().blockNum = blockNum;
		newBlock.name = newBlock.name + blockNum.ToString();
		foreach (Transform child in newBlock.transform)
		{
			child.name = child.name + blockNum.ToString();
		}
		blockNum++;
		Camera.main.GetComponent<CameraDrag>().available = true;
	}
}
