using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadConfirm : MonoBehaviour
{
    public static bool clickToLoad = false;
    public void confirmLoadSlot()
    {
        if(LoadScreen.waitForSelectSlot == true)
        {
            clickToLoad = true;
        }
		if(LoadBlockScreen.waitForSelectSlotBlock == true)
		{
			clickToLoad = true;
		}

	}
}
