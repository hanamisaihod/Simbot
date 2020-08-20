using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus_rotating : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateY(one, 3600, 3f).setLoopClamp();
        LeanTween.rotateY(two, -3600, 3f).setLoopClamp();
        LeanTween.rotateY(three, 3600, 3f).setLoopClamp();
        LeanTween.rotateY(four, -3600, 3f).setLoopClamp();
        LeanTween.rotateY(five, 3600, 3f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
