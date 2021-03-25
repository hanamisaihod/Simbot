using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public List<GameObject> mapBuildingObjects;
    public List<GameObject> stageObjects;
    public List<GameObject> blockProgrammingObjects;
    public GameObject pallete;
    public GameObject slideFrame;
    public GameObject blockTrashCan;
    public bool inBuildingMode;

    public void SwtichToCode()
    {
        if (!pallete.GetComponent<Palette>().moving && !slideFrame.GetComponent<NewSlide>().moving)
        {
            FindAllStageObjects();
            foreach (GameObject obj in blockProgrammingObjects)
            {
                if (obj)
                {
                    obj.SetActive(true);
                }
            }
            foreach (GameObject obj in mapBuildingObjects)
            {
                if (obj)
                {
                    obj.SetActive(false);
                }
            }
        }
        inBuildingMode = false;
    }

    public void FindAllStageObjects()
    {
        stageObjects.AddRange(GameObject.FindGameObjectsWithTag("StageObjects"));
        stageObjects.Add(GameObject.FindGameObjectWithTag("Building"));
        foreach (GameObject obj in stageObjects)
		{
            if (!mapBuildingObjects.Contains(obj))
			{
                mapBuildingObjects.Add(obj);
			}
		}
        for (int i = mapBuildingObjects.Count - 1; i > -1; i--)
        {
            if (mapBuildingObjects[i] == null)
            {
                mapBuildingObjects.RemoveAt(i);
            }
        }
    }

    public void SwitchToMap()
    {
        if (!pallete.GetComponent<Palette>().moving && !slideFrame.GetComponent<NewSlide>().moving)
        {
            foreach (GameObject obj in mapBuildingObjects)
            {
                if (obj)
                {
                    obj.SetActive(true);
                }
            }
            foreach (GameObject obj in blockProgrammingObjects)
            {
                if (obj)
                {
                    obj.SetActive(false);
                }
            }
        }
        inBuildingMode = true;
    }
}
