using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARSupportCheck;

public class ARChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ARSupportChecker.IsSupported())
        {

        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}