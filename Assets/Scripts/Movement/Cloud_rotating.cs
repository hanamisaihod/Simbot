using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_rotating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateY(gameObject, 720, 420f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
