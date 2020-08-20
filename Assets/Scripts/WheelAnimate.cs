using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimate : MonoBehaviour
{
    void Start()
    {
        LeanTween.rotateZ(gameObject, -720, 2f).setEaseInSine();
        
        LeanTween.rotateZ(gameObject, -720, 1f).setLoopClamp().setDelay(2f);

        //Accel = Math.Clamp(1,-1) // accel base on holding time
        //gameObject.transform.rotation = Quaternion.Slerp( gameObject.transform.rotation, Quaternion.Euler(0f,Accel,0f),1f);
    }

}
