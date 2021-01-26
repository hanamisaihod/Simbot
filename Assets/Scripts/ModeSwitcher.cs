using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public List<GameObject> mapBuildingObjects;
    public List<GameObject> blockProgrammingObjects;
    public GameObject pallete;
    public GameObject slideFrame;
    public GameObject blockTrashCan;

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
    }

    private void FindAllStageObjects()
    {
        mapBuildingObjects.AddRange(GameObject.FindGameObjectsWithTag("StageObjects"));
        mapBuildingObjects.Add(GameObject.FindGameObjectWithTag("Building"));
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
    }
}
