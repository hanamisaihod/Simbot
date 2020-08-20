using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class atGoal : MonoBehaviour
{
    void Update()
    {
        if(PlayerPosition.PlayerObject != null)
        {
            Vector3 roundPos = PlayerPosition.PlayerObject.transform.position;
            roundPos.x = (float) Math.Round(roundPos.x,MidpointRounding.AwayFromZero);
            roundPos.y = (float) Math.Round(roundPos.y,MidpointRounding.AwayFromZero);
            roundPos.z = (float) Math.Round(roundPos.z,MidpointRounding.AwayFromZero);
            if(PlayerPosition.PlayerObject != null)
            {
                //Debug.Log("Player at: "+ roundPos + "Goal at: " + gameObject.transform.position);
                if(roundPos == gameObject.transform.position)
                {
                    CanvasFX_Controller.clearTrigger = true;
                }
            }
        }
        
        
    }
}
