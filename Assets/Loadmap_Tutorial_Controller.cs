using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadmap_Tutorial_Controller : MonoBehaviour
{
    public GameObject creativeFrame;
    public GameObject missionTutorial;
    public GameObject creativeTutorial;

	private void Update()
	{
		if (creativeFrame)
		{
			if (creativeFrame.activeInHierarchy)
			{
				if (creativeTutorial)
				{
					if (!creativeTutorial.activeInHierarchy)
					{
						creativeTutorial.SetActive(true);
					}
				}
			}
			else
			{
				if (missionTutorial)
				{
					if (!missionTutorial.activeInHierarchy)
					{
						missionTutorial.SetActive(true);
					}
				}
			}
		}
	}

}
